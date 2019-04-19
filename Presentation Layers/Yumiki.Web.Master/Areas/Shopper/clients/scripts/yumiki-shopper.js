(function (win, doc, $, yumiki) {
    yumiki.shopper = {
        errorLogType: '',
        defaultObject: null,

        //Init shopper namespace
        init: function (errorLogType) {
            yumiki.shopper.errorLogType = errorLogType;
        }
    };
}(window, document, jQuery, yumiki));