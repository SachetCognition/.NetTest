(function ($) {

    // Alias
    var $s = $.sav2000;
    // Constructor
    $s.ErrorDisplay = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);
        if (options.displaymessage != null) {
            $(element).parent().addClass("error");
          
           // $(element).children().find("a").first().focus();
       
            
        } else {
            $(element).parent().removeClass("error");
        }
        if (options.errorcollection != null) {
            $(element).parent().addClass("error");
     
           
          //  $(element).children().find("a").first().focus();
       
         
        } else {
            $(element).parent().removeClass("error");
            
        }
    };

    // JQuery extention function
    $.fn.ErrorDisplay = function (options) {
        var name = "ErrorDisplay";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.ErrorDisplay(element, opt);
        },
        options).data(name);
    };

}(jQuery));