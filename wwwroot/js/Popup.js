/* This file defines common javascript for PopUp */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.Popup = function (options) {
        var tmpOptions = {
            Page_IsValid: false,
            Page_Validators: [],
            "url": "",
            "isresizable": false,
            "returncall": null,
            "widthpopup": 1000,
            "heightpopup": 500,
            "left": null,
            "top": null,
            "title": null,
            "dataForPopupPage": "",
            "pathToBasePopup": "Popup/BasePopup"
        };

        // merge options into tmpOptions
        var popupOptions = $.extend(true, tmpOptions, options);

        // initialize
        var init = function () {
            if (popupOptions.left === null) {
                var width = screen.width;
                popupOptions.left = (width - popupOptions.widthpopup) / 2;
            }
            if (options.top === null) {
                var height = screen.height;
                popupOptions.top = (height - popupOptions.heightpopup) / 2;
            }
            createPopUp();
        },
            createPopUp = function () {
                var arguments = {
                    "DataForPopupPage": popupOptions.dataForPopupPage,
                    "PopupUrl": popupOptions.url,
                    "Title": popupOptions.title
                };
                var resize;
                if (popupOptions.isresizable) {
                    resize = "yes";
                } else {
                    resize = "no";
                }

                var urlspecification = window.location.pathname.split("/");
                var port = "";
                if (window.location.port) {
                    port = ":" + window.location.port;
                }

                var basePopupPath = "Popup/BasePopup";
                if (popupOptions.pathToBasePopup) {
                    basePopupPath = popupOptions.pathToBasePopup;
                }
                
                var strUrl = window.location.protocol + "//" + window.location.host + port + "/" + urlspecification[1] + "/" + basePopupPath;

                var values = window.showModalDialog(strUrl, arguments, "dialogLeft:" + popupOptions.left + "px;dialogTop:" + popupOptions.top
                    + "px;dialogWidth:" + popupOptions.widthpopup + "px;dialogHeight:" + popupOptions.heightpopup + "px;edge: Raised; center: Yes;"
                    + " status: No;status:no;resizable:" + resize + ";unadorned:no;help:no;scroll:no;minimize:no;maximize:no");
                if ($.isFunction(popupOptions.returncall)) {
                    popupOptions.returncall(values);
                }
            };

        init();
    };

    $s.PopupWindow = function (options) {
        try {
            var height = (options.heightpopup == undefined || options.heightpopup == null || options.heightpopup > 650) ? screen.height - 50 : options.heightpopup;
            var width = (options.widthpopup == undefined || options.widthpopup == null || options.widthpopup > 1000) ? screen.width - 50 : options.widthpopup;
            var left = (options.left == undefined || options.left == null) ? ((screen.width - width) / 2) : options.left;
            var top = (options.top == undefined || options.top == null) ? ((screen.height - height) / 2) : options.top;
            var windowAttr = 'width=' + width + ',height=' + height + ',top=' + top + 'px';
            windowAttr += ',left=' + left + 'px,scrollbars=1';
            if (options.isresizable) {
                windowAttr += ',resizable=1';
            } else {
                windowAttr += ',resizable=0';
            }
            if (options.displayToolbar) {
                windowAttr += ',toolbar=1';
            } else {
                windowAttr += ',toolbar=0';
            }
            if (options.displayStatusbar) {
                windowAttr += ',status=1';
            } else {
                windowAttr += ',status=0';
            }
            if (options.displayMenubar) {
                windowAttr += ',menubar=1';
            } else {
                windowAttr += ',menubar=0';
            }
            if (options.displayLocation) {
                windowAttr += ',location=1';
            } else {
                windowAttr += ',location=0';
            }
            windowAttr += ',directories=0';
            windowAttr += ',screenX=10,screenY=10';

            if ($.trim(options.url) === "") {
                return false;
            }

            var newWin = window.open(options.url, options.title, windowAttr, true);

            newWin.opener = self;
            newWin.focus();
        } catch (e) {
            // ignore exception as nothing can be done here
        }

        return false;
    };
}(jQuery));