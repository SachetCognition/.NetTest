(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.RadioButtonList = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        // For each input radio bind on click event
        if (this.onClick) {
            $('input[type="radio"]', element).on('click', this.onClick);
        }

        // For each input radio bind on change event
        if (this.onChange) {
            $('input[type="radio"]', element).on('change', this.onChange);
        }
    };

    // JQuery extension function
    $.fn.radioButtonList = function (options) {
        var name = "TestRadioButtonModelSourceItems";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.RadioButtonList(element, opt);
        },
        options).data(name);
    };

}(jQuery));