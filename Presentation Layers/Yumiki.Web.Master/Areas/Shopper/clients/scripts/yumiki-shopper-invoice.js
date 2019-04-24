(function (win, doc, $, yumiki) {
    yumiki.shopper.invoice = {
        showActiveText: 'Show Active Invoices',
        showInactiveText: 'Show Inactive Invoices',
        longDateFormat: '',

        init: function (serviceUrls, longDateFormat, defaultDetailObject, defaultExtraFeeObject) {
            yumiki.shopper.invoice.longDateFormat = longDateFormat;

            var app = angular.module('shopperInvoice', ['ui.bootstrap', 'yumiki-module']);

            yumiki.shopper.invoice.initService(app, serviceUrls);
            yumiki.shopper.invoice.initListController(app);
            yumiki.shopper.invoice.initDialogController(app, 'Invoice', defaultDetailObject, defaultExtraFeeObject);
        },

        //Service to communicate with Server.
        initService: function (app, serviceUrls) {
            app
                .service('DataService', function ($http) {
                    this.getList = function (showInactive) {
                        return $http.get(serviceUrls.getAllUrl, { params: { 'showInactive': showInactive } });
                    };

                    this.getByID = function (id) {
                        return $http.get(serviceUrls.getByIdUrl, { params: { 'invoiceId': id } });
                    };

                    this.getProducts = function (term) {
                        return $http.get(serviceUrls.getProductsByTerm, { params: { 'term': term } });
                    };

                    this.getFeeTypeByTerm = function (term) {
                        return $http.get(serviceUrls.getFeeTypeByTerm, { params: { 'term': term, 'forReceipt': false, 'forInvoice': true, 'forAdditionFee': false } });
                    };

                    this.save = function (object) {
                        return $http.post(serviceUrls.saveUrl, object);
                    };
                })
                .directive('productautocomplete', function (DataService) {
                    return {
                        restrict: 'A',
                        require: 'ngModel',
                        link: function (scope, element, attrs, ngModel) {
                            element.autocomplete({
                                source: function (request, response) {
                                    DataService.getProducts(request.term).then(
                                        function success(result) {
                                            response($.map(result.data, function (item) {
                                                return {
                                                    label: item.ProductCode + ' - ' + item.ProductName,
                                                    value: item.ProductCode + ' - ' + item.ProductName,
                                                    data: item
                                                };
                                            }));
                                        },
                                        function error(result) {
                                            response(result.data);
                                        }
                                    );
                                },
                                minLength: 3,
                                select: function (event, ui) {
                                    ngModel.$setViewValue(ui.item.value);
                                    scope.selected_product = ui.item.data;

                                    scope.object.OriginalPrice = scope.selected_product.OriginalPrice;
                                    scope.object.UnitPrice = scope.selected_product.Price;

                                    scope.$apply();
                                },
                                create: function () {
                                    $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                                        return $('<li>')
                                            .append('<a>' + item.data.ProductCode + ' - ' + item.data.ProductName + '</a>')
                                            .appendTo(ul);
                                    };
                                }
                            });
                        }
                    };
                })
                .directive('feetypeautocomplete', function (DataService) {
                    return {
                        restrict: 'A',
                        require: 'ngModel',
                        link: function (scope, element, attrs, ngModel) {
                            element.autocomplete({
                                source: function (request, response) {
                                    DataService.getFeeTypeByTerm(request.term).then(
                                        function success(result) {
                                            response($.map(result.data, function (item) {
                                                return {
                                                    label: item.FeeTypeName,
                                                    value: item.FeeTypeName,
                                                    data: item
                                                };
                                            }));
                                        },
                                        function error(result) {
                                            response(result.data);
                                        }
                                    );
                                },
                                minLength: 3,
                                select: function (event, ui) {
                                    ngModel.$setViewValue(ui.item.value);
                                    scope.selected_fee = ui.item.data;
                                    scope.$apply();
                                },
                                create: function () {
                                    $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                                        return $('<li>')
                                            .append('<a>' + item.data.FeeTypeName + '</a>')
                                            .appendTo(ul);
                                    };
                                }
                            });
                        }
                    };
                });
        },

        //CONTROLLER FOR LIST
        initListController: function (app) {
            app.controller('invoiceController', function ($scope, $rootScope, DataService) {
                $scope.inactiveButtonName = yumiki.shopper.invoice.showInactiveText;

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
                    if ($scope.inactiveButtonName == yumiki.shopper.invoice.showInactiveText) {
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
                    if ($scope.inactiveButtonName == yumiki.shopper.invoice.showInactiveText) {
                        $scope.inactiveButtonName = yumiki.shopper.invoice.showActiveText;
                    } else {
                        $scope.inactiveButtonName = yumiki.shopper.invoice.showInactiveText;
                    }
                }
            });
        },

        //CONTROLLER FOR DIALOG
        initDialogController: function (app, dialogName, defaultDetailObject, defaultExtraFeeObject) {
            app.controller('invoiceDialogController', function ($scope, $rootScope, DataService) {
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

                //Display Details dialog and pass data to Dialog.
                $scope.onShowDetailDialog = function (detail) {
                    //Call DialogController
                    $rootScope.$broadcast("OnDetailLoad", detail);
                };

                $rootScope.$on('OnDetailChanged', function (event, detail) {
                    detail.InvoiceID = $scope.object.ID;
                    detail.Amount = detail.UnitPrice * detail.Quantity;

                    var isExisted = false;

                    angular.forEach($scope.object.InvoiceDetails, function (item) {
                        if (item.ID == detail.ID || item.ProductCode == detail.ProductCode) {
                            item.InvoiceID = detail.InvoiceID;
                            item.ProductID = detail.ProductID;
                            item.ProductCode = detail.ProductCode;
                            item.ProductName = detail.ProductName;
                            item.OriginalPrice = detail.OriginalPrice;
                            item.UnitPrice = detail.UnitPrice;
                            item.Quantity = detail.Quantity;
                            item.Amount = detail.Amount;
                            item.Descriptions = detail.Descriptions;
                            item.Product = detail.Product;

                            isExisted = true;
                        }
                    });

                    if (!isExisted) {
                        $scope.object.InvoiceDetails.unshift(detail);
                    }

                    calculateAmount();
                });

                $scope.onDeleteDetail = function (detail) {
                    var index = $scope.object.InvoiceDetails.indexOf(detail);
                    $scope.object.InvoiceDetails.splice(index, 1);

                    calculateAmount();
                };

                //Display Extra Fees dialog and pass data to Dialog.
                $scope.onShowExtraFeeDialog = function (extraFee) {
                    //Call DialogController
                    $rootScope.$broadcast("OnExtraFeeLoad", extraFee);
                };

                $rootScope.$on('OnExtraFeeChanged', function (event, extraFee) {
                    extraFee.InvoiceID = $scope.object.ID;

                    var isExisted = false;

                    angular.forEach($scope.object.InvoiceExtraFees, function (item) {
                        if (item.ID == extraFee.ID) {
                            item.InvoiceID = extraFee.InvoiceID;
                            item.FeeTypeID = extraFee.FeeTypeID;
                            item.FeeTypeName = extraFee.FeeTypeName;
                            item.Amount = extraFee.Amount;
                            item.Descriptions = extraFee.Descriptions;
                            item.FeeType = extraFee.FeeType;

                            isExisted = true;
                        }
                    });

                    if (!isExisted) {
                        $scope.object.InvoiceExtraFees.unshift(extraFee);
                    }

                    calculateAmount();
                });

                $scope.onDeleteExtraFee = function (extraFee) {
                    var index = $scope.object.InvoiceExtraFees.indexOf(extraFee);
                    $scope.object.InvoiceExtraFees.splice(index, 1);

                    calculateAmount();
                };

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
                function reload() {
                    $rootScope.$broadcast("OnReload");
                }

                //Calculate Amount.
                function calculateAmount() {
                    $scope.object.TotalAmount = 0;

                    angular.forEach($scope.object.InvoiceDetails, function (item) {
                        $scope.object.TotalAmount += parseInt(item.Amount);
                    });

                    angular.forEach($scope.object.InvoiceExtraFees, function (item) {
                        $scope.object.TotalAmount += parseInt(item.Amount);
                    });
                }

            });

            app.controller('detailDialogController', function ($scope, $rootScope) {
                //Deligate event is called by other controllers.
                $rootScope.$on('OnDetailLoad', function (event, detail) {
                    $('#dlgAddEditDetail').modal({ backdrop: 'static' });

                    if (detail) {
                        $scope.dialogTitle = "Edit Detail...";
                        $scope.object = angular.copy(detail);

                        $scope.selected_product = $scope.object.Product;
                    } else {
                        $scope.dialogTitle = "New Detail...";
                        $scope.object = reset();

                        $scope.object.ID = '' + Date.now();
                    }
                });

                //Reset object in new mode.
                function reset() {
                    $scope.dialogForm.$setPristine();
                    return angular.copy(defaultDetailObject);
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {
                        $scope.object.Product = $scope.selected_product;

                        $scope.object.ProductID = $scope.selected_product.ID;
                        $scope.object.ProductName = $scope.selected_product.ProductName;
                        $scope.object.ProductCode = $scope.selected_product.ProductCode;

                        $('#dlgAddEditDetail').modal('hide');
                        reload();
                    }
                };

                //After saving, pass object to parent form
                function reload() {
                    $rootScope.$broadcast("OnDetailChanged", $scope.object);
                }
            });

            app.controller('extraFeeDialogController', function ($scope, $rootScope) {
                //Deligate event is called by other controllers.
                $rootScope.$on('OnExtraFeeLoad', function (event, extraFee) {
                    $('#dlgAddEditExtraFee').modal({ backdrop: 'static' });

                    if (extraFee) {
                        $scope.dialogTitle = "Edit Extra Fee...";
                        $scope.object = angular.copy(extraFee);

                        $scope.selected_fee = $scope.object.FeeType;
                    } else {
                        $scope.dialogTitle = "New Extra Fee...";
                        $scope.object = reset();

                        $scope.object.ID = '' + Date.now();
                    }
                });

                //Reset object in new mode.
                function reset() {
                    $scope.dialogForm.$setPristine();
                    return angular.copy(defaultExtraFeeObject);
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {
                        $scope.object.FeeType = $scope.selected_fee;

                        $scope.object.FeeTypeID = $scope.selected_fee.ID;
                        $scope.object.FeeTypeName = $scope.selected_fee.FeeTypeName;

                        $('#dlgAddEditExtraFee').modal('hide');
                        reload();
                    }
                };

                //After saving, pass object to parent form
                function reload() {
                    $rootScope.$broadcast("OnExtraFeeChanged", $scope.object);
                }
            });
        }
    };
}(window, document, jQuery, yumiki));