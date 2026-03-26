namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Web.Mvc;

    using System;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

    public class DropDownListBuilder : ComponentBuilderBase<DropDownListComponent, DropDownListBuilder>
    {
        public DropDownListBuilder(DropDownListComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public DropDownListBuilder DataBind(System.Collections.Generic.IEnumerable<SelectListItem> items)
        {
            if (items != null) { this.Component.Items = new System.Collections.Generic.List<SelectListItem>(items); }
            return this;
        }

        public DropDownListBuilder DataBind(System.Collections.Generic.IEnumerable<SelectListItem> items, string selectedValue)
        {
            if (items != null) { this.Component.Items = new System.Collections.Generic.List<SelectListItem>(items); }
            this.Component.SelectedValue = selectedValue;
            return this;
        }

        public DropDownListBuilder CssClass(string value) { this.Component.CssClass = value; return this; }
        public DropDownListBuilder OnChange(string value) { this.Component.OnChange = value; return this; }
        public DropDownListBuilder DefaultText(string value) { this.Component.DefaultText = value; return this; }
        public DropDownListBuilder IsDivNeeded(bool value) { this.Component.IsDivNeeded = value; return this; }
        public DropDownListBuilder CssClassSelectDiv(string value) { this.Component.CssClassSelectDiv = value; return this; }

        public DropDownListBuilder CustomLabel(Action<CustomLabelBuilder> configurator)
        {
            if (this.Component.CustomLabel == null)
            {
                this.Component.CustomLabel = new CustomLabelComponent(this.Component.HtmlHelper);
            }
            var builder = new CustomLabelBuilder(this.Component.CustomLabel, this.Component.ModelMetadata);
            configurator(builder);
            return this;
        }
    }
}
