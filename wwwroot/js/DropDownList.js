(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.DropDown = function (element, options) {
        // Public attributes
        this.element = element;
        
        // Constructor code
        $.extend(this, options);
        
        if (this.onChange) {
            element.change(this.onChange);
        }
        
        // If some items are specified, do the client-side binding
        if (this.items) {
            this.bind(this.items);
        }
    };

    // Client-side bind function
    // items : { selected, text, value }
    $s.DropDown.bind = function (element, items) {
        var select = $("select", element);
        for (var i = 0; i < items.length; ++i) {
            var option = document.createElement("option");
            option.value = items[i].value;
            option.innerHTML = items[i].text;
            if (items[i].selected) {
                option.setAttribute("selected", "selected");
            }
            select[0].appendChild(option);
        }
    };
    
    // JQuery extention function
    $.fn.dropDown = function (options) {
        var name = "dropDown";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DropDown(element, opt);
        },
        options).data(name);
    };

}(jQuery));