namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System;
    using System.Globalization;
    using System.Linq;
    using Equant.SAV2000.ComponentLibrary.Common;
    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

    using Resources = Equant.SAV2000.ComponentLibrary.Common.Resources;
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// The data table component.
    /// </summary>
    public class DataTableComponent : ComponentBase
    {
        /// <summary>
        /// The default row id column name.
        /// </summary>
        public const string DefaultRowIdColumnName = "RowIdColumn";

        /// <summary>
        /// The default select column name.
        /// </summary>
        public const string DefaultSelectColumnName = "SelectColumn";


        /// <summary>
        /// The default delete column name.
        /// </summary>
        public const string DefaultDeleteColumnName = "DeleteColumn";

        /// <summary>
        /// The default modify column name.
        /// </summary>
        public const string DefaultModifyColumnName = "ModifyColumn";

        /// <summary>
        /// The default select class.
        /// </summary>
        public const string DefaultSelectClass = "SelectClassIdentifier";

        /// <summary>
        /// The select all id.
        /// </summary>
        public string SelectAllName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
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

        /// <summary>
        /// Gets or sets the caption of the table
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "This property should be easily set from view")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification =
                "Criteria property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended"
            )]
        public List<DataTableColumn> Columns { get; set; }

        /// <summary>
        /// Gets or sets the error page url.
        /// </summary>
        public string ErrorPageUrl { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class used to display the element containing the record limit message.
        /// </summary>
        public string CssClassRecordsLimit { get; set; }

        /// <summary>
        /// Gets or sets the css class refresh time.
        /// </summary>
        public string CssClassRefreshTime { get; set; }

        /// <summary>
        /// Gets or sets the css class select.
        /// </summary>
        public string CssClassSelect { get; set; }

        /// <summary>
        /// Gets or sets the css class delete.
        /// </summary>
        public string CssClassDelete { get; set; }

        /// <summary>
        /// Gets or sets the css class modify
        /// </summary>
        public string CssClassModify { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "This property should be easily set from view")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification =
                "Criteria property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended"
            )]
        public List<CriteriaParameter> Criteria { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public DataTable Data { get; set; }

        /// <summary>
        /// Gets or sets the dom layout.
        /// </summary>
        public string DomLayout { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for sorting ascending
        /// </summary>
        public string AscTooltip { get; set; }

        /// <summary>
        /// Gets or sets the tooltip for sorting descending
        /// </summary>
        public string DescTooltip { get; set; }


        public DataTableContext DataContext { get; set; }

        /// <summary>
        /// Gets or sets the initial display index.
        /// </summary>
        public int InitialDisplayIndex { get; set; }

        /// <summary>
        /// Gets or sets the initial sorting.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "This property should be easily set from view")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification =
                "InitialSorting property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended"
            )]
        public List<KeyValuePair<string, SortDirection>> InitialSorting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating wheter to activate filters or not
        /// </summary>
        public bool IsFilter { get; set; }

        /// <summary>
        /// Gets or sets the title of Modify Button
        /// </summary>
        public string ModifyToolTip { get; set; }

        /// <summary>
        /// Gets or sets the title of Delete Button
        /// </summary>
        public string DeleteToolTip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is horizontal scrolling.
        /// </summary>
        public bool IsHorizontalScrolling { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is paging.
        /// </summary>
        public bool IsPaginate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is isReorder.
        /// </summary>
        public bool IsReorder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is select column.
        /// </summary>
        public bool IsSelect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is refresh time.
        /// </summary>
        public bool IsDisplayRefreshTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if record number is displayed.
        /// </summary>
        public bool IsDisplayRecordInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleteIcon is needed.
        /// </summary>
        public bool IsDeleteNeeded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether modifyicon is needed.
        /// </summary>
        public bool IsModifyNeeded { get; set; }

        /// <summary>
        /// Gets or sets the Modify Column Index
        /// </summary>
        public int ModifyColumnnIndex { get; set; }

        /// <summary>
        /// Gets or sets the Delete Column Index
        /// </summary>
        public int DeleteColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets the Select Column Index
        /// </summary>
        public int SelectColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Select Column is sortable
        /// </summary>
        public bool IsSelectColumnSortable { get; set; }

        /// <summary>
        /// Gets or Sets the OnDeleteClick event
        /// </summary>
        public string OnDeleteClick { get; set; }

        /// <summary>
        /// Gets or Sets the onModifyClick event
        /// </summary>
        public string OnModifyClick { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is select all.
        /// </summary>
        public bool IsSelectAll { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether is show header.
        /// </summary>
        public bool IsShowHeader { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is vertical scrolling.
        /// </summary>
        public bool IsVerticalScrolling { get; set; }

        /// <summary>
        /// Gets or sets the on ajax error.
        /// </summary>
        public string OnAjaxError { get; set; }

        /// <summary>
        /// Gets or sets the callback function that will be executed after each draw of the DataTable
        /// </summary>
        public string OnDraw { get; set; }

        /// <summary>
        /// Gets or sets the callback function that will be executed just before calling the controller
        /// </summary>
        public string OnServerParams { get; set; }

        /// <summary>
        /// Gets or sets the on sort.
        /// </summary>
        public string OnSort { get; set; }

        /// <summary>
        /// Gets or sets the number of row to display per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the pagination number of pages.
        /// </summary>
        public int PaginationNumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the message to display in case the records limit is reached.
        /// </summary>
        public string RecordsLimitMessage { get; set; }

        /// <summary>
        /// Gets or sets the records limit message long.
        /// </summary>
        public string RecordsLimitMessageLong { get; set; }

        /// <summary>
        /// Gets or sets the record limit message title.
        /// </summary>
        public string RecordLimitMessageTitle { get; set; }

        /// <summary>
        /// Gets or sets the refresh time inverval in ms
        /// </summary>
        public int RefreshTimeInverval { get; set; }

        /// <summary>
        /// Gets or sets the refresh time label format.
        /// </summary>
        public string RefreshTimeLabelFormat { get; set; }

        /// <summary>
        /// Gets or sets the row id column name.
        /// </summary>
        public string RowIdColumnName { get; set; }

        /// <summary>
        /// Gets or sets the select column id.
        /// </summary>
        public string SelectColumnName { get; set; }

        /// <summary>
        /// Gets or sets the Delete column id
        /// </summary>
        public string DeleteColumnName { get; set; }

        /// <summary>
        /// Gets or sets the Modify column id
        /// </summary>
        public string ModifyColumnName { get; set; }

        /// <summary>
        /// Gets or sets the select all tooltip.
        /// </summary>
        public string SelectAllTooltip { get; set; }

        /// <summary>
        /// Gets or sets Select all column text
        /// </summary>
        public string SelectAllText { get; set; }

        /// <summary>
        /// Gets or sets the select class.
        /// </summary>
        public string SelectClassIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the select Column click function name.
        /// </summary>
        public string OnSelectColumnClick { get; set; }

        /// <summary>
        /// Property to check if multi-sorting is required in list or not
        /// </summary>
        public bool IsMultiSortRequired { get; set; }

        /// <summary>
        /// Gets or sets the initial select state.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "This property should be easily set from view")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification =
                "This property just use the dictionnary as a simple container, it does not require any features that a collection will provide and is not intended to be extended"
            )]
        public Dictionary<string, bool> InitialSelectState { get; set; }

        /// <summary>
        /// Gets or sets the service uri.
        /// </summary>
        public string ServiceUri { get; set; }

        /// <summary>
        /// Gets or sets the limit of total records retrieve upon which a message will be displayed
        /// </summary>
        public int TotalRecordsLimit { get; set; }

        #region Encryption

        /// <summary>
        /// Gets or sets a value indicating whether is encryption required.
        /// </summary>
        public bool IsEncryptionRequired { get; set; }

        /// <summary>
        /// Gets or sets the encrypted parameters.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "This property is part of a POCO which must be easily serializable"),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "This property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
        public List<string> EncryptedParameters { get; set; }
        #endregion

        public override ReadOnlyCollection<CssResource> CssResources
        {
            get
            {
                return
                    new ReadOnlyCollection<CssResource>(
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

        /// <summary>
        /// Gets the js resources.
        /// </summary>
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
                                         typeof(ErrorComponents)),
                                     new JsResource(
                                         "JsClueTip",
                                         "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.cluetip.custom.js",
                                         310,
                                         typeof(Locator)),
                                     new JsResource(
                                         "JsImageToolTip",
                                         "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ImageToolTip.js",
                                         320,
                                         typeof(ImageToolTipComponent))
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

        /// <summary>
        /// The calculate display start.
        /// </summary>
        /// <param name="displayStart"></param>
        /// <param name="totalRecords">
        /// The total records.
        /// </param>
        /// <param name="displayLength">
        /// The display length.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int CalculateDisplayStart(int displayStart, int totalRecords, int displayLength)
        {
            if (displayStart >= totalRecords)
            {
                return ((totalRecords - 1) / displayLength) * displayLength;
            }

            return displayStart;
        }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(TextWriter writer)
        {
            // Parameters sanity checks
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

        /// <summary>
        /// The write init script.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteInitScript(TextWriter writer)
        {
            var request = this.HtmlHelper.ViewContext.HttpContext.Request;
            var path = request.PathBase.Value;
            if (string.IsNullOrEmpty(this.ErrorPageUrl))
            {
                this.ErrorPageUrl = string.Format(CultureInfo.InvariantCulture, "{0}/Home/SubErr?NUMFEN={1}&COOKIENAME={2}",
                     string.IsNullOrEmpty(path) ? string.Empty : path.TrimEnd('/'), request.Query["NumFen"].ToString(), request.Query["COOKIENAME"].ToString());
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
            // Safety protection : remove already existing select columns (in case of several tables using the same columns definitions).
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
            // Safety protection : remove already existing delete columns (in case of several tables using the same columns definitions).
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
            // Safety protection : remove already existing modify columns (in case of several tables using the same columns definitions).
            this.Columns.RemoveAll(c => c.PropertyName.Equals(this.ModifyColumnName));
            this.Columns.Insert(this.ModifyColumnnIndex, modifyColumn);
        }
    }
}
