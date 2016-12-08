//constant string
var const_Error = 'Error';
var const_Warn = 'Warning';
var const_Info = 'Information';

//Global variables - May be modified in somewhere esle
var noSession = "E_NO_SESSION";

//Show message dialog in client side.
var clientMessage = function (message, details, logType) {
    $("#txtDialogTitle").empty();
    switch (logType) {
        case 'ERROR':
            $("#txtDialogTitle").append(const_Error);
            break;
        case 'INFO':
            $("#txtDialogTitle").append(const_Info);
            break;
        case 'WARN':
            $("#txtDialogTitle").append(const_Warn);
            break;
        default:
            $("#txtDialogTitle").append('Message from System...');
            break;
    }

    $("#txtMessageDetails").empty();
    if (details == null || details == undefined || details == "") {
        $("#btnMessageDetails").hide();
    }
    else {
        $("#btnMessageDetails").show();
        $("#txtMessageDetails").append("<p>" + details + "</p>");
    }

    $("#txtMessage").empty();
    if (typeof message == 'string') {
        $("#txtMessage").append("<p>" + message + "</p>");
    }
    else {
        $("#txtMessage").append("<p>" + message.ExceptionMessage + "</p>");
    }
    

    $("#dlgMessageDialog").modal({ backdrop: 'static' });

    if (message.includes(noSession)) {
        location.href = location.href;
    }
}

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

function openNav() {
    document.getElementById("sbnSideBarNav").style.width = "250px";
}

function closeNav() {
    document.getElementById("sbnSideBarNav").style.width = "0";
}