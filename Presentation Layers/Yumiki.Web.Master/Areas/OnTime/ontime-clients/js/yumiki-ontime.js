(function (win, doc, $, yumiki) {
    yumiki.ontime = {
        initDateTimePicker: function () {
            $(".yumiki-control-date").datetimepicker({
                format: 'DD MMM YYYY',
                ignoreReadonly: true
            });
        }
    }
}(window, document, jQuery, yumiki));