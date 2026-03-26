namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class FileUploadComponent : ComponentBase
    {
        public FileUploadComponent() : base() { }
        public FileUploadComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string CssClass { get; set; }
        public string Accept { get; set; }
        public bool AllowMultiple { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new FileUploadHtmlBuilder(this).Build();
        }

        public override string WriteInitScript() { return string.Empty; }
    }
}
