/* this Jquery extension is used for HyperLink Mode */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.HyperLink = function (element, options) {
        //hold on to the initial object
        var that = this;
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        //HyperLink linkmode enumeration
        var linkMode = {
            HyperLink: "HyperLink", 
            Button: "Button"
        };

        var initHyperLinkMode = function () {
            // For each anchor tag bind on click event
            if (options.onClick) {
                element.click(options.onClick);
            }
        };

        var initButtonMode = function () {

            var clickHandler = function (e) {
                var toSubmit = true;
                if (options.onClick) {
                    //click handler 'this' reference contains the element for which click was called, passing it as this to call onclick handler
                    //defined by the view
                    toSubmit = options.onClick.call(this, e);
                }
                if (toSubmit) {
                    var subForm = document.forms;
                    if (that.causeValidation) {
                        if (!$(subForm[0]).valid()) {
                            // $(".input-validation-error").first().focus();
                            return;
                        }
                    }
                    //<AR = '2015.309917-D.01' date = '07/03/2016' author = 'mangoel'>
                    if (!($('#hdnFormActionName').length > 0)) {
                        var formAction = document.createElement('input');
                        formAction.type = 'hidden';
                        formAction.name = 'hdnFormActionName';
                        formAction.id = 'hdnFormActionName';
                        formAction.value = subForm[0].action;
                        subForm[0].appendChild(formAction);
                    } else {
                        $('#hdnFormActionName').val(subForm[0].action);
                    }
                    //</AR>
                    if (subForm[0].action.indexOf('menuname') === -1) {
                        if (options.actionUrl && options.actionUrl != "#") {
                            subForm[0].action = options.actionUrl;
                        }

                    }
                    subForm[0].action = subForm[0].action.replace('menuname', options.actionUrl);
                    //subForm.action=subForm[0].action; // + "/" + element[0].name);
                    //subForm.attr("name", element[0].name);
                    //add hidden field to submit so that view model is correctly populated when submit
                    var nameToSend = that.name;
                    if (nameToSend) {
                        //if no value is provided then use the name itself.
                        var valueToSend = nameToSend;
                        if (that.value && that.value !== null && that.value !== '') {
                            valueToSend = that.value;
                        }
                        var input = document.createElement('input');
                        input.type = 'hidden';
                        input.name = nameToSend;
                        input.value = valueToSend;

                        subForm[0].appendChild(input);
                    }
                    if (that.causeValidation) {
                        $(subForm[0]).submit();
                    } else {
                        subForm[0].submit();
                    }
                }


            };

            // For hyperlink bind OnClick event
            element.click(clickHandler);
        };

        //the RenderMode decides if it is a HyperLink or a Button (ImageButton or LinkButton)
        switch (this.renderMode) {
            case linkMode.HyperLink:
                initHyperLinkMode();
                break;
            case linkMode.Button:
                initButtonMode();
                break;
        }

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

    // JQuery extension function
    $.fn.hyperLink = function (options) {
        var name = "hyperLink";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.HyperLink(element, opt);
        },
        options).data(name);
    };

}(jQuery));