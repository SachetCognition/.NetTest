namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownMenuBuilder : ComponentBuilderBase<DropDownMenuComponent, DropDownMenuBuilder>
    {
        public DropDownMenuBuilder(DropDownMenuComponent component) : base(component) { }
        public DropDownMenuBuilder(DropDownMenuComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public DropDownMenuBuilder MenuItems(IEnumerable<DropdownMenu> items)
        {
            if (items != null)
            {
                this.Component.MenuItems.Clear();
                foreach (var item in items)
                {
                    this.Component.MenuItems.Add(item);
                }
            }
            return this;
        }

        public DropDownMenuBuilder MenuItems(Action<List<DropdownMenu>> configurator)
        {
            if (configurator != null)
            {
                configurator(this.Component.MenuItems);
            }
            return this;
        }

        public DropDownMenuBuilder CssClass(string cssClass)
        {
            this.Component.CssClass = cssClass;
            return this;
        }

        public DropDownMenuBuilder OnItemClick(string onClick)
        {
            this.Component.OnItemClick = onClick;
            return this;
        }
    }
}
