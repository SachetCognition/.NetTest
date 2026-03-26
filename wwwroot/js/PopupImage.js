/* this Jquery extension is used for PopupImage Component */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.PopupImage = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        var dataForPopupPage = "",
        callBack = function (response) {
            if (!response) {
                return;
            }

            setValueOnControl(options.controlForTextId, response.text);
            setValueOnControl(options.controlForValueId, response.value);

            if (options.callbackFunction) {
                options.callbackFunction(response);
            }
        },

        setValueOnControl = function (controlId, value) {
            if (value) {
                if (controlId) {
                    var control = $("#" + controlId);
                    if (control.length > 0) {
                        control.val(value);
                    }
                }
            }
        };

        this.clickHandler = function () {
            var isresizable = false,
                widthpopup = 1000,
                heightpopup = 500,
                left = null,
                top = null,
                title = null
            ;

            if (options.isresizable) {
                isresizable = options.isresizable;
            }

            if (options.widthpopup) {
                widthpopup = options.widthpopup;
            }

            if (options.heightpopup) {
                heightpopup = options.heightpopup;
            }

            if (options.left) {
                left = options.left;
            }

            if (options.top) {
                top = options.top;
            }

            if (options.title) {
                title = options.title;
            }

            if (options.dataForPopupPage) {
                dataForPopupPage = options.dataForPopupPage;
            }

            var openPopUp = true;

            if (options.preOpenFunction) {
                openPopUp = options.preOpenFunction();
            }

            if (openPopUp) {
                
                if (options.openAsWindow) {
                    $s.PopupWindow({
                        "url": options.popupUrl,
                        "isresizable": isresizable,
                        "widthpopup": widthpopup,
                        "heightpopup": heightpopup,
                        "left": left,
                        "top": top,
                        "title": title,
                        "displayToolbar": options.displayToolbar,
                        "displayStatusbar": options.displayStatusbar,
                        "displayMenubar": options.displayMenubar,
                        "displayLocation": options.displayLocation
                    });
                } else {
                    $s.Popup({
                        "url": options.popupUrl,
                        "pathToBasePopup": options.pathToBasePopup,
                        "isresizable": isresizable,
                        "returncall": callBack,
                        "widthpopup": widthpopup,
                        "heightpopup": heightpopup,
                        "left": left,
                        "top": top,
                        "title": title,
                        "dataForPopupPage": dataForPopupPage
                    });
                }
            }

            return false;
        };

        this.updateDataForPopup = function (newData) {
            if (newData !== null) {
                dataForPopupPage = newData;
            }
        };

    };


    // JQuery extension function
    $.fn.popupImage = function (options) {
        var name = "PopupImage";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.PopupImage(element, opt);
        },
        options).data(name);
    };
}(jQuery));