namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This builder class generate 
    /// </summary>
    public class DataTableOptionsSerializer
    {
        private readonly DataTableComponent dataTableComponent;

        private readonly List<DataTableColumnsOption> columns;

        public DataTableOptionsSerializer(DataTableComponent dataTableComponent)
        {
            this.dataTableComponent = dataTableComponent;
            this.columns = new List<DataTableColumnsOption>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "for datatable javascript component some value need to be lowercase")]
        public string Serialize()
        {
            // Generate an array telling if the columns content should be HTML encoded or not
            var columnsHtmlEncode = new List<bool>();

            // Generate an array of the filter tooltips
            var tooltipsFilter = new List<string>();

            // Column filters option
            var filterColumnOptions = new List<DataTableColumnFilterColumnOption>();

            // Text max lengths (column name, length)
            var textMaxLengths = new Dictionary<string, int>();

            foreach (var column in this.dataTableComponent.Columns)
            {
                string columnType;
                if (column.ColumnType == DataTableColumnType.Moment)
                {
                    columnType = "moment-" + Common.Resources.ApplicationStrings.MomentFormat;
                }
                else
                {
                    columnType = column.ColumnType.ToString().ToLower(CultureInfo.InvariantCulture);
                }

                this.columns.Add(
                    new DataTableColumnsOption
                    {
                        IsVisible = column.IsVisible,
                        Class = column.CssClass,
                        Name = column.PropertyName,
                        IsSortable = column.IsSortable,
                        Data =
                            string.IsNullOrEmpty(column.PropertyName)
                                ? null
                                : new JRaw(string.Format(CultureInfo.InvariantCulture, "\"{0}\"", column.PropertyName)),
                        Render = string.IsNullOrEmpty(column.Render) ? null : new JRaw(column.Render),
                        DefaultContent = column.DefaultValue,
                        ColumnType = columnType
                    });
                columnsHtmlEncode.Add(column.IsEncodeHtml);
                tooltipsFilter.Add(column.TooltipFilter);
                var columnFilterType = (column.IsSearchable ? DataTableColumnFilterColumnTypeOption.Text : DataTableColumnFilterColumnTypeOption.Null);
                filterColumnOptions.Add(new DataTableColumnFilterColumnOption { ColumnType = columnFilterType, MaxLength = column.FilterMaxLength });
                if (column.TextMaxLength > 0)
                {
                    textMaxLengths.Add(column.PropertyName, column.TextMaxLength);
                }
            }

            // Adapt criteria parameters list
            var criteriaParameterAdaptors = dataTableComponent.Criteria.Select(criteriaParameter => new CriteriaParameterAdaptor(criteriaParameter,
                dataTableComponent.IsEncryptionRequired, this.dataTableComponent.EncryptedParameters ?? new List<string>())).ToList();

            return JsonConvert.SerializeObject(
                    new
                    {
                        dataTableInit = this.SerializeDataTableInit(),
                        errorPageUrl = dataTableComponent.ErrorPageUrl,
                        showHeader = dataTableComponent.IsShowHeader,
                        ascToolTip = dataTableComponent.AscTooltip,
                        descToolTip = dataTableComponent.DescTooltip,
                        totalRecordLimit = dataTableComponent.TotalRecordsLimit,
                        cssClassRecordLimit = dataTableComponent.CssClassRecordsLimit,
                        recordLimitMessage = dataTableComponent.RecordsLimitMessage,
                        recordLimitMessageLong = dataTableComponent.RecordsLimitMessageLong,
                        recordLimitMessageTitle = dataTableComponent.RecordLimitMessageTitle,
                        cssClassRefreshTime = dataTableComponent.CssClassRefreshTime,
                        refreshTimeLabelFormat = dataTableComponent.RefreshTimeLabelFormat,
                        displayRefreshTime = dataTableComponent.IsDisplayRefreshTime,
                        displayRecordInfo = dataTableComponent.IsDisplayRecordInfo,
                        momentFormat = Common.Resources.ApplicationStrings.MomentFormat,
                        paginationNumberOfPages = dataTableComponent.PaginationNumberOfPages,
                        criteria = criteriaParameterAdaptors,
                        refreshTimeInverval = dataTableComponent.RefreshTimeInverval,
                        htmlEncode = columnsHtmlEncode,
                        ajaxErrorCallback = string.IsNullOrEmpty(dataTableComponent.OnAjaxError) ? null : new JRaw(dataTableComponent.OnAjaxError),
                        constants =
                            new
                            {
                                contextDisplayStart = DataTableContext.DisplayStartKey,
                                contextSorting = DataTableContext.SortingKey,
                                contextSelectState = DataTableContext.SelectStateKey
                            },
                        remove = dataTableComponent.IsDeleteNeeded ? new
                        {
                            rowIdColumnName = dataTableComponent.RowIdColumnName,
                            deleteColumnName = dataTableComponent.DeleteColumnName,
                            delete = dataTableComponent.IsDeleteNeeded,
							//<PERF desc='remove useless data in JSON' author='blerouic' date='17-Feb-2016'>
                            ondeleteClick = string.IsNullOrEmpty(dataTableComponent.OnDeleteClick) ? null : new JRaw(dataTableComponent.OnDeleteClick),
                            deleteTitle = dataTableComponent.DeleteToolTip
                        }
                            : null,
                        modify = dataTableComponent.IsModifyNeeded ? new
                        {
                            rowIdColumnName = dataTableComponent.RowIdColumnName,
                            modifyColumnName = dataTableComponent.ModifyColumnName,
                            modify = dataTableComponent.IsModifyNeeded,
                            //<PERF desc='remove useless data in JSON' author='blerouic' date='17-Feb-2016'>
                            onmodifyClick = string.IsNullOrEmpty(dataTableComponent.OnModifyClick) ? null : new JRaw( dataTableComponent.OnModifyClick),
                            modifyTitle = dataTableComponent.ModifyToolTip
                        } : null,
                        selection =
                            dataTableComponent.IsSelect ? new
                            {
                                rowIdColumnName = dataTableComponent.RowIdColumnName,
                                selectColumnName = dataTableComponent.SelectColumnName,
                                selectAllName = dataTableComponent.SelectAllName,
                                selectClass = dataTableComponent.SelectClassIdentifier,
                                selectState = dataTableComponent.InitialSelectState,
                                selectCheckBoxClick = dataTableComponent.OnSelectColumnClick,
                            }
                                : null,
                        sortCallback = string.IsNullOrEmpty(dataTableComponent.OnSort) ? null : new JRaw(dataTableComponent.OnSort),
                        filterColumns = filterColumnOptions,
                        tooltipsFilter,
                        language = new
                        {
                            filter = Common.Resources.ApplicationStrings.LBL000025,
                            page = Common.Resources.ApplicationStrings.LBL000026
                        },
                        textMaxLengths
                    },
                    //<PERF desc='remove useless data in JSON' author='blerouic' date='17-Feb-2016'>
                    new JsonSerializerSettings { 
                                NullValueHandling = NullValueHandling.Ignore
                            }
                    );
        }

        private DataTableOptions SerializeDataTableInit()
        {
            // Activate Reorder if needed
            var domLayoutPrefix = dataTableComponent.IsReorder && dataTableComponent.IsShowHeader ? "R" : string.Empty;

            // Compute initial sorting
            var initialSorting = this.InitialSortingToJson();

            return new DataTableOptions
            {
                IsMultiSelect = dataTableComponent.IsMultiSortRequired,
                IsServerSide = !string.IsNullOrEmpty(dataTableComponent.ServiceUri),
                AjaxSource = string.IsNullOrEmpty(dataTableComponent.ServiceUri) ? null : dataTableComponent.ServiceUri,
                DeferRender = true,
                Columns = columns,
                Data = dataTableComponent.Data,
                Dom = string.Format(CultureInfo.InvariantCulture, "{0}{1}", domLayoutPrefix, dataTableComponent.DomLayout),
                ServerMethod = "POST",
                DrawCallBack = string.IsNullOrEmpty(dataTableComponent.OnDraw) ? null : new JRaw(dataTableComponent.OnDraw),
                ServerParams =
                    string.IsNullOrEmpty(dataTableComponent.OnServerParams) ? null : new JRaw(dataTableComponent.OnServerParams),
                PaginationType = dataTableComponent.IsPaginate ? "SwanPagination" : null,
                DisplayLength = dataTableComponent.PageSize,
                IsFilter = dataTableComponent.IsFilter,
                IsPaginate = dataTableComponent.IsPaginate,
                DisplayStart = dataTableComponent.InitialDisplayIndex > 0 ? dataTableComponent.InitialDisplayIndex : 0,
                Sorting = string.IsNullOrEmpty(initialSorting) ? null : new JRaw(initialSorting),
                ScrollX = dataTableComponent.IsHorizontalScrolling ? "100%" : null,
                ScrollY = dataTableComponent.IsVerticalScrolling ? "100%" : null,
                ScrollCollapse = dataTableComponent.IsHorizontalScrolling,
                ColReorder = new DataTableColumnsReorderOption { AllowReorder = dataTableComponent.IsReorder, AllowResize = false },
                Language =
                    new DataTableLanguageOption
                    {
                        EmptyTable = Common.Resources.ApplicationStrings.Datatable_sEmptyTable,
                        Info = Common.Resources.ApplicationStrings.Datatable_sInfo,
                        InfoEmpty = Common.Resources.ApplicationStrings.Datatable_sInfoEmpty,
                        InfoThousands = Common.Resources.ApplicationStrings.Datatable_sInfoThousands,
                        LoadingRecords = Common.Resources.ApplicationStrings.Datatable_sLoadingRecords,
                        Paginate =
                            new DataTableLanguagePaginateOption
                            {
                                First =
                                    Common.Resources.ApplicationStrings
                                    .TIP000012,
                                Last =
                                    Common.Resources.ApplicationStrings
                                    .TIP000014,
                                Next =
                                    Common.Resources.ApplicationStrings
                                    .TIP000011,
                                Previous =
                                    Common.Resources.ApplicationStrings
                                    .TIP000013
                            },
                        Aria =
                            new DataTableLanguageAriaOption
                            {
                                SortAscending =
                                    string.Format(
                                        CultureInfo.InvariantCulture,
                                        ": {0}",
                                        Common.Resources.ApplicationStrings
                                    .LBL000027),
                                SortDescending =
                                    string.Format(
                                        CultureInfo.InvariantCulture,
                                        ": {0}",
                                        Common.Resources.ApplicationStrings
                                    .LBL000028)
                            },
                        Processing = Common.Resources.ApplicationStrings.Datatable_sProcessing,
                        ZeroRecords = Common.Resources.ApplicationStrings.Datatable_sZeroRecords,
                        InfoFiltered = Common.Resources.ApplicationStrings.Datatable_sInfoFiltered
                    },
                IsEncryptionRequired = dataTableComponent.IsEncryptionRequired,
                EncryptedParameters = this.dataTableComponent.EncryptedParameters ?? new List<string>()
            };
        }

        /// <summary>
        /// This method generate the correct JSON array which specifies the sort of the datatable
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "Lower is needed for javascript serialization")]
        private string InitialSortingToJson()
        {
            var initialSortingIndexes = dataTableComponent.InitialSorting.Select(x => new KeyValuePair<int, string>(dataTableComponent.Columns.FindIndex(y => y.PropertyName.Equals(x.Key)),
                x.Value.ToString().ToLower(CultureInfo.InvariantCulture))).Where(x => x.Key >= 0);
            var dynamicList = initialSortingIndexes.Select(x => new List<object> { x.Key, x.Value }).ToList();
            return JArray.FromObject(dynamicList).ToString(Formatting.None, new JsonConverter[0]);
        }

    }
}
