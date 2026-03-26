namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalMenuBuilder : ComponentBuilderBase<HorizontalMenuComponent, HorizontalMenuBuilder>
    {
        public HorizontalMenuBuilder(HorizontalMenuComponent component) : base(component) { }
        public HorizontalMenuBuilder(HorizontalMenuComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
