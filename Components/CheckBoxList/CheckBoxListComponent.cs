using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList;

public class CheckBoxListComponent : ComponentBase
{
    public CheckBoxListComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        SourceItems = new List<CheckBoxListItem>();
        CheckBoxListLabel = new SpanLabelComponent(htmlHelper);
        IsDisabled = false;
        OnClick = "null";
        OnChange = "null";
        Title = string.Empty;
    }

    public string? CssClassFieldSet { get; set; }
    public string? CssClass { get; set; }
    public string? CssClassDisabled { get; set; }
    public string? OnClick { get; set; }
    public string? OnChange { get; set; }
    public new bool IsDisabled { get; set; }
    public string? Title { get; set; }
    public string? CssClassOuterDiv { get; set; }
    public bool IsOuterDivNeeded { get; set; }
    public string? CssClassCheckBoxDiv { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsCheckBoxList",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBoxList.js",
                    200,
                    typeof(CheckBoxListComponent))
            });

    public List<CheckBoxListItem> SourceItems { get; set; }
    public SpanLabelComponent CheckBoxListLabel { get; set; }

    public override IHtmlContent ToHtml()
    {
        return new CheckBoxListHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { onClick = new JRaw(OnClick), onChange = new JRaw(OnChange) });
        return $"$('#{Id}').checkBoxList({options});";
    }
}
