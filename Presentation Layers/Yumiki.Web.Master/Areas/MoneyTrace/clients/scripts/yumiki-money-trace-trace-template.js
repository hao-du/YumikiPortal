(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.traceTemplate = {
        showActiveText: 'Show Active Template',
        showInactiveText: 'Show Inactive Template',
        errorLogType: '',

        initTraceTemplate: function (defaultObject, getAllTemplatesUrl, getTemplateByIdUrl, saveTemplateUrl, getTagUrl, getCurrencyUrl, getUserUrl, errorLogType) {
            var app = angular.module('traceTemplate', ['ui.bootstrap']);

            yumiki.moneyTrace.traceTemplate.initService(app, getAllTemplatesUrl, getTemplateByIdUrl, saveTemplateUrl, getCurrencyUrl, getUserUrl);
            yumiki.moneyTrace.traceTemplate.initTraceTemplateListController(app);
            yumiki.moneyTrace.traceTemplate.initTraceTemplateDialogController(app, defaultObject);

            yumiki.jquery.autocomplete('.auto-complete', getTagUrl);

            yumiki.moneyTrace.traceTemplate.errorLogType = errorLogType;
        },

        //Service to communicate with Server.
        initService: function (app, getAllTraceTemplateUrl, getTraceTemplateByIdUrl, saveTraceTemplateUrl, getCurrencyUrl, getUserUrl) {
            app.service('DataService', function ($http) {
                this.getTraceTemplateList = function (showInactive) {
                    return $http.get(getAllTraceTemplateUrl, { params: { 'showInactive': showInactive } });
                };

                this.getTraceTemplateByID = function (traceTemplateID) {
                    return $http.get(getTraceTemplateByIdUrl, { params: { 'traceTemplateID': traceTemplateID } });
                };

                this.save = function (traceTemplate) {
                    return $http.post(saveTraceTemplateUrl, traceTemplate);
                };

                this.getCurrencyList = function () {
                    return $http.get(getCurrencyUrl, { params: { 'showInactive': false } });
                };

                this.getUsers = function () {
                    return $http.get(getUserUrl, { params: { 'showInactive': false } });
                };
            });

        },

        //Controller to display items' list
        initTraceTemplateListController: function (app) {
            app.controller('traceTemplateController', function ($scope, $rootScope, $http, DataService) {
                $scope.inactiveButtonName = yumiki.moneyTrace.traceTemplate.showInactiveText;

                //To determine when load active or inactive list.
                $scope.isStatusChanged = false;

                //Display Dialog and pass data to Dialog.
                $scope.showDialog = function (traceTemplateID) {
                    //Call DialogController
                    $rootScope.$broadcast("OnLoad", traceTemplateID);
                }

                //Event for other controllers to reload data.
                $rootScope.$on('OnReloadData', function (event) {
                    $scope.loadData();
                });

                //Load active or inactive list.
                $scope.loadData = function () {
                    var showInactive = true;
                    if ($scope.inactiveButtonName == yumiki.moneyTrace.traceTemplate.showInactiveText) {
                        showInactive = false;
                    }

                    if ($scope.isStatusChanged) {
                        showInactive = !showInactive;
                    }

                    yumiki.message.displayLoadingDialog(true);
                    DataService.getTraceTemplateList(showInactive).then(
                        function mySucces(response) {
                            $scope.traceTemplateList = response.data;

                            if ($scope.isStatusChanged) {
                                if ($scope.inactiveButtonName == yumiki.moneyTrace.traceTemplate.showInactiveText) {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.traceTemplate.showActiveText;
                                } else {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.traceTemplate.showInactiveText;
                                }
                                $scope.isStatusChanged = false;
                            }
                            yumiki.message.displayLoadingDialog(false);
                        }, function myError(response) {
                            $scope.isStatusChanged = false;
                            yumiki.message.displayLoadingDialog(false);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.traceTemplate.errorLogType);
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
        initTraceTemplateDialogController: function (app, defaultObject) {
            app.controller('traceTemplateDialogController', function ($scope, $rootScope, $http, DataService) {
                $scope.traceTemplateID = undefined;
                $scope.isActiveDisabled = false;

                //Used to be called by other controllers.
                $rootScope.$on('OnLoad', function (event, traceTemplateID) {
                    $('#dlgTraceTemplate').modal({ backdrop: 'static' });

                    if (traceTemplateID != undefined && traceTemplateID != null) {
                        $scope.traceTemplateID = traceTemplateID;
                        $scope.isActiveDisabled = false;
                        $scope.dialogTitle = "Edit TraceTemplate";

                        yumiki.message.displayLoadingDialog(true);

                        DataService.getTraceTemplateByID($scope.traceTemplateID)
                            .then(function mySucces(response) {
                                $scope.traceTemplate = response.data;
                                yumiki.message.displayLoadingDialog(false);

                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.traceTemplate.errorLogType);
                            });

                    } else {
                        $scope.dialogTitle = "New TraceTemplate";
                        $scope.isActiveDisabled = true;

                        $scope.traceTemplate = $scope.resetTraceTemplate();
                    }
                });

                //Reset object in new mode.
                $scope.resetTraceTemplate = function () {
                    $scope.traceTemplateForm.$setPristine();
                    return defaultObject;
                }

                //Save new or update object
                $scope.save = function (isValid) {
                    if (isValid) {
                        yumiki.message.displayLoadingDialog(true);
                        DataService.save($scope.traceTemplate)
                            .then(function mySucces(response) {
                                $scope.traceTemplateID = undefined;
                                $('#dlgTraceTemplate').modal('hide');

                                $scope.ReloadData();
                            }, function myError(response) {
                                yumiki.message.displayLoadingDialog(false);

                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.traceTemplate.errorLogType);
                            });
                    }
                };

                //After saving, reload list.
                $scope.ReloadData = function () {
                    $rootScope.$broadcast("OnReloadData");
                };

                //Bind datasource to Dropdown Conctrols
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
                };
            });
        }
    };
}(window, document, jQuery, yumiki));