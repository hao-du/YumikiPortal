(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.trace.banking = {
        init: function (defaultObject) {
            //Controller to display dialog
            yumiki.moneyTrace.trace.app.controller('bankingTraceDialogController', function ($scope, $rootScope, $http, DataService) {
                $scope.traceID = undefined;
                $scope.isActiveCheckboxDisabled = false;

                //Used to be called by other controllers.
                $rootScope.$on('OnBankingTraceLoad', function (event, traceID) {
                    $('#dlgBankingTrace').modal({ backdrop: 'static' });

                    if (traceID != undefined && traceID != null) {
                        $scope.traceID = traceID;
                        $scope.isActiveCheckboxDisabled = false;
                        $scope.dialogTitle = "Edit Banking Trace";

                        yumiki.message.displayLoadingDialog(true);

                        DataService.getTraceByID($scope.traceID)
                            .then(
                            function mySucces(response) {
                                $scope.trace = response.data;

                                //Format date to display on TraceDate input
                                $scope.trace.TraceDate = moment($scope.trace.TraceDate).format(yumiki.moneyTrace.trace.longDateFormat);

                                yumiki.message.displayLoadingDialog(false);

                            },
                            function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                            });

                    } else {
                        $scope.dialogTitle = "New Banking Trace";
                        $scope.isActiveCheckboxDisabled = true;

                        $scope.trace = $scope.resetTrace();
                    }
                });

                //Reset object in new mode.
                $scope.resetTrace = function () {
                    $scope.bankingTraceForm.$setPristine();
                    return defaultObject;
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {

                        yumiki.message.displayLoadingDialog(true);

                        DataService.save($scope.trace)
                            .then(function mySucces(response) {
                                $scope.traceID = undefined;
                                $('#dlgBankingTrace').modal('hide');

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

                    DataService.getBanks().then(
                        function mySucces(response) {
                            $scope.banks = response.data;
                        }, function myError(response) {
                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                        });
                }
            });
        },
    };
}(window, document, jQuery, yumiki));