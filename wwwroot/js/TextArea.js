(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.TextArea = function (element, options) {
        // Public attributes
        this.element = element;
       
        // Constructor code
        $.extend(this, options);

        var charCounter = function (e) {
         //   var copyPaste = false;
            var txtarea = $("#" + element[0].id);
            var maxLength = parseInt(options.MaxLength, 10);
            //The check is added to see if textarea value is empty or not
            if (txtarea.val() != undefined) {
               
                //if ((stringOracle.length) > maxLength) {
                //    //if ((stringOracle.length) > maxLength + 1) {
                //    //    copyPaste = true;
                //    //}
                //    var lastChar = stringOracle.substr(maxLength - 1, 1);
                //    if (lastChar == "\n") {
                //        var shortenedtext;
                //        if (options.IsRichText) {
                //            // CKEditor adds extra HTML at the beginning and end of a text block - e.g. <p>. Hence, more text
                //            // than text area has to be shortened
                //            shortenedtext = stringOracle.substring(0, maxLength - 6).replace(/\n/g, '').replace(/\r/g, '\n');
                //            window.CKEDITOR.instances[element[0].id].setData(shortenedtext);
                //        } else {
                //            shortenedtext = stringOracle.substring(0, maxLength - 1).replace(/\n/g, '').replace(/\r/g, '\n');
                //        }
                //        txtarea.val(shortenedtext);
                //    } else {
                //        var shortenedtext1;
                //        if (options.IsRichText) {
                //            // CKEditor adds extra HTML at the beginning and end of a text block - e.g. <p>. Hence, more text
                //            // than text area has to be shortened
                //            shortenedtext1 = stringOracle.substring(0, maxLength - 5).replace(/\n/g, '').replace(/\r/g, '\n');
                //            window.CKEDITOR.instances[element[0].id].setData(shortenedtext1);
                //        } else {
                //            shortenedtext1 = stringOracle.substring(0, maxLength).replace(/\n/g, '').replace(/\r/g, '\n');
                //        }
                //        txtarea.val(shortenedtext1);
                //    }
                //}

              
               // debugger;
                var pos = $("#" + element[0].id).scrollTop();
               
            
                // Piece of code added to fix an issue with loss of focus when the lblNbCharClientID value is set (in IE) .replace(/\r(?!\n)|\n(?!\r)/g, '\r\n')
                var enterCharNumber = txtarea.val().split("\n").length - 1;
                $("#" + options.lblNbCharClientID).text(jQuery.validator.format(options.counterFormat, maxLength - (txtarea.val().length + enterCharNumber), maxLength));
           
                if (e != null && e.type != "blur") {
                    $("#" + options.textAreaClientID).focus();
                    $("#" + options.textAreaClientID).scrollTop(pos);
                }
                
                if (options.IsRichText) {
                    if (e != null && e.data != null && e.ctrlKey && e.data.keyCode === 8) {
                        $("#" + options.lblNbCharClientID).text("abc");
                    }
                }
            }
        };
        //To provide functionality to count lines in comment box for maximum length 
        var callLineCounter = function (e) {
            var pos;
            var txtare = $("#" + element[0].id);
            if (txtare.val().length != 0) {
                var splitVal = $.trim(txtare.val()).split("\n");
                if (splitVal.length > options.MaxLength) {
                    var truncText = splitVal.slice(0, options.MaxLength).join("\n");
                    // The refresh must be done before open the error popup because the popup trigger an "onblur" method which recall the CallLineCounter method 
                    //and without executing the end of the method.
                    splitVal = truncText.split("\n");
                    pos = $("#" + element[0].id).scrollTop();
                    $("#" + options.lblNbCharClientID).text(jQuery.validator.format(options.counterFormat, (options.MaxLength - splitVal.length), options.MaxLength));
                    alert(options.maxErrMesg);
                    window.CKEDITOR.instances[element[0].id].setData(truncText);
                } else {
                    pos = $("#" + element[0].id).scrollTop();
                    $("#" + options.lblNbCharClientID).text(jQuery.validator.format(options.counterFormat, (options.MaxLength - splitVal.length), options.MaxLength));
                }
            } else {
                pos = $("#" + element[0].id).scrollTop();
                $("#" + options.lblNbCharClientID).text(jQuery.validator.format(options.counterFormat, options.MaxLength, options.MaxLength));
            }
            // Piece of code added to fix an issue with loss of focus when the lblNbCharClientID value is set (in IE) 
            if (e != null && e.type != "blur") {
                $("#" + element[0].id).focus();
                $("#" + element[0].id).scrollTop(pos);
            }
        };

        var attachEventsCharCounter = function (isRichText) {
            charCounter();
            if (parseInt(options.MaxLength, 10) > 0) {
                if (isRichText) {
                    window.CKEDITOR.instances[element[0].id].on('key', function (e) {
                        window.CKEDITOR.instances[element[0].id].updateElement();
                        charCounter(e);
                    });
                    window.CKEDITOR.instances[element[0].id].on('blur', function (e) {
                        window.CKEDITOR.instances[element[0].id].updateElement();
                        charCounter(e);
                    });
                    window.CKEDITOR.instances[element[0].id].on('contentDom', function () {
                        window.CKEDITOR.instances[element[0].id].document.on('keyup', function (event) { charCounter(event); });
                    });
                } else {
                    $("#" + element[0].id).bind("keyup", charCounter);
                    $("#" + element[0].id).bind("blur", charCounter);
                    $("#" + element[0].id).bind("focus", charCounter);
                }

            }
        };
        
        var attachEventsLineCounter = function (isRichText) {
            callLineCounter();
            if (parseInt(options.MaxLength, 10) > 0) {
                if (isRichText) {
                    window.CKEDITOR.instances[element[0].id].on('key', function (e) {
                        window.CKEDITOR.instances[element[0].id].updateElement();
                        callLineCounter(e);
                    });
                  
                } else {
                    $("#" + element[0].id).bind("keyup", callLineCounter);
                    
                }
            }
        };

        // For each event set for the component
        if (this.onFocus) {
            element.focusin(this.onFocus);
        }
        
        if (this.onTextChange) {
            element.change(this.onTextChange);
        }
        
        if (this.onBlur) {
            element.focusout(this.onBlur);
        }
        if (this.IsRichText) {
            window.CKEDITOR.replace(element[0].id, {
                toolbar: this.ToolBar,
                width: '100%',
                extraPlugins: 'onchange',
                //gtemplier, 10/11/2015, AR "2015.016472-A.01", readonly mode not work
                readOnly: !options.IsUpdatable
            });
            
            window.CKEDITOR.instances[element[0].id].on('change', function () {
                
                window.CKEDITOR.instances[element[0].id].updateElement();
                charCounter();
            });

            CKEDITOR.on('dialogDefinition', function (ev) {
                // Take the dialog name and its definition from the event data.
                var dialogName = ev.data.name;
                var dialogDefinition = ev.data.definition;
                //debugger;
                // Check if the definition is from the dialog window you are interested in (the "Link" dialog window).
                if (dialogName == 'link') {
                    // Get a reference to the "Link Info" tab.
                    var infoTab = dialogDefinition.getContents('target');

                    // Set the default value for the URL field.
                    var urlField = infoTab.get('linkTargetType');
                    urlField['default'] = '_blank';
                }
            });

            

         

            if (this.onFocus) {
                window.CKEDITOR.instances[element[0].id].on('focus', this.onFocus);
            }
            
            if (this.onBlur) {
                window.CKEDITOR.instances[element[0].id].on('blur', this.onBlur);
            }
            
            if (this.onTextChange) {
                window.CKEDITOR.instances[element[0].id].on('change', this.onTextChange);
            }
        }
        
        //To provide functionality to count characters on copy paste in comment box for maximum length 

        if (options.counterMode == "CharCounter") {
            attachEventsCharCounter(options.IsRichText);
        } else {
            attachEventsLineCounter(options.IsRichText);
        }
    };

    // JQuery extention function
    $.fn.textArea = function (options) {
        var name = "textArea";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.TextArea(element, opt);
        },
        options).data(name);
    };

}(jQuery));

//Custom Max length validation for handling specific issue of \r\n when enter key is pressed because by default browser only count it as 1 instead server count these as 2 characters
$.validator.addMethod("extMaxLength", function (value, element) {
    var maxlen = parseInt($(element).attr('data-val-length-max'));
    if (maxlen > 0) {
        var remaining = (maxlen - (parseInt($(element).val().replace(/(\r\n|\n|\r)/gm, '\r\n').length)));
        if (remaining < 0) {
            return false;
        }
    }
    return true;
});

// Add unobtrusive adaptor for date format

$.validator.unobtrusive.adapters.add("extMaxLength", [], function (options) {
    options.rules["extMaxLength"] = [];
    options.messages["extMaxLength"] = options.message;
});