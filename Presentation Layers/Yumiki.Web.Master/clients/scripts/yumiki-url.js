(function (win, doc, $, yumiki) {
    yumiki.url = {
        getURLParameter: function (param) {
            var result = new RegExp(param + "=([^&]*)", "i").exec(window.location.search);
            return result && unescape(result[1]) || ""; 
        },
    };
}(window, document, jQuery, yumiki));