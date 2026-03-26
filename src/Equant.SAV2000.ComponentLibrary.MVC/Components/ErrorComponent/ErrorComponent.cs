namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorComponent : ComponentBase
    {
        public ErrorComponent() : base() { }
        public ErrorComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public List<ErrorMessageModel> ErrorMessages { get; set; } = new List<ErrorMessageModel>();
        public string CssClass { get; set; } = "error-container";

        public override void WriteHtml(TextWriter writer)
        {
            new ErrorHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(TextWriter writer) { }
    }

    public class ErrorComponents : ComponentBase
    {
        public ErrorComponents() : base() { }
        public ErrorComponents(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public List<ErrorMessageModel> ErrorMessages { get; set; } = new List<ErrorMessageModel>();
        public string CssClass { get; set; } = "error-container";

        public override void WriteHtml(TextWriter writer)
        {
            new ErrorHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(TextWriter writer) { }
    }

    public class ErrorHtmlBuilder : HtmlBuilderBase<ComponentBase>
    {
        private readonly List<ErrorMessageModel> errorMessages;
        private readonly string cssClass;

        public ErrorHtmlBuilder(ErrorComponent component)
        {
            this.Component = component;
            this.errorMessages = component.ErrorMessages;
            this.cssClass = component.CssClass;
        }

        public ErrorHtmlBuilder(ErrorComponents component)
        {
            this.Component = component;
            this.errorMessages = component.ErrorMessages;
            this.cssClass = component.CssClass;
        }

        public override void Build(TextWriter writer)
        {
            if (writer != null && this.Component.IsVisible && this.errorMessages != null && this.errorMessages.Count > 0)
            {
                var container = new TagBuilder("div");
                container.AddCssClass(this.cssClass);
                container.Attributes["id"] = this.Component.Id;

                foreach (var error in this.errorMessages)
                {
                    var errorDiv = new TagBuilder("div");
                    errorDiv.AddCssClass("error-message");
                    errorDiv.InnerHtml.AppendHtml(error.ErrorHtml ?? string.Empty);

                    using (var sw = new StringWriter())
                    {
                        errorDiv.WriteTo(sw, HtmlEncoder.Default);
                        container.InnerHtml.AppendHtml(sw.ToString());
                    }
                }

                container.WriteTo(writer, HtmlEncoder.Default);
            }
        }
    }
}
