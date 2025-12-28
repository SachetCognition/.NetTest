using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables.Options;

// Extended DataTableOptions for the serializer
public class DataTableOptions
{
    [JsonProperty("bSort")]
    public bool? IsMultiSelect { get; set; }

    [JsonProperty("bServerSide")]
    public bool? IsServerSide { get; set; }

    [JsonProperty("sAjaxSource")]
    public string? AjaxSource { get; set; }

    [JsonProperty("bDeferRender")]
    public bool? DeferRender { get; set; }

    [JsonProperty("aoColumns")]
    public List<DataTableColumnsOption>? Columns { get; set; }

    [JsonProperty("aaData")]
    public DataTable? Data { get; set; }

    [JsonProperty("sDom")]
    public string? Dom { get; set; }

    [JsonProperty("sServerMethod")]
    public string? ServerMethod { get; set; }

    [JsonProperty("fnDrawCallback")]
    public JRaw? DrawCallBack { get; set; }

    [JsonProperty("fnServerParams")]
    public JRaw? ServerParams { get; set; }

    [JsonProperty("sPaginationType")]
    public string? PaginationType { get; set; }

    [JsonProperty("iDisplayLength")]
    public int? DisplayLength { get; set; }

    [JsonProperty("bFilter")]
    public bool? IsFilter { get; set; }

    [JsonProperty("bPaginate")]
    public bool? IsPaginate { get; set; }

    [JsonProperty("iDisplayStart")]
    public int? DisplayStart { get; set; }

    [JsonProperty("aaSorting")]
    public JRaw? Sorting { get; set; }

    [JsonProperty("sScrollX")]
    public string? ScrollX { get; set; }

    [JsonProperty("sScrollY")]
    public string? ScrollY { get; set; }

    [JsonProperty("bScrollCollapse")]
    public bool? ScrollCollapse { get; set; }

    [JsonProperty("oColReorder")]
    public DataTableColumnsReorderOption? ColReorder { get; set; }

    [JsonProperty("oLanguage")]
    public DataTableLanguageOption? Language { get; set; }

    [JsonProperty("isEncryptionRequired")]
    public bool? IsEncryptionRequired { get; set; }

    [JsonProperty("encryptedParameters")]
    public List<string>? EncryptedParameters { get; set; }
}

public class DataTableColumnsOption
{
    [JsonProperty("bVisible")]
    public bool IsVisible { get; set; }

    [JsonProperty("sClass")]
    public string? Class { get; set; }

    [JsonProperty("sName")]
    public string? Name { get; set; }

    [JsonProperty("bSortable")]
    public bool IsSortable { get; set; }

    [JsonProperty("mData")]
    public JRaw? Data { get; set; }

    [JsonProperty("mRender")]
    public JRaw? Render { get; set; }

    [JsonProperty("sDefaultContent")]
    public string? DefaultContent { get; set; }

    [JsonProperty("sType")]
    public string? ColumnType { get; set; }
}

public class DataTableColumnsReorderOption
{
    [JsonProperty("bReorder")]
    public bool AllowReorder { get; set; }

    [JsonProperty("bResize")]
    public bool AllowResize { get; set; }
}

public class DataTableLanguageOption
{
    [JsonProperty("sEmptyTable")]
    public string? EmptyTable { get; set; }

    [JsonProperty("sInfo")]
    public string? Info { get; set; }

    [JsonProperty("sInfoEmpty")]
    public string? InfoEmpty { get; set; }

    [JsonProperty("sInfoThousands")]
    public string? InfoThousands { get; set; }

    [JsonProperty("sLoadingRecords")]
    public string? LoadingRecords { get; set; }

    [JsonProperty("oPaginate")]
    public DataTableLanguagePaginateOption? Paginate { get; set; }

    [JsonProperty("oAria")]
    public DataTableLanguageAriaOption? Aria { get; set; }

    [JsonProperty("sProcessing")]
    public string? Processing { get; set; }

    [JsonProperty("sZeroRecords")]
    public string? ZeroRecords { get; set; }

    [JsonProperty("sInfoFiltered")]
    public string? InfoFiltered { get; set; }
}

public class DataTableLanguagePaginateOption
{
    [JsonProperty("sFirst")]
    public string? First { get; set; }

    [JsonProperty("sLast")]
    public string? Last { get; set; }

    [JsonProperty("sNext")]
    public string? Next { get; set; }

    [JsonProperty("sPrevious")]
    public string? Previous { get; set; }
}

public class DataTableLanguageAriaOption
{
    [JsonProperty("sSortAscending")]
    public string? SortAscending { get; set; }

    [JsonProperty("sSortDescending")]
    public string? SortDescending { get; set; }
}

public class DataTableColumnFilterColumnOption
{
    [JsonProperty("type")]
    public DataTableColumnFilterColumnTypeOption ColumnType { get; set; }

    [JsonProperty("maxLength")]
    public int? MaxLength { get; set; }
}

public enum DataTableColumnFilterColumnTypeOption
{
    Null,
    Text,
    Select
}
