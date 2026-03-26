/* this Jquery extension is used for the WeekYear Component */
(function ($) {

    // Alias
    var $s = $.sav2000;

    //regex pattern to parse week string
    var weekYearRegex = {
        "English": "^[W|w]{1}(([+-]{1}[0-99]{1,2})?)$",
        "French": "^[S|s]{1}(([+-]{1}[0-99]{1,2})?)$"
    };

    // Constructor
    $s.WeekYear = function (element, options) {
        // Public attributes
        this.element = element;

        //private variables and functions
        var
            //present date time value retrieved from values set for html controls i.e. both textbox and dropdown lists
            controlDate = null,
            //text field which is attached to datepicker
            txtWeek = $("#" + options.txtWeekId),
            txtYear = $("#" + options.txtYearId),
            yearLabel = $("#" + options.yearLabelId),
            validator = null,

            /* this function is used to run validation and trigger date change if the control date variable is already set */
            /* this is to avoid double parsing for setWeekChanged method */
            setWeekChangeDirect = function (hideValidation, priorDate) {
                var otherDateObj = $("#" + options.associatedDateHtmlId).weekYear();

                //just hide errors
                if (hideValidation) {
                    hideWeekErrors();
                }
                    //run validation again to refresh new errors if any
                else {
                    //!!Attention!! Date validation must be run only after setting the current datetime value
                    //trigger date change validation now
                    runTxtWeekValidation();

                    //run validation for the other associated date control too
                    if (otherDateObj) {
                        otherDateObj.runWeekYearValidation();
                    }
                }

                //if both of the dates are defined then compare dates
                if (controlDate && priorDate) {
                    //if different then trigger change event
                    if (priorDate.getTime() !== controlDate.getTime()) {
                        triggerWeekChange();
                    }
                }
                    //since both are not defined, then trigger change even if one is defined
                else if (controlDate || priorDate) {
                    triggerWeekChange();
                }
            },

            /* this function is is used to synchronize date input with the control date variable */
            setWeekChanged = function (hideValidation) {
                //get previous date before setting new date
                var priorDate = controlDate;
                //set internal datetime
                setDateTime();

                //when the date is set directly from external setDate method
                setWeekChangeDirect(hideValidation, priorDate);
            },

            //this triggers date change event
            triggerWeekChange = function () {
                var weekChangeEvent = jQuery.Event("weekChanged");
                //send week year value
                weekChangeEvent.week = txtWeek.val();
                weekChangeEvent.year = txtYear.val();
                if (controlDate && controlDate instanceof Date) {
                    weekChangeEvent.date = new Date(controlDate.getTime());
                } else {
                    weekChangeEvent.date = null;
                }

                element.trigger(weekChangeEvent);
            },

            //sync date with week
            syncWeek = function () {
                var strWeek = $(txtWeek).val();
                //this will check for empty
                if (strWeek === "") {
                    //show and set value on year
                    resetYear();
                    setDatesAndCallChange(null);
                    return true;
                }

                var parsedDate;
                var parsedWeek = $s.WeekYear.parseWeekFromString(strWeek, options.weekFormat);
                //if week is a valid week formatted string, then reset and hide year
                //parsed week can be zero in a week formatted string
                if (parsedWeek !== null) {
                    resetYear();
                    parsedDate = $s.WeekYear.getFirstDayOfWeekFromCurrentWeek(parsedWeek, options.timeZoneOffset, options.utcMode);
                    setDatesAndCallChange(parsedDate);
                    return true;
                }

                //now check for week as number, as week is not a formatted string
                var week = $s.WeekYear.parseWeekNumber(strWeek);
                //this will check for zero as well
                if (!week) {
                    setYear();
                    setDatesAndCallChange(null);
                    return true;
                }

                //if week is a number, then set year and call week change
                setYear();
                var strYear = $(txtYear).val();
                var parsedYear = $s.WeekYear.parseYear(strYear);
                if (!parsedYear) {
                    setDatesAndCallChange(null);
                    return true;
                }

                //now that both week and year are valid number, parse Date and call change
                parsedDate = $s.WeekYear.getFirstDayfromWeekAndYear(week, parsedYear);
                setDatesAndCallChange(parsedDate);
                return true;
            },

            //this sets the control date prior to calling the change events for date
            setDatesAndCallChange = function(parsedDate) {
                var priorDate = controlDate;
                controlDate = parsedDate;
                setWeekChangeDirect(false, priorDate);
            },
            
            syncYearWithWeek = function () {
                //now change date values
                setWeekChanged(false);
            },

            //this sets the date time javascript object
            setDateTime = function () {
                controlDate = getControlDateTime();
            },

            //this returns current week parsed as date and time from html controls
            getControlDateTime = function () {
                var strWeek = $(txtWeek).val();
                //parse Week as week format string
                var parsedWeek = $s.WeekYear.parseWeekFromString(strWeek, options.weekFormat);
                //check for only null as zero value for the week is allowed
                if (parsedWeek !== null) {
                    return $s.WeekYear.getFirstDayOfWeekFromCurrentWeek(parsedWeek, options.timeZoneOffset, options.utcMode);
                }
                
                //try to parse week as number
                parsedWeek = $s.WeekYear.parseWeekNumber(strWeek);
                //zero is not allowed if week is a number
                if (!parsedWeek) {
                    return null;
                }

                var strYear = $(txtYear).val();
                //now parse Year
                var parsedYear = $s.WeekYear.parseYear(strYear);
                if (!parsedYear) {
                    return null;
                }

                //now week and year are both established to be numbers
                return $s.WeekYear.getFirstDayfromWeekAndYear(parsedWeek, parsedYear);
            },

            //this function initializes week year control
            init = function () {
                //set control date at this moment
                setDateTime();

                //sync date on week value change
                txtWeek.change(syncWeek);
                //sync date on year change
                txtYear.change(syncYearWithWeek);

                // For input textbox bind change event
                if (options.onWeekChange) {
                    element.bind('weekChanged', options.onWeekChange);
                }

                //get validator object
                if (txtWeek.length > 0) {
                    var form = txtWeek[0].form;
                    validator = $(form).validate();
                }

                //disable weekformat validation on keyup and focusout, because it is running before the user
                //has chance to correct the field.
                txtWeek.keyup(function () { return false; });
                txtWeek.focusout(function () { return false; });
                //disable focusout , keyup for year field, as it is unnecessarily hiding errors
                txtYear.keyup(function () { return false; });
                txtYear.focusout(function () { return false; });
            },

            //!!Attention!! Week validation must be run only after setting the current datetime value
            //this runs week text box validation on change of year
            runTxtWeekValidation = function () {
                //do not run any validations if the element is hidden
                if (txtWeek.is(":hidden")) {
                    return;
                }

                if (validator) {
                    if (txtWeek.length > 0) {
                        validator.element(txtWeek[0]);
                    }
                }
            },

            /*<summary>
                Method to set year in year textbox
                <params>yearValue</params>
            </summary>*/
            setYear = function (year) {
                //set year if a valid year
                if (arguments.length === 1) {
                    if (year !== "" && year.length <= 4) {
                        txtYear.val(year);
                    }
                }
                
                if (txtYear.val() === "") {
                    var currentDate = new Date();
                    txtYear.val(currentDate.getFullYear());
                }
                
                txtYear.show();
                yearLabel.show();
            },

            //method to hide Year and reset value
            resetYear = function () {
                txtYear.val("");
                txtYear.hide();
                yearLabel.hide();
            },

            //this clears the values
            clearWeekYearValues = function () {
                txtWeek.val("");
                resetYear();
            },

            //this hides all the errors of the date control displayed through validation plugin, if any
            hideWeekErrors = function () {
                if (txtWeek.length === 0) {
                    return;
                }

                //get error span
                var spanError = $("span[for='" + txtWeek[0].id + "']");
                if (spanError.length === 0) {
                    return;
                }

                //reset css on the affected parent elements
                var parentErrorSpan = spanError.parent();
                spanError.remove();
                parentErrorSpan.removeClass("field-validation-error").addClass("field-validation-valid");
                var mainDiv = $("#" + options.mainDivId);
                if (mainDiv.length > 0) {
                    mainDiv.removeClass("error").addClass("success");
                }
            };

        // Constructor code
        $.extend(this, options);

        //this gets the current date set in the control
        this.getDate = function () {
            return controlDate;
        };

        //this sets the date on the control
        //!! The week year control must always contain a value even by default, therefore this will not set any non valid dates
        this.setDate = function (strWeek, strYear) {
            //check week for values
            if (!strWeek || strWeek.length > 4) {
                return false;
            }

            var parsedDate = $s.WeekYear.parseWeekFromString(strWeek, options.weekFormat);
            //if week is empty or is a valid week formatted string, then reset and hide year
            if (parsedDate) {
                //set week value
                txtWeek.val(strWeek);
                resetYear();
                setDatesAndCallChange(parsedDate);
                return true;
            }

            var week = $s.WeekYear.parseWeekNumber(strWeek);
            if (!week) {
                return false;
            }

            //now week is a number is established
            
            //check year for values
            if (!strYear || strYear.length > 4) {
                return false;
            }

            //now parse Year
            var parsedYear = $s.WeekYear.parseYear(strYear);
            if (!parsedYear) {
                return null;
            }

            txtWeek.val(strWeek);
            txtYear.val(strYear);
            parsedDate = $s.WeekYear.getFirstDayfromWeekAndYear(week, parsedYear);
            setDatesAndCallChange(parsedDate);
            return true;
        };

        //this clears the Week Year values
        this.clearWeek = function () {
            clearWeekYearValues();
            setWeekChanged(true);
        },

        //this function runs the date control validation
        this.runWeekYearValidation = function () {
            runTxtWeekValidation();
        };

        //this hides all the errors of the date control, if any are displayed by validation plugin
        this.hideDateErrors = function () {
            hideWeekErrors();
        };


        //initialize date control
        init();
    };

    // this is used to set correct error type if week cannot be parsed
    $s.WeekYear.weekError = {
        None: 0,
        WeekInvalid: 1,
        YearNotPresent: 2
    };

    //this parses Date from week string
    $s.WeekYear.parseWeekFromString = function(strWeek, weekFormat) {
        //return if any of strWeek and Format are null
        if (!strWeek || !weekFormat) {
            return null;
        }

        //return null 
        if (weekFormat !== "English" && weekFormat !== "French") {
            return null;
        }

        var pattern;
        pattern = new RegExp(weekYearRegex[weekFormat]);

        var match = strWeek.match(pattern);
        if (!match) {
            return null;
        }

        //if there is only D and J, then return current Date
        if (!match[1]) {
            return 0;
        }

        return parseInt(match[1], 10);
    },
    
    //this returns first day of the week after adding given weeks to current date
    $s.WeekYear.getFirstDayOfWeekFromCurrentWeek = function(deltaWeeks, offset, utcMode) {
        var weekDate;
        weekDate = $s.DateTimeCommon.getCurrentDate(offset, utcMode);
        var x = deltaWeeks * 7;
        weekDate.setDate(weekDate.getDate() + x);
        return $s.WeekYear.getFirstDayOfWeek(weekDate);
    };

    //this returns first day of the week for the given date
    $s.WeekYear.getFirstDayOfWeek = function(weekDate) {
        var res = new Date(weekDate.getTime());
        //Monday is '1' in javascript
        while (res.getDay() !== 1) {
            res.setDate(res.getDate()-1);
        }

        return res;
    };

    //this parses week as integer
    $s.WeekYear.parseWeekNumber = function (strWeek) {
        //this will exclude null, empty, zero, undefined
        //note: week can't be zero
        if (!strWeek)
        {
            return null;
        }

        var pattern = new RegExp(/^\d{1,2}$/);
        if (!strWeek.match(pattern)) {
            return null;
        }
        
        var weekParsed = parseInt(strWeek, 10);

        //week can only be between 1 to 53
        if (weekParsed < 1 || weekParsed > 53)
        {
            return null;
        }

        return weekParsed;
    };

    //this parses year as integer
    $s.WeekYear.parseYear = function (year) {
        //this will exclude null, empty, zero, undefined
        if (!year)
        {
            return null;
        }

        var pattern = new RegExp(/^\d{4}$/);
        if (!year.match(pattern)) {
            return null;
        }
        
        var yearParsed = parseInt(year, 10);
        if (yearParsed < 1901 || yearParsed > 9999) {
            return null;
        }

        return yearParsed;
    };

    //this parses date from both week and year string
    $s.WeekYear.getFirstDayfromWeekAndYear = function (week, year) {
        // Attention! Javascript consider months in the range 0 - 11
        var res = new Date(year, 0, 4, 0, 0, 0, 0);
        res = $s.WeekYear.getFirstDayOfWeek(res);
        var x = 7 * (week - 1);
        res.setDate(res.getDate() + x);
        return res;
    };

    //this parses date first from week string, if it fails then from both week and year string
    $s.WeekYear.ParseWeekAndYear = function (strWeek, strYear, format, offset, utcMode) {
        var result = {
            isValid: false,
            errorType: $s.WeekYear.weekError.WeekInvalid,
            weekParsed: null
        }

        //this will exclude null, empty, zero, undefined
        if (!strWeek || !format) {
            return result;
        }

        var week = $s.WeekYear.parseWeekFromString(strWeek, format);

        //since parsed week from week string format can be zero, therefore check for null
        if (week !== null) {
            result.isValid = true;
            result.errorType = $s.WeekYear.weekError.None;
            result.weekParsed = $s.WeekYear.getFirstDayOfWeekFromCurrentWeek(week, offset, utcMode);
            return result;
        }

        week = $s.WeekYear.parseWeekNumber(strWeek);

        if (!week) {
            return result;
        }

        //now week has been established as a valid number, so check for year

        //first check for zero value
        if (strYear === 0) {
            return result;
        }
        
        //if strYear is empty or not provided then set appropriate result. The year 0 is excluded above
        if (!strYear) {
            result.errorType = $s.WeekYear.weekError.YearNotPresent;
            return result;
        }

        var year = $s.WeekYear.parseYear(strYear);
        
        if (!year) {
            return result;
        }

        //now return appropriate result
        result.weekParsed = $s.WeekYear.getFirstDayfromWeekAndYear(week, year);
        result.isValid = true;
        result.errorType = $s.WeekYear.weekError.None;
        return result;
    };

    // JQuery extention function
    $.fn.weekYear = function (options) {
        var name = "weekYear";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.WeekYear(element, opt);
        },
        options).data(name);
    };

}(jQuery));


// Add the week format attribute to the validators list
$.validator.addMethod("weekformat", function (value, element, params) {
    var strYear = $("#" + params.yearId).val();
    //if week and year value is blank then no need to check week format
    if (value === "" && strYear === "") {
        return true;
    }

    var offsetJson = $(element).attr("data-val-offset");
    var offset = $.parseJSON(offsetJson);
    var utcModeJson = $(element).attr("data-val-utcmode");
    var utcMode = $.parseJSON(utcModeJson);
    var result = $.sav2000.WeekYear.ParseWeekAndYear(value, strYear, params.format, offset, utcMode);
    if (!result.isValid) {
        if (result.errorType === $.sav2000.WeekYear.weekError.YearNotPresent) {
            $(element.form).validate().settings.messages[element.name]['weekformat'] = params.msgMissingYear;
        } else {
            $(element.form).validate().settings.messages[element.name]['weekformat'] = params.msgIncorrectWeek;
        }
        return false;
    }

    //finally, if it is here, then return true
    return true;
});

// Add unobtrusive adaptor for week format
$.validator.unobtrusive.adapters.add("weekformat", ["format", "yearId", "msgMissingYear", "msgIncorrectWeek"], function (options) {
    var params = {
        format: options.params.format,
        yearId: options.params.yearId,
        msgMissingYear: options.params.msgMissingYear,
        msgIncorrectWeek: options.params.msgIncorrectWeek
    };

    options.rules["weekformat"] = params;
    options.messages["weekformat"] = options.message;

});

// Add unobtrusive adaptor for week conditional required
$.validator.unobtrusive.adapters.add("weekconditionalrequired", ["otherweekid"], function (options) {
    options.rules["required"] = {
        depends: function () {
            //check only if the otherweekid is provided.
            if (!options.params.otherweekid) {
                return false;
            }
            var otherWeekObjectId = "#" + options.params.otherweekid;
                var otherWeekOutDiv = $(otherWeekObjectId);

                //no need to check further if other date object main div is hidden
                if (otherWeekOutDiv.is(":hidden")) {
                    return false;
                }

                var objOtherWeek = otherWeekOutDiv.weekYear();

                if (!objOtherWeek) {
                    return false;
                }

                var otherDate = objOtherWeek.getDate();

                //other date must be filled for this element to be required
                if (otherDate) {
                    return true;
                }
            

            return false;
        }
    };
    options.messages["required"] = options.message;
});

// Add the endweekgreaterthan to the validators list
$.validator.addMethod("endweekgreaterthan", function (value, element, params) {
    //if week control value is empty,then no need to check with other week
    if (!value) {
        return true;
    }

    var objWeekId = $(element).attr("data-val-weekObjectId");
    var objWeek = $("#" + objWeekId).weekYear();
    var date = objWeek.getDate();

    //if there is no value in date field, then no need to validate
    if (date === null) {
        return true;
    }

    if (!params.lesserWeekObjectId) {
        //if other date is not found, return true
        return true;
    }

    var objOtherWeek = $(params.lesserWeekObjectId).weekYear();
        var otherDate = objOtherWeek.getDate();

        //other date must be filled for this validation to be performed
        if (!otherDate) {
            return true;
        }
        //return true only if date is greater than or equal to other date. This is because check can be performed within same week
        return (date >= otherDate);
});

// Add unobtrusive validator
$.validator.unobtrusive.adapters.add("endweekgreaterthan", ["lesserweekid"], function (options) {
    var params = {
        lesserWeekObjectId: "#" + options.params.lesserweekid
    };
    options.rules["endweekgreaterthan"] = params;
    options.messages["endweekgreaterthan"] = options.message;
});