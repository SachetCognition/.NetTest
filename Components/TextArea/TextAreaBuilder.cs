namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class TextAreaBuilder : ComponentBuilderBase<TextAreaComponent, TextAreaBuilder>
    {
        public TextAreaBuilder(TextAreaComponent component) : base(component) { }
        public TextAreaBuilder(TextAreaComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
