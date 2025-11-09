(function ($) {

    // Alias
    var $s = $.sav2000;
    $s.Duration = function (element, options) {

        this.element = element;
        $.extend(this, options);

        //Initialization 
        var init = function () {
            $('#UpSpinner' + options.comId).bind("click", upSpinnerClick);
            $('#DownSpinner' + options.comId).bind("click", downSpinnerClick);
            $('#imgEraser' + options.comId).bind("click", eragerClick);
            $('#Hour' + options.comId).bind('keypress', numericOnly);
            $('#Minut' + options.comId).bind('keypress', numericOnly);
            $('#Hour' + options.comId).bind('keyup', validateMaxHourValue);
            $('#Minut' + options.comId).bind('keyup', validateMaxMinValue);
        },
        //Up spinner click 
         upSpinnerClick = function () {

             var hours = $('#Hour' + options.comId).val();
             var minuts = $('#Minut' + options.comId).val();

             // Checking max value of duration control should not exceed to max thresh host time.
             if (parseInt(hours) >= parseInt(options.hourMaxValue) && parseInt(minuts) >= parseInt(options.timeInterval)) {
                 $('#Hour' + options.comId).val(hours);
                 $('#Minut' + options.comId).val(minuts);
             } else {
                 var totalMinuts = calTime(hours, minuts, options.timeInterval, true);
                 $('#Hour' + options.comId).val(getHours(totalMinuts));
                 $('#Minut' + options.comId).val(getMinuts(totalMinuts));
             }
         },
         //Down Spinner click 
         downSpinnerClick = function () {

             var hours = $('#Hour' + options.comId).val();
             var minuts = $('#Minut' + options.comId).val();
             var totalMinuts = calTime(hours, minuts, options.timeInterval, false);
             $('#Hour' + options.comId).val(getHours(totalMinuts));
             $('#Minut' + options.comId).val(getMinuts(totalMinuts));
         },
         //Erage button Click 
         eragerClick = function () {
             $('#Hour' + options.comId).val('');
             $('#Minut' + options.comId).val('');
         },
         //Numeric data validation 
            numericOnly = function (e) {
                if (e.keyCode == '9' || e.keyCode == '16') {
                    return;
                }
                var code;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                if (e.which == 46)
                    return false;
                if (code == 8 || code == 46) {
                    return true;
                }
                if (code < 48 || code > 57)
                    return false;
            },
            //Max hour value validation 
            validateMaxHourValue = function () {
                var val = $('#txtHour' + options.comId).val();
                val = parseInt(val);
                if (isNaN(val)) {
                    return false;
                }
                if (val < 0 || val > parseInt(options.hourMaxValue)) {
                    return false;
                } else {
                    return true;
                }
            },
            //Max minute value validation 
            validateMaxMinValue = function () {
                var val = $('#Minut' + options.comId).val();
                if (isNaN(val)) {
                    return false;
                }
                if (val == 0 || val == parseInt(options.timeInterval)) {
                    return true;
                } else {
                    return false;
                }
            };

        init();

        //Calculate time for time duration component 
        var calTime = function (hours, minuts, tmInt, isUp) {

            var hour = parseInt(hours);
            var min = parseInt(minuts);
            var timeInt = parseInt(tmInt);

            //Checking null values 
            if (isNaN(hour)) {
                return -1;
            }
            if (isNaN(min)) {
                return -1;
            }
            if (isNaN(timeInt)) {
                return -1;
            }
            //End checking null values 

            //Calculate max thresh hold time
            var maxThreshHoldTime = parseInt(options.hourMaxValue) * 60 + tmInt;

            //Capture current time which is came from screen
            var previousTime = hour * 60 + min;

            if (isUp == true) { // Calculation while click on Up Spinner click 
                if (min >= tmInt) {
                    hour = hour + getIntHours(min);
                    min = getIntMinuts(min);
                    if (min >= tmInt) {
                        hour += 1;
                        min = 0;
                    } else {
                        min = tmInt;
                    }

                } else {
                    min = tmInt;
                }
            } else { // Calculation while click on down spinner click 
                if (min > tmInt) {
                    if (hour != 0)
                        hour = hour - getIntHours(min);
                    min = getIntMinuts(min);
                    if (min < tmInt) {
                        min = 0;
                    } else {
                        min = tmInt;
                    }
                } else {
                    if (min == 0) {
                        hour -= 1;
                        min = tmInt;
                    } else {
                        min = 0;
                    }
                }
            }
            //Current calculated minutes after calculation 
            var currentTime = hour * 60 + min;

            //Final result yeild after calculation 
            if (currentTime > maxThreshHoldTime) {
                if (isUp == true)
                    return previousTime;
                else {
                    return currentTime;
                }
            } else {
                return currentTime;
            }
        }

        //Get formated Hours for display in GUI
        var getHours = function (minuts) {
            if (minuts <= 0)
                return '00';

            var hours = Math.floor(parseInt(minuts) / 60);
            if (hours < 10)
                return '0' + hours;

            return Math.floor(parseInt(minuts) / 60);
        }

        //Get integer hours for calculation 
        var getIntHours = function (minuts) {
            if (minuts <= 0)
                return 0;
            return Math.floor(parseInt(minuts) / 60);
        }

        //Get formated minutes for display in GUI
        var getMinuts = function (minuts) {
            if (minuts <= 0)
                return '00';
            var min = parseInt(minuts) % 60;
            if (min < 10)
                return '0' + min;

            return parseInt(minuts) % 60;
        }

        //Get integer minutes for calculation 
        var getIntMinuts = function (minuts) {
            if (minuts <= 0)
                return 0;
            return parseInt(minuts) % 60;
        }
    };

    $.fn.duration = function (options) {
        var name = "duration";
        return $s.create(
            this,
            name,
            function (element, opt) {
                return new $s.Duration(element, opt);
            }, options
        ).data(name);
    };

}(jQuery));