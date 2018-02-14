(function (win, doc, $, yumiki) {
    yumiki.moneyTrace.trace.controls = {
        init: function (currentDate, monthYearFormat, getTagUrl) {
            yumiki.moneyTrace.trace.controls.initHighlightedFocusText();
            yumiki.moneyTrace.trace.controls.initMonthFilter(currentDate, monthYearFormat);
            yumiki.moneyTrace.trace.controls.initTagAutocomplete(getTagUrl);
        },

        //When focus on textbox, highlight all words.
        initHighlightedFocusText: function () {
            $("input:text").focus(function () { $(this).select(); });
        },

         //Initialise month filter.
        initMonthFilter: function (currentDate, monthYearFormat) {
            $("#txtMonthFilter").val(moment(currentDate).format(monthYearFormat));
        },

        initTagAutocomplete: function (getTagUrl) {
            yumiki.jquery.autocomplete('.auto-complete', getTagUrl);
        },    
    };
    }(window, document, jQuery, yumiki));