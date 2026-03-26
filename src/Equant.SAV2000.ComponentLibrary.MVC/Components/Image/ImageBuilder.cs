namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageBuilder : ComponentBuilderBase<ImageComponent, ImageBuilder>
    {
        public ImageBuilder(ImageComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ImageBuilder(ImageComponent component)
            : base(component)
        {
        }

        public ImageBuilder Src(string value) { this.Component.Src = value; return this; }
        public ImageBuilder Alt(string value) { this.Component.Alt = value; return this; }
        public ImageBuilder Title(string value) { this.Component.Title = value; return this; }
        public ImageBuilder Width(int value) { this.Component.Width = value; return this; }
        public ImageBuilder Height(int value) { this.Component.Height = value; return this; }
    }
}
