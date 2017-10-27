(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.report = {
        errorLogType: '',
        chart: null,
        reportType: {},
        currentDate : '',
        longDateFormat : '',

        //reportControlID: canvas for chart.js
        initReport: function (reportControlID, requestParameterObject, serviceUrls, reportType, currentDate, longDateFormat, errorLogType) {
            yumiki.moneyTrace.report.reportControlID = reportControlID;
            yumiki.moneyTrace.report.errorLogType = errorLogType;
            yumiki.moneyTrace.report.reportType = reportType;

            yumiki.moneyTrace.report.currentDate = currentDate;
            yumiki.moneyTrace.report.longDateFormat = longDateFormat;

            yumiki.moneyTrace.report.initTagAutocomplete(serviceUrls.getTagUrl);
            yumiki.moneyTrace.report.initReportChart(reportControlID);

            var app = angular.module('report', ['ui.bootstrap', 'yumiki-module']);

            yumiki.moneyTrace.report.initService(app, serviceUrls);
            yumiki.moneyTrace.report.initReportController(app, requestParameterObject);
        },

        //Service to communicate with Server.
        initService: function (app, serviceUrls) {
            app.service('DataService', function ($http) {
                this.generateReport = function (requestObject) {
                    return $http.post(serviceUrls.reportGenerateReportUrl, requestObject);
                };

                this.getReportTypes = function () {
                    return $http.get(serviceUrls.reportGetReportTypesUrl);
                };

                this.getTransactionTypes = function () {
                    return $http.get(serviceUrls.reportGetTransactionTypes);
                };

                this.getCurrencyList = function () {
                    return $http.get(serviceUrls.currencyGetAllWithShareableItemsUrl, { params: { 'showInactive': false } });
                };
            });
        },

        //Controller to display report
        initReportController: function (app, requestParameterObject) {
            app.controller('reportController', function ($scope, $http, DataService) {
                $scope.showReport = false;

                $scope.requestObject = requestParameterObject;

                $scope.loadData = function () {
                    yumiki.message.displayLoadingDialog(true);

                    var isCurrencyLoaded, isReportTypeLoaded, isTransactionTypeLoaded;
                    isCurrencyLoaded = isReportTypeLoaded = isTransactionTypeLoaded = false;

                    //Load Currencies to DropdownList
                    DataService.getCurrencyList().then(
                        function mySucces(response) {
                            $scope.currencyList = response.data;

                            isCurrencyLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded && isTransactionTypeLoaded);

                        }, function myError(response) {
                            isCurrencyLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded && isTransactionTypeLoaded);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.report.errorLog);
                        }
                    );

                    //Load Report Types to DropdownList
                    DataService.getReportTypes().then(
                        function mySucces(response) {
                            $scope.reportTypes = response.data;

                            isReportTypeLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded && isTransactionTypeLoaded);

                            $scope.updateDates();

                        }, function myError(response) {
                            isReportTypeLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded && isTransactionTypeLoaded);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.report.errorLog);
                        }
                    );

                    //Load Transaction Types to DropdownList
                    DataService.getTransactionTypes().then(
                        function mySucces(response) {
                            $scope.transactionTypes = response.data;

                            isTransactionTypeLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded && isTransactionTypeLoaded);

                        }, function myError(response) {
                            isTransactionTypeLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded && isTransactionTypeLoaded);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.report.errorLog);
                        }
                    );
                };

                //Update Start Date and End Date
                $scope.updateDates = function () {
                    $scope.isEndDateVisible = false;
                    $scope.startDateLabel = "Start Date";

                    if (!$scope.requestObject.StartDate) {
                        $scope.requestObject.StartDate = yumiki.moneyTrace.report.currentDate;
                    }

                    switch ($scope.requestObject.ReportType) {
                        case yumiki.moneyTrace.report.reportType.month:
                            $scope.startDateLabel = "Month";

                            $scope.requestObject.StartDate = moment($scope.requestObject.StartDate).startOf('month').format(yumiki.moneyTrace.report.longDateFormat);
                            $scope.requestObject.EndDate = moment($scope.requestObject.EndDate).endOf('month').format(yumiki.moneyTrace.report.longDateFormat);
                            break;
                        case yumiki.moneyTrace.report.reportType.period:
                            $scope.isEndDateVisible = true;

                            $scope.requestObject.EndDate = moment($scope.requestObject.StartDate).endOf('month').format(yumiki.moneyTrace.report.longDateFormat);
                            break;
                        case yumiki.moneyTrace.report.reportType.year:
                            $scope.startDateLabel = "Year";

                            $scope.requestObject.StartDate = moment($scope.requestObject.StartDate).startOf('year').format(yumiki.moneyTrace.report.longDateFormat);
                            $scope.requestObject.EndDate = moment($scope.requestObject.EndDate).endOf('year').format(yumiki.moneyTrace.report.longDateFormat);

                            break;
                    }
                };

                $scope.getReport = function (isValid) {
                    if (isValid) {
                        yumiki.message.displayLoadingDialog(true);

                        DataService.generateReport($scope.requestObject).then(
                            function success(response) {
                                var results = response.data.Records;

                                var data, labels;
                                data = [];
                                labels = [];
                                results.forEach(function (result) {
                                    data.push(result.Value);
                                    labels.push(result.Label);
                                });

                                if (yumiki.moneyTrace.report.chart) {
                                    yumiki.moneyTrace.report.chart.config.data.datasets[0].data = data;
                                    yumiki.moneyTrace.report.chart.data.labels = labels;
                                    yumiki.moneyTrace.report.chart.update();
                                }

                                $scope.showReport = true;

                                yumiki.message.displayLoadingDialog(false);

                            }, function error(response) {
                                yumiki.message.displayLoadingDialog(false);
                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.report.errorLogType);

                                $scope.showReport = false;
                            }
                        );
                    }
                };
            });
        },

        initTagAutocomplete: function (getTagUrl) {
            yumiki.jquery.autocomplete('.auto-complete', getTagUrl);
        }, 

        initReportChart: function (reportControlID) {
            var options = {
                elements: {
                    line: {
                        tension: 0
                    }
                },
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true,
                            callback: function (value, index, values) {
                                if (parseInt(value) >= 1000 || parseInt(value) <= -1000) {
                                    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                } else {
                                    return value;
                                }
                            }
                        }
                    }]
                },
                tooltips: {
                    callbacks: {
                        label: function (tooltipItem, data) {
                            return tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        }
                    }
                }
            };

            yumiki.moneyTrace.report.chart = new Chart(reportControlID, {
                type: 'line',
                data: {
                    labels: [],
                    datasets: [{
                        backgroundColor: '#f00',
                        borderColor: '#f00',
                        data: [],
                        label: '',
                        fill: 'false'

                    }]
                },
                options: Chart.helpers.merge(options)
            });
        },

        closeLoadingDialog: function (canClose) {
            if (canClose) {
                yumiki.message.displayLoadingDialog(false);
            }
        },
    };
}(window, document, jQuery, yumiki));