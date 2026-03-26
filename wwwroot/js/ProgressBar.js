(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.ProgressBar = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);     
        // If some items are specified, do the client-side binding
        if (this.items) {
            this.bind(this.items);
        }
    };

    //bind method to dynamically update the value of the progress bar
    $s.ProgressBar.bind = function (element, items) {
        var calc = 0;
        if (items.total != 0) {
            calc = (items.expected * 100 / items.total);
        }
        if (items.displayexpected == "true") {
            $(element[0]).text(items.expected + '(' + items.actual + ')');
        } else {
            $(element[0]).text(items.expected);
        }
        var divprogress = $('div',element[1]);
        divprogress.css('width', calc + "%").attr('aria-valuenow', calc);
        if (items.displayexpected == "true") {
            $('span.sr-only', divprogress).text(items.expected + '(' + items.actual + ')');
        } else {
            $('span.sr-only', divprogress).text(items.expected);
        }
       
    };
    // JQuery extension function
    $.fn.progressBar = function (options) {
        var name = "progressBar";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.ProgressBar(element, opt);
        },
        options).data(name);
    };

}(jQuery));