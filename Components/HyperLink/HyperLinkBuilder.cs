namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkBuilder : ComponentBuilderBase<HyperLinkComponent, HyperLinkBuilder>
    {
        public HyperLinkBuilder(HyperLinkComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public HyperLinkBuilder Text(string value) { this.Component.Text = value; return this; }
        public HyperLinkBuilder Href(string value) { this.Component.Href = value; return this; }
        public HyperLinkBuilder CssClass(string value) { this.Component.CssClass = value; return this; }
        public HyperLinkBuilder ImageUrl(string value) { this.Component.ImageUrl = value; return this; }
        public HyperLinkBuilder Title(string value) { this.Component.Title = value; return this; }
        public HyperLinkBuilder ActionUrl(string value) { this.Component.ActionUrl = value; return this; }
        public HyperLinkBuilder Target(string value) { this.Component.Target = value; return this; }
        public HyperLinkBuilder OnClick(string value) { this.Component.OnClick = value; return this; }
        public HyperLinkBuilder Css(string value) { this.Component.CssClass = value; return this; }
    }
}
