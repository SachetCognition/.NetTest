(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.Image = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);
       
        if (options.CustomTooltip === "HOVER") {

            $(element).qtip({
                content: $(element)[0].getAttribute('title'),
                style: {
                    classes: 'qtip-light qtip-shadow'
                },
                position: {
                    my: 'top left',
                    at: 'bottom right',
                    target: $(element)
               
                },
                });
          
        } else if (options.CustomTooltip === "CLICK") {
            
            $(element).qtip({
                content: $(element)[0].getAttribute('title'),
                    show: {
                        event: 'click',
                        delay: 0
                    },
                    hide: {
                        fixed: true,
                        event: 'unfocus'
                    },
                   
                    style: {
                        classes: 'qtip-light qtip-shadow'
                    }
                });
          
        }
        
    };
    
    // JQuery extention function
    $.fn.image = function (options) {
        var name = "image";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.Image(element, opt);
        },
        options).data(name);
    };

}(jQuery));