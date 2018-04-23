(function (win, doc, $, yumiki) {
    yumiki.wellCovered = {
        onListSubmit: function (id) {
            //Submit form
            var form = $('#' + id);
            form.submit();
        },

        onFormSubmit: function (action) {
            if (!action) {
                return false;
            }
            //Set value in hidden field 'action' to be URL param
            $('#action').val(action);
            //Submit form after filling all params
            var form = $('#action').closest('form');
            form.submit();
        },

        initDateTimePicker: function (timeFormat, dateFormat, datetimeFormat) {
            if (timeFormat) {
                $(".yumiki-control-time").datetimepicker({
                    format: timeFormat,
                    ignoreReadonly: true
                });
            }

            if (datetimeFormat) {
                $(".yumiki-control-datetime").datetimepicker({
                    format: datetimeFormat,
                    ignoreReadonly: true
                });
            }

            if (dateFormat) {
                $(".yumiki-control-date").datetimepicker({
                    format: dateFormat,
                    ignoreReadonly: true
                });
            }
        },

        initSearchEvent: function (searchBoxName, searchButtonName, formName) {
            $(".yumiki-search-item").mark(
                $('#' + searchBoxName).val(),
                {
                    separateWordSearch: true,
                    diacritics: true,
                    accuracy: "complementary",
                    wildcards: "enabled"
                }
            );

            $('#' + searchBoxName)
            .on({
                'keypress': function (e) {
                    if (e.which == 13) {
                        if ($('#' + searchBoxName).val()) {
                            yumiki.wellCovered.onListSubmit(formName);
                        }
                    }
                }
                , 'focus': function () {
                    this.selectionStart = 0;
                    this.selectionEnd = this.value.length;
                }
            });

            $('#' + searchButtonName).click(function () {
                if ($('#' + searchBoxName).val()) {
                    yumiki.wellCovered.onListSubmit(formName);
                }
            });
        }
    }
}(window, document, jQuery, yumiki));