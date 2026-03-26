(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.TextBox = function (element, options) {
        // Public attributes
        this.element = element;
       
        // Constructor code
        $.extend(this, options);

        var init = function () {
            //example to attach custom date change event from javascript
                if (options.isNumeric) {
                    $('#' + options.id).bind('keypress', numericOnly);
                }
        },
         numericOnly = function (e) {
             if (e.keyCode == '9' || e.keyCode == '16') {
                 return;
             }
             var code;
             if (e.keyCode) code = e.keyCode;
             else if (e.which) code = e.which;
             if (e.which == 46)
                 return false;
             if (code == 8 || code == 46)
                 return true;
             if (code < 48 || code > 57)
                 return false;
         }
        // For each input checkbox bind on click event
        if (this.onFocus) {
            element.focusin(this.onFocus);
        }
        
        // For each input checkbox bind on change event
        if (this.onTextChange) {
            element.change(this.onTextChange);
        }
        
        if (this.onBlur) {
            element.focusout(this.onBlur);
        }

        $(".numericOnly").bind('keypress', function (e) {
            if (e.keyCode == '9' || e.keyCode == '16') {
                return;
            }
            var code;
            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;
            if (e.which == 46)
                return false;
            if (code == 8 || code == 46)
                return true;
            if (code < 48 || code > 57)
                return false;
        });
        init();
    };

    // JQuery extention function
    $.fn.textBox = function (options) {
        var name = "textBox";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.TextBox(element, opt);
        },
        options).data(name);
    };

}(jQuery));