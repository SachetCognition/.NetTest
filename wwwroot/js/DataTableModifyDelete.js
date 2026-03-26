(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.DataTableModifyDelete = function (element, options) {

        // Public attributes
        this.element = element;
      
        // Private attributes
        var that = this;
        var initialized = false;
      
        // Initialization function for callbacks
        function init() {
            
            // Initialize render function of select column
            if (that.remove) {
                $.grep(that.dataTableInit.aoColumns, function(column) {
                    return column.sName == that.deleteColumnName;
                })[0].mRender = deleteRender;
            }

            if (that.modify) {
                $.grep(that.dataTableInit.aoColumns, function(column) {
                    return column.sName == that.modifyColumnName;
                })[0].mRender = modifyRender;
            }

            // Set up pre-draw callback
            var previousPreDrawCallback = that.dataTableInit.fnPreDrawCallback;
            that.dataTableInit.fnPreDrawCallback = function (settings) {
                preDrawCallback(settings, previousPreDrawCallback);
            };
        }

        // Initialisation function
        function preDrawCallback(settings, previousPreDrawCallback) {

            if (previousPreDrawCallback) {
                previousPreDrawCallback(settings);
            }

            function handleEvent(e, type, node) {
                var api = that.table.api();
                var cellData = api.cell(node).data();
                var tr = $(node).closest("tr")[0];
                var rowData = api.row(tr).data();
                if (type === "remove") {
                    that.remove.ondeleteClick(cellData, "display", rowData);
                }
                else if (type === "modify") {
                    that.modify.onmodifyClick(cellData, "display", rowData);
                }
                e.stopPropagation();
                return false;
            }

            if (!initialized) {
                // save datatable instance
                that.table = settings.oInstance;
                
                // Register click on delete icon
                if (that.remove) {
                    $(document).on("click", "[id^=" + that.element[0].id + "_deleteanchor_]", function (e) {
                        return handleEvent(e, "remove", $(this).closest("td")[0]);
                    });
                }
                
                // Register click on modify icon
                if (that.modify) {
                    $(document).on("click", "[id^=" + that.element[0].id + "_modifyanchor_]", function (e) {
                        return handleEvent(e, "modify", $(this).closest("td")[0]);
                    });
                }
                
                initialized = true;
            }

        }

        function render(opts, cellData, type, rowData) {
            var hideAccess, title = "";
            
            if (cellData) {
                var data = (typeof cellData === 'object') ? cellData : JSON.parse(cellData);
                if (data) {
                    if (data.Tooltip) {
                        title = data.Tooltip;
                        if (!data.HideAccess) {
                            data.HideAccess = data.Tooltip;
                        }
                    }
                    if (data.HideAccess) {
                        hideAccess = $('<span>').attr({"class": "hide-access"}).text(data.HideAccess);
                    }
                }
            }

            var img = $("<img>").attr({
                id: that.element[0].id + opts.idImageSubstring + rowData[that.rowIdColumnName],
                src: opts.imagePath,
                title: title,
                alt: title
            });

            var divid = that.element[0].id + opts.idAnchorSubstring + rowData[that.rowIdColumnName];
            var item = $('<a>').attr({ id: divid, href: "#" }).append(img);
            
            return hideAccess ? $.fn.outerHtml(hideAccess) + $.fn.outerHtml(item) : $.fn.outerHtml(item);
        }
        
        // Render the delete icon
        function deleteRender(cellData, type, rowData) {
            var opts = {
                "title": that.remove.deleteTitle ? that.remove.deleteTitle : "Delete",
                "idAnchorSubstring": "_deleteanchor_",
                "idImageSubstring": "_deleteimg_",
                "imagePath": "/Images/trash.png"
            }
            return render(opts, cellData, type, rowData);
        }
      
        // Render the modify icon
        function modifyRender(cellData, type, rowData) {
            var opts = {
                "title": that.modify.modifyTitle ? that.modify.modifyTitle : "Modify",
                "idAnchorSubstring": "_modifyanchor_",
                "idImageSubstring": "_modifyimg_",
                "imagePath": "/Images/picto-modif.png"
            }
            return render(opts, cellData, type, rowData);
        }

        // Constructor code
        $.extend(this, options);
        init();
    };

    // JQuery extention function
    $.fn.dataTableModifyDelete = function (options) {
        var name = "dataTableModifyDelete";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DataTableModifyDelete(element, opt);
        },
        options).data(name);
    };

}(jQuery));
