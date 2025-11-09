(function ($) {

    var $s = $.sav2000;
    $s.TreeCommon = {
        //this returns current date
        initTree: function (options) {

            var nbcolumn = 0;
            var myWidth = 0;
            var myWidthcell1 = 0;
            var windowWidth = $(window).width();
            var maxWidthColumn = 0;
            var divWidth = 0;
            //GET NB OF COLUMN
            $('#' + options.id + ' .cell').each(function () {

                var class_cell = $(this).attr("class").substr(10);
                if (parseInt(class_cell) >= parseInt(nbcolumn)) {
                    nbcolumn = class_cell;
                }
            });
            //alert('nb de colonne ='+nbcolumn);
            for (var i = 1 ; i <= nbcolumn; i++) {

                if (i == 1) {
                    $("#" + options.id + " .cell.cell-" + i).each(function () {

                        var lvl = $(this).parents("ul").attr("class");
                        var decal = lvl.substr(4);
                        var myWidthTotal = $(this).width() + decal * 20;
                        if (parseInt(myWidthTotal) >= parseInt(myWidthcell1)) {
                            myWidthcell1 = parseInt(myWidthTotal);
                            myWidthcell1 = myWidthcell1 + 10;
                        }
                    });
                    $("#" + options.id + " .cell.cell-" + i).each(function () {

                        var lvl = $(this).parents("ul").attr("class");
                        var decal = lvl.substr(4);
                        $(this).css("width", parseInt(myWidthcell1) - (decal * 20));
                        $(this).css("white-space", "nowrap");
                        divWidth = parseInt(myWidthcell1) - (decal * 20);
                    });
                }
                else {
                    $("#" + options.id + " .cell.cell-" + i).each(function () {
                        var myWidthTotal = $(this).width();
                        if (parseInt(myWidthTotal) >= parseInt(myWidth)) {
                            myWidth = myWidthTotal;
                            myWidth = myWidth + 10;
                        }
                    });
                    $("#" + options.id + " .cell.cell-" + i).each(function () {
                        $(this).css("width", parseInt(myWidth));
                        $(this).css("white-space", "nowrap");
                    });
                   
                    divWidth = divWidth + myWidth;
                    myWidth = 0;
                }
            };

          
            var treeWidth = divWidth + 100;
            if (parseInt($("#" + options.id).parent().css("width")) <= treeWidth) {
                $("#" + options.id).css("width", treeWidth);
            } else {
                $("#" + options.id).css("width",parseInt($("#" + options.id).parent().css("width"))-20);
            }

            var height = $("#" + options.id).find(".lvl-0").css("height");
            if (height != undefined) {
                $("#" + options.id).parent().css("height", parseInt(height) + 35);
            }
          

            /*--------------------------------------*\
                TREE ANIM
            \*--------------------------------------*/
            $('#' + options.id + ' li:has(ul)').addClass('parent_li').find('span.glyphicon');//.attr('title', options.Title);
            $('#' + options.id + ' li.parent_li span.glyphicon').each(function () {
                
                if ($(this).hasClass('glyphicon-minus')) {
                    $(this).parent('li.parent_li').addClass('parentlast');
                }
            });
            $('#' + options.id + ' li.parent_li span.glyphicon').each(function () {
               
                if ($(this).hasClass('glyphicon-plus')) {
                    $(this).parent('li.parent_li').append("<span class='hide-access'></span>");
                    $(this).parent('li.parent_li').find('ul').hide();
                } else {
                    $(this).parent('li.parent_li').append("<span class='hide-access'></span>");
                }

            });
           
            $('#'+ options.id+' li.parent_li a').click(function () {
                
                var children = $(this).parent('li.parent_li').find('ul');
               
                if (children.is(":visible")) {
                    children.hide();
                   // $(this).attr('title', options.CloseTitle);
                    $(this).find('span').addClass('glyphicon-plus').removeClass('glyphicon-minus');
                    $(this).parent('li.parent_li').find('span.hide-access').remove();
                    $(this).parent('li.parent_li').removeClass('parentlast');
                    $(this).parent('li.parent_li').append("<span class='hide-access'></span>");
                    var treeheight = $("#" + options.id).find(".lvl-0").css("height");
                    if (treeheight != undefined) {
                        $("#" + options.id).parent().css("height", parseInt(treeheight) + 35);
                    }
                } else {
                    children.show();
                    //$(this).attr('title', options.Title);
                    $(this).find('span').addClass('glyphicon-minus').removeClass('glyphicon-plus');
                    $(this).parent('li.parent_li').find('li.parent_li').find('a').find('span').addClass('glyphicon-minus').removeClass('glyphicon-plus');
                    $(this).parent('li.parent_li').find('span.hide-access').remove();
                    $(this).parent('li.parent_li').addClass('parentlast');
                    $(this).parent('li.parent_li').append("<span class='hide-access'></span>");
                    var treeheight2 = $("#" + options.id).find(".lvl-0").css("height");
                    if (treeheight2 != undefined) {
                        $("#" + options.id).parent().css("height", parseInt(treeheight2) + 35);
                    }
                }
                return true;
            });
        },

     

        checkBoxClicked: function (options) {
            $('#' + options.id + ' [type=checkbox]').each(function () {
                var selected = [];
                $('#' + options.id + ' input:checked').each(function () {
                    selected.push($(this).attr('Id').replace(options.id, ""));
                });
                $("#" + options.checkedValuesId).val(selected);
            });
        }
    };

}(jQuery));
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

        if (options.Columns != null) {
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
        var init = function () {


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
(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.TreeView = function (element, options) {
        // Public attributes
        this.element = element;
        // Constructor code
        $.extend(this, options);

        var init = function () {
            $s.TreeCommon.initTree(options);
            $('#' + options.id + ' [type=checkbox]').each(function () {
                $(this).click(checkBoxClicked);
            });
        }
        ,

        checkBoxClicked = function () {
            var selected = [];
            $('#' + options.id + ' input:checked').each(function () {
                selected.push($(this).attr('Id').replace(options.id, ""));
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