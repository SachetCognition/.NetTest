(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CheckBox = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        // Update the status of the hidden field when the checkbox is changed
        element.change(function () {
            $(this).siblings("#" + this.id + "Hidden").val(this.checked);
        });

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
    $.fn.checkBox = function (options) {
        var name = "checkBox";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.CheckBox(element, opt);
        },
        options).data(name);
    };

}(jQuery));