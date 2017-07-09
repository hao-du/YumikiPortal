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

        //Reference Link: http://jqueryui.com/autocomplete/#multiple
        //Using Jquey Autocomplete
        initTagAutocomplete: function (getTagUrl) {
            $('.auto-complete').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: 'GET',
                        url: getTagUrl,
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: { keyword: yumiki.jquery.extractLast(request.term) },
                        success: function (data) {
                            response(data);
                        }
                    });
                },
                search: function () {
                    // custom minLength
                    var term = yumiki.jquery.extractLast(this.value);
                    if (term.length < 2) {
                        return false;
                    }
                },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    var terms = yumiki.jquery.split(this.value);
                    terms.pop();
                    // add the selected item
                    terms.push(ui.item.value);
                    // add placeholder to get the comma-and-space at the end
                    terms.push("");
                    this.value = terms.join(",");
                    return false;
                }
            });
        },    
    };
    }(window, document, jQuery, yumiki));