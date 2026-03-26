(function ($) {

    // Alias
    var $s = $.sav2000;

    // Constructor
    // Options :
    // contextClientId
    // 
    $s.DataTable = function (element, options) {

        // Public attributes
        
        this.element = element;
        
        // Private attributes
        
        var table = null;
        var contextField = null;
        var that = this;
        var divWrapper = null;
        var head = null;
        var divScroll = null;
        var divScrollHead = null;
        var divScrollBody = null;
        var processingModal = null;
        var scrollXEnabled = false;
        var dataTableSelection = null;
        var dataTableModifyDelete = null;
        var initialized = false;
        
        // Private methods
        
        // Initialisation method
        function init() {
            
            // Set up specific data retrival
            that.dataTableInit.fnServerData = serverData;

            // Set up columns callbacks
            initColumns();

            // Set up server params function
            var userServerParams = that.dataTableInit.fnServerParams;
            that.dataTableInit.fnServerParams = function (data) {
                serverParams(data, userServerParams);
            };

            // Set up pre draw callback
            var userPreDrawCallback = that.dataTableInit.fnPreDrawCallback;
            that.dataTableInit.fnPreDrawCallback = function(settings) {
                preDrawCallback(settings, userPreDrawCallback);
            };
            
            // Set up draw callback
            var userDrawCallback = that.dataTableInit.fnDrawCallback;
            that.dataTableInit.fnDrawCallback = function (settings) {
                drawCallback(settings, userDrawCallback);
            };

            // set imageToolTip for datatable error message (blerouic)            
            $('#' + element[0].id + '_ErrorIdInfoIcon')
                .imageToolTip({
                    "onClick": null,
                    "ToolTipId": element[0].id + "_ErrorIdInfoIcon",
                    "Mode": "Click"
                });
            

            // If a sortCallback has been specified set up pre sort redraw callback
            if (that.sortCallback) {
                that.dataTableInit.fnPreSortRedrawCallback = function (settings) {
                    table._fnProcessingDisplay(settings, true);
                    blockUI(settings, true);
                    settings._iDisplayStart = 0;
                    updateContext(settings);
                    return that.sortCallback(settings.aoColumns[settings.aaSorting[0][0]].sName, settings.aaSorting[0][1], settings);
                };
            }

            // Initialize selection plugin
            if (that.selection) {
                dataTableSelection = $(element).dataTableSelection({
                    "dataTableInit": that.dataTableInit,
                    "rowIdColumnName": that.selection.rowIdColumnName,
                    "selectColumnName": that.selection.selectColumnName,
                    "selectAllName": that.selection.selectAllName,
                    "selectClass": that.selection.selectClass,
                    "userSelectClass": that.selection.userSelectClass,
                    "selectState": that.selection.selectState,
                    "selectCheckBoxClick":that.selection.selectCheckBoxClick,
                    "searching": false
            });
            }

            if (that.remove || that.modify) {
                if (that.remove == null) {
                    dataTableModifyDelete = $(element).dataTableModifyDelete({
                        "dataTableInit": that.dataTableInit,
                        "modifyColumnName": that.modify.modifyColumnName,
                        "modify": that.modify,
                        "onmodify": that.modify.onmodifyClick,
                        "rowIdColumnName": that.modify.rowIdColumnName,
                        "modifytitle": that.modify.modifyTitle,
                        "searching": false
                    });
                } else if (that.modify == null) {
                    dataTableModifyDelete = $(element).dataTableModifyDelete({
                        "dataTableInit": that.dataTableInit,
                        "rowIdColumnName": that.remove.rowIdColumnName,
                        "deleteColumnName": that.remove.deleteColumnName,
                        "remove": that.remove,
                        "ondelete": that.remove.ondeleteClick,
                        "deletetitle": that.remove.deleteTitle,
                        "searching": false
                    });
                } else {
                    dataTableModifyDelete = $(element).dataTableModifyDelete({
                        "dataTableInit": that.dataTableInit,
                        "modifyColumnName": that.modify.modifyColumnName,
                        "modify": that.modify,
                        "onmodify": that.modify.onmodifyClick,
                        "rowIdColumnName": that.remove.rowIdColumnName,
                        "deleteColumnName": that.remove.deleteColumnName,
                        "remove": that.remove,
                        "ondelete": that.remove.ondeleteClick,
                        "searching": false
                    });
                }
            }

            // Set moment format for sorting of date
            $.fn.dataTable.momentSort(that.momentFormat);

            // When a column is moved execute again the javascript returned by render methods
            element.on('column-reorder', function(e, settings, reorderInfo) {
                executeJavascript();
            });
            
            if (that.dataTableInit.bServerSide) {
                element.on('processing.dt', function (e, settings, show) {
                    blockUI(settings, show);
                }).on('xhr.dt', function(e, settings, json) {
                    // Update the last refresh date
                    that.updateTimeUtc = json.sUpdateTimeUtc;
                    that.updateTimeLocal = json.sUpdateTimeLocal;

                    // set if serevr provide all data or not
                    that.isPartialData = json.bIsPartialData;
                    if (that.isPartialData) {
                        // blerouic: hide 'on total of' message by changing totalDisplayRecords
                        json.iTotalRecords = json.iTotalDisplayRecords;
                    }

                    // Restore the page that the controller return (to handle case where an out of range request is done)
                    if ((json.iDisplayStart != undefined) && json.iDisplayStart >= 0) {
                        settings._iDisplayStart = json.iDisplayStart;

                    }
                });
            }
    
            // Initialize jquery dataTable plugin
            table = element.dataTable(that.dataTableInit);
        }

        function blockUI(settings, show) {
            // Create modal processing span when we use server-side mode
            if (!processingModal) {
                processingModal = $("<span id='" + element[0].id + "_processing_modal' class='" + settings.oClasses.sProcessing + "_modal' />").appendTo(settings.nTableWrapper);
            }
            if (show) {
                settings.bSort = false;
                processingModal[0].style.visibility = "visible";
            } else {
                processingModal[0].style.visibility = "hidden";
                settings.bSort = true;
            }
        }
        
        // Specific data retrival function
        function serverData(url, data, callback, settings) {
            var errorsToRetry = [12030, 12031, 12152, 12007, 12029, 12002];
            // a setTimeout 0 is used to trigger the AJAX call as it resolve some issue with IE8
            setTimeout(function () {
                settings.jqXHR = $.ajax({
                    "url": url,
                    "data": data,
                    "dataType": "json",
                    "cache": false,
                    "type": settings.sServerMethod,
                    "success": function (json) {
                        $(settings.oInstance).trigger('xhr', [settings, json]);
                        if (json.iListError != undefined) {
                            settings._iListError = json.iListError;

                        }
                        callback(json);
                    },
                    "error": handleAjaxError,
                    "shouldRetry": function (jqXhr, retryCount, requestMethod) {
                        // For errorsToRetry retries this request 5 times, with a delay of 250ms between retries
                        return ($.inArray(jqXhr.status, errorsToRetry) > -1) && (retryCount < 5) && $.Deferred(function (dfr) {
                            setTimeout(function () {
                                dfr.resolve(true);
                            }, 250);
                        }).promise();
                    }
                });
            }, 0);
        }

        // Set up custom init complete
        function initDraw(settings) {
            divWrapper = $("#" + element[0].id + "_wrapper");

            // Update context on selection change
            if (that.selection) {
                $(dataTableSelection).on("selection selectionall", function () {
                    updateContext(settings);
                });
            }
            
            // Cache some selectors, set up scrolling
            scrollXEnabled = (settings.oInit.sScrollX == undefined) || (settings.oInit.sScrollX === "") ? false : true;
            if (scrollXEnabled) {
                head = $("table:first", divWrapper);
                divScroll = $(".dataTables_scroll", divWrapper);
                divScrollHead = $(".dataTables_scrollHead", divScroll);
                divScrollBody = $(".dataTables_scrollBody", divScroll);
                divScrollBody.attr("tabindex", "-1");
            } else {
                head = $("thead", element);
            }

            // Activate the column filter plugin
            if (settings.oInit.bFilter) {
                settings.oInstance.columnFilter({
                    sPlaceHolder: "head:after",
                    bEnterKeyFilter: true,
                    aoColumns: that.filterColumns,
                    bUseColVis: true,
                    sSearchMode: "startWith",
                    bStopEnterPropagation: true
                });
                
                var filterThs = $('tr.filter-search td', $(head));
                $.each(filterThs, function (index, value) {
                    var input = $("input", this);
                    if (input.length) {
                        // add filter tooltip
                        if (that.tooltipsFilter[index]) {
                            input.attr("title", that.tooltipsFilter[index]);
                        }
                        // add hidden label for accessibility
                        var hideaccess = that.language.filter + ' ' + $(settings.aoColumns[index].nTh).text();
                        input.before('<span class="hide-access">' + hideaccess + '</span>');
                    }
                });
            }
            
            // Set maximum number of pages
            if (that.paginationNumberOfPages) {
                $.fn.dataTableExt.oPagination.iFullNumbersShowPages = that.paginationNumberOfPages;
            }

            // Specific label for pagination
            $.fn.dataTableExt.oPagination.page = that.language.page;

            // Append context field to wrapper
            var contextId = element[0].id + "_Context";
            contextField = $('<input>').attr({
                type: 'hidden',
                id: contextId,
                name: element[0].getAttribute("name") + ".Context"
            });
            contextField.appendTo(settings.nTableWrapper);

            // Hide header if needed
            if (!that.showHeader) {
                head.css("display", "none");
            }
            
            // Enable autorefresh if needed
            if (settings.oFeatures.bServerSide && that.refreshTimeInverval > 0) {
                setInterval(function () { settings.oInstance.fnStandingRedraw(); }, that.refreshTimeInverval);
            }

            // manage the selection color of a 'tr' (put the class lineSelect on the selected row)
            $('#' + element[0].id + ' tbody').click(function (event) {
                // Exception for the tr with the class 'disableLine'
                if ($(event.target.parentNode).closest('tr').hasClass('disabledLine')) {
                    return false;
                }
                var trRowSelectedCust = $(this).children("tr.selectedLine");
                if (trRowSelectedCust && trRowSelectedCust.length > 0) {
                    $(trRowSelectedCust).removeClass('selectedLine');
                }
                $(event.target.parentNode).closest('tr').addClass('selectedLine');
               return true;
            });

            initialized = true;
        }

        // Set up custom server paramaters callbacks
        function serverParams(data, userCallback) {
            $(that.criteria).each(function () {
                if (this.value) {
                    data.push({ "name": this.propertyName, "value": this.value });
                } else if (this.clientId) {
                    var value = evaluateCriteria(this);
                    data.push({ "name": this.propertyName, "value": value });
                }
            });
            
            if (userCallback) {
                userCallback(data);
            }
        }
        
        // Setup custom pre draw callbacks
        function preDrawCallback(settings, userPreDrawCallback) {

            // Destroy all tooltip when a redraw is done
            $('[data-qtip]', element).qtip('destroy', true);

            if (userPreDrawCallback) {
                userPreDrawCallback(settings);
            }

        }

        // Set up custom draw callbacks
        function drawCallback(settings, userDrawCallback) {

            if ((!settings.oFeatures.bServerSide && !initialized) || (settings.oFeatures.bServerSide && !initialized)) {
                // First draw launch initialization
                initDraw(settings);
            }

            // Setup last refresh date
            if (that.displayRefreshTime) {
                $("#" + element[0].id + "_time", divWrapper).remove();

                var sDateTime = that.refreshTimeLabelFormat
                    .replace("{Local}", now(false))
                    .replace("{UTC}", now(true));

                $("<span id='" + element[0].id + "_time' class='" + that.cssClassRefreshTime + "'>" + sDateTime + "</span>").prependTo($(".clear", divWrapper));
            }

            // Setup record limit message placeholder
            var sRecordLimitId = element[0].id + "_recordLimit";
            //<AR id='2015.012287-A.01' author='NidSharma' date='22-12-2015'>
            if (that.isPartialData || ((that.totalRecordLimit > 0) && (settings.fnRecordsTotal() > that.totalRecordLimit) && settings._iRecordsDisplay > that.totalRecordLimit)) {
                $("#" + element[0].id + "_ErrorId").show();
            } else {
                $("#" + element[0].id + "_ErrorId").hide();
                $("#" + sRecordLimitId, divWrapper).remove();
            }
            //</AR id='2015.012287-A.01' author='NidSharma'>
            // Setup record number information
            if (!that.displayRecordInfo) {
                $("#" + element[0].id + "_info", divWrapper).remove();
            }
            
            // Update context
            updateContext(settings);

            // Remove sort icon when no result
            if (settings.oFeatures.bSort) {
                if (settings.fnRecordsDisplay() == 0) {
                    var ths = $("th", head);
                    ths.removeClass(settings.oClasses.sSortable);
                    ths.removeClass(settings.oClasses.sSortAsc);
                    ths.removeClass(settings.oClasses.sSortDesc);
                    ths.addClass(settings.oClasses.sSortableNone);
                    ths.unbind('click.DT');
                } else {
                    for (var i = 0; i < settings.aoColumns.length; i++) {
                        if (settings.aoColumns[i].bSortable !== false) {
                            $(settings.aoColumns[i].nTh).unbind('click.DT');
                            $(settings.aoColumns[i].nTh).unbind('keypress.DT');
                            $(settings.aoColumns[i].nTh).unbind('selectstart.DT');
                            $(settings.aoColumns[i].nTh).removeClass(settings.oClasses.sSortableNone);
                            settings.oInstance.fnSortListener(settings.aoColumns[i].nTh, i);
                        } else {
                            $(settings.aoColumns[i].nTh).addClass(settings.oClasses.sSortableNone);
                        }
                    }
                }

                // to provide tooltip on sorting icon
                var theads = $("th", head);
                $.each(theads, function (i, val) {
                    if ($(val).hasClass(settings.oClasses.sSortAsc) || $(val).hasClass(settings.oClasses.sSortable)) {
                        $(val).attr("title", that.descToolTip);
                    } else if ($(val).hasClass(settings.oClasses.sSortDesc)) {
                        $(val).attr("title", that.ascToolTip);
                    }
                });
            }

            // Adds the jquery numeric filter on the datatable input field (only positive integer is accepted)
            var input = $("#" + settings.sTableId + "_nInput");
            if (input.length > 0) {
                input.numeric({ decimal: false, negative: false });
            }

            // Disable tabulation on scroll body th elements
            if (scrollXEnabled) {
                $("th", divScrollBody).attr("tabindex", "-1");
            }

            // Adjust columns sizing
            if (settings.oFeatures.bServerSide) {
                settings.oInstance.fnAdjustColumnSizing(false);
            }
            
            // Set aria-hidden to true to hide the scrollhead header from AT
            var theadDivScrollHead = $("table", divScrollHead);
            theadDivScrollHead.attr("aria-hidden", true);
            theadDivScrollHead.attr("role", "presentation");
            
            // Add qtip to the cells which need it
            $('[data-qtip]', element).each(function () {
                $(this).qtip({
                    content: this.getAttribute('data-qtip'),
                    show: {
                        event: 'click',
                        delay: 0
                    },
                    hide: {
                        fixed: true,
                        event: 'unfocus'
                    },
                    position: {
                        my: 'center',
                        at: 'center',
                        target: $(this)
                    },
                    style: {
                        classes: 'qtip-light qtip-shadow'
                    }
                });
            });

            if (userDrawCallback) {
                userDrawCallback(settings);
            }
        }

        // Execute javascript contained in the table cells
        function executeJavascript() {
            var scripts = $.map($("td script[type*=javascript]", element), function (val, i) {
                return val.innerHTML;
            }).join("");
            $.globalEval(scripts);
        }
        
        // Criteria evaluation function
        function evaluateCriteria(criteria) {
            // Type of evaluation
            var evalType = {
                Value: "Value",
                Checked: "Checked"
            };

            // Value evaluation function
            function value() {
                return $("#" + criteria.clientId).val();
            };

            // Checkd evalution function
            function checked() {
                return $("#" + criteria.clientId).is(":checked");
            };
            
            // Evaluate the criteria
            switch (criteria.evalType) {
                case evalType.Value:
                    return value();
                case evalType.Checked:
                    return checked();
                default:
                    return value();
            }
        };
        
        // Set up columns callbacks
        function initColumns() {
            var columns = that.dataTableInit.aoColumns;;
            for (var i = 0; i < columns.length; ++i) {
                var column = columns[i];

                // If no render function is specified, encode the HTML if needed
                if (!column.mRender && that.htmlEncode[i]) {
                    column.mRender = function (cellData, type, rowData) {
                        
                        return $.fn.htmlEscape(cellData);
                    };
                }
                
                // Set up text max length if needed
                var columnName = column.sName;
                if (that.textMaxLengths[columnName]) {
                    
                    var render = column.mRender;
                    column.mRender = (function(textMaxLength) {
                        return function(data, type, row, meta) {
                            if (render) {
                                render(data, type, row, meta);                                
                            }
                            if (type === "display" && data.length > textMaxLength) {
                                return data.substring(0, textMaxLength) + " ...";
                            } else {
                                return data;
                            }
                        };
                    })(that.textMaxLengths[columnName]);
                  
                    var createdCell = column.fnCreatedCell;
                    column.fnCreatedCell = (function(textMaxLength) {
                        return function(cell, cellData, rowData, rowIndex, colIndex) {
                            if (createdCell) {
                                createdCell(cell, cellData, rowData, rowIndex, colIndex);
                            }
                           
                           
                            //AR id="2015.011026-A.01" Date="27-Aug-2015" Author="pAggarwal" Action="Modification" Release="Iteration2">
                            if (cellData.length > textMaxLength) {
                                cell.setAttribute("data-qtip", cellData.replace(/\n/g, "<br />"));
                            }
                        };
                    })(that.textMaxLengths[columnName]);
                }
            }
        }
        
        function updateContext(settings) {
           if (contextField.length > 0) {
                var context = new Object();
                context[that.constants.contextDisplayStart] = settings._iDisplayStart;
                context[that.constants.contextSorting] = sortingByName(settings);
                    if (that.selection) {
                   context[that.constants.contextSelectState] = dataTableSelection.selectState;
                }
                that.setContext(context);
            }
        }

        // Method which returns the current date and time with UTC or local format
        // By default if no server time has been returned by the JSON response then it use moment.js
        // to determine client machine date and time
        function now(bUtc) {
            if (bUtc) {
                if (that.updateTimeUtc) {
                    return that.updateTimeUtc;
                } else {
                    return moment().utc().format(that.momentFormat);
                }
            } else {
                if (that.updateTimeLocal) {
                    return that.updateTimeLocal;
                } else {
                    return moment().format(that.momentFormat);
                }
            }
        }
        
        function sortingByName(settings) {
            var sorting = new Array();
            for (var i in settings.aaSorting) {
                var sortInfo = new Array();
                sortInfo.push(settings.aoColumns[settings.aaSorting[i][0]].sName);
                sortInfo.push(settings.aaSorting[i][1]);
                sorting.push(sortInfo);
            }
            return sorting;
        }
        
        function handleAjaxError(xhr, textStatus, error) {
            // Only treat completed XHR
            if (xhr.readyState == 4) {
                var contentType = xhr.getResponseHeader("content-type") || "";
                if (contentType.indexOf('json') > -1) {
                    var response = $.parseJSON(xhr.responseText);

                    if (that.ajaxErrorCallback) {
                        that.ajaxErrorCallback(response.ExceptionType, response.Message, response.ExceptionMessage, response.StackTrace);
                    } else {
                        table.oApi._fnProcessingDisplay(table.fnSettings(), false);
                        if (that.errorPageUrl) {
                            fnSubmitErrorForm(response.ExceptionType, response.Message, response.ExceptionMessage, response.StackTrace, response.Source);
                        } else {
                            alert(response.ExceptionType + "\n" + response.Message + "\n" + response.ExceptionMessage + "\n" + response.StackTrace);
                        }
                    }
                } else {
                    // Content type is not json, the xhr content is sent instead of the stacktrace
                    that.ajaxErrorCallback(textStatus, xhr.status, error, JSON.stringify(xhr));
                }
            }
        }
        
        function fnSubmitErrorForm(exceptionType, message, exceptionMessage, stackTrace, source) {
            var form = $('<form action="' + that.errorPageUrl + '&clientSideError=1&QUIT=True" method="POST" style="display: none;">' +
                      '<input type="text" name="exceptionType" value="' + exceptionType + '" />' +
                      '<input type="text" name="message" value="' + message + '" />' +
                      '<input type="text" name="exceptionMessage" value="' + exceptionMessage + '" />' +
                      '<input type="text" name="stackTraceInfo" value="' + $.fn.htmlEscape(stackTrace) + '" />' +
                      '<input type="text" name="sourceInfo" value="' + source + '" />' +
                      '</form>');
            $('body').append(form);
            $(form).submit();
        }

        // Priviledged functions
        
        // Get the current context
        this.getContext = function () {
            if (contextField.length > 0) {
                return JSON.parse(contextField.val());
            } else return null;
        };

        // Set the context
        this.setContext = function (context) {
            if (contextField.length > 0) {
                contextField.val(JSON.stringify(context));
            }
        };

        // Reset the context
        this.resetContext = function () {
            contextField.val("");
        };
        
        // Method that refresh the table
        this.standingRedraw = function() {
            table.fnStandingRedraw();
        };
        
        // Method to get the content of a specific column
        this.getColumnContent = function (columnName) {
            var columns = table.fnSettings().aoColumns;
            var index = $.fn.indexOf(columns, function (elem) {
                return elem.sName == columnName;
            });
            return table.$("td:eq(" + index + ")");
        };

        // Method to adjust the size of the datatable and each column
        this.adjustColumnSizing = function () {
            if (table) {
                table.fnAdjustColumnSizing(false);
            }
        }
        
        // Constructor code
        
        $.extend(this, options);
        init();
    };

    // JQuery extention function
    // Extention name is dataTableCore and not dataTable in order to not conflict with jquery.dataTables.js
    $.fn.dataTableCore = function (options) {
        var name = "dataTable";
        return $s.create(
            this,
            name,
            function (element, opt) {
                return new $s.DataTable(element, opt);
            },
            options).data(name);
    };

}(jQuery));