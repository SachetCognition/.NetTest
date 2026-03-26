namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System;
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

    public class DropDownListBuilder : ComponentBuilderBase<DropDownListComponent, DropDownListBuilder>
    {
        public DropDownListBuilder(DropDownListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }
        public DropDownListBuilder DataBind(SelectList dataSource) { Component.DataSource = dataSource; return this; }
        public DropDownListBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public DropDownListBuilder OnChange(string value) { Component.OnChange = value; return this; }
        public DropDownListBuilder CssClassSelectDiv(string value) { return this; }
        public DropDownListBuilder CustomLabel(Action<CustomLabelBuilder> configurator) { return this; }
    }
}
