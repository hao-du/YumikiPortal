(function (win, doc, $, yumiki) {
    yumiki.shopper.receipt = {
        showActiveText: 'Show Active Receipts',
        showInactiveText: 'Show Inactive Receipts',
        longDateFormat: '',

        init: function (serviceUrls, longDateFormat, defaultDetailObject, defaultExtraFeeObject) {
            yumiki.shopper.receipt.longDateFormat = longDateFormat;

            var app = angular.module('shopperReceipt', ['ui.bootstrap', 'yumiki-module']);

            yumiki.shopper.receipt.initService(app, serviceUrls);
            yumiki.shopper.receipt.initListController(app);
            yumiki.shopper.receipt.initDialogController(app, 'Receipt', defaultDetailObject, defaultExtraFeeObject);
        },

        //Service to communicate with Server.
        initService: function (app, serviceUrls) {
            app
                .service('DataService', function ($http) {
                    this.getList = function (showInactive) {
                        return $http.get(serviceUrls.getAllUrl, { params: { 'showInactive': showInactive } });
                    };

                    this.getByID = function (id) {
                        return $http.get(serviceUrls.getByIdUrl, { params: { 'receiptId': id } });
                    };

                    this.getProducts = function (term) {
                        return $http.get(serviceUrls.getProductsByTerm, { params: { 'term': term } });
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
                                                    label: item.ProductCode + ' ' + item.ProductName,
                                                    value: item.ProductCode + ' ' + item.ProductName,
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
                                    scope.selected_object = ui.item.data;
                                    scope.$apply();
                                },
                                create: function () {
                                    $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                                        return $('<li>')
                                            .append('<a>' + item.data.ProductName + '</a>')
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
            app.controller('receiptController', function ($scope, $rootScope, DataService) {
                $scope.inactiveButtonName = yumiki.shopper.receipt.showInactiveText;

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
                    if ($scope.inactiveButtonName == yumiki.shopper.receipt.showInactiveText) {
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
                    if ($scope.inactiveButtonName == yumiki.shopper.receipt.showInactiveText) {
                        $scope.inactiveButtonName = yumiki.shopper.receipt.showActiveText;
                    } else {
                        $scope.inactiveButtonName = yumiki.shopper.receipt.showInactiveText;
                    }
                }
            });
        },

        //CONTROLLER FOR DIALOG
        initDialogController: function (app, dialogName, defaultDetailObject, defaultExtraFeeObject) {
            app.controller('receiptDialogController', function ($scope, $rootScope, DataService) {
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
                    detail.ReceiptID = $scope.object.ID;
                });

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
            });

            app.controller('detailDialogController', function ($scope, $rootScope) {
                //Deligate event is called by other controllers.
                $rootScope.$on('OnDetailLoad', function (event, detail) {
                    $('#dlgAddEditDetail').modal({ backdrop: 'static' });

                    if (detail) {
                        $scope.dialogTitle = "Edit Detail...";
                        $scope.object = detail;
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
                        $scope.object.ProductID = $scope.selected_object.ID;
                        $scope.object.ProductName = $scope.selected_object.ProductName;
                        $scope.object.ProductCode = $scope.selected_object.ProductCode;

                        $('#dlgAddEditDetail').modal('hide');
                        reload();
                    }
                };

                //After saving, pass object to parent form
                function reload() {
                    $rootScope.$broadcast("OnDetailChanged", $scope.object);
                }
            });
        }
    };
}(window, document, jQuery, yumiki));