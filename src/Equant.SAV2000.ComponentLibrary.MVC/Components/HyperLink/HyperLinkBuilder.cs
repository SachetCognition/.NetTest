namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkBuilder : ComponentBuilderBase<HyperLinkComponent, HyperLinkBuilder>
    {
        public HyperLinkBuilder(HyperLinkComponent component) : base(component) { }
        public HyperLinkBuilder(HyperLinkComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public HyperLinkBuilder Href(string url) { Component.Href = url; return this; }
        public HyperLinkBuilder Text(string text) { Component.Text = text; return this; }
        public HyperLinkBuilder CssClass(string cssClass) { Component.CssClass = cssClass; return this; }
        public HyperLinkBuilder Css(string css) { Component.CssClass = css; return this; }
        public HyperLinkBuilder Target(string target) { Component.Target = target; return this; }
        public HyperLinkBuilder Title(string title) { Component.Title = title; return this; }
        public HyperLinkBuilder ActionUrl(string url) { Component.ActionUrl = url; return this; }
        public HyperLinkBuilder ImageUrl(string url) { Component.ImageUrl = url; return this; }
        public HyperLinkBuilder HtmlAttributes(object attributes)
        {
            if (attributes != null)
            {
                foreach (var prop in attributes.GetType().GetProperties())
                {
                    Component.HtmlAttributes[prop.Name] = prop.GetValue(attributes);
                }
            }
            return this;
        }
    }
}
