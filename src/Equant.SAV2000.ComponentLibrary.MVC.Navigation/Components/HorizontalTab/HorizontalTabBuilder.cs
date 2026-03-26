namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalTabBuilder : ComponentBuilderBase<HorizontalTabComponent, HorizontalTabBuilder>
    {
        public HorizontalTabBuilder(HorizontalTabComponent component) : base(component) { }
        public HorizontalTabBuilder(HorizontalTabComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public HorizontalTabBuilder Tabs(IEnumerable<TabItem> tabs)
        {
            if (tabs != null)
            {
                this.Component.Tabs.Clear();
                foreach (var tab in tabs)
                {
                    this.Component.Tabs.Add(tab);
                }
            }
            return this;
        }

        public HorizontalTabBuilder Tabs(Action<List<TabItem>> configurator)
        {
            if (configurator != null)
            {
                configurator(this.Component.Tabs);
            }
            return this;
        }

        public HorizontalTabBuilder ActiveTab(string activeTab)
        {
            this.Component.ActiveTab = activeTab;
            return this;
        }

        public HorizontalTabBuilder CssClass(string cssClass)
        {
            this.Component.CssClass = cssClass;
            return this;
        }

        public HorizontalTabBuilder OnTabChange(string onTabChange)
        {
            this.Component.OnTabChange = onTabChange;
            return this;
        }
    }
}
