namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text.Encodings.Web;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public abstract class ComponentBase
    {
        public ComponentBase() { }

        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public string AccessText { get; set; }
        public string AccessHrText { get; set; }
        public string AccessMinText { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsDisabled { get; set; }
        public bool IsUpdatable { get; set; } = true;
        public bool IsMandatory { get; set; }
        public string MandatoryMessage { get; set; }
        public string ExternalLabelText { get; set; }
        public Dictionary<string, object> HtmlAttributes { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> ConditionalAnnotations { get; set; } = new Dictionary<string, object>();
        public virtual ReadOnlyCollection<JsResource> JsResources { get { return new List<JsResource>().AsReadOnly(); } }
        public virtual ReadOnlyCollection<CssResource> CssResources { get { return new List<CssResource>().AsReadOnly(); } }
        public ModelMetadata ModelMetadata { get; set; }
        public IHtmlContent ValidationString { get; set; }

        public virtual string ToHtmlString()
        {
            return string.Empty;
        }

        public IDictionary<string, object> GetUnobtrusiveValidationAttributes()
        {
            return new Dictionary<string, object>();
        }

        public void AddValidationAttribute(string key, string value)
        {
            if (!HtmlAttributes.ContainsKey(key))
                HtmlAttributes[key] = value;
        }

        public void AddValidationAttributeProperty(string key, string value)
        {
            if (!HtmlAttributes.ContainsKey(key))
                HtmlAttributes[key] = value;
        }

        public void AddValidationAttributeProperty(string key, string value, string extra)
        {
            if (!HtmlAttributes.ContainsKey(key))
                HtmlAttributes[key] = value;
        }

        public abstract IHtmlContent RenderHtml();
        public abstract string RenderInitScript();
    }
}
