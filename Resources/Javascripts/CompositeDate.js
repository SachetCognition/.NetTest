/* this Jquery extension is used for the CompositeDate Component */
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CompositeDate = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        //this function initializes date control
        var//dropdown containing date types
            ddlDateTypes = $("#" + options.ddlDateTypesId),
            andLabel = options.andLabelId? $("#" + options.andLabelId): null,
            firstDate = options.firstDateId? $("#" + options.firstDateId): null,
            secondDate = options.secondDateId? $("#" + options.secondDateId): null,
            firstWeek = options.firstWeekId? $("#" + options.firstWeekId): null,
            secondWeek = options.secondWeekId? $("#" + options.secondWeekId): null,
            controlLabel = options.controlLabelId? $("#" + options.controlLabelId): null,
            
            //function to call after dateTypeHasBeenChanged
            dateTypeChanged = function () {
                //var dateTypeSelected = getDateTypeSelected($(this).val());
                var dateTypeSelected = $(this).val();
                if (dateTypeSelected) {
                    switch (dateTypeSelected) {
                        case "ModelEqual":
                        case "ModelGreaterThan":
                        case "ModelGreaterThanOrEqual":
                        case "ModelLessThan":
                        case "ModelLessThanOrEqual":
                            hideBothWeek();
                            clearBothDates();
                            changeDatesToModel();
                            showFirstDate();
                            hideSecondDate();
                            hideAndLabel();
                            setFirstDateAsFor();
                            break;
                        case "ModelBetween":
                            hideBothWeek();
                            clearBothDates();
                            changeDatesToModel();
                            showBothDates();
                            showAndLabel();
                            setFirstDateAsFor();
                            break;
                        case "Empty":
                        case "Equal":
                        case "GreaterThan":
                        case "GreaternThanOrEqual":
                        case "LessThan":
                        case "LessThanOrEqual":
                            hideBothWeek();
                            clearBothDates();
                            changeDatesToStandard();
                            showFirstDate();
                            hideSecondDate();
                            hideAndLabel();
                            setFirstDateAsFor();
                            break;
                        case "EqualCurrentDate":
                            hideBothDates();
                            clearBothDates();
                            changeDatesToStandard();
                            hideBothWeek();
                            clearBothWeek();
                            hideAndLabel();
                            setDateTypeAsFor();
                            break;
                        case "Between":
                            hideBothWeek();
                            clearBothDates();
                            changeDatesToStandard();
                            showBothDates();
                            showAndLabel();
                            setFirstDateAsFor();
                            break;
                        case "WeekBetween":
                            hideBothDates();
                            clearBothWeek();
                            showBothWeek();
                            showAndLabel();
                            setFirstWeekAsFor();
                            break;
                        case "Week":
                            hideBothDates();
                            clearBothWeek();
                            showFirstWeek();
                            hideSecondWeek();
                            hideAndLabel();
                            setFirstWeekAsFor();
                            break;
                    }
                }
            },

            setFirstDateAsFor = function () {
                if (options.firstDateTextId) {
                    controlLabel.attr("for", options.firstDateTextId);
                }
            },

             setFirstWeekAsFor = function () {
                 if (options.firstWeekTextId) {
                     controlLabel.attr("for", options.firstWeekTextId);
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

            //hide second part for date
            hideSecondDate = function () {
                if (secondDate) {
                    //hide errors as well
                    secondDate.dateTime().clearDate();
                    //hide parent div
                    secondDate.parent().hide();
                }
            },

            hideBothWeek = function () {
                if (firstWeek) {
                    //hide errors as well
                    firstWeek.weekYear().clearWeek();
                    //hide parent div
                    firstWeek.parent().hide();
                }

                if (secondWeek) {
                    //hide errors as well
                    secondWeek.weekYear().clearWeek();
                    //hide parent div
                    secondWeek.parent().hide();
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

            clearBothWeek = function () {
                if (firstWeek) {
                    firstWeek.weekYear().clearWeek();
                }

                if (secondWeek) {
                    secondWeek.weekYear().clearWeek();
                }
            },

            showBothWeek = function () {
                if (firstWeek) {
                    firstWeek.parent().show();
                }

                if (secondWeek) {
                    secondWeek.parent().show();
                }
            },

            showAndLabel = function () {
                if (andLabel) {
                    andLabel.show();
                }
            },

            showFirstWeek = function () {
                if (firstWeek) {
                    firstWeek.parent().show();
                }
            },

            hideSecondWeek = function () {
                if (secondWeek) {
                    secondWeek.parent().hide();
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
            };

        init();
    };

    // JQuery extension function
    $.fn.compositeDate = function (options) {
        var name = "CompositeDate";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.CompositeDate(element, opt);
        },
        options).data(name);
    };

}(jQuery));