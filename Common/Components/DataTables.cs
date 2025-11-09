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
    }

    public class DataTableColumnsOption
    {
        public string[] Targets { get; set; } = Array.Empty<string>();
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
    }

    public enum EMenuCtrlType
    {
        None = 0,
        Edit = 1,
        Delete = 2,
        View = 3,
        Custom = 4
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
        public string MenuType { get; set; } = string.Empty;
        public string OnclickEvent { get; set; } = string.Empty;
        public string ReturnChildMenu { get; set; } = string.Empty;
    }
}
