(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.trace.transfer = {
        init: function (defaultObject) {
            //Controller to display dialog
            yumiki.moneyTrace.trace.app.controller('transferTraceDialogController', function ($scope, $rootScope, $http, DataService) {
                $scope.traceID = undefined;
                $scope.isActiveCheckboxDisabled = false;

                //Used to be called by other controllers.
                $rootScope.$on('OnTransferTraceLoad', function (event, traceID) {
                    $('#dlgTransferTrace').modal({ backdrop: 'static' });

                    if (traceID != undefined && traceID != null) {
                        $scope.traceID = traceID;
                        $scope.isActiveCheckboxDisabled = false;
                        $scope.dialogTitle = "Edit Transfer Trace";

                        yumiki.message.displayLoadingDialog(true);

                        DataService.getTraceByID($scope.traceID)
                            .then(function mySucces(response) {
                                $scope.trace = response.data;

                                //Format date to display on TraceDate input
                                $scope.trace.TraceDate = moment($scope.trace.TraceDate).format(yumiki.moneyTrace.trace.longDateFormat);

                                yumiki.message.displayLoadingDialog(false);

                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                            });

                    } else {
                        $scope.dialogTitle = "New Transfer Trace";
                        $scope.isActiveCheckboxDisabled = true;

                        $scope.trace = $scope.resetTrace();
                    }
                });

                //Reset object in new mode.
                $scope.resetTrace = function () {
                    $scope.transferTraceForm.$setPristine();
                    return defaultObject;
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {

                        yumiki.message.displayLoadingDialog(true);

                        DataService.save($scope.trace)
                            .then(function mySucces(response) {
                                $scope.traceID = undefined;
                                $('#dlgTransferTrace').modal('hide');

                                $scope.ReloadData();
                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                            });
                    }
                };

                //After saving, reload list.
                $scope.ReloadData = function () {
                    $rootScope.$broadcast("OnReloadData");
                };

                // Bind datasource to controls such as Dropdownlist
                $scope.bindControls = function () {
                    DataService.getCurrencyList().then(
                        function mySucces(response) {
                            $scope.currencyList = response.data;
                        }, function myError(response) {
                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                        });

                    DataService.getUsers().then(
                        function mySucces(response) {
                            $scope.userList = response.data;
                        }, function myError(response) {
                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                        });
                }
            });
        },
    };
}(window, document, jQuery, yumiki));