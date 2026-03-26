(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.TreeView = function(element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        var init = function () {
            $s.TreeCommon.initTree(options);
            $('#'+options.id+' [type=checkbox]').each(function () {
                $(this).click(checkBoxClicked);
            });
        }
        ,

        checkBoxClicked = function() {
            var selected = [];
            $('#' + options.id + ' input:checked').each(function() {
                selected.push($(this).attr('Id').replace(options.id,""));
            });
            $("#" + options.checkedValuesId).val(selected);
  
    };
        
        init();
    };

    $.fn.treeView = function (options) {
    var name = "treeView";
    return $s.create(
    this,
    name,
    function (element, opt) {
        return new $s.TreeView(element, opt);
    },
    options).data(name);
};
}(jQuery));