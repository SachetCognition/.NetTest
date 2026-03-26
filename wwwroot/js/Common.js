(function($) {
    var $s = $.sav2000 = {

        // Create function used by components
        // If the component has already been created it does not initialize it again
        create: function (query, name, fnInit, options) {
            return query.each(function () {
                if (!$(this).data(name)) {
                    var component = fnInit($(this), options);
                    $(this).data(name, component);
                }
            });
        },

        // Error displaying function
        error: function (fileName, functionName, functionArgs, errorMessage) {
            alert(fileName + " : " + functionName + " : " + JSON.stringify(functionArgs, null, 4) + " : " + errorMessage);
        }
        
    };

}(jQuery));
