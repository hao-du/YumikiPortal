(function (win, doc, $, yumiki) {
    yumiki.shopper.report = {        chart: null,
        reportType: {},
        currentDate : '',
        longDateFormat : '',

        //reportControlID: canvas for chart.js
        initReport: function (reportControlID, requestParameterObject, serviceUrls, reportType, currentDate, longDateFormat) {
            yumiki.shopper.report.reportControlID = reportControlID;
            yumiki.shopper.report.reportType = reportType;

            yumiki.shopper.report.currentDate = currentDate;
            yumiki.shopper.report.longDateFormat = longDateFormat;

            yumiki.shopper.report.initTagAutocomplete(serviceUrls.getTagUrl);
            yumiki.shopper.report.initReportChart(reportControlID);

            var app = angular.module('report', ['ui.bootstrap', 'yumiki-module']);

            yumiki.shopper.report.initService(app, serviceUrls);
            yumiki.shopper.report.initReportController(app, requestParameterObject);
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
            });
        },

        //Controller to display report
        initReportController: function (app, requestParameterObject) {
            app.controller('reportController', function ($scope, $http, DataService) {
                $scope.showReport = false;

                $scope.requestObject = requestParameterObject;

                $scope.loadData = function () {
                    yumiki.message.displayLoadingDialog(true);

                    var isReportTypeLoaded = false;

                    //Load Report Types to DropdownList
                    DataService.getReportTypes().then(
                        function mySucces(response) {
                            $scope.reportTypes = response.data;

                            isReportTypeLoaded = true;
                            yumiki.shopper.report.closeLoadingDialog(isReportTypeLoaded);

                            $scope.updateReportType();

                        }, function myError(response) {
                            isReportTypeLoaded = true;
                            yumiki.shopper.report.closeLoadingDialog(isReportTypeLoaded);

                            yumiki.message.clientMessage(response.data, '', yumiki.shopper.report.errorLog);
                        }
                    );
                };

                //Update Start Date and End Date on DateTime Input Changed
                $scope.updateDates = function (index) {
                    let startTime = moment($scope.requestObject.StartDate);
                    let endTime = moment($scope.requestObject.EndDate);

                    switch (index) {
                        //Start Time Changed
                        case 0:
                            if (startTime > endTime) {
                                $scope.requestObject.EndDate = moment($scope.requestObject.StartDate).endOf('month').format(yumiki.shopper.report.longDateFormat);
                            }
                            break;
                        //End Time Changed
                        case 1:
                            if (startTime > endTime) {
                                $scope.requestObject.StartDate = moment($scope.requestObject.EndDate).startOf('month').format(yumiki.shopper.report.longDateFormat);
                            }
                            break;
                    }
                };

                //Update Start Date and End Date on Report Type Changed
                $scope.updateReportType = function (type) {
                    $scope.isEndDateVisible = false;
                    $scope.startDateLabel = "Start Date";

                    if (!$scope.requestObject.StartDate) {
                        $scope.requestObject.StartDate = yumiki.shopper.report.currentDate;
                    }

                    switch ($scope.requestObject.ReportType) {
                        case yumiki.shopper.report.reportType.month:
                            $scope.startDateLabel = "Month";

                            $scope.requestObject.StartDate = moment($scope.requestObject.StartDate).startOf('month').format(yumiki.shopper.report.longDateFormat);
                            $scope.requestObject.EndDate = moment($scope.requestObject.EndDate).endOf('month').format(yumiki.shopper.report.longDateFormat);
                            break;
                        case yumiki.shopper.report.reportType.period:
                            $scope.isEndDateVisible = true;

                            $scope.requestObject.EndDate = moment($scope.requestObject.StartDate).endOf('month').format(yumiki.shopper.report.longDateFormat);
                            break;
                        case yumiki.shopper.report.reportType.year:
                            $scope.startDateLabel = "Year";

                            $scope.requestObject.StartDate = moment($scope.requestObject.StartDate).startOf('year').format(yumiki.shopper.report.longDateFormat);
                            $scope.requestObject.EndDate = moment($scope.requestObject.EndDate).endOf('year').format(yumiki.shopper.report.longDateFormat);

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

                                if (yumiki.shopper.report.chart) {
                                    yumiki.shopper.report.chart.config.data.datasets[0].data = data;
                                    yumiki.shopper.report.chart.data.labels = labels;
                                    yumiki.shopper.report.chart.update();
                                }

                                $scope.showReport = true;

                                yumiki.message.displayLoadingDialog(false);

                            }, function error(response) {
                                yumiki.message.displayLoadingDialog(false);
                                yumiki.message.clientMessage(response.data, '', yumiki.shopper.errorLogType);

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

            yumiki.shopper.report.chart = new Chart(reportControlID, {
                type: 'bar',
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