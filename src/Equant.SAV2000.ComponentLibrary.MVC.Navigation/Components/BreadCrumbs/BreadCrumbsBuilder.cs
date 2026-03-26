namespace Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class BreadCrumbsBuilder : ComponentBuilderBase<BreadCrumbsComponent, BreadCrumbsBuilder>
    {
        public BreadCrumbsBuilder(BreadCrumbsComponent component) : base(component) { }
        public BreadCrumbsBuilder(BreadCrumbsComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public BreadCrumbsBuilder Items(IEnumerable<BreadCrumbsItem> items)
        {
            if (items != null)
            {
                this.Component.Items.Clear();
                foreach (var item in items)
                {
                    this.Component.Items.Add(item);
                }
            }
            return this;
        }

        public BreadCrumbsBuilder Items(Action<List<BreadCrumbsItem>> configurator)
        {
            if (configurator != null)
            {
                configurator(this.Component.Items);
            }
            return this;
        }

        public BreadCrumbsBuilder Separator(string separator)
        {
            this.Component.Separator = separator;
            return this;
        }

        public BreadCrumbsBuilder CssClass(string cssClass)
        {
            this.Component.CssClass = cssClass;
            return this;
        }
    }
}
