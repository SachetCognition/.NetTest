namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorBuilder : ComponentBuilderBase<ErrorComponents, ErrorBuilder>
    {
        public ErrorBuilder(ErrorComponents component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public ErrorBuilder ErrorCollection(List<ErrorMessageModel> value) { this.Component.ErrorCollection = value; return this; }
        public new ErrorBuilder Id(string value) { this.Component.Id = value; return this; }
        public ErrorBuilder AccessibleText(string value) { this.Component.AccessibleText = value; return this; }
        public ErrorBuilder DivCssClass(string value) { this.Component.DivCssClass = value; return this; }
    }
}
