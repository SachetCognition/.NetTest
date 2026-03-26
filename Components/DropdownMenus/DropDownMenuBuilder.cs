namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DropDownMenuBuilder : ComponentBuilderBase<DropDownMenuComponent, DropDownMenuBuilder>
    {
        public DropDownMenuBuilder(DropDownMenuComponent component) : base(component) { }
        public DropDownMenuBuilder(DropDownMenuComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
