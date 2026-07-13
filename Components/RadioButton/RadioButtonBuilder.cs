namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonBuilder : ComponentBuilderBase<RadioButtonComponent, RadioButtonBuilder>
    {
        public RadioButtonBuilder(RadioButtonComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
