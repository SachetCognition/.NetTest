namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using System;

    public class DataTableColumnOption
    {
        public string Name { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public bool Searchable { get; set; } = true;
        public bool Orderable { get; set; } = true;
        public bool Visible { get; set; } = true;
    }

    public class DataTableAjaxOption
    {
        public string Url { get; set; } = string.Empty;
        public string Type { get; set; } = "POST";
        public object? Data { get; set; }
    }

    public class DataTablePagingOption
    {
        public bool Enabled { get; set; } = true;
        public int PageLength { get; set; } = 10;
        public int[] LengthMenu { get; set; } = new[] { 10, 25, 50, 100 };
    }
}
