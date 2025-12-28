using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;

/// <summary>
/// The class which defines the CheckBox Component.
/// </summary>
public class CheckBoxComponent : ComponentBase
{
    public CheckBoxComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        IsChecked = false;
        IsDisabled = false;
        OnClick = "null";
        OnChange = "null";
        Title = string.Empty;
    }

    public override ReadOnlyCollection<JsResource> JsResources =>
        new ReadOnlyCollection<JsResource>(
            new List<JsResource>
            {
                new JsResource(
                    "JsCheckBox",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBox.js",
                    200,
                    typeof(CheckBoxComponent))
            });

    public string? CssClass { get; set; }

    public string? CssClassDisabled { get; set; }

    public string OnClick { get; set; }

    public string OnChange { get; set; }

    public bool IsChecked
    {
        get => HtmlAttributes.ContainsKey("checked");
        set
        {
            if (value)
            {
                HtmlAttributes["checked"] = "checked";
            }
            else
            {
                if (HtmlAttributes.ContainsKey("checked"))
                {
                    HtmlAttributes.Remove("checked");
                }
            }
        }
    }

    public bool IsDisabled { get; set; }

    public string Title { get; set; }

    public override IHtmlContent ToHtml()
    {
        return new CheckBoxHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        var options = JsonConvert.SerializeObject(new { onClick = new JRaw(OnClick), onChange = new JRaw(OnChange) });
        return $"$('#{Id}').checkBox({options});";
    }
}
