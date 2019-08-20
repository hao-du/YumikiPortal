(function (win, doc, $, yumiki) {
    yumiki.shopper.product = {
        showActiveText: 'Show Active Products',
        showInactiveText: 'Show Inactive Products',

        init: function (getAllUrl, getByIdUrl, getInvoicesByProductIDUrl, getReceiptsByProductIDUrl, saveUrl) {
            var app = angular.module('shopperProduct', ['ui.bootstrap']);

            yumiki.shopper.product.initService(app,
                getAllUrl,
                getByIdUrl,
                getInvoicesByProductIDUrl,
                getReceiptsByProductIDUrl,
                saveUrl);


            yumiki.shopper.product.initListController(app);
            yumiki.shopper.product.initDialogController(app, 'Product');
            yumiki.shopper.product.initInvoiceListDialogController(app);
            yumiki.shopper.product.initReceiptListDialogController(app);
        },

        //Service to communicate with Server.
        initService: function (app, getAllUrl, getByIdUrl, getInvoicesByProductIDUrl, getReceiptsByProductIDUrl, saveUrl) {
            app.service('DataService', function ($http) {
                this.getList = function (showInactive) {
                    return $http.get(getAllUrl, { params: { 'showInactive': showInactive } });
                };

                this.getByID = function (id) {
                    return $http.get(getByIdUrl, { params: { 'productId': id } });
                };

                this.getInvoicesByProductID = function (productID) {
                    return $http.get(getInvoicesByProductIDUrl, { params: { 'productId': productID } });
                };

                this.getReceiptsByProductID = function (productID) {
                    return $http.get(getReceiptsByProductIDUrl, { params: { 'productId': productID } });
                };

                this.save = function (object) {
                    return $http.post(saveUrl, object);
                };
            });

        },

        //CONTROLLER FOR LIST
        initListController: function (app) {
            app.controller('productController', function ($scope, $rootScope, DataService) {
                $scope.inactiveButtonName = yumiki.shopper.product.showInactiveText;

                //To determine when load active or inactive list.
                $scope.isStatusChanged = false;

                //Display dialog and pass data to Dialog.
                $scope.onShowDialog = function (id) {
                    //Call DialogController
                    $rootScope.$broadcast("OnLoad", id);
                };

                //Display dialog and pass data to Dialog.
                $scope.onShowInvoiceListDialog = function (productid) {
                    //Call DialogController
                    $rootScope.$broadcast("OnInvoiceListLoad", productid);
                };

                //Display dialog and pass data to Dialog.
                $scope.onShowReceiptListDialog = function (productid) {
                    //Call DialogController
                    $rootScope.$broadcast("OnReceiptListLoad", productid);
                };

                //Event for other controllers to reload data.
                $rootScope.$on('OnReload', function (event) {
                    $scope.onLoad();
                });

                //Load data
                $scope.onLoad = function () {
                    var showInactive = true;
                    if ($scope.inactiveButtonName === yumiki.shopper.product.showInactiveText) {
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
                    if ($scope.inactiveButtonName === yumiki.shopper.product.showInactiveText) {
                        $scope.inactiveButtonName = yumiki.shopper.product.showActiveText;
                    } else {
                        $scope.inactiveButtonName = yumiki.shopper.product.showInactiveText;
                    }
                }
            });
        },

        //CONTROLLER FOR DIALOG
        initDialogController: function (app, dialogName) {
            app.controller('productDialogController', function ($scope, $rootScope, DataService) {
                $scope.id = undefined;
                $scope.isActiveDisabled = false;

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
        },

        //CONTROLLER FOR INVOICE LIST DIALOG
        initInvoiceListDialogController: function (app) {
            app.controller('invoiceListDialogController', function ($scope, $rootScope, DataService) {
                //Deligate event is called by other controllers.
                $rootScope.$on('OnInvoiceListLoad', function (event, productID) {
                    $('#dlgInvoiceListObject').modal({ backdrop: 'static' });

                    if (productID) {
                        yumiki.message.displayLoadingDialog(true);

                        DataService.getInvoicesByProductID(productID).then(
                            function success(response) {
                                $scope.invoiceList = response.data;

                                yumiki.message.displayLoadingDialog(false);
                            },
                            function error(response) {
                                yumiki.message.displayLoadingDialog(false);
                                yumiki.message.clientMessage(response.data, '', yumiki.shopper.errorLogType);
                            });
                    } else {
                        $scope.invoiceList = null;
                    }
                });
            });
        },

        //CONTROLLER FOR INVOICE LIST DIALOG
        initReceiptListDialogController: function (app) {
            app.controller('receiptListDialogController', function ($scope, $rootScope, DataService) {
                //Deligate event is called by other controllers.
                $rootScope.$on('OnReceiptListLoad', function (event, productID) {
                    $('#dlgReceiptListObject').modal({ backdrop: 'static' });

                    if (productID) {
                        yumiki.message.displayLoadingDialog(true);

                        DataService.getReceiptsByProductID(productID).then(
                            function success(response) {
                                $scope.receiptList = response.data;

                                yumiki.message.displayLoadingDialog(false);
                            },
                            function error(response) {
                                yumiki.message.displayLoadingDialog(false);
                                yumiki.message.clientMessage(response.data, '', yumiki.shopper.errorLogType);
                            });
                    } else {
                        $scope.receiptList = null;
                    }
                });
            });
        }
    };
}(window, document, jQuery, yumiki));