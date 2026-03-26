/* this Jquery extension is used for the DateTime Component */
(function ($) {
    // Alias
    var $s = $.sav2000;
    
    //regex pattern to parse model date
    var modelDateRegex = {
        "mm/dd/yy": "^[D|d]{1}(([+-]{1}[0-999]{1,3})?)$",
        "dd/mm/yy": "^[J|j]{1}(([+-]{1}[0-999]{1,3})?)$"
    };

    // Constructor
    $s.DateTime = function (element, options) {
        // Public attributes
        this.element = element;

        var timeItem = function(hours, minutes) {
            this.hours = hours;
            this.minutes = minutes;
        };

        //private variables and functions
        var that = this,//keep reference of the DateTime object in that
            //present date time value retrieved from values set for html controls i.e. both textbox and dropdown lists
            controlDate = null,
            //text field which is attached to datepicker
            txtDate = $("#" + options.txtDateId),
            ddlHour = $("#" + options.ddlHourId),
            ddlMinute = $("#" + options.ddlMinuteId),
            eraseButton = $("#" + options.eraseButtonId),
            currentDateImage = $("#" + options.currentDateImageId),
            dateTypeInput = $("#" + options.dateTypeId),
            hiddenHour = $("#" + options.hiddenHourId),
            hiddenMinute = $("#" + options.hiddenMinuteId),
            validator = null,
            //French language settings
            settings =
            {
                "fr-FR": {
                    monthNames: ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
                    monthNamesShort: ['Jan', 'Fév', 'Mar', 'Avr', 'Mai', 'Jun', 'Jul', 'Aoû', 'Sep', 'Oct', 'Nov', 'Déc'],
                    dayNames: ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'],
                    dayNamesShort: ['Dim', 'Lun', 'Mar', 'Mer', 'Jeu', 'Ven', 'Sam'],
                    dayNamesMin: ['Di', 'Lu', 'Ma', 'Me', 'Je', 'Ve', 'Sa'],
                    closeText: 'Fermer',
                    prevText: '&#x3c;Préc',
                    nextText: 'Suiv&#x3e;',
                    currentText: 'Courant'
                },
                // Spainish language settings
                "es-ES": {
                    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                    dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
                    dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
                    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
                    closeText: 'Cerrar',
                    prevText: '&#x3c;Ant',
                    nextText: 'Sig&#x3e;',
                    currentText: 'Hoy'
                }
            },

            /* this function is used to run validation and trigger date change if the control date variable is already set */
            /* this is to avoid double parsing of date, if it is already parsed once */
            setDateChangeDirect = function(hideValidation, priorDate) {
                var otherDateObj = $("#" + options.associatedDateHtmlId).dateTime();
             //   debugger;
                //just hide errors
                if (hideValidation) {
                    hideDateErrors();
                }
                //run validation again to refresh new errors if any
                else {
                    //!!Attention!! Date validation must be run only after setting the current datetime value
                    //trigger date change validation now
                    runTxtDateValidation();

                    //run validation for the other associated date control too
                    if (otherDateObj) {
                        var dateObj = otherDateObj.getDate();
                        if (dateObj == null || dateObj == 'undefined') {
                        }
                        else { otherDateObj.runDateControlValidation(); }
                    }
                }

                //if both of the dates are defined then compare dates
                if (controlDate && priorDate) {
                    //if different then trigger change event
                    if (priorDate.getTime() !== controlDate.getTime()) {
                        triggerDateChange();
                    }
                }
                    //since both are not defined, then trigger change even if one is defined
                else if (controlDate || priorDate) {
                    triggerDateChange();
                }
            },
            
            /* this function is is used to synchronize date input with the control date variable */
            setDateChanged = function (hideValidation) {
                //get previous date before setting new date
                var priorDate = controlDate;
                //set internal datetime
                setDateTime();

                //when the date is set directly from external setDate method
                setDateChangeDirect(hideValidation, priorDate);
            },

            //this triggers date change event
            triggerDateChange = function () {
                var dateChangeEvent = jQuery.Event("dateChanged");
                if (controlDate && controlDate instanceof Date) {
                    //create new instance of date so that date change event handler cannot change date value of private variable
                    var newDate = new Date(controlDate.getTime());
                    dateChangeEvent.date = newDate;
                } else {
                    dateChangeEvent.date = null;
                }

                dateChangeEvent.dateType = thisDateType();
                
                element.trigger(dateChangeEvent);
            },

            //this updates the data to parse if the date is entered without forward slash and with two digits for year
            //e.g. if date is entered as MMDDYY or MMDDYYYY or MM/DD/YY for english format
            updateInput = function(data) {
                var arrDate = [], dateArray, dt, year;
                if (data.indexOf("/") === -1 && data !== "") {
                    arrDate.push(data.substr(0, 2));
                    arrDate.push(data.substr(2, 2));
                    arrDate.push(data.substr(4, data.length - 4));
                    data = arrDate.join("/");
                }

                dateArray = data.split('/');
                if (dateArray.length === 3) {
                    if (dateArray[2].length == 2) {
                        dt = $s.DateTimeCommon.getCurrentDate(options.timeZoneOffset, options.utcMode);
                        year = dt.getFullYear() + " ";
                        year = year.substr(0, 2);
                        dateArray[2] = year + dateArray[2];
                        return dateArray.join("/");
                    }
                }
                return data;
            },
            
            //this syncs control date with date text input after date text is changed
            syncDate = function () {
                var strDate = $(txtDate).val();
                var data;
                var parsedDate;
                if (thisDateType() === $s.DateTime.dateType.Standard) {
                    data = updateInput(strDate);
                    parsedDate = $s.DateTime.parseDateFromString(data, options.dateFormat, thisDateType(), options.timeZoneOffset, options.utcMode);
                } else {
                    parsedDate = $s.DateTime.parseDateFromString(strDate, options.dateFormat, thisDateType(), options.timeZoneOffset, options.utcMode);
                }
                
                // change date to 0 padded format in case it is not
                if (parsedDate) {
                    if (thisDateType() === $s.DateTime.dateType.Standard) {
                        var formattedDateString = $s.DateTime.getDateTimeString(parsedDate, options.dateFormat);
                        txtDate.val(formattedDateString);
                    }
                }
                // zero padding change ends
                //set hour minute fields if time is displayed
                if (options.displayTime) {
                    if (strDate === "" || !parsedDate) {
                        setHoursMinutesDisabled();
                        parsedDate = null;
                    } else {
                        setHoursMinutesEnabled();
                        var timeObject = parseTime();
                        if (timeObject) {
                            setTimeOnDate(parsedDate, timeObject.hours, timeObject.minutes);
                        } else {
                            //time couldn't be parsed, currentDate is undefined, set it to null
                            parsedDate = null;
                        }
                    }
                } else {
                    if (strDate === "" || !parsedDate) {
                        resetHiddenTime();
                        parsedDate = null;
                    } else {
                        setHiddenTime();
                        //set time to zero, as without this, somehow two equal dates weren't equal
                        setTimeOnDate(parsedDate, 0, 0);
                    }
                }

                //now change date value
                var priorDate = controlDate;
                controlDate = parsedDate;
                setDateChangeDirect(false, priorDate);
            },

            syncTimeWithDate = function () {
                //now change date value
                setDateChanged(false);
            },

            parseTime = function () {
                if (options.displayTime) {
                    // check if hours and minute are empty
                    if (ddlHour.val() !== options.timeDefaultValue && ddlMinute.val() !== options.timeDefaultValue) {
                        var timeObject = new timeItem(null, null);
                        timeObject.hours = ddlHour.val();
                        timeObject.minutes = ddlMinute.val();
                        return timeObject;
                    }
                }

                return null;
            },

            //this returns if the date type is Model or Standard
            thisDateType = function () {
                var type = txtDate.attr("data-val-dateformat-type");
                if (!type) {
                    return "";
                }

                if (type !== $s.DateTime.dateType.Model && type !== $s.DateTime.dateType.Standard) {
                    return "";
                }
                
                return type;
            },
            
            //this sets the date time javascript object
            setDateTime = function () {
                controlDate = getControlDateTime();
            },

            //this returns current date and time parsed from html controls
            getControlDateTime = function () {
                var strDate = $(txtDate).val();
                var parsedDate = $s.DateTime.parseDateFromString(strDate, options.dateFormat, thisDateType(), options.timeZoneOffset, options.utcMode);
                //date from parse date method must be null if it is unable to parse it
                if (parsedDate) {
                    if (options.displayTime) {
                        var timeObject = parseTime();
                        if (timeObject) {
                            setTimeOnDate(parsedDate, timeObject.hours, timeObject.minutes);
                        } else {
                            //time couldn't be parsed, currentDate is undefined, set it to null
                            parsedDate = null;
                        }
                    } else {
                        setTimeOnDate(parsedDate, 0, 0);
                    }
                } else {
                    if (options.displayTime) {
                        setHoursMinutesDropdownDisable(strDate);
                    }
                }

                return parsedDate;
            },

            setHoursMinutesDropdownDisable = function (strDate) {
                if (strDate === "") {
                    setHoursMinutesValue(options.timeDefaultValue, options.timeDefaultValue);
                } else {
                    setHoursMinutesValue(ddlHour.val(), ddlMinute.val());
                }
                
                ddlHour.attr("disabled", "disabled");
                ddlMinute.attr("disabled", "disabled");
            },


            //this sets the time values for given date object
            setTimeOnDate = function (dateValue, hours, minutes) {
                dateValue.setHours(hours);
                dateValue.setMinutes(minutes);
                dateValue.setSeconds(0);
                dateValue.setMilliseconds(0);
            },
            
        /*<summary>
        Method to define date picker settings
        </summary>*/
            getDatePickerSettings = function () {
                var dpSettingsFrom;

                dpSettingsFrom = {
                    changeMonth: true,
                    changeYear: true,
                    duration: '',
                    firstDay: 1
                };

                if (options.calendarImagePath && options.calendarImagePath !== "") {
                    dpSettingsFrom.showOn = 'button';
                    dpSettingsFrom.buttonImageOnly = true;
                    dpSettingsFrom.buttonImage = options.calendarImagePath;
                    dpSettingsFrom.buttonText = options.calendarImageText;
                }

                if (options.dateFormat) {
                    dpSettingsFrom.dateFormat = options.dateFormat;
                } else {
                    dpSettingsFrom.dateFormat = "mm/dd/yy";
                }

                if (options.culture !== "en-US") {
                    $.extend(dpSettingsFrom, settings[options.culture]);
                }

                var minDate;
                //set minimum date
                if (options.startFromCurrentDate) {
                    minDate = $s.DateTimeCommon.getCurrentDate(options.timeZoneOffset, options.utcMode);
                }
                else {
                    minDate = new Date();
                    // Set the minimum date to 1901 1 Jan in date Picker
                    minDate.setDate(1);
                    minDate.setMonth(0);
                    minDate.setFullYear(1901);
                    dpSettingsFrom.minDate = minDate;
                    minDate.setHours(0);
                    minDate.setMinutes(0);
                    minDate.setSeconds(0);
                    minDate.setMilliseconds(0);
                }
                

                dpSettingsFrom.minDate = minDate;
                return dpSettingsFrom;
            },

            //this function initializes date control
            init = function () {
                //set control date at this moment
                setDateTime();
                
                //all the following tasks are not required if the control is not updatable
                if (!options.isUpdatable) {
                    return;
                }

                if (thisDateType() === $s.DateTime.dateType.Standard) {
                    var dpSettingsFrom = getDatePickerSettings();
                    $(txtDate).datepicker(dpSettingsFrom);
                }
                //sync dates on textbox value change
                txtDate.change(syncDate);
                //sync dates on dropdown change
                ddlHour.change(syncTimeWithDate);
                ddlMinute.change(syncTimeWithDate);
                //clean date fields on erase button click
                eraseButton.click(eraseDateTimeData);
                //populate current date on current date image click
                currentDateImage.click(populateCurrentDate);

                // For input textbox bind change event
                if (options.onDateChange) {
                    element.bind('dateChanged', options.onDateChange);
                }
                
                //get validator object
                if (txtDate.length > 0) {
                    var form = txtDate[0].form;
                    validator = $(form).validate();
                }
                
                //disable dateformat validation on keyup and focusout, because it is running before the user
                //has chance to correct the field.
                txtDate.keyup(function () { return false; });
                txtDate.focusout(function () { return false; });
            },
            
            //!!Attention!! Date validation must be run only after setting the current datetime value
            //this runs date text box validation on change of dropdown
            runTxtDateValidation = function () {
                //do not run any validations if the element is hidden
                if (txtDate.is(":hidden")) {
                    return;
                }
                
                if (validator) {
                    if (txtDate.length > 0) {
                        validator.element(txtDate[0]);
                    }
                }
            },


            //retrieve current time for the user machine
            getCurrentTime = function () {
                var mcDate, mcHours, mcMinutes;
                mcDate = $s.DateTimeCommon.getCurrentDateTime(options.timeZoneOffset, options.utcMode);
                mcHours = mcDate.getHours();
                mcMinutes = mcDate.getMinutes();

                return new timeItem(mcHours, mcMinutes);
            },

            /*<summary>
                Method to set hour and minute in hour minute controls
                <params>hourValue</params>
            </summary>*/
            setHoursMinutesValue = function (hourValue, minValue) {
                ddlHour.val(hourValue);
                ddlMinute.val(minValue);
            },

            //method to enable hour and minute dropdowns and set value
            setHoursMinutesEnabled = function () {
                if (!options.displayTime) {
                    return;
                }
                //if both hour and minutes are set then no need to set time
                if (ddlHour.val() !== null && ddlMinute.val() !== null && ddlHour.val() !== options.timeDefaultValue && ddlMinute.val() !== options.timeDefaultValue) {
                    return;
                }

                //set current user machine time as the current time
                var machineTime = getCurrentTime();
                setHoursMinutesValue(machineTime.hours, machineTime.minutes);
                ddlHour.removeAttr("disabled");
                ddlMinute.removeAttr("disabled");
            },

            //method to disable hour and minute dropdowns and reset values
            setHoursMinutesDisabled = function () {
                //first clear the hour minute fields as they can be disabled only after values have been cleared
                setHoursMinutesValue(options.timeDefaultValue, options.timeDefaultValue);
                ddlHour.attr("disabled", "disabled");
                ddlMinute.attr("disabled", "disabled");
            },
            
            //set time in hidden inputs. !Hidden input is used when time dropdowns are not to be displayed!
             setHiddenTime = function () {
                 if (hiddenHour.length === 0 || hiddenMinute.length === 0) {
                     return;
                 }
                 
                 hiddenHour.val("0");
                 hiddenMinute.val("0");
             },
             
             //reset hidden time
             resetHiddenTime = function () {
                 if (hiddenHour.length === 0 || hiddenMinute.length === 0) {
                     return;
                 }

                 hiddenHour.val(options.timeDefaultValue);
                 hiddenMinute.val(options.timeDefaultValue);
             },
            
            //Method to reset all DateTime Values and call date change and run validations
            eraseDateTimeData = function () {
                clearDateTimeValues();
                setDateChanged(false);
            },

            //this function populates the current date according to correct utc mode and time offset
            populateCurrentDate = function() {
                var currentMachineDate = $s.DateTimeCommon.getCurrentDateTime(options.timeZoneOffset, options.utcMode);
                that.setDate(currentMachineDate);
            },
            
            //this clears the values
            clearDateTimeValues = function () {
                txtDate.val("");
                txtDate.focus();
                if (options.displayTime) {
                    ddlHour.val(options.timeDefaultValue);
                    ddlMinute.val(options.timeDefaultValue);
                    setHoursMinutesDisabled();
                } else {
                    resetHiddenTime();
                }
            },
            
            //this returns date string from the given javascript date
            getDateString = function (date) {
                switch (thisDateType()) {
                    case $s.DateTime.dateType.Standard:
                        return $s.DateTime.getDateTimeString(date, options.dateFormat);
                    case $s.DateTime.dateType.Model:
                        return $s.DateTime.getModelDateString(date, options.dateFormat, options.timeZoneOffset, options.utcMode);
                    default:
                        return "";
                }
            },
            
            //this hides all the errors of the date control displayed through validation plugin, if any
            hideDateErrors = function () {
                if (txtDate.length === 0) {
                    return;
                }

                //get error span
                var spanError = $("span[for='" + txtDate[0].id + "']");
                if (spanError.length === 0) {
                    return;
                }
               // debugger;
                //reset css on the affected parent elements
                var parentErrorSpan = spanError.parent();
                spanError.remove();
                parentErrorSpan.removeClass("field-validation-error").addClass("field-validation-valid");
                var mainDiv = $("#" + options.dateDivId);
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
        this.setDate = function (newDate) {
            //do nothing for readonly mode
            if (!options.isUpdatable) {
                return false;
            }
            
            //ignore if the passed date is not of date type
            if (!newDate || !(newDate instanceof Date)) {
                return false;
            }

            var strDate = getDateString(newDate);
            if (!strDate) {
                return false;
            }

            txtDate.val(strDate);
            var dateToSet = new Date(newDate.getTime());
            if (options.displayTime) {
                setTimeOnDate(dateToSet, dateToSet.getHours(), dateToSet.getMinutes());
                setHoursMinutesValue(dateToSet.getHours(), dateToSet.getMinutes());
                //enable hour and minute too
                ddlHour.removeAttr("disabled");
                ddlMinute.removeAttr("disabled");
            } else {
                setTimeOnDate(dateToSet, 0, 0);
                setHiddenTime();
            }

            // previous date to use for date change event
            var priorDate = controlDate;
            controlDate = dateToSet;
            
            setDateChangeDirect(false, priorDate);
            return true;
        };
        
        //this clears the date
        this.clearDate = function() {
            clearDateTimeValues();
            setDateChanged(true);
        },
        
        //this function runs the date control validation
        this.runDateControlValidation = function() {
            runTxtDateValidation();
        };
        
        //this function converts the date control to model date control at run time
        this.convertToModel = function () {
            //do nothing if it is already model
            if (thisDateType() === $s.DateTime.dateType.Model) {
                return false;
            }

            //change datetype attribute
            txtDate.attr("data-val-dateformat-type", $s.DateTime.dateType.Model);

            //change value of hidden input for date Type
            dateTypeInput.val($s.DateTime.dateType.Model);

            //destroy date picker
            txtDate.datepicker("destroy");
            clearDateTimeValues();
            txtDate.attr("maxlength", "5");
            setDateChanged(true);
            return true;
        };
        
        //this function converts the date control to standard date control at run time
        this.convertToStandard = function () {
            //do nothing if it is already model
            if (thisDateType() === $s.DateTime.dateType.Standard) {
                return false;
            }

            //change datetype attribute
            txtDate.attr("data-val-dateformat-type", $s.DateTime.dateType.Standard);

            //change value of hidden input for date Type
            dateTypeInput.val($s.DateTime.dateType.Standard);

            //initialize date picker
            var dpSettingsFrom = getDatePickerSettings();
            $(txtDate).datepicker(dpSettingsFrom);

            clearDateTimeValues();
            txtDate.attr("maxlength", "10");
            setDateChanged(true);
            return true;
        };

        //this hides all the errors of the date control, if any are displayed by validation plugin
        this.hideDateErrors = function () {
            hideDateErrors();
        };

        this.dateType = function() {
            return thisDateType();
        };

        //initialize date control
        init();
    };
    
    /* enum for model (e.g. D) /standard type (e.g dd/mm/yy) of date */
    $s.DateTime.dateType = {
        Model: "model",
        Standard: "standard"
    };

    //this parses Date from date string
    $s.DateTime.parseDateFromString = function(strDate, dateFormat, type, offset, utcMode) {
        if (!type) {
            return null;
        }

        switch (type) {
        case $s.DateTime.dateType.Standard:
            return $s.DateTime.parseFromStandardDate(strDate, dateFormat);
        case $s.DateTime.dateType.Model:
            return $s.DateTime.parseFromModelDate(strDate, dateFormat, offset, utcMode);
        default:
            return null;
        }
    };
        //this parses Date from model date string
    $s.DateTime.parseFromModelDate = function(strDate, dateFormat, offset, utcMode) {
        //return if any of strDate and dateFormat are null
        if (!strDate || !dateFormat) {
            return null;
        }

        //return null 
        if (dateFormat !== "dd/mm/yy" && dateFormat !== "mm/dd/yy") {
            return null;
        }

        var pattern;
        pattern = new RegExp(modelDateRegex[dateFormat]);

        var match = strDate.match(pattern);
        if (!match) {
            return null;
        }

        var currentDate;
        currentDate = $s.DateTimeCommon.getCurrentDate(offset, utcMode);


        //if there is only D and J, then return current Date
        if (!match[1]) {
            return currentDate;
        }

        currentDate.setDate(currentDate.getDate() + parseInt(match[1]));
        return currentDate;
    };

    /*<summary>
        Method to validate the input as a valid date
        <params>value of date control textBox</params>
        </summary>*/
    $s.DateTime.parseFromStandardDate = function (strDate, dateFormat) {
        //return if any of strDate and dateFormat are null
        if (!strDate || !dateFormat) {
            return null;
        }

        //return null 
        if (dateFormat !== "dd/mm/yy" && dateFormat !== "mm/dd/yy") {
            return null;
        }

        var pattern, dateArray, day, month, year, daysInMonth, i, sourceDate;
        // Regular expression used to check if date is in correct format
        pattern = new RegExp(/^\d{1,2}(\/)\d{1,2}\1\d{4}$/);
        if (strDate.match(pattern)) {
            dateArray = strDate.split('/');
            day = dateFormat === "dd/mm/yy" ? dateArray[0] : dateArray[1];
            // Attention! Javascript consider months in the range 0 - 11
            month = dateFormat === "dd/mm/yy" ? dateArray[1] - 1 : dateArray[0] - 1;
            year = dateArray[2];
            if (month > 11) {
                return null;
            } else {
                // set the no of days in a month
                daysInMonth = [];
                for (i = 0; i < 12; i++) {
                    daysInMonth[i] = 31;
                    if (i == 3 || i == 5 || i == 8 || i == 10) {
                        daysInMonth[i] = 30;
                    }
                    if (i == 1) {
                        daysInMonth[i] = 29;
                    }
                }
                // If input is invalid day, then return false
                if (day < 1 || day > 31 || (month === 1 && day > $s.DateTime.daysInFebruary(year)) || day > daysInMonth[month]) {
                    return null;
                }
            }

            // This instruction will create a date object                    
            sourceDate = new Date(year, month, day);
            if (year != sourceDate.getFullYear()) {
                return null;
            }
            if (month != sourceDate.getMonth()) {
                return null;
            }
            if (day != sourceDate.getDate()) {
                return null;
            }
            return sourceDate;
        } else {
            return null;
        }
    };

    $s.DateTime.getDateTimeString = function (date, dateFormat) {
        if (!date || !(date instanceof Date) || !dateFormat) {
            return "";
        }
        
        if (dateFormat !== "dd/mm/yy" && dateFormat !== "mm/dd/yy") {
            return "";
        }

        var dateCopy = new Date(date.getTime());
        var day = ("0" + dateCopy.getDate()).slice(-2), month = ("0" + (dateCopy.getMonth() + 1)).slice(-2), year = dateCopy.getFullYear();
        var value = dateFormat === "dd/mm/yy" ? day + "/" + month + "/" + year: month + "/" + day + "/" + year;
        return value;
    };

    $s.DateTime.getModelDateString = function (date, dateFormat, timeZoneOffset, utcMode) {
        if (!date || !(date instanceof Date) || !dateFormat) {
            return "";
        }
        
        if (dateFormat !== "dd/mm/yy" && dateFormat !== "mm/dd/yy") {
            return "";
        }
        
        //get date without time
        var onlyDate = new Date(date.getTime());
        onlyDate.setHours(0);
        onlyDate.setMinutes(0);
        onlyDate.setSeconds(0);
        onlyDate.setMilliseconds(0);
        var currentdate = $s.DateTimeCommon.getCurrentDate(timeZoneOffset, utcMode);
        //get difference as days
        var diff = (onlyDate - currentdate) / (1000 * 60 * 60 * 24);
        var dateString;
        var days = "";
        if (diff > 0) {
            days = "+" + diff.toString();
        } else if (diff < 0) {
            days = diff.toString();
        }
        
        if (dateFormat === "mm/dd/yy") {
            dateString = "D" + days;
        } else {
            dateString = "J" + days;
        }

        return dateString;
    };

    /*<summary>
        Method to find days in February in a particular year
        <params>year</params>
    </summary>*/
    $s.DateTime.daysInFebruary = function (year) {
        var leapyear = 29;
        if ((year % 4 === 0) && (!(year % 100 === 0) || year % 400 === 0)) {
            return leapyear;
        } else {
            return 28;
        }
    };

    // JQuery extention function
    $.fn.dateTime = function (options) {
        var name = "dateTime";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DateTime(element, opt);
        },
        options).data(name);
    };

}(jQuery));

// Add the date format attribute to the validators list
$.validator.addMethod("dateformat", function (value, element, params) {
    //if date value is blank then no need to check date format
    if (value === "") {
        return true;
    }

    //get the type of date, if it is model or standard
    var type = $(element).attr("data-val-dateformat-type");

    if (!type) {
        return false;
    }

    var offsetJson = $(element).attr("data-val-offset");
    var offset = $.parseJSON(offsetJson);
    var utcModeJson = $(element).attr("data-val-utcmode");
    var utcMode = $.parseJSON(utcModeJson);

    //parse date and if correct return true
    if (!$.sav2000.DateTime.parseDateFromString(value, params.format, type, offset, utcMode)) {
        $(element.form).validate().settings.messages[element.name]['dateformat'] = params.msgIncorrectDate;
        return false;
    }

    if (params.displayTime === "false") {
        return true;
    }

    //check hour and Minute DropDown
    var hourDropDownVal = $("#" + params.hourDropDown).val();
    var minuteDropDownVal = $("#" + params.minuteDropDown).val();

    if (hourDropDownVal === params.defaultValue || minuteDropDownVal === params.defaultValue) {
        $(element.form).validate().settings.messages[element.name]['dateformat'] = params.msgIncorrectTime;
              return false;
    }

    //finally, if it is here, then return true
    return true;
});

// Add unobtrusive adaptor for date format
$.validator.unobtrusive.adapters.add("dateformat", ["format", "hourDropDown", "minuteDropDown", "defaultValue", "msgIncorrectDate"
    , "msgIncorrectTime", "displayTime"], function (options) {
        var params = {
            format: options.params.format,
            defaultValue: options.params.defaultValue,
            hourDropDown: options.params.hourDropDown,
            minuteDropDown: options.params.minuteDropDown,
            msgIncorrectDate: options.params.msgIncorrectDate,
            msgIncorrectTime: options.params.msgIncorrectTime,
            displayTime: options.params.displayTime
        };

        options.rules["dateformat"] = params;
        options.messages["dateformat"] = options.message;

    });

// Add unobtrusive adaptor for date conditional required
$.validator.unobtrusive.adapters.add("dateconditionalrequired", function (options) {
    options.rules["required"] = {
        depends: function () {

           // debugger;
            var otherDateObjectId = "#" + $("#" + this.id).attr("otherdateid");
            if (otherDateObjectId === "#undefined") return true;

                var otherDateOutDiv = $(otherDateObjectId);

                //no need to check further if other date object main div is hidden
                if (otherDateOutDiv.is(":hidden") || $("#" + this.parentElement.id).is(":hidden")) {
                    return false;
                }

                var objOtherDate = otherDateOutDiv.dateTime();

                if (!objOtherDate) {
                    return false;
                }

                var otherDate = objOtherDate.getDate();

                //other date must be filled for this element to be required
                if (otherDate) {
                    return true;
                }
            

            return false;
        }
    };
    options.messages["required"] = options.message;
});

//just add required rule for date required
$.validator.unobtrusive.adapters.addBool("daterequired", "required");

// Add the enddategreaterthan to the validators list
$.validator.addMethod("enddategreaterthan", function (value, element, params) {
    //if date control value is empty,then no need to check with other date
    if (!value) {
        return true;
    }
    
    var objDateId = $(element).attr("data-val-dateObjectId");
    var objDate = $("#" + objDateId).dateTime();
    var date = objDate.getDate();

    //if there is no value in date field, then no need to validate
    if (date === null) {
        return true;
    }
    var otherDateObjectId = "#" + $(element).attr("otherdateid");
    if (otherDateObjectId === "#undefined" || otherDateObjectId === "#") return true;

    var objOtherDate = $(otherDateObjectId).dateTime();
        var otherDate = objOtherDate.getDate();

        //other date must be filled for this validation to be performed
        if (!otherDate) {
            return true;
        }

        //return true only if date is greater than other date
        return (date > otherDate);
});

// Add unobtrusive validator
$.validator.unobtrusive.adapters.add("enddategreaterthan",  function (options) {

 
    options.rules["enddategreaterthan"] = [];
    options.messages["enddategreaterthan"] = options.message;
});


// Add the date format attribute to the validators list
$.validator.addMethod("lessthancurrentdate", function (value, element, params) {
    var objDateTimeId = $(element).attr("data-val-dateObjectId");
    var objDateTime = $("#" + objDateTimeId).dateTime();
    var dateValue = objDateTime.getDate();

    if (dateValue === null) {
        return true;
    }

    var offsetJson = $(element).attr("data-val-offset");
    var utcModeJson = $(element).attr("data-val-utcmode");
    var offset = $.parseJSON(offsetJson);
    var utcMode = $.parseJSON(utcModeJson);
    var currDate = $.sav2000.DateTimeCommon.getCurrentDate(offset, utcMode);

    return (currDate <= dateValue);
});

// Add unobtrusive adaptor for date format
$.validator.unobtrusive.adapters.add("lessthancurrentdate", [], function (options) {
    options.rules["lessthancurrentdate"] = [];
    options.messages["lessthancurrentdate"] = options.message;
});