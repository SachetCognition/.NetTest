namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipBuilder : ComponentBuilderBase<ImageToolTipComponent, ImageToolTipBuilder>
    {
        public ImageToolTipBuilder(ImageToolTipComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ImageToolTipBuilder Text(string value)
        {
            Component.Text = value;
            return this;
        }

        public ImageToolTipBuilder ImageUrl(string value)
        {
            Component.ImageUrl = value;
            return this;
        }

        public ImageToolTipBuilder ToolTipText(string value)
        {
            Component.ToolTipText = value;
            return this;
        }

        /// <summary>
        /// </summary>
        public ImageToolTipBuilder CssClassImage(string cssClass)
        {
            Component.CssClassImage = cssClass;
            return this;
        }

        public ImageToolTipBuilder PersistanceMode(PersistanceMode mode)
        {
            Component.PersistanceMode = mode;
            return this;
        }

        public ImageToolTipBuilder Title(string title)
        {
            Component.Title = title;
            return this;
        }

        public ImageToolTipBuilder AlternateText(string altText)
        {
            Component.AlternateText = altText;
            return this;
        }

        public ImageToolTipBuilder Css(string css)
        {
            Component.Css = css;
            return this;
        }

        public ImageToolTipBuilder CssClassSpan(string cssClass)
        {
            Component.CssClassSpan = cssClass;
            return this;
        }

        public ImageToolTipBuilder CssClassInnerSpan(string cssClass)
        {
            Component.CssClassInnerSpan = cssClass;
            return this;
        }
    }
}
