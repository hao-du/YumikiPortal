(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.bank = {
        showActiveText: 'Show Active Bank',
        showInactiveText: 'Show Inactive Bank',
        errorLogType : '',

        initBank: function (defaultObject, getAllBankUrl, getBankByIdUrl, saveBankUrl, errorLogType) {
            var app = angular.module('bank', ['ui.bootstrap']);

            yumiki.moneyTrace.bank.initService(app, getAllBankUrl, getBankByIdUrl, saveBankUrl);
            yumiki.moneyTrace.bank.initBankListController(app);
            yumiki.moneyTrace.bank.initBankDialogController(app, defaultObject);

            yumiki.moneyTrace.bank.errorLogType = errorLogType;
        },

        //Service to communicate with Server.
        initService: function (app, getAllBankUrl, getBankByIdUrl, saveBankUrl) {
            app.service('DataService', function ($http) {
                this.getBanks = function (showInactive, currentPage, itemsPerPage) {
                    return $http.get(getAllBankUrl, { params: { 'showInactive': showInactive, 'currentPage': currentPage, 'itemsPerPage': itemsPerPage } });
                };

                this.getBankByID = function (bankID) {
                    return $http.get(getBankByIdUrl, { params: { 'bankID': bankID } });
                };

                this.save = function (bank) {
                    return $http.post(saveBankUrl, bank);
                };
            });

        },

        //Controller to display items' list
        initBankListController: function (app) {
            app.controller('bankController', function ($scope, $rootScope, $http, DataService) {
                $scope.inactiveButtonName = yumiki.moneyTrace.bank.showInactiveText;

                //To determine when load active or inactive list.
                $scope.isStatusChanged = false;

                //For paging
                $scope.viewby = 20;
                $scope.itemsPerPage = $scope.viewby;
                $scope.totalItems = 0;
                $scope.currentPage = 1;
                $scope.maxSize = 5; //Number of pager buttons to show

                //Display Dialog and pass data to Dialog.
                $scope.showDialog = function (bankID) {
                    //Call DialogController
                    $rootScope.$broadcast("OnLoad", bankID);
                }

                //Event for other controllers to reload data.
                $rootScope.$on('OnReloadData', function (event) {
                    $scope.loadData();
                });

                //Load active or inactive list.
                $scope.loadData = function () {
                    var showInactive = true;
                    if ($scope.inactiveButtonName == yumiki.moneyTrace.bank.showInactiveText) {
                        showInactive = false;
                    }

                    if ($scope.isStatusChanged) {
                        showInactive = !showInactive;
                    }

                    yumiki.message.displayLoadingDialog(true);
                    DataService.getBanks(showInactive, $scope.currentPage, $scope.itemsPerPage).then(
                        function mySucces(response) {
                            $scope.banks = response.data.Records;
                            $scope.currentPage = response.data.CurrentPage;
                            $scope.totalItems = response.data.TotalItems;
                            $scope.itemsPerPage = response.data.ItemsPerPage;

                            if ($scope.isStatusChanged) {
                                if ($scope.inactiveButtonName == yumiki.moneyTrace.bank.showInactiveText) {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.bank.showActiveText;
                                } else {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.bank.showInactiveText;
                                }
                                $scope.isStatusChanged = false;
                            }
                            yumiki.message.displayLoadingDialog(false);
                        }, function myError(response) {
                            $scope.isStatusChanged = false;
                            yumiki.message.displayLoadingDialog(false);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.bank.errorLogType);
                        });
                };

                //When click on "ShowInActiveButton", reload data with active status.
                $scope.loadDataWithStatus = function () {
                    $scope.isStatusChanged = true;
                    $scope.loadData();
                }
            });
        },

        initBankDialogController: function (app, defaultObject) {
            //Controller to display dialog
            app.controller('bankDialogController', function ($scope, $rootScope, $http, DataService) {
                $scope.bankID = undefined;
                $scope.isActiveCheckboxDisabled = false;

                //Used to be called by other controllers.
                $rootScope.$on('OnLoad', function (event, bankID) {
                    $('#dlgBank').modal({ backdrop: 'static' });

                    if (bankID != undefined && bankID != null) {
                        $scope.bankID = bankID;
                        $scope.isActiveCheckboxDisabled = false;
                        $scope.dialogTitle = "Edit Bank";

                        yumiki.message.displayLoadingDialog(true);

                        DataService.getBankByID($scope.bankID)
                            .then(function mySucces(response) {
                                $scope.bank = response.data;
                                yumiki.message.displayLoadingDialog(false);

                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.bank.errorLogType);
                            });

                    } else {
                        $scope.dialogTitle = "New Bank";
                        $scope.isActiveCheckboxDisabled = true;

                        $scope.bank = $scope.resetBank();
                    }
                });

                //Reset object in new mode.
                $scope.resetBank = function () {
                    $scope.bankForm.$setPristine();
                    return angular.copy(defaultObject);
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {
                        yumiki.message.displayLoadingDialog(true);
                        DataService.save($scope.bank)
                            .then(function mySucces(response) {
                                $scope.bankID = undefined;
                                $('#dlgBank').modal('hide');

                                $scope.ReloadData();
                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.bank.errorLogType);
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