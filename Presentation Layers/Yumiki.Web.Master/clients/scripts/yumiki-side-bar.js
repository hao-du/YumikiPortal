

(function (win, doc, $, yumiki) {
    yumiki.sideBar = {
        openNav: function () {
            $("#sbnSideBarNav").width("250px");
        },

        closeNav: function () {
            $("#sbnSideBarNav").width("0px");
        }
    };
}(window, document, jQuery, yumiki));