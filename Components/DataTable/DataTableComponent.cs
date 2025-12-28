using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.Common;
using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
using Equant.SAV2000.ComponentLibrary.Common.Helper;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
using Resources = Equant.SAV2000.ComponentLibrary.Common.Resources;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

public class DataTableComponent : ComponentBase
{
    public const string DefaultRowIdColumnName = "RowIdColumn";
    public const string DefaultSelectColumnName = "SelectColumn";
    public const string DefaultDeleteColumnName = "DeleteColumn";
    public const string DefaultModifyColumnName = "ModifyColumn";
    public const string DefaultSelectClass = "SelectClassIdentifier";

    public string SelectAllName { get; private set; } = string.Empty;

    public DataTableComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
            Caption = string.Empty;
            Columns = new List<DataTableColumn>();
            CssClass = "display table dataTable";
            CssClassRefreshTime = "majdate";
            CssClassRecordsLimit = "alert error form datatableerror";
            Data = null;
            DomLayout = "<\"clear\"i>rt<lp>";
            IsHorizontalScrolling = true;
            InitialDisplayIndex = -1;
            InitialSorting = new List<KeyValuePair<string, SortDirection>>();
            IsVerticalScrolling = false;
            IsPaginate = true;
            IsReorder = true;
            IsSelect = false;
            IsDisplayRefreshTime = true;
            IsDisplayRecordInfo = true;
            IsDeleteNeeded = false;
            IsModifyNeeded = false;
            IsSelectAll = true;
            IsShowHeader = true;
            PageSize = 10;
            PaginationNumberOfPages = 17;
            RefreshTimeLabelFormat = Resources.ApplicationStrings.Datatable_sRefreshTime;
            RefreshTimeInverval = -1;
            RowIdColumnName = DefaultRowIdColumnName;
            SelectColumnName = DefaultSelectColumnName;
            DeleteColumnName = DefaultDeleteColumnName;
            ModifyColumnName = DefaultModifyColumnName;
            SelectClassIdentifier = DefaultSelectClass;
            DeleteToolTip = Resources.ApplicationStrings.LBL000029;
            ModifyToolTip = Resources.ApplicationStrings.LBL000030;
            SelectAllTooltip = Resources.ApplicationStrings.LBL000032;
            ServiceUri = string.Empty;
            TotalRecordsLimit = 0;
            RecordsLimitMessage = Resources.ApplicationStrings.RecordsLimitMessageShort;
            RecordsLimitMessageLong = Resources.ApplicationStrings.RecordsLimitMessageLong;
            RecordLimitMessageTitle = Resources.ApplicationStrings.ErrorTitle;
            AscTooltip = Resources.ApplicationStrings.TIP000021;
            DescTooltip = Resources.ApplicationStrings.TIP000025;
            Criteria = new List<CriteriaParameter>();
            SelectColumnIndex = 0;
            DeleteColumnIndex = 1;
            ModifyColumnnIndex = 2;
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
                if (IsFilter)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsjquerydataTablesdatatablescolumnFilter",
                            "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.dataTables.columnFilter.js",
                            240,
                            typeof(Locator)));
                }
                if (IsReorder)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsjquerydataTablescolMoveResize",
                            "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.dataTables.colMoveResize.js",
                            250,
                            typeof(Locator)));
                }
                if (IsSelect)
                {
                    jsResource.Add(
                        new JsResource(
                            "JsDataTableSelection",
                            "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTableSelection.js",
                            285,
                            typeof(DataTableComponent)));
                }
                if (IsModifyNeeded || IsDeleteNeeded)
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

    public override IHtmlContent ToHtml()
    {
        if (!string.IsNullOrEmpty(ServiceUri) && (Data != null))
        {
            throw new NotSupportedException(
                "ServiceUri (server-side processing) and Data (client-side processing) properties cannot be used at the same time");
        }

        if (IsSelect && !Columns.Any(x => x.PropertyName.Equals(RowIdColumnName)))
        {
            throw new NotSupportedException("When using select feature, a row identifier column should be specified");
        }

        var specialColumns = new Dictionary<int, Action>();

        if (IsDeleteNeeded)
        {
            specialColumns.Add(DeleteColumnIndex, DeleteColumnAction);
        }

        if (IsSelect)
        {
            specialColumns.Add(SelectColumnIndex, SelectColumnAction);
        }

        if (IsModifyNeeded)
        {
            specialColumns.Add(ModifyColumnnIndex, ModifyColumnAction);
        }

        foreach (var action in specialColumns.OrderBy(x => x.Key).Select(x => x.Value))
        {
            action();
        }

        return new DataTableHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var request = HtmlHelper.ViewContext.HttpContext.Request;
        var path = request.PathBase.Value ?? string.Empty;
        if (string.IsNullOrEmpty(ErrorPageUrl))
        {
            var numFen = request.Query["NumFen"].FirstOrDefault() ?? string.Empty;
            var cookieName = request.Query["COOKIENAME"].FirstOrDefault() ?? string.Empty;
            ErrorPageUrl = string.Format(CultureInfo.InvariantCulture, "{0}/Home/SubErr?NUMFEN={1}&COOKIENAME={2}",
                 string.IsNullOrEmpty(path) ? string.Empty : path.TrimEnd('/'), numFen, cookieName);
        }

        return string.Format("$('#{0}').dataTableCore({1});", Id, new DataTableOptionsSerializer(this).Serialize());
    }

    private void SelectColumnAction()
    {
        SelectAllName = string.Format(CultureInfo.InvariantCulture, "{0}_selectAll", Id);
        var selectColumn = new DataTableColumn
        {
            CssClass = CssClassSelect,
            PropertyName = SelectColumnName,
            IsSortable = IsSelectColumnSortable,
            IsEncodeHtml = false,
            IsSearchable = false,
            ColumnType = DataTableColumnType.Html,
            HeaderText = SelectAllText
        };
        if (IsSelect && IsSelectAll)
        {
            selectColumn.HeaderTemplate = new SelectAllDataTableHeaderTemplate { Id = SelectAllName, Name = SelectAllName, ToolTip = SelectAllTooltip, Title = SelectAllText };
        }
        Columns.RemoveAll(c => c.PropertyName.Equals(SelectColumnName));
        Columns.Insert(SelectColumnIndex, selectColumn);
    }

    private void DeleteColumnAction()
    {
        var deleteColumn = new DataTableColumn
        {
            PropertyName = DeleteColumnName,
            IsSortable = false,
            IsEncodeHtml = false,
            IsSearchable = false,
            CssClass = CssClassDelete,
            ColumnType = DataTableColumnType.Html,
            HeaderText = Resources.ApplicationStrings.LBL000017,
            ShowHeaderText = false
        };
        Columns.RemoveAll(c => c.PropertyName.Equals(DeleteColumnName));
        Columns.Insert(DeleteColumnIndex, deleteColumn);
    }

    private void ModifyColumnAction()
    {
        var modifyColumn = new DataTableColumn
        {
            PropertyName = ModifyColumnName,
            IsSortable = false,
            IsEncodeHtml = false,
            IsSearchable = false,
            CssClass = CssClassModify,
            ColumnType = DataTableColumnType.Html,
            ShowHeaderText = false,
            HeaderText = Resources.ApplicationStrings.LBL000016
        };
        Columns.RemoveAll(c => c.PropertyName.Equals(ModifyColumnName));
        Columns.Insert(ModifyColumnnIndex, modifyColumn);
    }
}
