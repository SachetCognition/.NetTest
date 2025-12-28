using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

public class DataTableBuilder : ComponentBuilderBase<DataTableComponent, DataTableBuilder>
{
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
            Component.Caption = value;
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
            Component.ErrorPageUrl = value;
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
            Component.CssClass = value;
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
            Component.CssClassRefreshTime = value;
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
            Component.CssClassSelect = value;
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
            Component.CssClassDelete = value;
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
            Component.CssClassModify = value;
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
            Component.Columns = value;
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
            Component.Criteria = value;
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
            Component.Data = value;
            return this;
        }

        /// <summary>
        /// Sets the value of onDelete Event
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder OnDeleteClick(string value)
        {
            Component.OnDeleteClick = value;
            return this;
        }

        /// <summary>
        /// Sets the value of onModify Event
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder OnModifyClick(string value)
        {
            Component.OnModifyClick = value;
            return this;
        }

        /// <summary>
        /// Sets the value of Modify Button Title
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder ModifyTooltip(string value)
        {
            Component.ModifyToolTip = value;
            return this;
        }

        /// <summary>
        /// Sets the value of Delete Button Title
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder DeleteToolTip(string value)
        {
            Component.DeleteToolTip = value;
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
            Component.DomLayout = value;
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
                Component.DataContext = context;
                Component.InitialDisplayIndex = context.DisplayStart;
                Component.InitialSelectState = context.SelectState;
                if (context.Sorting != null)
                {
                    Component.InitialSorting = context.Sorting;
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
            Component.IsHorizontalScrolling = value;
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
            Component.IsFilter = value;
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
            Component.IsPaginate = value;
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
            Component.IsReorder = value;
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
            Component.IsSelect = value;
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
            Component.IsSelectColumnSortable = value;
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
            IsSelect(value);
            Component.SelectColumnIndex = index;
            return this;
        }

        /// <summary>
        /// property to check if delete is required or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder IsDelete(bool value)
        {
            Component.IsDeleteNeeded = value;
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
            IsDelete(value);
            Component.DeleteColumnIndex = index;
            return this;
        }

        /// <summary>
        /// Sets if modify feature is needed
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder IsModify(bool value)
        {
            Component.IsModifyNeeded = value;
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
            IsModify(value);
            Component.ModifyColumnnIndex = index;
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
            Component.IsSelectAll = value;
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
            Component.IsShowHeader = value;
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
            Component.IsVerticalScrolling = value;
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
            Component.IsDisplayRefreshTime = value;
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
            Component.IsDisplayRecordInfo = value;
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
            Component.InitialDisplayIndex = value;
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
            Component.InitialSelectState = value;
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
            Component.InitialSorting = value;
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
            Component.OnAjaxError = value;
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
            Component.OnDraw = value;
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
            Component.OnServerParams = value;
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
            Component.OnSort = value;
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
            Component.PageSize = value;
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
            Component.RefreshTimeInverval = value;
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
            Component.RefreshTimeLabelFormat = value;
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
            Component.SelectAllTooltip = value;
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
            Component.OnSelectColumnClick = value;
            return this;
        }


        /// <summary>
        /// To set Select All Text 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTableBuilder SelectAllText(string value)
        {
            Component.SelectAllText = value;
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
            Component.ServiceUri = value;
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
            Component.TotalRecordsLimit = value;
            return this;
        }

        /// <summary>
        /// Property to check if multi-sorting required or not
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>return the object of data table builder</returns>
        public DataTableBuilder IsMultiSortRequired(bool value)
        {
            Component.IsMultiSortRequired = value;
            return this;
        }

        public DataTableBuilder IsEncryptionRequired(bool value)
        {
            Component.IsEncryptionRequired = value;
            return this;
        }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
        Justification = "This property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
    public DataTableBuilder EncryptedParameters(List<string> value)
    {
        Component.EncryptedParameters = value;
        return this;
    }
}
