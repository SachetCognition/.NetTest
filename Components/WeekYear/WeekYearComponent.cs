using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

public class WeekYearComponent : ComponentBase
{
    public WeekYearComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        Value = new WeekYearWithFormat();
        OnChange = "null";
    }

    public WeekYearWithFormat Value { get; set; }
    public string? CssClass { get; set; }
    public string? CssClassReadOnly { get; set; }
    public string? OnChange { get; set; }
    public string? Title { get; set; }
    public string? AccessText { get; set; }
    public string? WeekLabel { get; set; }
    public string? YearLabel { get; set; }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsWeekYear",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.WeekYear.js",
                    200,
                    typeof(WeekYearComponent))
            });

    public override IHtmlContent ToHtml()
    {
        return new WeekYearHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { onChange = new JRaw(OnChange) });
        return $"$('#{Id}').weekYear({options});";
    }
}
