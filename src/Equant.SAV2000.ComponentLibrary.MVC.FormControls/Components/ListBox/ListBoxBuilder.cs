namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    public class ListBoxBuilder : ComponentBuilderBase<ListBoxComponent, ListBoxBuilder>
    {
        public ListBoxBuilder(ListBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public ListBoxBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public ListBoxBuilder Size(int value) { Component.Size = value; return this; }
        public ListBoxBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
        public ListBoxBuilder SelectedValues(List<string> values) { Component.SelectedValues = values; return this; }

        public ListBoxBuilder DataBind(DataCollection dataSource)
        {
            if (dataSource != null) Component.DataSource = dataSource;
            return this;
        }
    }
}
