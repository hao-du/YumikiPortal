//constant string
var const_Error = 'Error';
var const_Warn = 'Warning';
var const_Info = 'Information';

//Startup method to set all global settings to all all modules.
$.fn.wrapOverflowX = function (id) {
    var newDiv = $('<div/>').addClass('table-responsive');
    this.wrapAll(newDiv);
};

$.fn.showLoadingBar = function () {
    $('#dlgLoadingBar').modal({ backdrop: 'static' });
};

$.fn.hideLoadingBar = function () {
    $('#dlgLoadingBar').modal('hide');
};