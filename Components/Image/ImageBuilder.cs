namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageBuilder : ComponentBuilderBase<ImageComponent, ImageBuilder>
    {
        public ImageBuilder(ImageComponent component) : base(component) { }
        public ImageBuilder(ImageComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
