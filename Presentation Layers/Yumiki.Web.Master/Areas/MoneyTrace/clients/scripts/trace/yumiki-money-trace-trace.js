(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.trace = {
        currentDate: '',
        monthYearFormat: '',
        longDateFormat: '',

        traceType: {},

        errorLog: '',

        app: {},

        init: function (serviceUrls, currentDate, monthYearFormat, longDateFormat, traceType, errorLog) {

            yumiki.moneyTrace.trace.currentDate = currentDate;
            yumiki.moneyTrace.trace.monthYearFormat = monthYearFormat;
            yumiki.moneyTrace.trace.longDateFormat = longDateFormat;
            yumiki.moneyTrace.trace.traceType = traceType;
            yumiki.moneyTrace.trace.errorLog = errorLog;

            yumiki.moneyTrace.trace.app = angular.module('moneyTrace', ['ui.bootstrap', 'yumiki-module']);

            yumiki.moneyTrace.trace.initServices(serviceUrls);

            yumiki.moneyTrace.trace.list.init();
        },

        //Services to communicate with Server.
        initServices: function (serviceUrls) {
            yumiki.moneyTrace.trace.app.service('DataService', function ($http) {
                this.getCurrencyList = function () {
                    return $http.get(serviceUrls.currencyGetAllWithShareableItemsUrl, { params: { 'showInactive': false } });
                };

                this.getBanks = function () {
                    return $http.get(serviceUrls.bankGetAllWithShareableItemsUrl, { params: { 'showInactive': false } });
                };

                this.getUsers = function () {
                    return $http.get(serviceUrls.userGetAllUrl, { params: { 'showInactive': false } });
                };

                this.getTraceList = function (datetime, showInactive, isDisplayedAll, currentPage, itemsPerPage) {
                    if (datetime == '') {
                        datetime = moment(yumiki.moneyTrace.trace.currentDate).format(yumiki.moneyTrace.trace.monthYearFormat);
                    }

                    return $http.get(serviceUrls.traceGetAllUrl, { params: { 'showInactive': showInactive, 'month': datetime, 'isDisplayedAll': isDisplayedAll, 'currentPage': currentPage, 'itemsPerPage': itemsPerPage } });
                };

                this.getTotalSummaryTraces = function () {
                    return $http.get(serviceUrls.traceGetTotalSummaryUrl);
                };

                this.getBankSummaryTraces = function () {
                    return $http.get(serviceUrls.traceGetBankingSummaryUrl);
                };

                this.getTraceByID = function (traceID) {
                    return $http.get(serviceUrls.traceGetByIDUrl, { params: { 'traceID': traceID } });
                };

                this.save = function (trace) {
                    return $http.post(serviceUrls.traceSaveUrl, trace);
                };
            });
        },

    };
}(window, document, jQuery, yumiki));