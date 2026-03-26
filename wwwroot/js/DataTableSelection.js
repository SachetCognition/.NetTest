(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    $s.DataTableSelection = function (element, options) {
        
        // Public attributes
        this.element = element;
        this.selectState = new Object();
        
        // Private attributes
        var that = this;
        var pageMode;
        var initialized = false;
        var rowDataId = "";
        // Initialization function for callbacks
        function init() {
            // Initialize render function of select column
            $.grep(that.dataTableInit.aoColumns, function(column) {
                return column.sName == that.selectColumnName;
            })[0].mRender = render;
            
            // Set up pre-draw callback
            var previousPreDrawCallback = that.dataTableInit.fnPreDrawCallback;
            that.dataTableInit.fnPreDrawCallback = function(settings) {
                preDrawCallback(settings, previousPreDrawCallback);
            };

            // Set up header callback
            var previousHeaderCallback = that.dataTableInit.fnHeaderCallback;
            that.dataTableInit.fnHeaderCallback = function (head, data, start, end, display) {
                headerCallback(head, data, start, end, display, previousHeaderCallback);
            };
        }
        
        
        // Initialisation function
        function preDrawCallback(settings, previousPreDrawCallback) {
            if (previousPreDrawCallback) {
                previousPreDrawCallback(settings);
            }

            settings.selectedIds = that.selectState;

            if (!initialized) {
                // save datatable instance
                that.table = settings.oInstance;

                // setup page selection mode
                pageMode = settings.oInit.bServerSide ? "all" : "current";

                // initialize the selection
                if (!that.selectState) {
                    that.selectState = new Object();
                }

                // Bind select all checkbox
                if (that.selectAllName) {
                    $("input[name=" + that.selectAllName + "]").click(function (e) {
                        updateSelectedRows(this.checked, ":enabled");
                        updateHeader(); // update the second checkbox for accessibility
                        e.stopPropagation();
                    });
                }

                // Register select row checkbox click event
                $("#" + that.element[0].id).on('click', "." + that.selectClass, function (e) {
                    updateSelectedRow($(this), this.checked);
                    if (that.selectAllName) {
                        updateHeader();
                    }
                    if (e.target && e.target.id) {
                        if (that.selectCheckBoxClick) {
                            //var clickFunction = new Function(that.selectCheckBoxClick.replace('chkBoxId', rowDataId));
                            // set the rowid to the calling function
                            var clickFunction = new Function(that.selectCheckBoxClick + "('" + e.target.id + "','" + rowDataId + "')");
                            clickFunction();
                        }
                        // set focus on clicked checkbox. Focus is mandatory for IE9 else the focus can be lost in a parent IFRAME
                        $('input[name="' + e.target.name + '"]').focus();
                    }
                    e.stopPropagation();
                });

                initialized = true;
            }
            
        }
        
        function headerCallback(head, data, start, end, display, previousHeaderCallback) {
            if (previousHeaderCallback) {
                previousHeaderCallback(head, data, start, end, display);
            }
            
            if (that.selectAllName) {
                updateHeader();
            }
        }
        
        function isAllSelected() {
            var checkBoxes = that.table.$(":input." + that.selectClass + ":enabled", { "page" : pageMode });
            var selectedCheckBoxes = checkBoxes.filter(function () {
                return $(this).prop("checked");
            });
            return (selectedCheckBoxes.length > 0) && (selectedCheckBoxes.length == checkBoxes.length);
        };
        
        function getRowId(elem) {
            var closestTr = elem.closest("tr");
            var aData = that.table.fnGetData(closestTr[0]);
            return aData[that.rowIdColumnName];
        }

        function updateHeader() {
          
            var selectHeader = $("input[name=" + that.selectAllName + "]", that.table.fnSettings().nTableWrapper);
            if (selectHeader.length > 0) {
                selectHeader.prop('checked', isAllSelected(that.table));
                selectHeader.prop('disabled', that.table.$(":input." + that.selectClass + ":enabled", { "page": pageMode }).length == 0);

                // Do not propagate the event to the header in order to fix column move while clicking with ColReorderWithResize plugin
                selectHeader.mousedown(function (evt) {
                    return false;
                });
            }
        }
        
        function updateSelectedRows(checked, filter, checkboxes) {
            if (!checkboxes) {
                filter = typeof filter !== 'undefined' ? filter : "";
                checkboxes = that.table.$('.' + that.selectClass + filter, { "page" : pageMode });
            }
            $.each(checkboxes, function () {
                $(this).prop("checked", checked);
                
            });
            $.each(checkboxes, function () {
                updateSelectedRow($(this), checked, true);
                if (that.selectCheckBoxClick) {
                    //var clickFunction = new Function(that.selectCheckBoxClick.replace('chkBoxId', rowDataId));
                    // set the rowid to the calling function
                    var clickFunction = new Function(that.selectCheckBoxClick + "('" + $(this)[0].id + "','" + rowDataId + "')");
                    clickFunction();
                }
            });
            $(that).trigger('selectionall', [checked]);
        }
        
        function updateSelectedRow(input, isSelected, ignoreEvent) {
            var rowId = getRowId(input);
            rowDataId = rowId;
            if (that.selectState[rowId] != isSelected) {
                that.selectState[rowId] = isSelected;
                var cell = input.closest("td");
                var data = that.table.DataTable().cell(cell).data() || {};
                data.Checked = isSelected;
                that.table.DataTable().cell(cell).data(data);
                if (!ignoreEvent) {
                    $(that).trigger('selection', [rowId, isSelected]);
                }
            }
        }
        
        function render(cellData, type, rowData) {
            var hideAccess;
          
            var checkbox = $('<input>').attr({
                type: 'checkbox',
                id: that.element[0].id + '_select_' + rowData[that.rowIdColumnName],
                name: that.element[0].id + '_select_' + rowData[that.rowIdColumnName],
            }).addClass(that.selectClass);

            if (cellData) {
                var data = (typeof cellData === 'object') ? cellData : JSON.parse(cellData);
                if (data) {
                    if (data.Checked) {
                        checkbox.attr('checked', 'checked');
                    }
                    if (data.Disabled) {
                        checkbox.attr('disabled', 'disabled');
                    }
                    if (data.Hidden) {
                        checkbox.css('display', 'none');
                    }
                    if (data.Tooltip) {
                        // If no hide access is specified then use the tooltip
                        if (!data.HideAccess) {
                            data.HideAccess = data.Tooltip;
                        }
                        checkbox.attr('title', data.Tooltip);
                    }
                    if (data.HideAccess) {
                        if (data.Hidden) {
                            hideAccess = $('<label>').attr({
                                "for": that.element[0].id + '_image_' + rowData[that.rowIdColumnName],
                                "class": "hide-access",
                            }).text(data.HideAccess);
                            
                        } else {
                            hideAccess = $('<label>').attr({
                                "for": that.element[0].id + '_select_' + rowData[that.rowIdColumnName],
                                "class": "hide-access",
                            }).text(data.HideAccess);
                            
                        }
                        
                    }

                    if (data.ImageUrl && data.Hidden) {
                        var image = $('<img>');
                        image.attr('id', that.element[0].id + '_image_' + rowData[that.rowIdColumnName]);
                        image.attr('name', that.element[0].id + '_image_' + rowData[that.rowIdColumnName]);
                        image.attr('src', data.ImageUrl);
                        image.attr('alt', data.Tooltip);
                        image.attr('title', data.Tooltip);

                        if (data.Height) {
                            image.attr('height', data.Height);
                        }
                       
                        if (data.Width) {
                            image.attr('width', data.Width);
                        }
                        return hideAccess ? $.fn.outerHtml(hideAccess) + $.fn.outerHtml(image) : $.fn.outerHtml(image);
                    }

                }
            }
            
            // User selection always override cellData
            if (that.selectState && that.selectState.hasOwnProperty(rowData[that.rowIdColumnName])) {
                if (that.selectState[rowData[that.rowIdColumnName]]) {
                    checkbox.attr('checked', that.selectState[rowData[that.rowIdColumnName]]);
                } else {
                    checkbox.removeAttr('checked');
                }
            }
            return hideAccess ? $.fn.outerHtml(hideAccess) + $.fn.outerHtml(checkbox) : $.fn.outerHtml(checkbox);
        }
        
        // Priviledged functions
        
        // Return the user selection on all page of the list (only delta)
        this.getUserSelection = function () {
            var selectedRows = new Array();
            for (id in that.selectState) {
                if (that.selectState[id]) {
                    selectedRows[selectedRows.length++] = id;
                }
            }
            return selectedRows;
        };
        
        // Return the selection on the current page of the list (checkboxes status)
        this.getCurrentPageSelection = function () {
            return table.$('.' + that.selectClass + ":checked", { "page": pageMode }).map(function () {
                return that.getRowId($(this));
            }).get();
        };
        
        // Constructor code
        $.extend(this, options);
        init();
    };
    
    // JQuery extention function
    $.fn.dataTableSelection = function (options) {
        var name = "dataTableSelection";
        return $s.create(
        this,
        name,
        function (element, opt) {
            return new $s.DataTableSelection(element, opt);
        },
        options).data(name);
    };

}(jQuery));
