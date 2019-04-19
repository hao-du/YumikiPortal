(function (win, doc, $, yumiki) {
    yumiki.shopper.feeType = {
        showActiveText: 'Show Active Fee Types',
        showInactiveText: 'Show Inactive Fee Types',

        init: function (getAllUrl, getByIdUrl, saveUrl) {
            var app = angular.module('shopperFeeType', ['ui.bootstrap']);

            yumiki.shopper.feeType.initService(app, getAllUrl, getByIdUrl, saveUrl);
            yumiki.shopper.feeType.initListController(app);
            yumiki.shopper.feeType.initDialogController(app, 'Fee Type');
        },

        //Service to communicate with Server.
        initService: function (app, getAllUrl, getByIdUrl, saveUrl) {
            app.service('DataService', function ($http) {
                this.getList = function (showInactive) {
                    return $http.get(getAllUrl, { params: { 'showInactive': showInactive } });
                };

                this.getByID = function (id) {
                    return $http.get(getByIdUrl, { params: { 'feeTypeId': id } });
                };

                this.save = function (currency) {
                    return $http.post(saveUrl, currency);
                };
            });

        },

        //CONTROLLER FOR LIST
        initListController: function (app) {
            app.controller('feeTypeController', function ($scope, $rootScope, DataService) {
                $scope.inactiveButtonName = yumiki.shopper.feeType.showInactiveText;

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
                    if ($scope.inactiveButtonName == yumiki.shopper.feeType.showInactiveText) {
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
                    if ($scope.inactiveButtonName == yumiki.shopper.feeType.showInactiveText) {
                        $scope.inactiveButtonName = yumiki.shopper.feeType.showActiveText;
                    } else {
                        $scope.inactiveButtonName = yumiki.shopper.feeType.showInactiveText;
                    }
                }
            });
        },

        //CONTROLLER FOR DIALOG
        initDialogController: function (app, defaultObject, dialogName) {
            app.controller('feeTypeDialogController', function ($scope, $rootScope, DataService) {
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
        }
    };
}(window, document, jQuery, yumiki));