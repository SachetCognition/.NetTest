/* this Jquery extension is used for the Tab Component */
(function ($) {
    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.HorizontalTab = function (element,options) {

        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this,options);

        ///<summary>
        ///init events
        ///</summary>
        var init = function () {
            initNavTabs();
        };
     

        // For click event
        if (this.onClick) {
            element.click(this.onClick);
        } 


        var initNavTabs = function () {
            //$("#tabs").tabs();
        };

        // Constructor code
        $.extend(this);

        init();
    };

    // JQuery extension function
    $.fn.horizontalTab = function (options) {
        var name = "horizontalTab";
        return $s.create(
        this,
        name,
        function (element,opt) {
            return new $s.HorizontalTab(element,opt);
        },
        options).data(name);
    };

}(jQuery));