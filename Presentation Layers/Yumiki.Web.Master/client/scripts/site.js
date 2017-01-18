//constant string
var const_Error = 'Error';
var const_Warn = 'Warning';
var const_Info = 'Information';

//Global variables - May be modified in somewhere esle
var noSession = "E_NO_SESSION";

$(document).ready(function () {

    //For menu level 2, 3, .... n, the menus cannot drop down, add these code to drop them down.
    $('.navbar a.dropdown-toggle').on('click', function (e) {
        var $el = $(this);
        var $parent = $(this).offsetParent(".dropdown-menu");
        $(this).parent("li").toggleClass('open');

        if (!$parent.parent().hasClass('nav')) {
            $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
        }

        $('.nav li.open').not($(this).parents("li")).removeClass("open");

        return false;
    });
});

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

    if (message.indexOf(noSession) >= 0) {
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
    $("#sbnSideBarNav").width("250px");
}

function closeNav() {
    $("#sbnSideBarNav").width("0px");
}

//For jquery ui autocomplete
function split(val) {
    return val.split(/,\s*/);
}
function extractLast(term) {
    return split(term).pop();
}