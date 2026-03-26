namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class FileUploadHtmlBuilder : HtmlBuilderBase<FileUploadComponent>
    {
        public FileUploadHtmlBuilder(FileUploadComponent component) : base(component) { }
        public override void Build(HtmlTextWriter writer) { }
    }
}
