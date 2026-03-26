namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonListBuilder : ComponentBuilderBase<RadioButtonListComponent, RadioButtonListBuilder>
    {
        public RadioButtonListBuilder(RadioButtonListComponent component) : base(component) { }
        public RadioButtonListBuilder(RadioButtonListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
