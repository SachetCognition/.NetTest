namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class RadioButtonListBuilder : ComponentBuilderBase<RadioButtonListComponent, RadioButtonListBuilder>
    {
        public RadioButtonListBuilder(RadioButtonListComponent component) : base(component) { }
        public RadioButtonListBuilder(RadioButtonListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
