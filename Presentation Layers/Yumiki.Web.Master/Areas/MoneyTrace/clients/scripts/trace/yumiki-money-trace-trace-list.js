(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.trace.list = {
        showActiveText: 'Show Active Traces',
        showInactiveText: 'Show Inactive Traces',

        init: function () {

            //Controller to display items' list
            yumiki.moneyTrace.trace.app.controller('traceController', function ($scope, $rootScope, $http, DataService) {
                $scope.inactiveButtonName = yumiki.moneyTrace.trace.list.showInactiveText;
                $scope.monthFilter = moment(yumiki.moneyTrace.trace.currentDate).format(yumiki.moneyTrace.trace.monthYearFormat);
                $scope.isDisplayedAll = false;

                //For paging
                $scope.viewby = 20;
                $scope.itemsPerPage = $scope.viewby;
                $scope.totalItems = 0;
                $scope.currentPage = 1;
                $scope.maxSize = 5; //Number of pager buttons to show

                //To determine when load active or inactive list.
                $scope.isStatusChanged = false;

                //Display Normal Trace Dialog and pass data to Dialog.
                $scope.showTraceDialog = function (traceID, transactionType) {
                    //Call DialogController
                    if (transactionType == yumiki.moneyTrace.trace.traceType.income || transactionType == yumiki.moneyTrace.trace.traceType.outcome) {
                        $rootScope.$broadcast("OnNormalTraceLoad", traceID);
                    }
                    else if (transactionType == yumiki.moneyTrace.trace.traceType.banking) {
                        $rootScope.$broadcast("OnBankingTraceLoad", traceID);
                    }
                    else if (transactionType == yumiki.moneyTrace.trace.traceType.exchange) {
                        $rootScope.$broadcast("OnExchangeTraceLoad", traceID);
                    }
                    else if (transactionType == yumiki.moneyTrace.trace.traceType.transfer) {
                        $rootScope.$broadcast("OnTransferTraceLoad", traceID);
                    }
                };

                //Event for other controllers to reload data.
                $rootScope.$on('OnReloadData', function (event) {
                    $scope.loadData();
                });

                //Load active or inactive list.
                $scope.loadData = function () {
                    var showInactive = true;

                    if ($scope.inactiveButtonName == yumiki.moneyTrace.trace.list.showInactiveText) {
                        showInactive = false;
                    }

                    if ($scope.isStatusChanged) {
                        showInactive = !showInactive;
                    }

                    yumiki.message.displayLoadingDialog(true);

                    //Get total amount summary trace on left side.
                    DataService.getTotalSummaryTraces().then(
                        function mySucces(response) {
                            $scope.traceTotalSummaryList = response.data;
                        },
                        function myError(response) {
                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.traceType.errorLog);
                        });

                    //Get banking summary trace on left side.
                    DataService.getBankSummaryTraces().then(
                        function mySucces(response) {
                            $scope.traceBankingSummaryList = response.data;

                            $scope.total = 0;
                            for (var i = 0; i < response.data.length; i++) {
                                $scope.total += response.data[i][yumiki.moneyTrace.trace.totalAmountFieldName];
                            }
                        },
                        function myError(response) {
                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.traceType.errorLog);
                        });

                    //Get all records on right side.
                    DataService.getTraceList($scope.monthFilter, showInactive, $scope.isDisplayedAll, $scope.currentPage, $scope.itemsPerPage).then(
                        function mySucces(response) {
                            $scope.traceList = response.data.Records;
                            $scope.currentPage = response.data.CurrentPage;
                            $scope.totalItems = response.data.TotalItems;
                            $scope.itemsPerPage = response.data.ItemsPerPage;

                            if ($scope.isStatusChanged) {
                                if ($scope.inactiveButtonName == yumiki.moneyTrace.trace.list.showInactiveText) {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.trace.list.showActiveText;
                                } else {
                                    $scope.inactiveButtonName = yumiki.moneyTrace.trace.list.showInactiveText;
                                }
                                $scope.isStatusChanged = false;
                            }

                            yumiki.message.displayLoadingDialog(false);

                        },
                        function myError(response) {
                            $scope.isStatusChanged = false;

                            yumiki.message.displayLoadingDialog(false);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.traceType.errorLog);
                        });
                };

                $scope.isDisplayAllChange = function () {
                    $scope.currentPage = 1;
                    $scope.loadData();
                };

                $scope.pagingIndexChange = function () {
                    if ($scope.totalItems > $scope.viewby) {
                        $scope.loadData();
                    }
                };

                //When click on "ShowInActiveButton", reload data with active status.
                $scope.loadDataWithStatus = function () {
                    $scope.isStatusChanged = true;
                    $scope.loadData();
                }
            });
        },
    };
}(window, document, jQuery, yumiki));