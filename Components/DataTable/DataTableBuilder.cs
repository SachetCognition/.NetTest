namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Data;

    using Equant.SAV2000.ComponentLibrary.Common.Components;
    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;

    /// <summary>
    /// The data table builder.
    /// </summary>
    public class DataTableBuilder : ComponentBuilderBase<DataTableComponent, DataTableBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public DataTableBuilder(DataTableComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// Sets the table caption
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder Caption(string value)
        {
            this.Component.Caption = value;
            return this;
        }

        /// <summary>
        /// Sets the Error Page Url
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder ErrorPageUrl(string value)
        {
            this.Component.ErrorPageUrl = value;
            return this;
        }

        /// <summary>
        /// Sets the CSS class applied to the data table
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder CssClass(string value)
        {
            this.Component.CssClass = value;
            return this;
        }

        /// <summary>
        /// Sets the CSS class applied to the last refreshed time label
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder CssClassRefreshTime(string value)
        {
            this.Component.CssClassRefreshTime = value;
            return this;
        }

        /// <summary>
        /// Sets the CSS class applied to the selection column
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder CssClassSelect(string value)
        {
            this.Component.CssClassSelect = value;
            return this;
        }

        /// <summary>
        /// Sets the CSS class applied to the delete column
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder CssClassDelete(string value)
        {
            this.Component.CssClassDelete = value;
            return this;
        }

        /// <summary>
        /// Sets the CSS class applied to the modify column
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder CssClassModify(string value)
        {
            this.Component.CssClassModify = value;
            return this;
        }

        /// <summary>
        /// Sets the list of columns to use in the data table
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "Columns property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
        public DataTableBuilder Columns(List<DataTableColumn> value)
        {
            this.Component.Columns = value;
            return this;
        }

        /// <summary>
        /// Sets the list of criteria to use in the data table
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "Criteria property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
        public DataTableBuilder Criteria(List<CriteriaParameter> value)
        {
            this.Component.Criteria = value;
            return this;
        }

        /// <summary>
        /// Sets the data of the data table for client-side processing
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder Data(DataTable value)
        {
            this.Component.Data = value;
            return this;
        }

        /// <summary>
        /// Sets the value of onDelete Event
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder OnDeleteClick(string value)
        {
            this.Component.OnDeleteClick = value;
            return this;
        }

        /// <summary>
        /// Sets the value of onModify Event
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder OnModifyClick(string value)
        {
            this.Component.OnModifyClick = value;
            return this;
        }

        /// <summary>
        /// Sets the value of Modify Button Title
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder ModifyTooltip(string value)
        {
            this.Component.ModifyToolTip = value;
            return this;
        }

        /// <summary>
        /// Sets the value of Delete Button Title
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder DeleteToolTip(string value)
        {
            this.Component.DeleteToolTip = value;
            return this;
        }

        /// <summary>
        /// Sets the DOM property which is used internally by the data table jquery plugin to generate the data table layout
        /// </summary>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder DomLayout(string value)
        {
            this.Component.DomLayout = value;
            return this;
        }

        /// <summary>
        /// Set value
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public DataTableBuilder Value(DataTableContext context)
        {
            if (context != null)
            {
                this.Component.DataContext = context;
                this.Component.InitialDisplayIndex = context.DisplayStart;
                this.Component.InitialSelectState = context.SelectState;
                if (context.Sorting != null)
                {
                    this.Component.InitialSorting = context.Sorting;
                }
            }

            return this;
        }

        /// <summary>
        /// Sets if horizontal scrolling is activated or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsHorizontalScrolling(bool value)
        {
            this.Component.IsHorizontalScrolling = value;
            return this;
        }

        /// <summary>
        /// Sets if columns filters are activated or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsFilter(bool value)
        {
            this.Component.IsFilter = value;
            return this;
        }

        /// <summary>
        /// Sets if pagination is activated or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsPaginate(bool value)
        {
            this.Component.IsPaginate = value;
            return this;
        }

        /// <summary>
        /// Sets if columns reordering is activated or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsReorder(bool value)
        {
            this.Component.IsReorder = value;
            return this;
        }

        /// <summary>
        /// Sets if selection feature is activated or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsSelect(bool value)
        {
            this.Component.IsSelect = value;
            return this;
        }

        /// <summary>
        /// Sets if selection column is sortable or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsSelectSortable(bool value)
        {
            this.Component.IsSelectColumnSortable = value;
            return this;
        }

        /// <summary>
        /// Sets if selection feature is activated or not
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataTableBuilder IsSelect(bool value, int index)
        {
            this.IsSelect(value);
            this.Component.SelectColumnIndex = index;
            return this;
        }

        /// <summary>
        /// property to check if delete is required or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder IsDelete(bool value)
        {
            this.Component.IsDeleteNeeded = value;
            return this;
        }

        /// <summary>
        /// Sets if delete feature is needed
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="index"></param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsDelete(bool value, int index)
        {
            this.IsDelete(value);
            this.Component.DeleteColumnIndex = index;
            return this;
        }

        /// <summary>
        /// Sets if modify feature is needed
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder IsModify(bool value)
        {
            this.Component.IsModifyNeeded = value;
            return this;
        }

        /// <summary>
        /// Sets if modify feature is needed
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="index"></param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsModify(bool value, int index)
        {
            this.IsModify(value);
            this.Component.ModifyColumnnIndex = index;
            return this;
        }
        /// <summary>
        /// Sets if select all checkbox is activated or not when using selection feature
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsSelectAll(bool value)
        {
            this.Component.IsSelectAll = value;
            return this;
        }

        /// <summary>
        /// Sets if the header of the data table should be shown or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsShowHeader(bool value)
        {
            this.Component.IsShowHeader = value;
            return this;
        }

        /// <summary>
        /// Sets if vertical scrolling is activated or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsVerticalScrolling(bool value)
        {
            this.Component.IsVerticalScrolling = value;
            return this;
        }

        /// <summary>
        /// Sets if refresh time is displayed or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsDisplayRefreshTime(bool value)
        {
            this.Component.IsDisplayRefreshTime = value;
            return this;
        }

        /// <summary>
        /// Sets if record number information are displayed or not
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder IsDisplayRecordInfo(bool value)
        {
            this.Component.IsDisplayRecordInfo = value;
            return this;
        }

        /// <summary>
        /// Sets the initial display index of the data table
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder InitialDisplayIndex(int value)
        {
            this.Component.InitialDisplayIndex = value;
            return this;
        }

        /// <summary>
        /// Sets the initial selection state when selection feature is used
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder InitialSelectState(Dictionary<string, bool> value)
        {
            this.Component.InitialSelectState = value;
            return this;
        }

        /// <summary>
        /// Sets the initial sorting of the datable (this use a list of column name and sort direction pairs)
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "InitialSorting property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
        public DataTableBuilder InitialSorting(List<KeyValuePair<string, SortDirection>> value)
        {
            this.Component.InitialSorting = value;
            return this;
        }

        /// <summary>
        /// Sets the java-script callback function to call when an AJAX error occurs
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder OnAjaxError(string value)
        {
            this.Component.OnAjaxError = value;
            return this;
        }

        /// <summary>
        /// Sets the java-script callback function to call when the draw of the data table has finished
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder OnDraw(string value)
        {
            this.Component.OnDraw = value;
            return this;
        }

        /// <summary>
        /// Sets the java-script callback function to call just before query to server for data (server-side processing only)
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder OnServerParams(string value)
        {
            this.Component.OnServerParams = value;
            return this;
        }

        /// <summary>
        /// Sets the java-script callback function to call when a sort is done
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder OnSort(string value)
        {
            this.Component.OnSort = value;
            return this;
        }

        /// <summary>
        /// Sets the number of rows to display on a page
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder PageSize(int value)
        {
            this.Component.PageSize = value;
            return this;
        }

        /// <summary>
        /// Sets the automatic refresh time interval
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder RefreshTimeInverval(int value)
        {
            this.Component.RefreshTimeInverval = value;
            return this;
        }

        /// <summary>
        /// Sets the refresh time label format
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder RefreshTimeLabelFormat(string value)
        {
            this.Component.RefreshTimeLabelFormat = value;
            return this;
        }

        /// <summary>
        /// Sets the tooltip of the select all checkbox when selection feature is used
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder SelectAllToolTip(string value)
        {
            this.Component.SelectAllTooltip = value;
            return this;
        }
        /// <summary>
        /// Sets the function name which will call on click of check box
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder OnSelectColumnClick(string value)
        {
            this.Component.OnSelectColumnClick = value;
            return this;
        }


        /// <summary>
        /// To set Select All Text 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder SelectAllText(string value)
        {
            this.Component.SelectAllText = value;
            return this;
        }

        /// <summary>
        /// Sets the URL to query with AJAX call to retrieve the data table data
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder ServiceUri(string value)
        {
            this.Component.ServiceUri = value;
            return this;
        }

        /// <summary>
        /// Sets the total records limit until which a warning message is displayed
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="DataTableBuilder"/>.
        /// </returns>
        public DataTableBuilder TotalRecordsLimit(int value)
        {
            this.Component.TotalRecordsLimit = value;
            return this;
        }

        /// <summary>
        /// Property to check if multi-sorting required or not
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>return the object of data table builder</returns>
        public DataTableBuilder IsMultiSortRequired(bool value)
        {
            this.Component.IsMultiSortRequired = value;
            return this;
        }

        public DataTableBuilder IsEncryptionRequired(bool value)
        {
            this.Component.IsEncryptionRequired = value;
            return this;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "This property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
        public DataTableBuilder EncryptedParameters(List<string> value)
        {
            this.Component.EncryptedParameters = value;
            return this;
        }
    }
}
