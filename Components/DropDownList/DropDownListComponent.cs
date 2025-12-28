using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

public class DropDownListComponent : ComponentBase
{
    public DropDownListComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Items = new List<SelectListItem>();
        OnChange = "null";
        CustomLabel = new CustomLabelComponent(htmlHelper);
    }

    public List<SelectListItem> Items { get; set; }
    public string? SelectedValue { get; set; }
    public string? CssClass { get; set; }
    public string? CssClassReadOnly { get; set; }
    public string? OnChange { get; set; }
    public string? Title { get; set; }
    public string? AccessText { get; set; }
    public bool IsDivNeeded { get; set; } = true;
    public CustomLabelComponent CustomLabel { get; set; }
    public string? CssClassSelectDiv { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsDropDownList",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DropDownList.js",
                    200,
                    typeof(DropDownListComponent))
            });

    public override IHtmlContent ToHtml()
    {
        return new DropDownListHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { onChange = new JRaw(OnChange) });
        return $"$('#{Id}').dropDownList({options});";
    }
}
