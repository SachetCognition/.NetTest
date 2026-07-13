namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomValidator
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomValidatorBuilder : ComponentBuilderBase<CustomValidatorComponent, CustomValidatorBuilder>
    {
        public CustomValidatorBuilder(CustomValidatorComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
