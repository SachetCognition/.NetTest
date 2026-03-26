(function ($) {
    // Alias
    var $s = $.sav2000;
    var subForm = null;
    // Constructor
    $s.DropDownMenu = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);


        var resetClass = function(options) {
            $(document).click(function() {
                if (element[0]!=undefined) {
                    $('#li-' + element[0].id + '').each(function () {
                            $(this).children('> a').find('.close').remove();
                            $(this).children('> a').find('.open').remove();
                        });
                }

            });
            if (element[0] != undefined) {
            $('#li-' + element[0].id + '> a').click(function() {
                var accessSpan = $(this).find("span#" + options.accessSpanId);
                if (accessSpan.hasClass('open')) {
                    accessSpan.removeClass('open');
                    accessSpan.addClass('close');
                    accessSpan.text(options.accessTextClose);
                } else if (accessSpan.hasClass('close')) {
                    accessSpan.removeClass('close');
                    accessSpan.addClass('open');
                    accessSpan.text(options.accessTextOpen);
                }
            });
            }
            subForm = element.closest('form');

    };
        resetClass(options);
    };

    $s.onItemclick = function (items) {
        subForm.attr('action', items.href);
        subForm[0].action = subForm[0].action.replace('menuname', items.href);
        subForm.attr('action', subForm[0].action);
        subForm.submit();
    };
    // JQuery extension function
    $.fn.DropDownMenu = function (options) {
        $s.DropDownMenu(this, options);
    };
}(jQuery));