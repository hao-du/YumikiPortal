(function (win, doc, $, yumiki) {
    yumiki.jquery = {
        //For jquery ui autocomplete extract terms
        extractLast: function (term) {
            return split(term.split(/,\s*/)).pop();
        },
    };
}(window, document, jQuery, yumiki));