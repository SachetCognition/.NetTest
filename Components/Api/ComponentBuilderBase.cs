using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public abstract class ComponentBuilderBase<TComponent, TBuilder> : IHtmlContent
    where TComponent : ComponentBase
    where TBuilder : ComponentBuilderBase<TComponent, TBuilder>
{
    protected ComponentBuilderBase(TComponent component, ModelMetadata? modelMetadata)
    {
        Component = component ?? throw new ArgumentNullException(nameof(component));
        ModelMetadata = modelMetadata;
    }

    protected TComponent Component { get; }

    protected ModelMetadata? ModelMetadata { get; }

    public TBuilder Id(string value)
    {
        Component.Id = value;
        return (TBuilder)this;
    }

    public TBuilder Name(string value)
    {
        Component.Name = value;
        return (TBuilder)this;
    }

    public TBuilder Visible(bool value)
    {
        Component.IsVisible = value;
        return (TBuilder)this;
    }

    public TBuilder HtmlAttribute(string key, object value)
    {
        Component.HtmlAttributes[key] = value;
        return (TBuilder)this;
    }

    public TBuilder HtmlAttributes(IDictionary<string, object> attributes)
    {
        foreach (var attr in attributes)
        {
            Component.HtmlAttributes[attr.Key] = attr.Value;
        }
        return (TBuilder)this;
    }

    public void WriteTo(TextWriter writer, System.Text.Encodings.Web.HtmlEncoder encoder)
    {
        var html = Component.ToHtml();
        html.WriteTo(writer, encoder);
    }

    public IHtmlContent ToHtml()
    {
        return Component.ToHtml();
    }

    public string ToInitScript()
    {
        return Component.ToInitScript();
    }
}
