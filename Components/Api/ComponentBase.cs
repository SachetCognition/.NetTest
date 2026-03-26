namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;

    public abstract class ComponentBase
    {
        private Dictionary<string, object> validationAttributes;

        protected ComponentBase(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
            this.HtmlAttributes = new Dictionary<string, object>();
            this.IsVisible = true;
            this.validationAttributes = new Dictionary<string, object>();
        }

        public HtmlHelper HtmlHelper { get; set; }
        public ModelMetadata ModelMetadata { get; set; }
        public virtual string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, object> HtmlAttributes { get; set; }
        public bool IsVisible { get; set; }
        public string AccessText { get; set; }
        public MvcHtmlString ValidationString { get; set; }
        public bool IsUpdatable { get; set; }
        public string ExternalLabelText { get; set; }

        public virtual ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new List<JsResource>()); }
        }

        public virtual ReadOnlyCollection<CssResource> CssResources
        {
            get { return new ReadOnlyCollection<CssResource>(new List<CssResource>()); }
        }

        public abstract void WriteHtml(HtmlTextWriter writer);
        public abstract void WriteInitScript(HtmlTextWriter writer);

        public virtual string ToHtmlString()
        {
            using (var stringWriter = new StringWriter())
            using (var htmlTextWriter = new HtmlTextWriter(stringWriter))
            {
                WriteHtml(htmlTextWriter);
                return stringWriter.ToString();
            }
        }

        public IDictionary<string, object> GetUnobtrusiveValidationAttributes()
        {
            return validationAttributes;
        }

        public void EnableValidationAttribute()
        {
        }

        public void AddValidationAttribute(string key, object value)
        {
            validationAttributes[key] = value;
        }

        public void AddValidationAttributeProperty(string ruleName, string propertyName, object value)
        {
            var key = ruleName + "-" + propertyName;
            validationAttributes[key] = value;
        }
    }
}
