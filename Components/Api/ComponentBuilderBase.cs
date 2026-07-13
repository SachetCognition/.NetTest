namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    public abstract class ComponentBuilderBase<TComponent, TBuilder> : IHtmlString
        where TComponent : ComponentBase
        where TBuilder : ComponentBuilderBase<TComponent, TBuilder>
    {
        protected ComponentBuilderBase(TComponent component, ModelMetadata modelMetadata)
        {
            this.Component = component;
            if (component != null)
            {
                component.ModelMetadata = modelMetadata;
            }
        }

        public TComponent Component { get; protected set; }

        public virtual TBuilder Id(string value)
        {
            this.Component.Id = value;
            return (TBuilder)this;
        }

        public TBuilder Name(string value)
        {
            this.Component.Name = value;
            return (TBuilder)this;
        }

        public TBuilder IsVisible(bool value)
        {
            this.Component.IsVisible = value;
            return (TBuilder)this;
        }

        public TBuilder IsUpdatable(bool value)
        {
            this.Component.IsUpdatable = value;
            return (TBuilder)this;
        }

        public TBuilder AccessText(string value)
        {
            this.Component.AccessText = value;
            return (TBuilder)this;
        }

        public TBuilder HtmlAttributes(IDictionary<string, object> attributes)
        {
            if (attributes != null)
            {
                foreach (var kvp in attributes)
                {
                    this.Component.HtmlAttributes[kvp.Key] = kvp.Value;
                }
            }
            return (TBuilder)this;
        }

        public TBuilder HtmlAttributes(object attributes)
        {
            if (attributes != null)
            {
                var dict = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                foreach (var kvp in dict)
                {
                    this.Component.HtmlAttributes[kvp.Key] = kvp.Value;
                }
            }
            return (TBuilder)this;
        }

        public string ToHtmlString()
        {
            using (var stringWriter = new StringWriter())
            using (var htmlWriter = new HtmlTextWriter(stringWriter))
            {
                this.Component.WriteHtml(htmlWriter);
                using (var scriptWriter = new StringWriter())
                using (var scriptHtmlWriter = new HtmlTextWriter(scriptWriter))
                {
                    this.Component.WriteInitScript(scriptHtmlWriter);
                    var initScript = scriptWriter.ToString();
                    if (!string.IsNullOrWhiteSpace(initScript))
                    {
                        htmlWriter.WriteLine("<script>jQuery(function () {{ {0} }});</script>", initScript);
                    }
                }

                return stringWriter.ToString();
            }
        }
    }
}
