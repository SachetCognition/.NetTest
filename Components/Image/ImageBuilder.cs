namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageBuilder : ComponentBuilderBase<ImageComponent, ImageBuilder>
    {
        public ImageBuilder(ImageComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ImageBuilder Src(string src)
        {
            this.Component.Src = src;
            return this;
        }

        public ImageBuilder Alt(string alt)
        {
            this.Component.Alt = alt;
            return this;
        }

        public ImageBuilder CssClass(string cssClass)
        {
            this.Component.CssClass = cssClass;
            return this;
        }

        public ImageBuilder Width(int width)
        {
            this.Component.Width = width;
            return this;
        }

        public ImageBuilder Height(int height)
        {
            this.Component.Height = height;
            return this;
        }
    }
}
