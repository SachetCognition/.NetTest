namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class ImageHtmlBuilder : HtmlBuilderBase<ImageComponent>
    {
        public ImageHtmlBuilder(ImageComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
