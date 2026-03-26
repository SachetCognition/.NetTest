$.validator.addMethod("durationvalidator", function (value, element, params) {
    var startdate = $("#" + params.startDateId).dateTime().getDate();
    var enddate = $("#" + params.EndDateId).dateTime().getDate();
    var timedifference = enddate - startdate;
    var inputduration = $("#" + params.DurationId).dateDuration().getDuration();
    if (inputduration <= timedifference) {
        $("#" + params.DurationId).removeClass("error");
        return true;
    } else {
        $("#" + params.DurationId).addClass("error");
        return false;
        
        
    }

   
});

$.validator.unobtrusive.adapters.add("durationvalidator", ["startdateid", "enddateid", "datedurationid"], function (options) {
       var params = {
            startDateId: options.params.startdateid,
            EndDateId: options.params.enddateid,
            DurationId: options.params.datedurationid,
           
           };

        options.rules["durationvalidator"] = params;
        options.messages["durationvalidator"] = options.message;

    });