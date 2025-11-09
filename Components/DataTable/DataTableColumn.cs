namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System;

    /// <summary>
    /// The data table column.
    /// </summary>
    [Serializable]
    public class DataTableColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableColumn"/> class.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the CSS class applied to each cell of the column.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class fir the header element of the column. 
        /// The class will be applied to the TH element.
        /// </summary>
        public string CssClassHeader { get; set; }
        
        /// <summary>
        /// Gets or sets the header template
        /// </summary>
        public IDataTableHeaderTemplate HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the text value of the header of the column. This parameter takes precedence on the HeaderTemplate field.
        /// </summary>
        public string HeaderText { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string PropertyName { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether if the column can be sorted 
        /// </summary>
        public bool IsSortable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the column is visible. Default value is true. 
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the column is searchable or not
        /// Displaying of column filter is based on this value.
        /// </summary>
        public bool IsSearchable { get; set; }
        
        /// <summary>
        /// Gets or sets the default value for the cell when the data is null. 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the javascript function which will be used to render the cell
        /// </summary>
        public string Render { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the HTML of the cell should be encoded or not
        /// </summary>
        public bool IsEncodeHtml { get; set; }
        
        /// <summary>
        /// Gets or sets the tooltip for the column filter the column
        /// </summary>
        public string TooltipFilter { get; set; }

        /// <summary>
        /// Gets or sets the column type. It can be : String (default), Moment (to handle date) or Html (for cell with HTML content) 
        /// </summary>
        public DataTableColumnType ColumnType { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the text contained in the column, a tooltip is used to display the full text
        /// </summary>
        public int TextMaxLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum text length which can be used in the filter input 
        /// </summary>
        public int FilterMaxLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether header text will appear or not.
        /// </summary>
        public bool ShowHeaderText { get; set; }
    }
}
