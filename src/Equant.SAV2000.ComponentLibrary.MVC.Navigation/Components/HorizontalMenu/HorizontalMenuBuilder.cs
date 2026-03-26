namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalMenuBuilder : ComponentBuilderBase<HorizontalMenuComponent, HorizontalMenuBuilder>
    {
        public HorizontalMenuBuilder(HorizontalMenuComponent component) : base(component) { }
        public HorizontalMenuBuilder(HorizontalMenuComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public HorizontalMenuBuilder MenuItems(IEnumerable<HorizontalMenuItemInfo> items)
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

        public HorizontalMenuBuilder MenuItems(Action<List<HorizontalMenuItemInfo>> configurator)
        {
            if (configurator != null)
            {
                configurator(this.Component.MenuItems);
            }
            return this;
        }

        public HorizontalMenuBuilder ActiveItem(string activeItem)
        {
            this.Component.ActiveItem = activeItem;
            return this;
        }

        public HorizontalMenuBuilder CssClass(string cssClass)
        {
            this.Component.CssClass = cssClass;
            return this;
        }

        public HorizontalMenuBuilder OnItemClick(string onClick)
        {
            this.Component.OnItemClick = onClick;
            return this;
        }
    }
}
