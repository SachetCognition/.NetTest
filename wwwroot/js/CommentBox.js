(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.CommentBox = function (element, options) {
        // Public attributes
        this.element = element;

        // Constructor code
        $.extend(this, options);

        //To open search node popup
        //<retrun>false to avoid callback</return>
        var openSearchNodePopUpNode = function () {
            var txtareaValue = $("#" + options.textAreaClientID).val();
            var charcounter = $("#" + options.lblNbCharClientID).text();
            var jsonData = {
                "CommentBox_textvalue": txtareaValue,
                "CommentBox_charChounter": charcounter,
                "CommentBox_MaxLength": options.MaxLength,
                "IsUpdatable": options.IsUpdatable,
                "CommentBox_counterMode": options.counterMode
            };
            var jsonDataStr = JSON.stringify(jsonData);
            if (options.openpopupDelegate) {
                options.openpopupDelegate(callbackfunctionExpandPopup, jsonDataStr);
            }
            return false;
        };

        $("#" + options.btnHelpClientID).click(options.helpBtnFunction);
        $("#" + options.btnExpandClientID).click(openSearchNodePopUpNode);
    //Initialization method for comment box


            //Callback function with response when search node popup get closed
        //<params>response</params>
        
        var callbackfunctionExpandPopup = function (response) {
            if (response != null && response != "") {
                var value = JSON.parse(response);
                $("#" + options.textAreaClientID).val(value.CommentBox_textvalue_ret);
                $("#" + options.lblNbCharClientID).text(value.CommentBox_charChounter_ret);
                hideExpandButton();
            }
        };
        
        var hideExpandButton = function () {
            if (options.IsExpandButtonShown == "1") {
                
                $("#" + options.btnExpandClientID).show();

            }
        };
    };

    // JQuery extention function
    $.fn.commentBox = function (options) {
        var name = "commentBox";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.CommentBox(element, opt);
        },
        options).data(name);
    };

}(jQuery));