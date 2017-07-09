(function (win, doc, $, yumiki) {
    yumiki.jquery = {
        //For jquery ui autocomplete extract terms
        split: function (val) {
            return val.split(/,\s*/);
        },

        extractLast: function (term) {
            return yumiki.jquery.split(term).pop();
        },
    };
}(window, document, jQuery, yumiki));