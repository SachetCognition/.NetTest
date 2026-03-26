namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;

    public class DataTableBuilder : ComponentBuilderBase<DataTableComponent, DataTableBuilder>
    {
        public DataTableBuilder(DataTableComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public DataTableBuilder(DataTableComponent component)
            : base(component)
        {
        }

        public DataTableBuilder Caption(string value) { this.Component.Caption = value; return this; }
        public DataTableBuilder ErrorPageUrl(string value) { this.Component.ErrorPageUrl = value; return this; }
        public DataTableBuilder CssClass(string value) { this.Component.CssClass = value; return this; }
        public DataTableBuilder CssClassRefreshTime(string value) { this.Component.CssClassRefreshTime = value; return this; }
        public DataTableBuilder CssClassSelect(string value) { this.Component.CssClassSelect = value; return this; }
        public DataTableBuilder CssClassDelete(string value) { this.Component.CssClassDelete = value; return this; }
        public DataTableBuilder CssClassModify(string value) { this.Component.CssClassModify = value; return this; }
        public DataTableBuilder Columns(List<DataTableColumn> value) { this.Component.Columns = value; return this; }
        public DataTableBuilder Criteria(List<CriteriaParameter> value) { this.Component.Criteria = value; return this; }
        public DataTableBuilder Data(DataTable value) { this.Component.Data = value; return this; }
        public DataTableBuilder OnDeleteClick(string value) { this.Component.OnDeleteClick = value; return this; }
        public DataTableBuilder OnModifyClick(string value) { this.Component.OnModifyClick = value; return this; }
        public DataTableBuilder ModifyTooltip(string value) { this.Component.ModifyToolTip = value; return this; }
        public DataTableBuilder DeleteToolTip(string value) { this.Component.DeleteToolTip = value; return this; }
        public DataTableBuilder DomLayout(string value) { this.Component.DomLayout = value; return this; }

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

        public DataTableBuilder IsHorizontalScrolling(bool value) { this.Component.IsHorizontalScrolling = value; return this; }
        public DataTableBuilder IsFilter(bool value) { this.Component.IsFilter = value; return this; }
        public DataTableBuilder IsPaginate(bool value) { this.Component.IsPaginate = value; return this; }
        public DataTableBuilder IsReorder(bool value) { this.Component.IsReorder = value; return this; }
        public DataTableBuilder IsSelect(bool value) { this.Component.IsSelect = value; return this; }
        public DataTableBuilder IsSelectSortable(bool value) { this.Component.IsSelectColumnSortable = value; return this; }

        public DataTableBuilder IsSelect(bool value, int index)
        {
            this.IsSelect(value);
            this.Component.SelectColumnIndex = index;
            return this;
        }

        public DataTableBuilder IsDelete(bool value) { this.Component.IsDeleteNeeded = value; return this; }

        public DataTableBuilder IsDelete(bool value, int index)
        {
            this.IsDelete(value);
            this.Component.DeleteColumnIndex = index;
            return this;
        }

        public DataTableBuilder IsModify(bool value) { this.Component.IsModifyNeeded = value; return this; }

        public DataTableBuilder IsModify(bool value, int index)
        {
            this.IsModify(value);
            this.Component.ModifyColumnnIndex = index;
            return this;
        }

        public DataTableBuilder IsSelectAll(bool value) { this.Component.IsSelectAll = value; return this; }
        public DataTableBuilder IsShowHeader(bool value) { this.Component.IsShowHeader = value; return this; }
        public DataTableBuilder IsVerticalScrolling(bool value) { this.Component.IsVerticalScrolling = value; return this; }
        public DataTableBuilder IsDisplayRefreshTime(bool value) { this.Component.IsDisplayRefreshTime = value; return this; }
        public DataTableBuilder IsDisplayRecordInfo(bool value) { this.Component.IsDisplayRecordInfo = value; return this; }
        public DataTableBuilder InitialDisplayIndex(int value) { this.Component.InitialDisplayIndex = value; return this; }
        public DataTableBuilder InitialSelectState(Dictionary<string, bool> value) { this.Component.InitialSelectState = value; return this; }
        public DataTableBuilder InitialSorting(List<KeyValuePair<string, SortDirection>> value) { this.Component.InitialSorting = value; return this; }
        public DataTableBuilder OnAjaxError(string value) { this.Component.OnAjaxError = value; return this; }
        public DataTableBuilder OnDraw(string value) { this.Component.OnDraw = value; return this; }
        public DataTableBuilder OnServerParams(string value) { this.Component.OnServerParams = value; return this; }
        public DataTableBuilder OnSort(string value) { this.Component.OnSort = value; return this; }
        public DataTableBuilder PageSize(int value) { this.Component.PageSize = value; return this; }
        public DataTableBuilder RefreshTimeInverval(int value) { this.Component.RefreshTimeInverval = value; return this; }
        public DataTableBuilder RefreshTimeLabelFormat(string value) { this.Component.RefreshTimeLabelFormat = value; return this; }
        public DataTableBuilder SelectAllToolTip(string value) { this.Component.SelectAllTooltip = value; return this; }
        public DataTableBuilder OnSelectColumnClick(string value) { this.Component.OnSelectColumnClick = value; return this; }
        public DataTableBuilder SelectAllText(string value) { this.Component.SelectAllText = value; return this; }
        public DataTableBuilder ServiceUri(string value) { this.Component.ServiceUri = value; return this; }
        public DataTableBuilder TotalRecordsLimit(int value) { this.Component.TotalRecordsLimit = value; return this; }
        public DataTableBuilder IsMultiSortRequired(bool value) { this.Component.IsMultiSortRequired = value; return this; }
        public DataTableBuilder IsEncryptionRequired(bool value) { this.Component.IsEncryptionRequired = value; return this; }
        public DataTableBuilder EncryptedParameters(List<string> value) { this.Component.EncryptedParameters = value; return this; }
    }
}
