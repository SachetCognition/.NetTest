namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ImageBuilder : ComponentBuilderBase<ImageComponent, ImageBuilder>
    {
        public ImageBuilder(ImageComponent component) : base(component) { }
        public ImageBuilder(ImageComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
