namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Html;

    public class ErrorComponents : ComponentBase
    {
        public ErrorComponents() { }
        public override IHtmlContent RenderHtml() { return HtmlString.Empty; }
        public override string RenderInitScript() { return string.Empty; }
    }

    public class ErrorMessageModel
    {
        public string ErrorHtml { get; set; }
    }

    public class ErrorBuilder : ComponentBuilderBase<ErrorComponents, ErrorBuilder>
    {
        public ErrorBuilder(ErrorComponents component) : base(component) { }
        public ErrorBuilder(ErrorComponents component, Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public ErrorBuilder ErrorCollection(System.Collections.Generic.List<ErrorMessageModel> errors) { return this; }
        public ErrorBuilder AccessibleText(string text) { return this; }
        public ErrorBuilder DivCssClass(string css) { return this; }
    }
}
