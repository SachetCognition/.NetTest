namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipBuilder : ComponentBuilderBase<ImageToolTipComponent, ImageToolTipBuilder>
    {
        public ImageToolTipBuilder(ImageToolTipComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public ImageToolTipBuilder ToolTip(string value) { Component.ToolTip = value; return this; }
        public ImageToolTipBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public ImageToolTipBuilder Text(string value) { Component.Text = value; return this; }
        public ImageToolTipBuilder ImageUrl(string value) { Component.ImageUrl = value; return this; }
        public ImageToolTipBuilder CssClassImage(string value) { Component.CssClassImage = value; return this; }
        public ImageToolTipBuilder PersistanceMode(PersistanceMode value) { Component.PersistanceMode = value; return this; }
        public ImageToolTipBuilder Title(string value) { Component.ToolTip = value; return this; }
        public ImageToolTipBuilder AlternateText(string value) { return this; }
        public ImageToolTipBuilder Css(string value) { Component.CssClass = value; return this; }
        public ImageToolTipBuilder CssClassSpan(string value) { return this; }
        public ImageToolTipBuilder CssClassInnerSpan(string value) { return this; }
    }
}
