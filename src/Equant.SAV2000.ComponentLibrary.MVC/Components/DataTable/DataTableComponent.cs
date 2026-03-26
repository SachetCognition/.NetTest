namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common;
    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;

    using Resources = Equant.SAV2000.ComponentLibrary.Common.Resources;

    public class DataTableComponent : ComponentBase
    {
        public const string DefaultRowIdColumnName = "RowIdColumn";
        public const string DefaultSelectColumnName = "SelectColumn";
        public const string DefaultDeleteColumnName = "DeleteColumn";
        public const string DefaultModifyColumnName = "ModifyColumn";
        public const string DefaultSelectClass = "SelectClassIdentifier";

        public string SelectAllName { get; private set; }

        public DataTableComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.Caption = string.Empty;
            this.Columns = new List<DataTableColumn>();
            this.CssClass = "display table dataTable";
            this.CssClassRefreshTime = "majdate";
            this.CssClassRecordsLimit = "alert error form datatableerror";
            this.Data = null;
            this.DomLayout = "<\"clear\"i>rt<lp>";
            this.IsHorizontalScrolling = true;
            this.InitialDisplayIndex = -1;
            this.InitialSorting = new List<KeyValuePair<string, SortDirection>>();
            this.IsVerticalScrolling = false;
            this.IsPaginate = true;
            this.IsReorder = true;
            this.IsSelect = false;
            this.IsDisplayRefreshTime = true;
            this.IsDisplayRecordInfo = true;
            this.IsDeleteNeeded = false;
            this.IsModifyNeeded = false;
            this.IsSelectAll = true;
            this.IsShowHeader = true;
            this.PageSize = 10;
            this.PaginationNumberOfPages = 17;
            this.RefreshTimeLabelFormat = Resources.ApplicationStrings.Datatable_sRefreshTime;
            this.RefreshTimeInverval = -1;
            this.RowIdColumnName = DefaultRowIdColumnName;
            this.SelectColumnName = DefaultSelectColumnName;
            this.DeleteColumnName = DefaultDeleteColumnName;
            this.ModifyColumnName = DefaultModifyColumnName;
            this.SelectClassIdentifier = DefaultSelectClass;
            this.DeleteToolTip = Resources.ApplicationStrings.LBL000029;
            this.ModifyToolTip = Resources.ApplicationStrings.LBL000030;
            this.SelectAllTooltip = Resources.ApplicationStrings.LBL000032;
            this.ServiceUri = string.Empty;
            this.TotalRecordsLimit = 0;
            this.RecordsLimitMessage = Resources.ApplicationStrings.RecordsLimitMessageShort;
            this.RecordsLimitMessageLong = Resources.ApplicationStrings.RecordsLimitMessageLong;
            this.RecordLimitMessageTitle = Resources.ApplicationStrings.ErrorTitle;
            this.AscTooltip = Resources.ApplicationStrings.TIP000021;
            this.DescTooltip = Resources.ApplicationStrings.TIP000025;
            this.Criteria = new List<CriteriaParameter>();
            this.SelectColumnIndex = 0;
            this.DeleteColumnIndex = 1;
            this.ModifyColumnnIndex = 2;
        }

        public DataTableComponent() : this(null) { }

        public string Caption { get; set; }
        public List<DataTableColumn> Columns { get; set; }
        public string ErrorPageUrl { get; set; }
        public string CssClass { get; set; }
        public string CssClassRecordsLimit { get; set; }
        public string CssClassRefreshTime { get; set; }
        public string CssClassSelect { get; set; }
        public string CssClassDelete { get; set; }
        public string CssClassModify { get; set; }
        public List<CriteriaParameter> Criteria { get; set; }
        public DataTable Data { get; set; }
        public string DomLayout { get; set; }
        public string AscTooltip { get; set; }
        public string DescTooltip { get; set; }
        public DataTableContext DataContext { get; set; }
        public int InitialDisplayIndex { get; set; }
        public List<KeyValuePair<string, SortDirection>> InitialSorting { get; set; }
        public bool IsFilter { get; set; }
        public string ModifyToolTip { get; set; }
        public string DeleteToolTip { get; set; }
        public bool IsHorizontalScrolling { get; set; }
        public bool IsPaginate { get; set; }
        public bool IsReorder { get; set; }
        public bool IsSelect { get; set; }
        public bool IsDisplayRefreshTime { get; set; }
        public bool IsDisplayRecordInfo { get; set; }
        public bool IsDeleteNeeded { get; set; }
        public bool IsModifyNeeded { get; set; }
        public int ModifyColumnnIndex { get; set; }
        public int DeleteColumnIndex { get; set; }
        public int SelectColumnIndex { get; set; }
        public bool IsSelectColumnSortable { get; set; }
        public string OnDeleteClick { get; set; }
        public string OnModifyClick { get; set; }
        public bool IsSelectAll { get; set; }
        public bool IsShowHeader { get; set; }
        public bool IsVerticalScrolling { get; set; }
        public string OnAjaxError { get; set; }
        public string OnDraw { get; set; }
        public string OnServerParams { get; set; }
        public string OnSort { get; set; }
        public int PageSize { get; set; }
        public int PaginationNumberOfPages { get; set; }
        public string RecordsLimitMessage { get; set; }
        public string RecordsLimitMessageLong { get; set; }
        public string RecordLimitMessageTitle { get; set; }
        public int RefreshTimeInverval { get; set; }
        public string RefreshTimeLabelFormat { get; set; }
        public string RowIdColumnName { get; set; }
        public string SelectColumnName { get; set; }
        public string DeleteColumnName { get; set; }
        public string ModifyColumnName { get; set; }
        public string SelectAllTooltip { get; set; }
        public string SelectAllText { get; set; }
        public string SelectClassIdentifier { get; set; }
        public string OnSelectColumnClick { get; set; }
        public bool IsMultiSortRequired { get; set; }
        public Dictionary<string, bool> InitialSelectState { get; set; }
        public string ServiceUri { get; set; }
        public int TotalRecordsLimit { get; set; }
        public bool IsEncryptionRequired { get; set; }
        public List<string> EncryptedParameters { get; set; }

        public override ReadOnlyCollection<CssResource> CssResources
        {
            get
            {
                return new ReadOnlyCollection<CssResource>(
                    new List<CssResource>
                    {
                        new CssResource(
                            "Cssjqueryqtip",
                            "Equant.SAV2000.ComponentLibrary.Common.Resources.Css.jquery.qtip.css",
                            200,
                            typeof(Locator))
                    });
            }
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                var jsResource = new List<JsResource>
                {
                    new JsResource(
                        "BundledDatatableScripts",
                        "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.BundledDatatableScripts.js",
                        200,
                        typeof(Locator)),
                    new JsResource(
                        "JsDataTable",
                        "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTable.js",
                        295,
                        typeof(DataTableComponent)),
                    new JsResource(
                        "JsErrorDisplay",
                        "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ErrorDisplay.js",
                        300,
                        typeof(DataTableComponent)),
                    new JsResource(
                        "JsClueTip",
                        "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.cluetip.custom.js",
                        310,
                        typeof(Locator)),
                    new JsResource(
                        "JsImageToolTip",
                        "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ImageToolTip.js",
                        320,
                        typeof(DataTableComponent))
                };
                if (this.IsFilter)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsjquerydataTablesdatatablescolumnFilter",
                            "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.dataTables.columnFilter.js",
                            240,
                            typeof(Locator)));
                }
                if (this.IsReorder)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsjquerydataTablescolMoveResize",
                            "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.dataTables.colMoveResize.js",
                            250,
                            typeof(Locator)));
                }
                if (this.IsSelect)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsDataTableSelection",
                            "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTableSelection.js",
                            285,
                            typeof(DataTableComponent)));
                }
                if (this.IsModifyNeeded || this.IsDeleteNeeded)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsDataTableModifyDelete",
                            "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTableModifyDelete.js",
                            290,
                            typeof(DataTableComponent)));
                }
                return new ReadOnlyCollection<JsResource>(jsResource);
            }
        }

        public static int CalculateDisplayStart(int displayStart, int totalRecords, int displayLength)
        {
            if (displayStart >= totalRecords)
            {
                return ((totalRecords - 1) / displayLength) * displayLength;
            }
            return displayStart;
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.ServiceUri) && (this.Data != null))
            {
                throw new NotSupportedException(
                    "ServiceUri (server-side processing) and Data (client-side processing) properties cannot be used at the same time");
            }

            if (this.IsSelect && !this.Columns.Any(x => x.PropertyName.Equals(this.RowIdColumnName)))
            {
                throw new NotSupportedException("When using select feature, a row identifier column should be specified");
            }

            var specialColumns = new Dictionary<int, Action>();

            if (this.IsDeleteNeeded)
            {
                specialColumns.Add(this.DeleteColumnIndex, this.DeleteColumnAction);
            }

            if (this.IsSelect)
            {
                specialColumns.Add(this.SelectColumnIndex, this.SelectColumnAction);
            }

            if (this.IsModifyNeeded)
            {
                specialColumns.Add(this.ModifyColumnnIndex, this.ModifyColumnAction);
            }

            foreach (var action in specialColumns.OrderBy(x => x.Key).Select(x => x.Value))
            {
                action();
            }

            new DataTableHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(TextWriter writer)
        {
            if (this.HtmlHelper != null)
            {
                var request = this.HtmlHelper.ViewContext.HttpContext.Request;
                var pathBase = request.PathBase.Value ?? string.Empty;
                if (string.IsNullOrEmpty(this.ErrorPageUrl))
                {
                    var numFen = request.Query.ContainsKey("NumFen") ? request.Query["NumFen"].ToString() : string.Empty;
                    var cookieName = request.Query.ContainsKey("COOKIENAME") ? request.Query["COOKIENAME"].ToString() : string.Empty;
                    this.ErrorPageUrl = string.Format(CultureInfo.InvariantCulture, "{0}/Home/SubErr?NUMFEN={1}&COOKIENAME={2}",
                        string.IsNullOrEmpty(pathBase) ? string.Empty : pathBase.TrimEnd('/'), numFen, cookieName);
                }
            }

            if (writer != null)
            {
                writer.WriteLine("$('#{0}').dataTableCore({1});", this.Id, new DataTableOptionsSerializer(this).Serialize());
            }
        }

        private void SelectColumnAction()
        {
            this.SelectAllName = string.Format(CultureInfo.InvariantCulture, "{0}_selectAll", this.Id);
            var selectColumn = new DataTableColumn
            {
                CssClass = this.CssClassSelect,
                PropertyName = this.SelectColumnName,
                IsSortable = this.IsSelectColumnSortable,
                IsEncodeHtml = false,
                IsSearchable = false,
                ColumnType = DataTableColumnType.Html,
                HeaderText = this.SelectAllText
            };
            if (this.IsSelect && this.IsSelectAll)
            {
                selectColumn.HeaderTemplate = new SelectAllDataTableHeaderTemplate { Id = this.SelectAllName, Name = this.SelectAllName, ToolTip = this.SelectAllTooltip, Title = this.SelectAllText };
            }
            this.Columns.RemoveAll(c => c.PropertyName.Equals(this.SelectColumnName));
            this.Columns.Insert(this.SelectColumnIndex, selectColumn);
        }

        private void DeleteColumnAction()
        {
            var deleteColumn = new DataTableColumn
            {
                PropertyName = this.DeleteColumnName,
                IsSortable = false,
                IsEncodeHtml = false,
                IsSearchable = false,
                CssClass = this.CssClassDelete,
                ColumnType = DataTableColumnType.Html,
                HeaderText = Resources.ApplicationStrings.LBL000017,
                ShowHeaderText = false
            };
            this.Columns.RemoveAll(c => c.PropertyName.Equals(this.DeleteColumnName));
            this.Columns.Insert(this.DeleteColumnIndex, deleteColumn);
        }

        private void ModifyColumnAction()
        {
            var modifyColumn = new DataTableColumn
            {
                PropertyName = this.ModifyColumnName,
                IsSortable = false,
                IsEncodeHtml = false,
                IsSearchable = false,
                CssClass = this.CssClassModify,
                ColumnType = DataTableColumnType.Html,
                ShowHeaderText = false,
                HeaderText = Resources.ApplicationStrings.LBL000016
            };
            this.Columns.RemoveAll(c => c.PropertyName.Equals(this.ModifyColumnName));
            this.Columns.Insert(this.ModifyColumnnIndex, modifyColumn);
        }
    }
}
