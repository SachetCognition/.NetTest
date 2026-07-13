namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageHtmlBuilder : HtmlBuilderBase<ImageComponent>
    {
        public ImageHtmlBuilder(ImageComponent component) { this.Component = component; }
        public override void Build(HtmlTextWriter writer) { }
    }
}
