(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CustomHeader = function (element, options) {

        // Public attributes
        this.element = element;
        var that = this;

        // Constructor code
        if (options) {
            $.extend(this, options);
        }

        // Attach qtip on mover hover of custom header
        if (options.CustomTooltip === "HOVER") {
            $($(element)[0].children[1]).qtip({
                content: $(element)[0].children[1].getAttribute('title'),
                style: {
                    classes: 'qtip-light qtip-shadow'
                },
                position: {
                    my: 'top left',
                    at: 'bottom right',
                    target: $(element)[0].children[1]
                },
            });
        }

        ///<summary>
        ///Attach events
        ///</summary>
        var toggleAccordion = function () {          
                var actionToDo;
                if ($(element).hasClass('active')) {
                    $(element).find('.open').remove();
                    if (that.OnCollapse) {
                        actionToDo = that.OnCollapse;
                    }
                } else {
                    $(element).find('.close').remove();
                    if (that.OnUnCollapse) {
                        actionToDo = that.OnUnCollapse;
                    }
                }
                $(element).toggleClass("active").next().slideToggle("slow", actionToDo);
                return false;      
        };
 
        if (this.onClick) {
            element.click(this.onClick);
        } else {
            element.click(toggleAccordion);
        }

        return this;
        //</Popup>
    };


    // JQuery extension function
    $.fn.customHeader = function (options) {
        var name = "customHeader";
        return $s.create(
       this,
        name,
        function (element, opt) {
            return new $s.CustomHeader(element, opt);
        },
        options).data(name);
    };
}(jQuery));

