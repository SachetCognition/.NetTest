/* this Jquery extension is used for the ActionButton Component */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.ActionButton = function (element, options) {
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
    $.fn.actionButton = function (options) {
        var name = "actionButton";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.ActionButton(element, opt);
        },
        options).data(name);
    };

}(jQuery));