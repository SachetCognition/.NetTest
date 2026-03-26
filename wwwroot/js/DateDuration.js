(function ($) {
    var $s = $.sav2000;

    $s.DateDuration = function (element, options) {
        var validateMinuteTextBox;
        var validateSecondTextBox;
        var validateMiliSecondTextBox;
        var on = $("input").on('change', function () {
            //nipun
            var theId = $(this).attr("id");
            if ($("#" + theId).val() == "") {
                $("#" + theId).val(parseInt("0"));
            }
            if (theId == options.txtminId) {
                validateMinuteTextBox(theId);
            }
            if (theId == options.txtsecId) {
                validateSecondTextBox(theId);
            }
            if (theId == options.txtmilisecId) {
                validateMiliSecondTextBox(theId);
            }
        });
        validateMinuteTextBox = function (id) {
            var minval = parseInt($("#" + id).val());
            if (minval > 59) {
                var hourvalue = Math.floor(Math.abs(minval / 60));
                minval = minval % 60;
                $("#" + options.txthourId).val(parseInt($("#" + options.txthourId).val()) + hourvalue);
                $("#" + id).val(minval);
            }
        };
        validateSecondTextBox = function (id) {
            var secval = $("#" + id).val();
            if (secval > 59) {
                $("#" + id).val(59);
            }
        };
        validateMiliSecondTextBox = function (id) {
            var milisecval = $("#" + id).val();
            if (milisecval > 999) {
                var secvalue = Math.floor(Math.abs(milisecval / 1000));
                milisecval = milisecval % 1000;
                $("#" + options.txtsecId).val(parseInt($("#" + options.txtsecId).val()) + secvalue);
                $("#" + id).val(milisecval);
            }
        };

        this.onEditableModeClick = function () {
            $("#" + options.txthourId).removeAttr('readonly');
            $("#" + options.txtminId).removeAttr('readonly');
            $("#" + options.txtsecId).removeAttr('readonly');
            $("#" + options.txtmilisecId).removeAttr('readonly');
            $("#" + options.txthourId).removeClass("durationreadonly");
            $("#" + options.txtminId).removeClass("durationreadonly");
            $("#" + options.txtsecId).removeClass("durationreadonly");
            $("#" + options.txtmilisecId).removeClass("durationreadonly");

        }
        var init = function () {
            try {
                $("#" + options.txtsecId).setMask({ mask: '99', autoTab: false });
                $("#" + options.txthourId + ",#" + options.txtminId + ",#"
                    + options.txtmilisecId).setMask({ mask: '999', autoTab: false });
            } catch (ex) {
            }
            $("#" + options.txthourId).keyup(function () { return false; });
            $("#" + options.txthourId).focusout(function () { return false; });
        };


        this.getDuration = function () {
            var hoursvalue = $("#" + options.txtHourId).val();
            hoursvalue = parseInt(hoursvalue, 10) || 0;

            var minutesvalue = $("#" + options.txtMinId).val();
            minutesvalue = parseInt(minutesvalue, 10) || 0;

            var secondssvalue = $("#" + options.txtSecId).val();
            secondssvalue = parseInt(secondssvalue, 10) || 0;

            var millisecondssvalue = $("#" + options.milisecondtxtId).val();
            millisecondssvalue = parseInt(millisecondssvalue, 10) || 0;

            var timeDifference = (hoursvalue * 60 * 60 * 1000) + (minutesvalue * 60 * 1000) + (secondssvalue * 1000) + millisecondssvalue;
            return timeDifference;
        };

        init();
    };


    $.fn.dateDuration = function (options) {
        var name = "dateDuration";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DateDuration(element, opt);
        },
        options).data(name);
    };
}(jQuery));
