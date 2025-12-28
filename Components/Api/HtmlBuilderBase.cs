using System.Text;
using Microsoft.AspNetCore.Html;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public abstract class HtmlBuilderBase<TComponent> : IHtmlBuilder
    where TComponent : ComponentBase
{
    protected TComponent Component { get; set; } = default!;

    public abstract IHtmlContent Build();

    protected static HtmlString CreateHtmlString(string html)
    {
        return new HtmlString(html);
    }

    protected static void AppendAttribute(StringBuilder sb, string name, string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            sb.Append($" {name}=\"{System.Web.HttpUtility.HtmlAttributeEncode(value)}\"");
        }
    }

    protected static void AppendAttribute(StringBuilder sb, string name, object? value)
    {
        if (value != null)
        {
            sb.Append($" {name}=\"{System.Web.HttpUtility.HtmlAttributeEncode(value.ToString())}\"");
        }
    }
}
