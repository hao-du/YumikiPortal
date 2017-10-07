(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.report = {
        errorLogType: '',
        chart : null,

        //reportControlID: canvas for chart.js
        initReport: function (reportControlID, getTraceReporttUrl, errorLogType) {
            yumiki.moneyTrace.report.reportControlID = reportControlID;
            yumiki.moneyTrace.report.errorLogType = errorLogType;

            yumiki.moneyTrace.report.initReportChart(reportControlID);

            var app = angular.module('report', ['ui.bootstrap', 'yumiki-module']);

            yumiki.moneyTrace.report.initService(app, getTraceReporttUrl);
            yumiki.moneyTrace.report.initReportController(app);
        },

        //Service to communicate with Server.
        initService: function (app, getTraceReporttUrl) {
            app.service('DataService', function ($http) {
                this.getReport = function () {
                    return $http.get(getTraceReporttUrl);
                };
            });
        },

        //Controller to display report
        initReportController: function (app) {
            app.controller('reportController', function ($scope, $http, DataService) {
                //Load active or inactive list.
                $scope.loadData = function () {
                    DataService.getReport().then(
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
                        });
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
    };
}(window, document, jQuery, yumiki));