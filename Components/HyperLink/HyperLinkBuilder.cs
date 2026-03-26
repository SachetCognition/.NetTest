namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HyperLinkBuilder : ComponentBuilderBase<HyperLinkComponent, HyperLinkBuilder>
    {
        public HyperLinkBuilder(HyperLinkComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public HyperLinkBuilder ActionUrl(string value) { Component.ActionUrl = value; return this; }
        public HyperLinkBuilder Title(string value) { Component.Title = value; return this; }
        public HyperLinkBuilder Css(string value) { Component.CssClass = value; return this; }
        public HyperLinkBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public HyperLinkBuilder ImageUrl(string value) { Component.ImageUrl = value; return this; }
        public HyperLinkBuilder Text(string value) { Component.Text = value; return this; }
        public HyperLinkBuilder OnClick(string value) { Component.OnClick = value; return this; }
        public HyperLinkBuilder Target(string value) { Component.Target = value; return this; }
    }
}
