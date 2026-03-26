(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CheckBoxList = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        // For each input check-box bind on click event
        if (this.onClick) {
            $('input[type="checkbox"]', element).on('click', this.onClick);
        }
        
        // For each input check-box bind on change event
        if (this.onChange) {
            $('input[type="checkbox"]', element).on('change', this.onChange);
        }
    };

    // JQuery extension function
    $.fn.checkBoxList = function (options) {
        var name = "CheckBoxList";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.CheckBoxList(element, opt);
        },
        options).data(name);
    };

}(jQuery));