namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

    /// <summary>
    /// </summary>
    public class DropDownListBuilder : ComponentBuilderBase<DropDownListComponent, DropDownListBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownListBuilder"/> class.
        /// </summary>
        public DropDownListBuilder(DropDownListComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder CssClass(string cssClass)
        {
            Component.CssClass = cssClass;
            return this;
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder CssClassSelectDiv(string cssClass)
        {
            Component.CssClassSelectDiv = cssClass;
            return this;
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder DataBind(SelectList selectList)
        {
            Component.SelectList = selectList;
            return this;
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder CustomLabel(System.Action<CustomLabelBuilder> setup)
        {
            if (Component.CustomLabel == null)
            {
                Component.CustomLabel = new CustomLabelComponent(Component.HtmlHelper);
            }
            var builder = new CustomLabelBuilder(Component.CustomLabel, ModelMetadata);
            setup(builder);
            return this;
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder Disabled(bool isDisabled)
        {
            Component.IsDisabled = isDisabled;
            return this;
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder SelectedValue(string value)
        {
            Component.SelectedValue = value;
            return this;
        }

        /// <summary>
        /// </summary>
        public DropDownListBuilder OnChange(string onChange)
        {
            Component.OnChange = onChange;
            return this;
        }
    }
}
