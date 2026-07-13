namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextBoxBuilder : ComponentBuilderBase<TextBoxComponent, TextBoxBuilder>
    {
        public TextBoxBuilder(TextBoxComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
