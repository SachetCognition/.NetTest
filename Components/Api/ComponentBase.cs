namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;

    public abstract class ComponentBase : IComponent
    {
        protected ComponentBase(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
            this.HtmlAttributes = new Dictionary<string, object>();
            this.ValidationString = MvcHtmlString.Empty;
            this.IsVisible = true;
            this.IsUpdatable = true;
        }

        public HtmlHelper HtmlHelper { get; private set; }

        public virtual string Id { get; set; }

        public string Name { get; set; }

        public bool IsVisible { get; set; }

        public bool IsUpdatable { get; set; }

        public IDictionary<string, object> HtmlAttributes { get; private set; }

        public ModelMetadata ModelMetadata { get; set; }

        public MvcHtmlString ValidationString { get; set; }

        public string AccessText { get; set; }

        public virtual ReadOnlyCollection<JsResource> JsResources
        {
            get { return new List<JsResource>().AsReadOnly(); }
        }

        public virtual ReadOnlyCollection<CssResource> CssResources
        {
            get { return new List<CssResource>().AsReadOnly(); }
        }

        public abstract void WriteHtml(HtmlTextWriter writer);

        public abstract void WriteInitScript(HtmlTextWriter writer);

        public string ToHtmlString()
        {
            using (var stringWriter = new StringWriter())
            using (var htmlWriter = new HtmlTextWriter(stringWriter))
            {
                this.WriteHtml(htmlWriter);
                this.WriteInitScript(htmlWriter);
                return stringWriter.ToString();
            }
        }

        public IDictionary<string, object> GetUnobtrusiveValidationAttributes()
        {
            if (this.HtmlHelper != null && this.ModelMetadata != null && !string.IsNullOrEmpty(this.Name))
            {
                return this.HtmlHelper.GetUnobtrusiveValidationAttributes(this.Name, this.ModelMetadata);
            }
            return new Dictionary<string, object>();
        }

        public void EnableValidationAttribute()
        {
            if (!this.HtmlAttributes.ContainsKey("data-val"))
            {
                this.HtmlAttributes.Add("data-val", "true");
            }
        }

        public void AddValidationAttribute(string key, string value)
        {
            if (!this.HtmlAttributes.ContainsKey(key))
            {
                this.HtmlAttributes.Add(key, value);
            }
        }

        public void AddValidationAttributeProperty(string key, string value)
        {
            if (!this.HtmlAttributes.ContainsKey(key))
            {
                this.HtmlAttributes.Add(key, value);
            }
        }

        public void AddValidationAttributeProperty(string ruleName, string propertyName, string value)
        {
            var key = "data-val-" + ruleName + "-" + propertyName;
            if (!this.HtmlAttributes.ContainsKey(key))
            {
                this.HtmlAttributes.Add(key, value);
            }
        }
    }
}
