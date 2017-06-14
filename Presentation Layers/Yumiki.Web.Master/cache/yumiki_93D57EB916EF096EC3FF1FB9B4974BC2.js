﻿(function(n,t,i,r){r.moneyTrace={}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace={currentDate:"",monthYearFormat:"",longDateFormat:"",traceType:{},errorLog:"",app:{},init:function(n,t,i,u,f,e){r.moneyTrace.trace.currentDate=t;r.moneyTrace.trace.monthYearFormat=i;r.moneyTrace.trace.longDateFormat=u;r.moneyTrace.trace.traceType=f;r.moneyTrace.trace.errorLog=e;r.moneyTrace.trace.app=angular.module("moneyTrace",["ui.bootstrap","yumiki-module"]);r.moneyTrace.trace.initServices(n);r.moneyTrace.trace.list.init()},initServices:function(n){r.moneyTrace.trace.app.service("DataService",function(t){this.getCurrencyList=function(){return t.get(n.currencyGetAllWithShareableItemsUrl,{params:{showInactive:!1}})};this.getBanks=function(){return t.get(n.bankGetAllWithShareableItemsUrl,{params:{showInactive:!1}})};this.getUsers=function(){return t.get(n.userGetAllUrl,{params:{showInactive:!1}})};this.getTraceList=function(i,u,f,e,o){return i==""&&(i=moment(r.moneyTrace.trace.currentDate).format(r.moneyTrace.trace.monthYearFormat)),t.get(n.traceGetAllUrl,{params:{showInactive:u,month:i,isDisplayedAll:f,currentPage:e,itemsPerPage:o}})};this.getTotalSummaryTraces=function(){return t.get(n.traceGetTotalSummaryUrl)};this.getBankSummaryTraces=function(){return t.get(n.traceGetBankingSummaryUrl)};this.getTraceByID=function(i){return t.get(n.traceGetByIDUrl,{params:{traceID:i}})};this.save=function(i){return t.post(n.traceSaveUrl,i)}})}}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace.controls={init:function(n,t,i){r.moneyTrace.trace.controls.initHighlightedFocusText();r.moneyTrace.trace.controls.initMonthFilter(n,t);r.moneyTrace.trace.controls.initTagAutocomplete(i)},initHighlightedFocusText:function(){i("input:text").focus(function(){i(this).select()})},initMonthFilter:function(n,t){i("#txtMonthFilter").val(moment(n).format(t))},initTagAutocomplete:function(n){i(".auto-complete").autocomplete({source:function(t,u){i.ajax({type:"GET",url:n,contentType:"application/json; charset=utf-8",dataType:"json",data:{keyword:r.jquery.extractLast(t.term)},success:function(n){u(n)}})},search:function(){var n=r.jquery.extractLast(this.value);if(n.length<2)return!1},focus:function(){return!1},select:function(n,t){var i=r.jquery.split(this.value);return i.pop(),i.push(t.item.value),i.push(""),this.value=i.join(","),!1}})}}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace.list={showActiveText:"Show Active Traces",showInactiveText:"Show Inactive Traces",init:function(){r.moneyTrace.trace.app.controller("traceController",function(n,t,i,u){n.inactiveButtonName=r.moneyTrace.trace.list.showInactiveText;n.monthFilter=moment(r.moneyTrace.trace.currentDate).format(r.moneyTrace.trace.monthYearFormat);n.isDisplayedAll=!1;n.viewby=20;n.itemsPerPage=n.viewby;n.totalItems=0;n.currentPage=1;n.maxSize=5;n.isStatusChanged=!1;n.showTraceDialog=function(n,i){i==r.moneyTrace.trace.traceType.income||i==r.moneyTrace.trace.traceType.outcome?t.$broadcast("OnNormalTraceLoad",n):i==r.moneyTrace.trace.traceType.banking?t.$broadcast("OnBankingTraceLoad",n):i==r.moneyTrace.trace.traceType.exchange?t.$broadcast("OnExchangeTraceLoad",n):i==r.moneyTrace.trace.traceType.transfer&&t.$broadcast("OnTransferTraceLoad",n)};t.$on("OnReloadData",function(){n.loadData()});n.loadData=function(){var t=!0;n.inactiveButtonName==r.moneyTrace.trace.list.showInactiveText&&(t=!1);n.isStatusChanged&&(t=!t);r.message.displayLoadingDialog(!0);u.getTotalSummaryTraces().then(function(t){n.traceTotalSummaryList=t.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.traceType.errorLog)});u.getBankSummaryTraces().then(function(t){n.traceBankingSummaryList=t.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.traceType.errorLog)});u.getTraceList(n.monthFilter,t,n.isDisplayedAll,n.currentPage,n.itemsPerPage).then(function(t){n.traceList=t.data.Records;n.currentPage=t.data.CurrentPage;n.totalItems=t.data.TotalItems;n.itemsPerPage=t.data.ItemsPerPage;n.isStatusChanged&&(n.inactiveButtonName=n.inactiveButtonName==r.moneyTrace.trace.list.showInactiveText?r.moneyTrace.trace.list.showActiveText:r.moneyTrace.trace.list.showInactiveText,n.isStatusChanged=!1);r.message.displayLoadingDialog(!1)},function(t){n.isStatusChanged=!1;r.message.displayLoadingDialog(!1);r.message.clientMessage(t.data,"",r.moneyTrace.trace.traceType.errorLog)})};n.isDisplayAllChange=function(){n.currentPage=1;n.loadData()};n.pagingIndexChange=function(){n.totalItems>n.viewby&&n.loadData()};n.loadDataWithStatus=function(){n.isStatusChanged=!0;n.loadData()}})}}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace.normal={init:function(n){r.moneyTrace.trace.app.controller("normalTraceDialogController",function(t,u,f,e){t.traceID=undefined;t.isActiveCheckboxDisabled=!1;u.$on("OnNormalTraceLoad",function(n,u){i("#dlgNormalTrace").modal({backdrop:"static"});u!=undefined&&u!=null?(t.traceID=u,t.isActiveCheckboxDisabled=!1,t.dialogTitle="Edit Trace",r.message.displayLoadingDialog(!0),e.getTraceByID(t.traceID).then(function(n){t.trace=n.data;t.trace.TraceDate=moment(t.trace.TraceDate).format(r.moneyTrace.trace.longDateFormat);r.message.displayLoadingDialog(!1)},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})):(t.dialogTitle="New Trace",t.isActiveCheckboxDisabled=!0,t.trace=t.resetTrace())});t.resetTrace=function(){return t.traceForm.$setPristine(),n};t.save=function(n){n&&(t.trace.TransactionType=t.trace.Amount>=0?r.moneyTrace.trace.traceType.Income:r.moneyTrace.trace.traceType.Outcome,r.message.displayLoadingDialog(!0),e.save(t.trace).then(function(){t.traceID=undefined;i("#dlgNormalTrace").modal("hide");t.ReloadData()},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)}))};t.ReloadData=function(){u.$broadcast("OnReloadData")};t.bindControls=function(){e.getCurrencyList().then(function(n){t.currencyList=n.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})}})}}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace.banking={init:function(n){r.moneyTrace.trace.app.controller("bankingTraceDialogController",function(t,u,f,e){t.traceID=undefined;t.isActiveCheckboxDisabled=!1;u.$on("OnBankingTraceLoad",function(n,u){i("#dlgBankingTrace").modal({backdrop:"static"});u!=undefined&&u!=null?(t.traceID=u,t.isActiveCheckboxDisabled=!1,t.dialogTitle="Edit Banking Trace",r.message.displayLoadingDialog(!0),e.getTraceByID(t.traceID).then(function(n){t.trace=n.data;t.trace.TraceDate=moment(t.trace.TraceDate).format(r.moneyTrace.trace.longDateFormat);r.message.displayLoadingDialog(!1)},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})):(t.dialogTitle="New Banking Trace",t.isActiveCheckboxDisabled=!0,t.trace=t.resetTrace())});t.resetTrace=function(){return t.bankingTraceForm.$setPristine(),n};t.save=function(n){n&&(r.message.displayLoadingDialog(!0),e.save(t.trace).then(function(){t.traceID=undefined;i("#dlgBankingTrace").modal("hide");t.ReloadData()},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)}))};t.ReloadData=function(){u.$broadcast("OnReloadData")};t.bindControls=function(){e.getCurrencyList().then(function(n){t.currencyList=n.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)});e.getBanks().then(function(n){t.banks=n.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})}})}}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace.exchange={init:function(n){r.moneyTrace.trace.app.controller("exchangeTraceDialogController",function(t,u,f,e){t.traceID=undefined;t.isActiveCheckboxDisabled=!1;u.$on("OnExchangeTraceLoad",function(n,u){i("#dlgExchangeTrace").modal({backdrop:"static"});u!=undefined&&u!=null?(t.traceID=u,t.isActiveCheckboxDisabled=!1,t.dialogTitle="Edit Exchange Trace",r.message.displayLoadingDialog(!0),e.getTraceByID(t.traceID).then(function(n){t.trace=n.data;t.trace.TraceDate=moment(t.trace.TraceDate).format(r.moneyTrace.trace.longDateFormat);r.message.displayLoadingDialog(!1)},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})):(t.dialogTitle="New Exchange Trace",t.isActiveCheckboxDisabled=!0,t.trace=t.resetTrace())});t.resetTrace=function(){return t.exchangeTraceForm.$setPristine(),n};t.save=function(n){n&&(r.message.displayLoadingDialog(!0),e.save(t.trace).then(function(){t.traceID=undefined;i("#dlgExchangeTrace").modal("hide");t.ReloadData()},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)}))};t.ReloadData=function(){u.$broadcast("OnReloadData")};t.bindControls=function(){e.getCurrencyList().then(function(n){t.currencyList=n.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})}})}}})(window,document,jQuery,yumiki);
(function(n,t,i,r){r.moneyTrace.trace.transfer={init:function(n){r.moneyTrace.trace.app.controller("transferTraceDialogController",function(t,u,f,e){t.traceID=undefined;t.isActiveCheckboxDisabled=!1;u.$on("OnTransferTraceLoad",function(n,u){i("#dlgTransferTrace").modal({backdrop:"static"});u!=undefined&&u!=null?(t.traceID=u,t.isActiveCheckboxDisabled=!1,t.dialogTitle="Edit Transfer Trace",r.message.displayLoadingDialog(!0),e.getTraceByID(t.traceID).then(function(n){t.trace=n.data;t.trace.TraceDate=moment(t.trace.TraceDate).format(r.moneyTrace.trace.longDateFormat);r.message.displayLoadingDialog(!1)},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})):(t.dialogTitle="New Transfer Trace",t.isActiveCheckboxDisabled=!0,t.trace=t.resetTrace())});t.resetTrace=function(){return t.transferTraceForm.$setPristine(),n};t.save=function(n){n&&(r.message.displayLoadingDialog(!0),e.save(t.trace).then(function(){t.traceID=undefined;i("#dlgTransferTrace").modal("hide");t.ReloadData()},function(n){r.message.displayLoadingDialog(!1);r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)}))};t.ReloadData=function(){u.$broadcast("OnReloadData")};t.bindControls=function(){e.getCurrencyList().then(function(n){t.currencyList=n.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)});e.getUsers().then(function(n){t.userList=n.data},function(n){r.message.clientMessage(n.data,"",r.moneyTrace.trace.errorLog)})}})}}})(window,document,jQuery,yumiki);
