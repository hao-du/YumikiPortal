//Startup method to set all global settings to all all modules.
$(document).ready(function () {
    $.fn.wrapOverflowX = function (id) {
        var newDiv = $('<div/>').addClass('table-responsive');
        this.wrapAll(newDiv);
    };
});