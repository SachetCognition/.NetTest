(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.TreeGrid = function (element, options) {
        // Public attributes
        var check = false;
        this.element = element;
        // Constructor code
        $.extend(this, options);
       
        if (options.Columns != null)
        {
            if (options.Columns.length > 0) {
                for (var i = 0; i < options.Columns.length; i++) {
                for (var ii = 0; ii < options.data.length; ii++) {
                    var columnData = options.Columns[i];

                    var returnedHtml = columnData.mData(options.data[ii][columnData.PropertyName]);
                     
                    $("#spn" + options.id + options.Columns[i].PropertyName + options.data[ii][options.IdColumn]).html(returnedHtml);

                }
            }
            ;
        }
        }
        var init = function() {
            
           
            if (options.hasCheckBoxColumn) {
                element.treeView(options);
            } else {
                $s.TreeCommon.initTree(options);
            }
        };
        init();
    };

    $.fn.treegrid = function (options) {
      
        var name = "treegrid";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.TreeGrid(element, opt);
        },
        options).data(name);
    };
}(jQuery));