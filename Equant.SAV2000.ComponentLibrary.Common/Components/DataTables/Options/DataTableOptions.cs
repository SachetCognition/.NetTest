namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options
{
    using System.Collections.Generic;

    public class DataTableOptions
    {
        public bool IsMultiSelect { get; set; }
        public bool IsServerSide { get; set; }
        public string AjaxSource { get; set; }
        public bool DeferRender { get; set; }
        public List<DataTableColumnsOption> Columns { get; set; }
        public object Data { get; set; }
        public string Dom { get; set; }
        public string ServerMethod { get; set; }
        public object DrawCallBack { get; set; }
        public object ServerParams { get; set; }
        public string PaginationType { get; set; }
        public int DisplayLength { get; set; }
        public bool IsFilter { get; set; }
        public bool IsPaginate { get; set; }
        public int DisplayStart { get; set; }
        public object Sorting { get; set; }
        public string ScrollX { get; set; }
        public string ScrollY { get; set; }
        public bool ScrollCollapse { get; set; }
        public DataTableColumnsReorderOption ColReorder { get; set; }
        public DataTableLanguageOption Language { get; set; }
        public bool IsEncryptionRequired { get; set; }
        public List<string> EncryptedParameters { get; set; }
    }
}
