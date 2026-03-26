namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomHeaderBuilder : ComponentBuilderBase<CustomHeaderComponent, CustomHeaderBuilder>
    {
        public CustomHeaderBuilder(CustomHeaderComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
