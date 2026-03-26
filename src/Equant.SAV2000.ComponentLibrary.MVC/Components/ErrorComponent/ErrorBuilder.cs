namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorBuilder : ComponentBuilderBase<ErrorComponent, ErrorBuilder>
    {
        public ErrorBuilder(ErrorComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ErrorBuilder(ErrorComponent component)
            : base(component)
        {
        }

        public ErrorBuilder ErrorMessages(List<ErrorMessageModel> value) { this.Component.ErrorMessages = value; return this; }
        public ErrorBuilder CssClass(string value) { this.Component.CssClass = value; return this; }
    }
}
