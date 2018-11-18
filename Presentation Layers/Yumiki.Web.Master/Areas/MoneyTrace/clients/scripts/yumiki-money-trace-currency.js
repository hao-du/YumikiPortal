(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.currency = {
        showActiveText: 'Show Active Currency',
        showInactiveText: 'Show Inactive Currency',
        errorLogType : '',

        initCurrency: function (defaultObject, getAllCurrencyUrl, getCurrencyByIdUrl, saveCurrencyUrl, errorLogType) {
            var app = angular.module('currency', ['ui.bootstrap']);

            yumiki.moneyTrace.currency.initService(app, getAllCurrencyUrl, getCurrencyByIdUrl, saveCurrencyUrl);
            yumiki.moneyTrace.currency.initCurrencyListController(app);
            yumiki.moneyTrace.currency.initCurrencyDialogController(app, defaultObject);

            yumiki.moneyTrace.currency.errorLogType = errorLogType;
        },

        //Service to communicate with Server.
        initService: function (app, getAllCurrencyUrl, getCurrencyByIdUrl, saveCurrencyUrl) {
            app.service('DataService', function ($http) {
                this.getCurrencyList = function (showInactive) {
                    return $http.get(getAllCurrencyUrl, { params: { 'showInactive': showInactive } });
                };

                this.getCurrencyByID = function (currencyID) {
                    return $http.get(getCurrencyByIdUrl, { params: { 'currencyID': currencyID } });
                };

                this.save = function (currency) {
                    return $http.post(saveCurrencyUrl, currency);
                };
            });

        },

        //Controller to display items' list
        initCurrencyListController: function (app) {
            app.controller('currencyController', function ($scope, $rootScope, $http, DataService) {
                $scope.inactiveButtonName = yumiki.moneyTrace.currency.showInactiveText;

                //To determine when load active or inactive list.
                $scope.isStatusChanged = false;

                //Display Dialog and pass data to Dialog.
                $scope.showDialog = function (currencyID) {
                    //Call DialogController
                    $rootScope.$broadcast("OnLoad", currencyID);
                }

                //Event for other controllers to reload data.
                $rootScope.$on('OnReloadData', function (event) {
                    $scope.loadData();
                });

                //Load active or inactive list.
                $scope.loadData = function () {
                    var showInactive = true;
                    if ($scope.inactiveButtonName == yumiki.moneyTrace.currency.showInactiveText) {
                        showInactive = false;
                    }

                    if ($scope.isStatusChanged) {
                        showInactive = !showInactive;
                    }

                    yumiki.message.displayLoadingDialog(true);
                    DataService.getCurrencyList(showInactive).then(
                        function mySucces(response) {
                            $scope.currencyList = response.data;

                            if ($scope.isStatusChanged) {
                                if ($scope.inactiveButtonName == yumiki.moneyTrace.currency.showInactiveText) {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.currency.showActiveText;
                                } else {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.currency.showInactiveText;
                                }
                                $scope.isStatusChanged = false;
                            }
                            yumiki.message.displayLoadingDialog(false);
                        }, function myError(response) {
                            $scope.isStatusChanged = false;
                            yumiki.message.displayLoadingDialog(false);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.currency.errorLogType);
                        });
                };

                //When click on "ShowInActiveButton", reload data with active status.
                $scope.loadDataWithStatus = function () {
                    $scope.isStatusChanged = true;
                    $scope.loadData();
                }
            });
        },

        //Controller to display dialog
        initCurrencyDialogController: function (app, defaultObject) {
            app.controller('currencyDialogController', function ($scope, $rootScope, $http, DataService) {
                $scope.currencyID = undefined;
                $scope.isActiveDisabled = false;

                //Used to be called by other controllers.
                $rootScope.$on('OnLoad', function (event, currencyID) {
                    $('#dlgCurrency').modal({ backdrop: 'static' });

                    if (currencyID != undefined && currencyID != null) {
                        $scope.currencyID = currencyID;
                        $scope.isActiveDisabled = false;
                        $scope.dialogTitle = "Edit Currency";

                        yumiki.message.displayLoadingDialog(true);

                        DataService.getCurrencyByID($scope.currencyID)
                            .then(function mySucces(response) {
                                $scope.currency = response.data;
                                yumiki.message.displayLoadingDialog(false);

                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.currency.errorLogType);
                            });

                    } else {
                        $scope.dialogTitle = "New Currency";
                        $scope.isActiveDisabled = true;

                        $scope.currency = $scope.resetCurrency();
                    }
                });

                //Reset object in new mode.
                $scope.resetCurrency = function () {
                    $scope.currencyForm.$setPristine();
                    return defaultObject;
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {
                        yumiki.message.displayLoadingDialog(true);
                        DataService.save($scope.currency)
                            .then(function mySucces(response) {
                                $scope.currencyID = undefined;
                                $('#dlgCurrency').modal('hide');

                                $scope.ReloadData();
                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.currency.errorLogType);
                            });
                    }
                };

                //After saving, reload list.
                $scope.ReloadData = function () {
                    $rootScope.$broadcast("OnReloadData");
                };
            });
        }
    };
}(window, document, jQuery, yumiki));