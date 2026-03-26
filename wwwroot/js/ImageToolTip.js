
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.ImageToolTip = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        // For each input imagetooltip bind on click event
        if (this.onClick) {
            element.click(this.onClick);
        }

        /*var toolTipAnchor = element.children("a:first");

        var toolTipMode = {
            Click: "Click",
            Hover: "Hover"
        };

        var toolTipOptions = {
            local: true,
            cursor: 'pointer',
            showTitle: false,
            attribute: 'ToolTipId',
            width: toolTipAnchor.attr("WidthToolTip"),
            onShow: function (ct, ci) {
                ci.children(":first").addClass("displayblockTooltip");
            }
        }

        switch (this.Mode) {
            case toolTipMode.Click:
                toolTipOptions.activation = 'click';
                toolTipOptions.cluezIndex = 10000;
                break;
            case toolTipMode.Hover:
                toolTipOptions.activation = 'hover';
                break;
        };
        
        //toolTipAnchor.attr("ToolTipId", "#" + toolTipAnchor.attr("ToolTipId"));
       // toolTipAnchor.cluetip(toolTipOptions);
       */
        $(element).qtip({
            content: $("#" + options.ToolTipContentId).html(),
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

        // on click outside => close tooltip (executed only once by page by unbinding event)
        /* $(document).unbind('click.ImgToolTip').bind('click.ImgToolTip', function (e) {
             var isInClueTip = $(e.target).closest('#cluetip');
             if (isInClueTip.length === 0)
                 $('.cluetip-default').hide();
         });*/
    };

    // JQuery extention function
    $.fn.imageToolTip = function (options) {
        var name = "imageToolTip";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.ImageToolTip(element, opt);
        },
        options).data(name);
    };

}(jQuery));