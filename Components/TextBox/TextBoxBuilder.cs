namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class TextBoxBuilder : ComponentBuilderBase<TextBoxComponent, TextBoxBuilder>
    {
        public TextBoxBuilder(TextBoxComponent component) : base(component) { }
        public TextBoxBuilder(TextBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
