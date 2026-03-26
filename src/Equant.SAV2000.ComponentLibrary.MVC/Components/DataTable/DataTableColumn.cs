namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System;

    [Serializable]
    public class DataTableColumn
    {
        public DataTableColumn()
        {
            this.CssClass = string.Empty;
            this.HeaderTemplate = null;
            this.HeaderText = string.Empty;
            this.PropertyName = string.Empty;
            this.IsSearchable = true;
            this.IsSortable = false;
            this.IsVisible = true;
            this.DefaultValue = string.Empty;
            this.Render = null;
            this.IsEncodeHtml = true;
            this.ColumnType = DataTableColumnType.String;
            this.TextMaxLength = -1;
            this.FilterMaxLength = 30;
            this.ShowHeaderText = true;
        }

        public string CssClass { get; set; }
        public string CssClassHeader { get; set; }
        public IDataTableHeaderTemplate HeaderTemplate { get; set; }
        public string HeaderText { get; set; }
        public string PropertyName { get; set; }
        public bool IsSortable { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSearchable { get; set; }
        public string DefaultValue { get; set; }
        public string Render { get; set; }
        public bool IsEncodeHtml { get; set; }
        public string TooltipFilter { get; set; }
        public DataTableColumnType ColumnType { get; set; }
        public int TextMaxLength { get; set; }
        public int FilterMaxLength { get; set; }
        public bool ShowHeaderText { get; set; }
    }
}
