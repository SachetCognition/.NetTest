/* this Jquery extension is used for the Button Component */
(function ($) {

    // Alias
    var $s = $.sav2000;
    var isModal;
    var isdraggable;
    var isresizable;
    var closeText;
    // Constructor
    
    $s.PopOver = function (options) {
        // Public attributes
        if (navigator.appName.indexOf("Internet Explorer") != -1) {     //For v8 Resizable option should not be available
            if (navigator.appVersion.indexOf("MSIE 8") != -1) 
            {
                isresizable = false;
            }
        }
            
        
       var checkAutoopen=function() {
           if (isModal == undefined) {
               return options.autoopen;
           } else {
               return !isModal;
           }
        }

       
        $("#" + options.id).dialog({
            autoOpen: checkAutoopen(),
            height: options.height,
            width: options.width,
            modal: true,
            draggable: isdraggable,
            resizable: isresizable,
            drag: function(event, ui) {
                var fixPix = $(document).scrollTop();
                var popOverPosition = ui.position;
                popOverPosition.top = popOverPosition.top - fixPix;
                $(".ui-dialog").css("top", popOverPosition.top + "px");
            },
            beforeClose: function() {
                var isClose = true;
                if (options.beforeClose) {
                    isClose = options.beforeClose();
                }

                if (!isClose) {
                    return false;
                }

                $s.removeHtmlInIframe(options);
                return true;
            },
            create: function (event, ui) {
                if (options.showTitle != undefined && options.showTitle == false)
                $(".ui-widget-header").hide();
            }
           , close: options.closefunction

              

        });
    
        if (options.height != undefined) {
            $(".ui-dialog").css("height", options.height);
        }
        $('.ui-widget-header').parent().css('background', "#D9D9D9");

        //Applying isModal property to stop pover close after clicked outside the popover
        $(".ui-widget-overlay").click(function () {

            if (!isModal) {
                $("#" + options.id).dialog("close");
            }

        });
        $(".ui-button").attr('title', closeText);
        $("#" + options.id).dialog("open");
        $("#" + options.id).dialog('option', 'title', options.divTitle);
        $("#frame" + options.id).attr("src", options.Source);
        
        return false;
    };



    $s.removeHtmlInIframe = function (options) {
        var frame = document.getElementById("frame" + options.id),
     frameDoc = frame.contentDocument || frame.contentWindow.document;
        if (frameDoc.documentElement != null) {
            //frameDoc.removeChild(frameDoc.documentElement);
            var myNode = frameDoc.documentElement;
            while (myNode.firstChild) {
                frameDoc.documentElement.removeChild(myNode.firstChild);
            }
        }
    };

    $s.IsModal = function (options) {

        isModal = options.isModal;
        isdraggable = options.isDraggable;
        isresizable = options.isResizable;
        closeText = options.closeText;

    };
    
    $s.PopNavigatetoNext = function (options) {

        $("#" + options.id).dialog('option', 'title', options.divTitle);
        $("#" + "frame"+options.id).attr("src", options.Source);
        return false;
    };
    $s.close = function (options) {
       
        $("#" + options.id).dialog('close');
       
        return false;
    };
    $s.ChangePopOverTitle = function (options) {

        $("#" + options.id).dialog('option', 'title', options.divTitle);

        return false;
    };
}(jQuery));

