namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public enum PersistanceMode
    {
        Click = 0,
        Hover = 1
    }

    public class ImageToolTipBuilder : ComponentBuilderBase<ImageToolTipComponent, ImageToolTipBuilder>
    {
        public ImageToolTipBuilder(ImageToolTipComponent component) : base(component) { }
        public ImageToolTipBuilder(ImageToolTipComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public ImageToolTipBuilder Text(string text) { Component.Text = text; return this; }
        public ImageToolTipBuilder ImageUrl(string url) { Component.ImageUrl = url; return this; }
        public ImageToolTipBuilder ToolTipId(string id) { Component.ToolTipId = id; return this; }
        public ImageToolTipBuilder CssClassImage(string cssClass) { Component.CssClass = cssClass; return this; }
        public ImageToolTipBuilder PersistanceMode(PersistanceMode mode) { Component.PersistanceMode = mode; return this; }
        public ImageToolTipBuilder Title(string title) { Component.Title = title; return this; }
        public ImageToolTipBuilder AlternateText(string text) { Component.ImageAlt = text; return this; }
        public ImageToolTipBuilder Css(string css) { return this; }
        public ImageToolTipBuilder CssClassSpan(string css) { Component.CssClassSpan = css; return this; }
        public ImageToolTipBuilder CssClassInnerSpan(string css) { Component.CssClassInnerSpan = css; return this; }
    }
}
