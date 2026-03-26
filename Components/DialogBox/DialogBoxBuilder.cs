namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DialogBoxBuilder : ComponentBuilderBase<DialogBoxComponent, DialogBoxBuilder>
    {
        public DialogBoxBuilder(DialogBoxComponent component) : base(component) { }
        public DialogBoxBuilder(DialogBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
