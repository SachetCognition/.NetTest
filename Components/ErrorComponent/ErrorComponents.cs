namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorComponents : ComponentBase
    {
        public ErrorComponents(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<div id=\"{Id}\" class=\"error\"></div>");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }

    public class ErrorBuilder : ComponentBuilderBase<ErrorComponents, ErrorBuilder>
    {
        public ErrorBuilder(ErrorComponents component, Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ErrorBuilder ErrorCollection(System.Collections.Generic.List<ErrorMessageModel> errors)
        {
            return this;
        }

        public ErrorBuilder AccessibleText(string? text)
        {
            return this;
        }

        public ErrorBuilder DivCssClass(string cssClass)
        {
            return this;
        }
    }

    public class ErrorMessageModel
    {
        public string ErrorHtml { get; set; }
    }
}
