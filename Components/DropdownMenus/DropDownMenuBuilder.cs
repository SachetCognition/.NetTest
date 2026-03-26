namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownMenuBuilder : ComponentBuilderBase<DropDownMenuComponent, DropDownMenuBuilder>
    {
        public DropDownMenuBuilder(DropDownMenuComponent component) : base(component) { }
        public DropDownMenuBuilder(DropDownMenuComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
