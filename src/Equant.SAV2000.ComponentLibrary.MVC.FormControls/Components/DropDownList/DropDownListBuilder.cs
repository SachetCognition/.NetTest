namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownListBuilder : ComponentBuilderBase<DropDownListComponent, DropDownListBuilder>
    {
        public DropDownListBuilder(DropDownListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public DropDownListBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public DropDownListBuilder CssClassReadOnly(string value) { Component.CssClassReadOnly = value; return this; }
        public DropDownListBuilder SelectedValue(string value) { Component.SelectedValue = value; return this; }
        public DropDownListBuilder PlaceholderText(string value) { Component.PlaceholderText = value; return this; }
        public DropDownListBuilder OnChange(string value) { Component.OnChange = value; return this; }
        public DropDownListBuilder CascadeFrom(string value) { Component.CascadeFrom = value; return this; }
        public DropDownListBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }

        public DropDownListBuilder DataBind(DataCollection dataSource)
        {
            if (dataSource != null) Component.DataSource = dataSource;
            return this;
        }
    }
}
