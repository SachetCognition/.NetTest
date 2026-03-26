namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    public class ErrorBuilder
    {
        private ErrorComponents _component;
        private ModelMetadata _modelMetadata;

        public ErrorBuilder(ErrorComponents component, ModelMetadata modelMetadata)
        {
            _component = component;
            _modelMetadata = modelMetadata;
        }

        public ErrorBuilder ErrorCollection(List<ErrorMessageModel> errors) { return this; }
        public ErrorBuilder Id(string id) { _component.Id = id; return this; }
        public ErrorBuilder AccessibleText(string text) { return this; }
        public ErrorBuilder DivCssClass(string cssClass) { return this; }
    }
}
