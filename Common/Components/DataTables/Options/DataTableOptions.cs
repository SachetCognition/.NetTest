namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options;

public class DataTableOptions
{
    public bool? Paging { get; set; }
    public bool? Ordering { get; set; }
    public bool? Info { get; set; }
    public bool? Searching { get; set; }
    public int? PageLength { get; set; }
    public string? Dom { get; set; }
    public bool? ScrollX { get; set; }
    public bool? ScrollY { get; set; }
    public string? ScrollYHeight { get; set; }
    public List<List<object>>? Order { get; set; }
    public List<ColumnOptions>? Columns { get; set; }
    public AjaxOptions? Ajax { get; set; }
    public bool? ServerSide { get; set; }
    public bool? Processing { get; set; }
    public bool? StateSave { get; set; }
    public LanguageOptions? Language { get; set; }
}

public class ColumnOptions
{
    public string? Data { get; set; }
    public string? Name { get; set; }
    public bool? Orderable { get; set; }
    public bool? Searchable { get; set; }
    public bool? Visible { get; set; }
    public string? ClassName { get; set; }
    public string? Width { get; set; }
    public string? DefaultContent { get; set; }
    public string? Render { get; set; }
}

public class AjaxOptions
{
    public string? Url { get; set; }
    public string? Type { get; set; }
    public string? Data { get; set; }
    public string? DataSrc { get; set; }
}

public class LanguageOptions
{
    public string? EmptyTable { get; set; }
    public string? Info { get; set; }
    public string? InfoEmpty { get; set; }
    public string? InfoFiltered { get; set; }
    public string? LengthMenu { get; set; }
    public string? LoadingRecords { get; set; }
    public string? Processing { get; set; }
    public string? Search { get; set; }
    public string? ZeroRecords { get; set; }
    public PaginateOptions? Paginate { get; set; }
}

public class PaginateOptions
{
    public string? First { get; set; }
    public string? Last { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
}
