namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class FileUploadHtmlBuilder : HtmlBuilderBase<FileUploadComponent>
    {
        public FileUploadHtmlBuilder(FileUploadComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
