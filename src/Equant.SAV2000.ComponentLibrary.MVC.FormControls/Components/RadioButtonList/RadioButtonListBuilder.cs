namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class RadioButtonListBuilder : ComponentBuilderBase<RadioButtonListComponent, RadioButtonListBuilder>
    {
        public RadioButtonListBuilder(RadioButtonListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public RadioButtonListBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public RadioButtonListBuilder GroupName(string value) { Component.GroupName = value; return this; }
        public RadioButtonListBuilder SelectedValue(string value) { Component.SelectedValue = value; return this; }
        public RadioButtonListBuilder Layout(RadioButtonListLayout value) { Component.Layout = value; return this; }

        public RadioButtonListBuilder DataBind(IEnumerable<RadioButtonListItem> items)
        {
            if (items != null)
            {
                Component.Items.Clear();
                Component.Items.AddRange(items);
            }
            return this;
        }
    }
}
