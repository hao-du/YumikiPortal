//For menu level 2, 3, .... n, the menus cannot drop down, add these code to drop them down.
$('.navbar a.dropdown-toggle').on('click', function (e) {
    var $el = $(this);
    var $parent = $(this).offsetParent(".dropdown-menu");
    $(this).parent("li").toggleClass('open');

    if (!$parent.parent().hasClass('nav')) {
        $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
    }

    $('.nav li.open').not($(this).parents("li")).removeClass("open");

    return false;
});


(function (win, doc, $, yumiki) {
    yumiki.menu = {
        initSubLevelDropdown: function () {
            //For menu level 2, 3, .... n, the menus cannot drop down, add these code to drop them down.
            $('.navbar a.dropdown-toggle').on('click', function (e) {
                var $el = $(this);
                var $parent = $(this).offsetParent(".dropdown-menu");
                $(this).parent("li").toggleClass('open');

                if (!$parent.parent().hasClass('nav')) {
                    $el.next().css({ "top": $el[0].offsetTop, "left": $parent.outerWidth() - 4 });
                }

                $('.nav li.open').not($(this).parents("li")).removeClass("open");

                return false;
            });
        },
    };
}(window, document, jQuery, yumiki));