namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class RadioButtonBuilder : ComponentBuilderBase<RadioButtonComponent, RadioButtonBuilder>
    {
        public RadioButtonBuilder(RadioButtonComponent component) : base(component) { }
        public RadioButtonBuilder(RadioButtonComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
