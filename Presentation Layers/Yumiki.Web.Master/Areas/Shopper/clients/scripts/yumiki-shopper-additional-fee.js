(function (win, doc, $, yumiki) {
    yumiki.shopper.additionalFee = {
        showActiveText: 'Show Active Products',
        showInactiveText: 'Show Inactive Products',
        longDateFormat: '',

        init: function (serviceUrls, longDateFormat) {
            yumiki.shopper.additionalFee.longDateFormat = longDateFormat;

            var app = angular.module('shopperAdditionalFee', ['ui.bootstrap', 'yumiki-module']);

            yumiki.shopper.additionalFee.initService(app, serviceUrls);
            yumiki.shopper.additionalFee.initListController(app);
            yumiki.shopper.additionalFee.initDialogController(app, 'Additional Fee');
        },

        //Service to communicate with Server.
        initService: function (app, serviceUrls) {
            app.service('DataService', function ($http) {
                this.getList = function (showInactive) {
                    return $http.get(serviceUrls.getAllUrl, { params: { 'showInactive': showInactive } });
                };

                this.getByID = function (id) {
                    return $http.get(serviceUrls.getByIdUrl, { params: { 'additionalFeeId': id } });
                };

                this.getFeeTypes = function () {
                    return $http.get(serviceUrls.getFeeTypesUrl, { params: { 'showInactive': false, 'forReceipt': false, 'forInvoice': false, 'forAdditionFee': true } });
                };

                this.save = function (currency) {
                    return $http.post(serviceUrls.saveUrl, currency);
                };
            });

        },

        //CONTROLLER FOR LIST
        initListController: function (app) {
            app.controller('additionalFeeController', function ($scope, $rootScope, DataService) {
                $scope.inactiveButtonName = yumiki.shopper.additionalFee.showInactiveText;

                //To determine when load active or inactive list.
                $scope.isStatusChanged = false;

                //Display dialog and pass data to Dialog.
                $scope.onShowDialog = function (id) {
                    //Call DialogController
                    $rootScope.$broadcast("OnLoad", id);
                };

                //Event for other controllers to reload data.
                $rootScope.$on('OnReload', function (event) {
                    $scope.onLoad();
                });

                //Load data
                $scope.onLoad = function () {
                    var showInactive = true;
                    if ($scope.inactiveButtonName == yumiki.shopper.additionalFee.showInactiveText) {
                        showInactive = false;
                    }

                    if ($scope.isStatusChanged) {
                        showInactive = !showInactive;
                    }

                    yumiki.message.displayLoadingDialog(true);
                    DataService.getList(showInactive).then(
                        function success(response) {
                            $scope.list = response.data;

                            if ($scope.isStatusChanged) {
                                setButtonName();

                                $scope.isStatusChanged = false;
                            }

                            yumiki.message.displayLoadingDialog(false);
                        },
                        function error(response) {
                            $scope.isStatusChanged = false;

                            yumiki.message.displayLoadingDialog(false);
                            yumiki.message.clientMessage(response.data, '', yumiki.shopper.errorLogType);
                        });
                };

                //When click on "ShowInActiveButton", reload data with active status.
                $scope.onActiveStatusChanged = function () {
                    $scope.isStatusChanged = true;
                    $scope.onLoad();
                };

                //Private function
                function setButtonName() {
                    if ($scope.inactiveButtonName == yumiki.shopper.additionalFee.showInactiveText) {
                        $scope.inactiveButtonName = yumiki.shopper.additionalFee.showActiveText;
                    } else {
                        $scope.inactiveButtonName = yumiki.shopper.additionalFee.showInactiveText;
                    }
                }
            });
        },

        //CONTROLLER FOR DIALOG
        initDialogController: function (app, dialogName) {
            app.controller('additionalFeeDialogController', function ($scope, $rootScope, DataService) {
                $scope.id = undefined;
                $scope.isActiveDisabled = false;

                $scope.onLoad = function () {
                    DataService.getFeeTypes().then(
                        function success(response) {
                            $scope.feeTypes = response.data;
                        },
                        function error(response) {
                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                        });
                };

                //Deligate event is called by other controllers.
                $rootScope.$on('OnLoad', function (event, id) {
                    $('#dlgAddEditObject').modal({ backdrop: 'static' });

                    if (id) {
                        $scope.id = id;
                        $scope.isActiveDisabled = false;
                        $scope.dialogTitle = "Edit " + dialogName;

                        yumiki.message.displayLoadingDialog(true);

                        DataService.getByID($scope.id).then(
                            function success(response) {
                                $scope.object = response.data;

                                //Format date to display on TraceDate input
                                $scope.object.FeeIssueDate = moment($scope.object.FeeIssuedDate).format(yumiki.shopper.additionalFee.longDateFormat);

                                yumiki.message.displayLoadingDialog(false);
                            },
                            function error(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.shopper.errorLogType);
                            });

                    } else {
                        $scope.dialogTitle = "New " + dialogName;
                        $scope.isActiveDisabled = true;

                        $scope.object = reset();
                    }
                });

                //Reset object in new mode.
                function reset() {
                    $scope.dialogForm.$setPristine();
                    return angular.copy(yumiki.shopper.defaultObject);
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {
                        yumiki.message.displayLoadingDialog(true);

                        DataService.save($scope.object).then(
                            function success(response) {
                                $scope.id = undefined;
                                $('#dlgAddEditObject').modal('hide');

                                reload();
                            },
                            function error(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.shopper.errorLogType);
                            });
                    }
                };

                //After saving, reload list.
                function reload () {
                    $rootScope.$broadcast("OnReload");
                };
            });
        }
    };
}(window, document, jQuery, yumiki));