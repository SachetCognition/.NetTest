/* this Jquery extension is used for ClickToVoice */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.ClickToVoice = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        //<RFC id="2016.305315" date="03/10/2016" author="Hakumar" action="Modification">
        var applyqtip = function (anchorelement) {
            //Find Element Index with respect to window
            var elmentindex = $(anchorelement).offset().left;
            var windowWidth = $(window).width();

            $(anchorelement).qtip({
                content: $(anchorelement)[0].getAttribute('title'),
                style: {
                    classes: 'qtip-light qtip-shadow'
                },
                position: {
                    my: windowWidth - elmentindex > 280 ? 'top left' : 'right top',
                    at: windowWidth - elmentindex > 280 ? 'bottom right' : 'bottom left',
                    target: $(anchorelement)                    
                },

            });
        };            

        var init = function() {
            element.click(onClickToVoiceClick);
          
            //Access the controls from UI.
            anchorcontrol = $("#" + options.id);

            if (options.telephoneControlId != null) {
                $.txtcontrol = $("#" + options.telephoneControlId);

                if ($.txtcontrol != null) {
                    // Get the Text in the text control.
                    $.controltext = $.txtcontrol.val().replace(/\s+/g, '');
                    if ($.controltext != null) {
                        //get change notification from TextBox.
                        $.txtcontrol.keyup(getChangeNotification);
                        //length will be 1 always coz of the empty char.
                        if ($.controltext.length == 0) {
                            anchorcontrol.hide();
                        }
                    } 
                }
            }
        };
        // On click of the ClickToVoice image
        var onClickToVoiceClick = function() {
            if (options != null) {
                $.url = options.url;

                //open new window as popup.
                if ($.url != null) {
                    if (options.telephoneNumber != null) {
                        //Removing space from telephoneNumber used in URL
                        $.url = $.url.replace("{0}", options.telephoneNumber.replace(/ /g,''));
                    }
                    window.open($.url);
                }
            }
        },
        // When the TextBox value changes
        getChangeNotification = function () {
            $.txtcontrol = $("#" + options.telephoneControlId);
            if ($.txtcontrol.length > 0) {
                // Get the Text in the text control.
                $.controltext = $.txtcontrol.val();
                if ($.controltext != null) {
                    options.telephoneNumber = $.controltext;

                    if (($.controltext.length != 0)) {
                        // Change the title and alt of anchor and image respectively
                        var changedTitle = options.title.replace("{0}", options.telephoneNumber);
                        anchorcontrol.prop('title', changedTitle);
                        $(anchorcontrol.selector + '>img').prop('alt', changedTitle);
                        //<RFC id="2016.305315" date="03/10/2016" author="Hakumar" action="Modification">
                        applyqtip(anchorcontrol);                        
                        // Show the ClickToVoice imageif the text box is not empty
                        anchorcontrol.show();
                    } else {

                        anchorcontrol.hide();
                    }
                }
            }
        }, anchorcontrol;
        init();
        //<RFC id="2016.305315" date="03/10/2016" author="Hakumar" action="Modification">
        applyqtip(element);
    };

    // JQuery extension function
    $.fn.clickToVoice = function (options) {
        var name = "clickToVoice";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.ClickToVoice(element, opt);
        },
        options).data(name);
    };

}(jQuery));

