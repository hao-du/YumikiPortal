(function (win, doc, $, yumiki) {
    yumiki.jquery = {
        //For jquery ui autocomplete extract terms
        split: function (val) {
            return val.split(/,\s*/);
        },

        extractLast: function (term) {
            return yumiki.jquery.split(term).pop();
        },

        //Reference Link: http://jqueryui.com/autocomplete/#multiple
        //Using Jquey Autocomplete
        autocomplete: function (control, url) {
            $(control).autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: 'GET',
                        url: url,
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