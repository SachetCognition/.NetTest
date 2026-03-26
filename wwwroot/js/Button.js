/* this Jquery extension is used for the Button Component */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.SavButton = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        // For each anchor tag bind on click event
        if (this.onClick) {
            element.click(this.onClick);
        }
    };

    // JQuery extention function
    $.fn.savbutton = function (options) {
        var name = "button";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.SavButton(element, opt);
        },
        options).data(name);
    };

}(jQuery));