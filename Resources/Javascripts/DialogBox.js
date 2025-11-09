/* this Jquery extension is used for the ConfirmationBox Component */
(function ($) {

    // Alias
    var $s = $.sav2000;
    // Constructor
    $s.DialogBox = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        // For each anchor tag bind on click event
        if (options.ClientCallBack) {
            element.click(options.ClientCallBack);
        }

        $('#' + options.id).on('hidden.bs.modal', function () {
            $s.removeHtml(options);
        });
    };

    $s.removeHtml = function (options) {
        $('#' + options.contentdivid).empty();

    };

    $s.LoadHTML = function (divId, contentdivId, options) {
        $('#' + contentdivId).html(options);
        $('#' + divId).modal('show');
        return false;
    };

    $.fn.OpenDialogPopOver = function (options) {
        var cssClass = options.CssClass;
        var defaultFocusButton = options.DefaultFocusButton;

        if (cssClass != '' && cssClass != undefined) {
            $('#' + options.DivID + " .modal-dialog").removeClass("modal-dialog").addClass("modal-dialog").addClass(cssClass);
        }
        if (options.IsModel == "true") {
            $('#' + options.DivID).modal({
                remote: options.Source,
                keyboard: false,
                backdrop: 'static'
            });
        } else {
            $('#' + options.DivID).modal({
                remote: options.Source,
                keyboard: true,
                backdrop: true
            });
        }
        if (options.IsDraggable != '' && options.IsDraggable != undefined) {
            if (options.IsDraggable == "true") {
                $('#' + options.DivID).draggable();
            }
        }

         if (options.Buttons != null) {
            if (options.Buttons.length > 0) {
                var count = options.Buttons.length;
                var clientCallBak = options.ClientCallBack;
                var i = 0;
                $('#' + options.DivID + ' .modal-footer').find('a').each(function () {
                    var id = this.id;
                    if (i < count) {
                        var buttonText = options.Buttons[i].ButtonText;
                        var buttonAction = options.Buttons[i].ButtonAction;
                        var value = options.Buttons[i].Value;
                        var buttonTitle = options.Buttons[i].Title;
                        var css = options.Buttons[i].Css;
                        $(this).unbind('click');
                        //$(this).click(eval(clientCallBak));
                        if (clientCallBak != null || clientCallBak != '')
                            $(this).attr('onclick', clientCallBak);

                        $(this).attr('title', buttonTitle);
                        $(this).attr('href', buttonAction);
                        $(this).attr('value', value);
                        $(this).attr('class', css);
                        $('#' + id + ' span').html(buttonText);
                        i++;
                    } else {
                        $(this).hide();
                    }
                });
            }
         }

         if (navigator.appName.indexOf("Netscape") != -1) {
             $('#' + options.DivID).on('shown.bs.modal', function () {
                 setTimeout(function () { $(dialogId).focus(); }, 0);
             });
         } else {
             var dialogId = 'span#content_dialog' + options.DivID;
             setTimeout(function () { $(dialogId).focus(); }, 0);
         }

         $('#' + options.DivID).keyup(function (e) {
             var keycode = (event.keyCode ? event.keyCode : event.which);
             if (e.keyCode == $.ui.keyCode.ENTER) {
                 $('#' + defaultFocusButton).click();
                 return false;
             }
         });

    };



    // open dialog client method 

    $.fn.OpenDialogBox = function (options) {
        var defaultFocusButton = options.DefaultFocusButton;
        var divId = options.ID;
        var cssClass = options.CssClass;
        if (Option.Message != null || Option.Message != '') {
            $('#' + divId + ' .modal-body span').html(options.Message);
        }
        if (Option.Title != null || Option.Title != '') {
            $('#' + divId + ' .modal-title span').text(options.Title);

        }
        if (cssClass != '' && cssClass != undefined) {
            $('#' + divId + " .modal-dialog").removeClass("modal-dialog").addClass("modal-dialog").addClass(cssClass);
        }
        if (options.Buttons != null) {
            if (options.Buttons.length > 0) {
                var count = options.Buttons.length;
                var clientCallBak = options.ClientCallBack;
                var i = 0;
                $('#' + divId + ' .modal-footer').find('a').each(function () {
                    var id = this.id;
                    if (i < count) {
                        var buttonText = options.Buttons[i].ButtonText;
                        var buttonAction = options.Buttons[i].ButtonAction;
                        var value = options.Buttons[i].Value;
                        var buttonTitle = options.Buttons[i].Title;
                        var css = options.Buttons[i].Css;
                        $(this).unbind('click');
                        //$(this).click(eval(clientCallBak));
                        if (clientCallBak != null || clientCallBak != '')
                            $(this).attr('onclick', clientCallBak);

                        $(this).attr('title', buttonTitle);
                        $(this).attr('href', buttonAction);
                        $(this).attr('value', value);
                        $(this).attr('class', css);
                        $('#' + id + ' span').html(buttonText);
                        i++;
                    } else {
                        $(this).hide();
                    }
                });
            }
        }

        if (options.IsModel != '' && options.IsModel != undefined) {
            if (options.IsModel == "true") {
                $('#' + divId).modal({
                    keyboard: false,
                    backdrop: 'static'
                });

            } else {
                $('#' + divId).modal(
                    {
                        keyboard: true,
                        backdrop: true

                    }
                    );
            }
        }

        if (options.IsDraggable != '' && options.IsDraggable != undefined) {
            if (options.IsDraggable == "true") {
                $('#' + divId).draggable();
            }
        }

        var dialogId = 'span#content_dialog' + divId;
        if (navigator.appName.indexOf("Netscape") != -1) {
            $('#' + divId).on('shown.bs.modal', function () {
                $(dialogId).addClass("selectionFocus");
                setTimeout(function () { $(dialogId).focus(); }, 0);
            });
        } else {
            setTimeout(function () { $(dialogId).focus(); }, 0);
        }

        $('#' + divId).keyup(function (e) {
            if (navigator.appName.indexOf("Netscape") != -1) {
                e = e || window.event || {};
                var charCode = e.charCode || e.keyCode || e.which;
                if (charCode == 13) {
                    if ($('#' + defaultFocusButton).length > 0) {
                        e.stopImmediatePropagation();
                        $('#' + defaultFocusButton).click();
                    }
                }
            }
            else {

                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (e.keyCode == $.ui.keyCode.ENTER) {

                    if ($('#' + defaultFocusButton).length > 0) {
                        e.stopImmediatePropagation();
                        $('#' + defaultFocusButton).click();

                    }
                }
            }
        });


    }

    // JQuery extention function
    $.fn.DialogBox = function (options) {
        var name = "DialogBox";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DialogBox(element, opt);
        },
        options).data(name);
    };

}(jQuery));