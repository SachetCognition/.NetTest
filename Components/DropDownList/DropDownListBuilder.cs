namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System;
    using System.Web.Mvc;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

    public class DropDownListBuilder : ComponentBuilderBase<DropDownListComponent, DropDownListBuilder>
    {
        public DropDownListBuilder(DropDownListComponent component) : base(component) { }
        public DropDownListBuilder(DropDownListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public DropDownListBuilder DataBind(SelectList selectList) { return this; }
        public DropDownListBuilder CssClassSelectDiv(string cssClass) { return this; }
        public DropDownListBuilder CustomLabel(Action<CustomLabelBuilder> configure)
        {
            if (configure != null)
            {
                var labelComponent = new CustomLabelComponent();
                var labelBuilder = new CustomLabelBuilder(labelComponent);
                configure(labelBuilder);
                Component.Label = labelComponent;
            }
            return this;
        }
        public DropDownListBuilder HtmlAttributes(object attributes) { return this; }
        public DropDownListBuilder Title(string title) { return this; }
        public DropDownListBuilder CssClassLabel(string cssClass) { return this; }
    }
}
