﻿
(function(win,doc,$,yumiki){yumiki.moneyTrace={};}(window,document,jQuery,yumiki));

(function(win,doc,$,yumiki){yumiki.moneyTrace.bankAccount={showActiveText:'Show Active Account',showInactiveText:'Show Inactive Account',infoLogType:'',errorLogType:'',initBankAccount:function(defaultObject,getAllBankAccountUrl,getBankAccountByIdUrl,saveBankAccountUrl,saveBankingWithdrawingTraceUrl,getAllBankWithShareableItemsUrl,getAllCurrencyWithShareableItemsUrl,infoLogType,errorLogType,longDateFormat){var app=angular.module('bankAccount',['ui.bootstrap']);yumiki.moneyTrace.bankAccount.initService(app,getAllBankAccountUrl,getBankAccountByIdUrl,saveBankAccountUrl,saveBankingWithdrawingTraceUrl,getAllBankWithShareableItemsUrl,getAllCurrencyWithShareableItemsUrl);yumiki.moneyTrace.bankAccount.initBankAccountListController(app);yumiki.moneyTrace.bankAccount.initBankAccountDialogController(app,defaultObject,longDateFormat);yumiki.moneyTrace.bankAccount.infoLogType=infoLogType;yumiki.moneyTrace.bankAccount.errorLogType=errorLogType;},initService:function(app,getAllBankAccountUrl,getBankAccountByIdUrl,saveBankAccountUrl,saveBankingWithdrawingTraceUrl,getAllBankWithShareableItemsUrl,getAllCurrencyWithShareableItemsUrl){app.service('DataService',function($http){this.getBankAccounts=function(showInactive,currentPage,itemsPerPage){return $http.get(getAllBankAccountUrl,{params:{'showInactive':showInactive,'currentPage':currentPage,'itemsPerPage':itemsPerPage}});};this.getBankAccountByID=function(bankAccountID){return $http.get(getBankAccountByIdUrl,{params:{'bankAccountID':bankAccountID}});};this.save=function(bankAccount){return $http.post(saveBankAccountUrl,bankAccount);};this.saveBankingWithdrawingTrace=function(bankAccount){return $http.post(saveBankingWithdrawingTraceUrl,bankAccount);};this.getBanks=function(){return $http.get(getAllBankWithShareableItemsUrl,{params:{'showInactive':false}});};this.getCurrencyList=function(){return $http.get(getAllCurrencyWithShareableItemsUrl,{params:{'showInactive':false}});};});},initBankAccountListController:function(app){app.controller('bankAccountController',function($scope,$rootScope,$http,DataService){$scope.inactiveButtonName=yumiki.moneyTrace.bankAccount.showInactiveText;$scope.isStatusChanged=false;$scope.viewby=20;$scope.itemsPerPage=$scope.viewby;$scope.totalItems=0;$scope.currentPage=1;$scope.maxSize=5;$scope.showDialog=function(bankAccountID){$rootScope.$broadcast("OnLoad",bankAccountID);}
$rootScope.$on('OnReloadData',function(event){$scope.loadData();});$scope.loadData=function(){var showInactive=true;if($scope.inactiveButtonName==yumiki.moneyTrace.bankAccount.showInactiveText){showInactive=false;}
if($scope.isStatusChanged){showInactive=!showInactive;}
yumiki.message.displayLoadingDialog(true);DataService.getBankAccounts(showInactive,$scope.currentPage,$scope.itemsPerPage).then(function mySucces(response){$scope.bankAccounts=response.data.Records;$scope.currentPage=response.data.CurrentPage;$scope.totalItems=response.data.TotalItems;$scope.itemsPerPage=response.data.ItemsPerPage;if($scope.isStatusChanged){if($scope.inactiveButtonName==yumiki.moneyTrace.bankAccount.showInactiveText){$scope.inactiveButtonName=yumiki.moneyTrace.bankAccount.showActiveText;}else{$scope.inactiveButtonName=yumiki.moneyTrace.bankAccount.showInactiveText;}
$scope.isStatusChanged=false;}
yumiki.message.displayLoadingDialog(false);},function myError(response){$scope.isStatusChanged=false;yumiki.message.displayLoadingDialog(false);yumiki.message.clientMessage(response.data,'',yumiki.moneyTrace.bankAccount.errorLogType);});};$scope.loadDataWithStatus=function(){$scope.isStatusChanged=true;$scope.loadData();};});},initBankAccountDialogController:function(app,defaultObject,longDateFormat){app.controller('bankAccountDialogController',function($scope,$rootScope,$http,DataService){$scope.bankAccountID=undefined;$scope.isActiveCheckboxDisabled=false;$rootScope.$on('OnLoad',function(event,bankAccountID){$('#dlgBankAccount').modal({backdrop:'static'});if(bankAccountID!=undefined&&bankAccountID!=null){$scope.bankAccountID=bankAccountID;$scope.isActiveCheckboxDisabled=false;$scope.dialogTitle="Edit Account";yumiki.message.displayLoadingDialog(true);DataService.getBankAccountByID($scope.bankAccountID).then(function mySucces(response){$scope.bankAccount=response.data;$scope.bankAccount.WithdrawDate=moment($scope.bankAccount.WithdrawDate).format(longDateFormat);yumiki.message.displayLoadingDialog(false);},function myError(response){yumiki.message.displayLoadingDialog(false);yumiki.message.clientMessage(response.data,'',yumiki.moneyTrace.bankAccount.errorLogType);});}else{$scope.dialogTitle="New Account";$scope.isActiveCheckboxDisabled=true;$scope.bankAccount=$scope.resetBankAccount();}});$scope.resetBankAccount=function(){$scope.bankAccountForm.$setPristine();return defaultObject;}
$scope.save=function(isValid){if(isValid){yumiki.message.displayLoadingDialog(true);DataService.save($scope.bankAccount).then(function mySucces(response){$scope.bankAccountID=undefined;$('#dlgBankAccount').modal('hide');$scope.ReloadData();},function myError(response){yumiki.message.displayLoadingDialog(false);yumiki.message.clientMessage(response.data,'',yumiki.moneyTrace.bankAccount.errorLogType);});}};$scope.openWithdrawMessageDialog=function(){$('#dlgWithdrawMessageDialog').modal({backdrop:'static'});}
$scope.withdraw=function(){yumiki.message.displayLoadingDialog(true);DataService.saveBankingWithdrawingTrace($scope.bankAccount).then(function mySucces(response){$scope.bankAccountID=undefined;yumiki.message.displayLoadingDialog(false);yumiki.message.clientMessage("Populated Withdraw Trace.",'',yumiki.moneyTrace.bankAccount.infoLogType);},function myError(response){yumiki.message.displayLoadingDialog(false);yumiki.message.clientMessage(response.data,'',yumiki.moneyTrace.bankAccount.errorLogType);});$('#dlgWithdrawMessageDialog').modal('hide');};$scope.ReloadData=function(){$rootScope.$broadcast("OnReloadData");};$scope.bindControls=function(){DataService.getBanks().then(function mySucces(response){$scope.banks=response.data;},function myError(response){yumiki.message.clientMessage(response.data,'',yumiki.moneyTrace.bankAccount.errorLogType);});DataService.getCurrencyList().then(function mySucces(response){$scope.currencyList=response.data;},function myError(response){yumiki.message.clientMessage(response.data,'',yumiki.moneyTrace.bankAccount.errorLogType);});}});},};}(window,document,jQuery,yumiki));