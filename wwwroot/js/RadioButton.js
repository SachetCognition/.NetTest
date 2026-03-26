(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.RadioButton = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        // For each input checkbox bind on click event
        if (this.onClick) {
            element.click(this.onClick);
        }
        
        // For each input checkbox bind on change event
        if (this.onChange) {
            element.change(this.onChange);
        }
    };

    // JQuery extention function
    $.fn.radioButton = function (options) {
        var name = "radioButton";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.RadioButton(element, opt);
        },
        options).data(name);
    };

}(jQuery));