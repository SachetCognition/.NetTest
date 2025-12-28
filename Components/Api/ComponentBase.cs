using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public abstract class ComponentBase : IComponent
{
    private readonly IHtmlHelper _htmlHelper;

    protected ComponentBase(IHtmlHelper htmlHelper)
    {
        _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        HtmlAttributes = new Dictionary<string, object>();
        IsVisible = true;
        Id = string.Empty;
    }

    public IHtmlHelper HtmlHelper => _htmlHelper;

    public virtual string Id { get; set; }

    public string? Name { get; set; }

    public bool IsVisible { get; set; }

    public bool IsDisabled { get; set; }

    public bool IsReadOnly { get; set; }

    public IHtmlContent? ValidationString { get; set; }

    public ModelMetadata? ModelMetadata { get; set; }

    public IDictionary<string, object> HtmlAttributes { get; }

    private Dictionary<string, object> _validationAttributes = new Dictionary<string, object>();
    private Dictionary<string, Dictionary<string, object>> _validationAttributeProperties = new Dictionary<string, Dictionary<string, object>>();

    public virtual ReadOnlyCollection<JsResource> JsResources => new ReadOnlyCollection<JsResource>(new List<JsResource>());

    public virtual ReadOnlyCollection<CssResource> CssResources => new ReadOnlyCollection<CssResource>(new List<CssResource>());

    public abstract IHtmlContent ToHtml();

    public abstract string ToInitScript();

    public virtual string ToHtmlString()
    {
        var htmlContent = ToHtml();
        using var writer = new StringWriter();
        htmlContent.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        return writer.ToString();
    }

    public void EnableValidationAttribute()
    {
        HtmlAttributes["data-val"] = "true";
    }

    public void AddValidationAttribute(string key, object value)
    {
        _validationAttributes[key] = value;
        HtmlAttributes[$"data-val-{key}"] = value;
    }

    public void AddValidationAttributeProperty(string ruleName, string propertyName, object value)
    {
        if (!_validationAttributeProperties.ContainsKey(ruleName))
        {
            _validationAttributeProperties[ruleName] = new Dictionary<string, object>();
        }
        _validationAttributeProperties[ruleName][propertyName] = value;
        HtmlAttributes[$"data-val-{ruleName}-{propertyName}"] = value;
    }

    public IDictionary<string, object> GetUnobtrusiveValidationAttributes()
    {
        return _validationAttributes;
    }

    protected virtual void WriteHtml(StringBuilder writer)
    {
    }

    protected virtual void WriteInitScript(StringBuilder writer)
    {
    }
}
