/* this Jquery extension is used for the CompositeDate Component */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CompositeDateExt = function (element, options) {
      
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

       
        //this function initializes date control
        var //dropdown containing date types
            ddlDateTypes = $("#" + options.ddlDateTypesId),
            andLabel = options.andLabelId ? $("#" + options.andLabelId) : null;
            firstDate = options.firstDateId ? $("#" + options.firstDateId) : null,
            secondDate = options.secondDateId ? $("#" + options.secondDateId) : null,
            controlLabel = options.controlLabelId ? $("#" + options.controlLabelId) : null,
            dateDepth = options.dateDepthId ? $("#" + options.dateDepthId) : null,
            dateDepthDay = options.dateDepthdayId ? $("#" + options.dateDepthdayId) : null,
            lblDay = options.lblDayId ? $("#" + options.lblDayId) : null;
        //function to call after dateTypeHasBeenChanged
        dateTypeChanged = function () {               
            ddlDateTypes = $("#" + options.ddlDateTypesId);
            andLabel = options.andLabelId ? $("#" + options.andLabelId) : null;
            firstDate = options.firstDateId ? $("#" + options.firstDateId) : null;
            secondDate = options.secondDateId ? $("#" + options.secondDateId) : null;
            controlLabel = options.controlLabelId ? $("#" + options.controlLabelId) : null;
            dateDepth = options.dateDepthId ? $("#" + options.dateDepthId) : null;
            dateDepthDay = options.dateDepthdayId ? $("#" + options.dateDepthdayId) : null;
            lblDay = options.lblDayId ? $("#" + options.lblDayId) : null;
            if (options.isDisplayDayDepth) {
                hideDepth();
            }
            
                var dateTypeSelected = $(this).val();
                if (dateTypeSelected) {
                    switch (dateTypeSelected) {
                       
                        case "ModelBetween":
                            clearBothDates();
                            changeDatesToModel();
                            showBothDates();
                            if (andLabel) { andLabel.show(); }
                            setFirstDateAsFor();
                            break;
                        case "7":
                        case "5":
                        case "6":
                          
                            changeDatesToStandard();
                            showFirstDate();
                            hideSecondDate();
                            if (andLabel) { andLabel.hide(); }
                            setFirstDateAsFor();
                            break;
                        case "0":
                            hideBothDates();
                            clearBothDates();
                            changeDatesToStandard();
                            if (andLabel) { andLabel.hide(); }
                            if (options.isDisplayDayDepth) {
                                setDateTypeAsFor();
                                showDepth();
                            }
                            break;
                        case "9":                         
                            changeDatesToStandard();
                            showBothDates();
                            if (andLabel) { andLabel.show(); }
                            setFirstDateAsFor();
                            break;
                    }
                }
            },

            setFirstDateAsFor = function () {
                if (options.firstDateTextId) {
                    controlLabel.attr("for", options.firstDateTextId);
                }
            },

             setDateTypeAsFor = function () {
                 if (options.ddlDateTypesId) {
                     controlLabel.attr("for", options.ddlDateTypesId);
                 }
             },

            //change both dates to Model Type
            changeDatesToModel = function () {
                if (firstDate) {
                    var objFirstDate = firstDate.dateTime();
                    if (objFirstDate.dateType() !== $s.DateTime.dateType.Model) {
                        objFirstDate.convertToModel();
                    }
                }

                if (secondDate) {
                    var objSecondDate = secondDate.dateTime();
                    if (objSecondDate.dateType() !== $s.DateTime.dateType.Model) {
                        objSecondDate.convertToModel();
                    }
                }
            },

            //change both dates to standard type
            changeDatesToStandard = function () {
                if (firstDate) {
                    var objFirstDate = firstDate.dateTime();
                    if (objFirstDate.dateType() !== $s.DateTime.dateType.Standard) {
                        objFirstDate.convertToStandard();
                    }
                }

                if (secondDate) {
                    var objSecondDate = secondDate.dateTime();
                    if (objSecondDate.dateType() !== $s.DateTime.dateType.Standard) {
                        objSecondDate.convertToStandard();
                    }
                }
            },

            showBothDates = function () {
                if (firstDate) {
                    firstDate.parent().show();
                }

                if (secondDate) {
                    secondDate.parent().show();
                }
            },

            showFirstDate = function () {
                if (firstDate) {
                    firstDate.parent().show();
                }
            },

            showDepth = function () {
               
                    dateDepth.show();
                    dateDepthDay.show();
                    lblDay.show();
                
            },
        hideDepth = function () {
            
            dateDepth.hide();
            dateDepthDay.hide();
            lblDay.hide();

        }
        //hide second part for date
            hideSecondDate = function () {
                if (secondDate) {
                    //hide errors as well
                    secondDate.dateTime().clearDate();
                    //hide parent div
                    secondDate.parent().hide();
                }
            },

        clearBothDates = function () {
                if (firstDate) {
                    firstDate.dateTime().clearDate();
                }

                if (secondDate) {
                    secondDate.dateTime().clearDate();
                }
            },

            hideBothDates = function () {
                if (firstDate) {
                    //hides errors as well
                    firstDate.dateTime().clearDate();
                    //hide parent div
                    firstDate.parent().hide();
                }

                if (secondDate) {
                    //hides errors as well
                    secondDate.dateTime().clearDate();
                    //hide parent div
                    secondDate.parent().hide();
                }
            },

            showAndLabel = function () {
                if (andLabel) {
                    andLabel.show();
                }
            },

           
           hideAndLabel = function () {
               if (andLabel) {
                   andLabel.hide();
               }
           },

            init = function () {
                
                if (ddlDateTypes.length > 0) {
                    ddlDateTypes.change(dateTypeChanged);
                }
                if (ddlDateTypes[0][ddlDateTypes[0].selectedIndex].value !== "0") {
                    if (options.isDisplayDayDepth) {
                        hideDepth();
                    }
                }
            };

        init();
    };

    // JQuery extension function
    $.fn.compositeDateExt = function (options) {
        var name = "CompositeDate";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.CompositeDateExt(element, opt);
        },
        options).data(name);
    };

}(jQuery));