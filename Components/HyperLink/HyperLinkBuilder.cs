namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkBuilder : ComponentBuilderBase<HyperLinkComponent, HyperLinkBuilder>
    {
        public HyperLinkBuilder(HyperLinkComponent component) : base(component) { }
        public HyperLinkBuilder(HyperLinkComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public HyperLinkBuilder Href(string url) { return this; }
        public HyperLinkBuilder Text(string text) { return this; }
        public HyperLinkBuilder CssClass(string cssClass) { return this; }
        public HyperLinkBuilder Css(string css) { return this; }
        public HyperLinkBuilder Target(string target) { return this; }
        public HyperLinkBuilder Title(string title) { return this; }
        public HyperLinkBuilder ActionUrl(string url) { return this; }
        public HyperLinkBuilder ImageUrl(string url) { return this; }
        public HyperLinkBuilder HtmlAttributes(object attributes) { return this; }
    }
}
