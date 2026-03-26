namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class HyperLinkBuilder : ComponentBuilderBase<HyperLinkComponent, HyperLinkBuilder>
    {
        public HyperLinkBuilder(HyperLinkComponent component) : base(component) { }
        public HyperLinkBuilder(HyperLinkComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public HyperLinkBuilder ActionUrl(string url) { Component.ActionUrl = url; return this; }
        public HyperLinkBuilder ImageUrl(string url) { Component.ImageUrl = url; return this; }
        public HyperLinkBuilder Title(string title) { Component.Title = title; return this; }
    }
}
