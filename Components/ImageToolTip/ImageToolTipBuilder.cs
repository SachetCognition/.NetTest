namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipBuilder : ComponentBuilderBase<ImageToolTipComponent, ImageToolTipBuilder>
    {
        public ImageToolTipBuilder(ImageToolTipComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public ImageToolTipBuilder Text(string value) { this.Component.Text = value; return this; }
        public ImageToolTipBuilder ImageUrl(string value) { this.Component.ImageUrl = value; return this; }
        public ImageToolTipBuilder ToolTipId(string value) { this.Component.ToolTipId = value; return this; }
        public ImageToolTipBuilder Title(string value) { this.Component.Title = value; return this; }
        public ImageToolTipBuilder AlternateText(string value) { this.Component.AlternateText = value; return this; }
        public ImageToolTipBuilder Css(string value) { this.Component.Css = value; return this; }
        public ImageToolTipBuilder CssClassSpan(string value) { this.Component.CssClassSpan = value; return this; }
        public ImageToolTipBuilder CssClassInnerSpan(string value) { this.Component.CssClassInnerSpan = value; return this; }
        public ImageToolTipBuilder PersistanceMode(PersistanceMode value) { this.Component.PersistanceMode = value; return this; }
        public ImageToolTipBuilder CssClassImage(string value) { this.Component.CssClassImage = value; return this; }
    }
}
