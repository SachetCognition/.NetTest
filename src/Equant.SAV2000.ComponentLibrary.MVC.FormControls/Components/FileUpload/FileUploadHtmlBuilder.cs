namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class FileUploadHtmlBuilder : HtmlBuilderBase<FileUploadComponent>
    {
        public FileUploadHtmlBuilder(FileUploadComponent component) { this.Component = component; }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible) return HtmlString.Empty;

            var tag = new TagBuilder("input");
            tag.MergeAttribute("id", this.Component.Id);
            if (!string.IsNullOrEmpty(this.Component.Name))
                tag.MergeAttribute("name", this.Component.Name);
            tag.MergeAttribute("type", "file");

            if (!string.IsNullOrEmpty(this.Component.Accept))
                tag.MergeAttribute("accept", this.Component.Accept);
            if (this.Component.AllowMultiple)
                tag.MergeAttribute("multiple", "multiple");
            if (this.Component.IsDisabled)
                tag.MergeAttribute("disabled", "disabled");
            if (!string.IsNullOrEmpty(this.Component.CssClass))
                tag.AddCssClass(this.Component.CssClass);
            tag.MergeAttributes(this.Component.HtmlAttributes);

            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return tag;
        }
    }
}
