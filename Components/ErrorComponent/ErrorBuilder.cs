namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorBuilder : ComponentBuilderBase<ErrorComponents, ErrorBuilder>
    {
        public ErrorBuilder(ErrorComponents component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public ErrorBuilder ErrorCollection(List<ErrorMessageModel> errors) { return this; }
        public ErrorBuilder AccessibleText(string value) { return this; }
        public ErrorBuilder DivCssClass(string value) { return this; }
    }
}
