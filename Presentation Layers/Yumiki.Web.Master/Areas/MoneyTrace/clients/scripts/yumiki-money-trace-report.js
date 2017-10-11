(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.report = {
        errorLogType: '',
        chart: null,

        //reportControlID: canvas for chart.js
        initReport: function (reportControlID, requestParameterObject, serviceUrls, errorLogType) {
            yumiki.moneyTrace.report.reportControlID = reportControlID;
            yumiki.moneyTrace.report.errorLogType = errorLogType;

            yumiki.moneyTrace.report.initReportChart(reportControlID);

            var app = angular.module('report', ['ui.bootstrap', 'yumiki-module']);

            yumiki.moneyTrace.report.initService(app, serviceUrls);
            yumiki.moneyTrace.report.initReportController(app, requestParameterObject);
        },

        //Service to communicate with Server.
        initService: function (app, serviceUrls) {
            app.service('DataService', function ($http) {
                this.generateReport = function (requestObject) {
                    return $http.post(serviceUrls.reportGetReportUrl, requestObject);
                };

                this.getReportTypes = function () {
                    return $http.get(serviceUrls.reportGetReportTypesUrl);
                };

                this.getCurrencyList = function () {
                    return $http.get(serviceUrls.currencyGetAllWithShareableItemsUrl, { params: { 'showInactive': false } });
                };
            });
        },

        //Controller to display report
        initReportController: function (app, requestParameterObject) {
            app.controller('reportController', function ($scope, $http, DataService) {

                $scope.requestObject = requestParameterObject;

                $scope.loadData = function () {
                    yumiki.message.displayLoadingDialog(true);

                    var isCurrencyLoaded, isReportTypeLoaded;
                    isCurrencyLoaded = isReportTypeLoaded = false;

                    //Load Currencies to DropdownList
                    DataService.getCurrencyList().then(
                        function mySucces(response) {
                            $scope.currencyList = response.data;

                            isCurrencyLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded);

                        }, function myError(response) {
                            isCurrencyLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                        }
                    );

                    //Load Report Types to DropdownList
                    DataService.getReportTypes().then(
                        function mySucces(response) {
                            $scope.reportTypes = response.data;

                            isReportTypeLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded);

                        }, function myError(response) {
                            isReportTypeLoaded = true;
                            yumiki.moneyTrace.report.closeLoadingDialog(isCurrencyLoaded && isReportTypeLoaded);

                            yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.trace.errorLog);
                        }
                    );
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

                            }, function error(response) {
                                yumiki.message.displayLoadingDialog(false);
                                yumiki.message.clientMessage(response.data, '', yumiki.moneyTrace.report.errorLogType);
                            }
                        );
                    }
                };
            });
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