(function (win, doc, $, yumiki) {
    yumiki.shopper.productOffset = {
        showActiveText: 'Show Active Offsets',
        showInactiveText: 'Show Inactive Offsets',

        init: function (serviceUrls, dialogName) {
            var app = angular.module('shopperProductOffset', ['ui.bootstrap', 'yumiki-module']);

            yumiki.shopper.productOffset.initService(app, serviceUrls);
            yumiki.shopper.productOffset.initListController(app);
            yumiki.shopper.productOffset.initDialogController(app, dialogName);
        },

        //Service to communicate with Server.
        initService: function (app, serviceUrls) {
            app
                .service('DataService', function ($http) {
                    this.getList = function (showInactive, productid) {
                        if (!productid) {
                            productid = '';
                        }

                        return $http.get(serviceUrls.getAllUrl, { params: { 'showInactive': showInactive, 'productID': productid } });
                    };

                    this.getByID = function (id) {
                        return $http.get(serviceUrls.getByIdUrl, { params: { 'offsetID': id } });
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
                });
        },

        //CONTROLLER FOR LIST
        initListController: function (app) {
            app.controller('productOffsetController', function ($scope, $rootScope, DataService) {
                $scope.inactiveButtonName = yumiki.shopper.productOffset.showInactiveText;

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
                    if ($scope.inactiveButtonName === yumiki.shopper.productOffset.showInactiveText) {
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
                    if ($scope.inactiveButtonName === yumiki.shopper.productOffset.showInactiveText) {
                        $scope.inactiveButtonName = yumiki.shopper.productOffset.showActiveText;
                    } else {
                        $scope.inactiveButtonName = yumiki.shopper.productOffset.showInactiveText;
                    }
                }
            });
        },

        //CONTROLLER FOR DIALOG
        initDialogController: function (app, dialogName) {
            app.controller('productOffsetDialogController', function ($scope, $rootScope, DataService) {
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
                                $scope.selected_product = $scope.object.Product;

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

                        $scope.object.Product = $scope.selected_product;
                        $scope.object.ProductID = $scope.selected_product.ID;

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
                };
            });
        }
    };
}(window, document, jQuery, yumiki));