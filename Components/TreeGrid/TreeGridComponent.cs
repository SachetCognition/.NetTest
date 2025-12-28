using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.Common.Helper;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid;

public class TreeGridComponent : ComponentBase
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "TETHYS: This input is required."),
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "TETHYS: The list values are to be provided by the user.")]
    public List<TreeViewColumn> Columns { get; set; } = new List<TreeViewColumn>();

    private readonly ReadOnlyCollection<JsResource> _jsResources;

    public TreeGridComponent(IHtmlHelper htmlHelper, System.Data.DataTable treeData)
        : base(htmlHelper)
    {
        var jsRes = new List<JsResource>
        {
            new JsResource(
                "JsTreeCommon",
                "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TreeCommon.js",
                200,
                typeof(TreeGridComponent)),
        };

        _jsResources = new ReadOnlyCollection<JsResource>(jsRes);
        Data = treeData;
    }

    public System.Data.DataTable? Data { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources => _jsResources;

    public override IHtmlContent ToHtml()
    {
        return new TreeGridHtmlBuilder(this).Build();
    }

    public string? WidthCss { get; set; }
    public string? ColorToHighlightRow { get; set; }
    public IEnumerable<int>? IdsToHighlight { get; set; }
    public IEnumerable<string>? CheckedValues { get; set; }
    public string? KeyColumnName { get; set; }

    public override string ToInitScript()
    {
        var columns = new List<TreeCustomColumnData>();

        foreach (var column in Columns.Where(x => x.TreeColumnType == ColumnType.Custom).ToList())
        {
            columns.Add(
                new TreeCustomColumnData
                {
                    PropertyName = column.PropertyName,
                    Data = string.IsNullOrEmpty(column.Render) ? null : new JRaw(column.Render),
                });
        }

        string options;

        if (Columns.Any(x => x.TreeColumnType == ColumnType.CheckBox) && Columns.Any(x => x.TreeColumnType == ColumnType.Custom))
        {
            options = JsonConvert.SerializeObject(new
            {
                hasCheckBoxColumn = true,
                id = Id,
                data = Data,
                IdColumn = KeyColumnName,
                Columns = columns,
                checkedValuesId = "hdn" + Id,
            });
        }
        else if (Columns.Any(x => x.TreeColumnType == ColumnType.CheckBox))
        {
            options = JsonConvert.SerializeObject(new
            {
                hasCheckBoxColumn = true,
                id = Id,
                data = Data,
                checkedValuesId = "hdn" + Id,
            });
        }
        else if (Columns.Any(x => x.TreeColumnType == ColumnType.Custom))
        {
            options = JsonConvert.SerializeObject(new
            {
                hasCheckBoxColumn = false,
                id = Id,
                data = Data,
                IdColumn = KeyColumnName,
                Columns = columns,
            });
        }
        else
        {
            options = JsonConvert.SerializeObject(new { id = Id });
        }

        return string.Format("$('#{0}').treegrid({1});", Id, options);
    }
}
