namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables
{
    using System;

    public class CriteriaParameter
    {
        public string ClientId { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public CriteriaParameterEvaluationType EvalType { get; set; }
    }

    public enum CriteriaParameterEvaluationType
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        LessThan = 3,
        GreaterThanOrEqual = 4,
        LessThanOrEqual = 5,
        Contains = 6,
        StartsWith = 7,
        EndsWith = 8
    }

    public class DataTableOptions
    {
        public DataTableColumnsOption? Columns { get; set; }
        public bool ServerSide { get; set; }
        public int PageLength { get; set; } = 10;
        public string AjaxSource { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public bool ColReorder { get; set; }
        public string ColumnType { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string DefaultContent { get; set; } = string.Empty;
        public bool DeferRender { get; set; }
        public int DisplayLength { get; set; }
        public int DisplayStart { get; set; }
        public string Dom { get; set; } = string.Empty;
        public string DrawCallBack { get; set; } = string.Empty;
        public string EncryptedParameters { get; set; } = string.Empty;
        public bool IsEncryptionRequired { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMultiSelect { get; set; }
        public bool IsPaginate { get; set; }
        public bool IsServerSide { get; set; }
        public bool IsSortable { get; set; }
        public bool IsVisible { get; set; }
        public object Language { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PaginationType { get; set; } = string.Empty;
        public string Render { get; set; } = string.Empty;
        public bool ScrollCollapse { get; set; }
        public string ScrollX { get; set; } = string.Empty;
        public string ScrollY { get; set; } = string.Empty;
        public string ServerMethod { get; set; } = string.Empty;
        public string ServerParams { get; set; } = string.Empty;
        public object Sorting { get; set; }
    }

    public class DataTableColumnsOption
    {
        public string[] Targets { get; set; } = Array.Empty<string>();
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public string Class { get; set; } = string.Empty;
        public string ColumnType { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string DefaultContent { get; set; } = string.Empty;
        public bool IsSortable { get; set; }
        public bool IsVisible { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Render { get; set; } = string.Empty;
    }

    public enum EMenuCtrlType
    {
        None = 0,
        Edit = 1,
        Delete = 2,
        View = 3,
        Custom = 4,
        Add = 5,
        Export = 6,
        Import = 7,
        Print = 8,
        Refresh = 9,
        Search = 10,
        Linkbutton = 11,
        Redirect = 12,
        RedirectJs = 13,
        LinkbuttonJs = 14,
        Imagebutton = 15,
        ImagebuttonJs = 16
    }

    public class MenuItem
    {
        public string Text { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public EMenuCtrlType Type { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public string ActionUrl { get; set; } = string.Empty;
        public string MenuName { get; set; } = string.Empty;
        public EMenuCtrlType MenuType { get; set; }
        public string OnclickEvent { get; set; } = string.Empty;
        public bool Hidden { get; set; }
        
        public System.Collections.Generic.List<MenuItem> ReturnChildMenu()
        {
            return new System.Collections.Generic.List<MenuItem>();
        }
    }

    public class DataTableLanguageOption
    {
        public string Info { get; set; } = string.Empty;
        public string InfoEmpty { get; set; } = string.Empty;
        public string InfoFiltered { get; set; } = string.Empty;
        public string InfoThousands { get; set; } = string.Empty;
        public string LoadingRecords { get; set; } = string.Empty;
        public string Processing { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public string ZeroRecords { get; set; } = string.Empty;
        public object Paginate { get; set; }
    }

    public class DataTableColumnsReorderOption
    {
        public bool AllowReorder { get; set; }
        public bool AllowResize { get; set; }
    }

    public class DataTableColumnFilterColumnOption
    {
        public string ColumnType { get; set; } = string.Empty;
        public int MaxLength { get; set; }
        public string Filter { get; set; } = string.Empty;
    }

    public class DataTableColumnFilterColumnTypeOption
    {
        public string Text { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Select { get; set; } = string.Empty;
        public string Null { get; set; } = string.Empty;
    }
}
