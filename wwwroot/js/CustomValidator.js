/* this Jquery extension is used for CustomValidator Mode */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CustomValidator = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);
        
        var checkClientValidation = function (e) {
               // debugger;   
            //identify the error message comp.
            var tooltipid = "#" + element[0].id + "0InfoIconToolTip";
            var errtitleId = "#" + element[0].id + "0InfoIcon";
            var errId = "#" + element[0].id + "0Message";
            if (options.hasValue) {
                //display error component
                options.ControlId = this.id;
                var args = options.validationFunction(options);
                if (!args.isvalid) {
                    //display error component
                    if (args.updateMessage) {
                        //assign attributes proper values.
                        $(tooltipid).text(args.longErrorText);
                        $(errtitleId).find("a").attr("title", args.errorHelpTitle);
                        $(errId).text(args.errorText);
                       
                    }
                    element.show();
                    //To set focus for custom validator message.Changes done for AR2015.014898
                    $(errtitleId).find("a").focus();

                    //<AR = '2016.009600' date = '14/07/2016' author = 'amikumar'>
                    //Setting scrollbar in case error message is hidding behind floating bar
                    var scrollTo = 0;
                    if ($(".flottant").offset() != undefined) {
                        var overlap = !($(errtitleId).find("a").offset().top < $(".flottant").offset().top);
                        if (overlap) {
                            scrollTo = ($(errtitleId).find("a").offset().top) - 400;
                            if (scrollTo != 0) {
                                $('html, body').animate({
                                    scrollTop: scrollTo
                                }, 0);
                            }
                        }
                    }
                    //</AR = '2016.009600' date = '14/07/2016' author = 'amikumar'>

                    //<AR = '2016.009600' date = '14/07/2016' author = 'amikumar'>
                    //Setting scrollbar in case error message is hidding behind floating bar
                    var scrollTo = 0;
                    if ($(".flottant").offset() != undefined) {
                        var overlap = !($(errtitleId).find("a").offset().top < $(".flottant").offset().top);
                        if (overlap) {
                            scrollTo = ($(errtitleId).find("a").offset().top) - 400;
                            if (scrollTo != 0) {
                                $('html, body').animate({
                                    scrollTop: scrollTo
                                }, 0);
                            }
                        }
                    }
                    //</AR = '2016.009600' date = '14/07/2016' author = 'amikumar'>

                    //<AR = '2015.309917-D.01' date = '07/03/2016' author = 'mangoel'>
                    if ($('#hdnFormActionName').length > 0) {
                        document.forms[0].action = $('#hdnFormActionName').val();
                    }
                    //</AR>
                    if (options.ismultipleErrs) {
                        e.stopPropagation();
                    } else {
                        e.stopImmediatePropagation();
                    }
                    e.preventDefault();

                } else {
                    //hide error component
                    element.hide();
                }
            } else {
                if (options.errormessage != "") {
                    element.show();
                    e.preventDefault();
                    } else {
                    element.hide();
                };
            }
        },
        
           init = function () {
              
               element.hide();
               var controlObj = options.attachedControlId;
               var valCotrol = options.validationEnableControlId;
               //check if need to bind with the submit
               if (options.bindToSubmit) {
                   var formTag = element.closest("form");
                   formTag.submit(checkClientValidation);
               } else if (!options.bindToSubmit) {
                   if (controlObj != null) {
                       // bind the validation function with the associated control event
                       for (var i = 0; i < controlObj.length; i++) {
                           var elm = $("#" + controlObj[i]);
                           if (elm[0] != undefined) {
                               if (elm[0].type == "text" || elm[0].type == "textarea") {
                                   elm.blur(checkClientValidation);
                               }
                               else {
                                   elm.click(checkClientValidation);
                               }
                           }
                       }
                   }
               }

               // this code is use to fire custom validation in case if custom validator is having control id 
               // to fire custom validation 
               if (valCotrol != null) {
                   var ctrl = $("#" + valCotrol);
                   if (ctrl != undefined) {
                       ctrl.click(checkClientValidation);
                   }
               }
           };

        init();

    };




// JQuery extension function
    $.fn.customValidator = function (options) {
        var name = "customValidator";
        return $s.create(
            this,
            name,
            function (element, opt) {
                return new $s.CustomValidator(element, opt);
            },
            options).data(name);
    };

}(jQuery))