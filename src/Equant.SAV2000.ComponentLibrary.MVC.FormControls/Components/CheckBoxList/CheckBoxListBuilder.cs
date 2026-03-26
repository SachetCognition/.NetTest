namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

    public class CheckBoxListBuilder : ComponentBuilderBase<CheckBoxListComponent, CheckBoxListBuilder>
    {
        private readonly SpanLabelBuilder spanLabelBuilder;

        public CheckBoxListBuilder(CheckBoxListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata)
        {
            this.spanLabelBuilder = new SpanLabelBuilder(this.Component.CheckBoxListLabel, modelMetadata);
        }

        public CheckBoxListBuilder CssClassFieldSet(string value) { Component.CssClassFieldSet = value; return this; }
        public CheckBoxListBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public CheckBoxListBuilder CssClassDisabled(string value) { Component.CssClassDisabled = value; return this; }
        public CheckBoxListBuilder OnClick(string value) { Component.OnClick = value; return this; }
        public CheckBoxListBuilder OnChange(string value) { Component.OnChange = value; return this; }
        public CheckBoxListBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
        public CheckBoxListBuilder Title(string value) { Component.Title = value; return this; }
        public CheckBoxListBuilder IsOuterDivNeeded(bool value) { Component.IsOuterDivNeeded = value; return this; }
        public CheckBoxListBuilder CssClassOuterDiv(string value) { Component.CssClassOuterDiv = value; return this; }
        public CheckBoxListBuilder CssClassCheckBoxDiv(string value) { Component.CssClassCheckBoxDiv = value; return this; }

        public CheckBoxListBuilder SpanLabel(Action<SpanLabelBuilder> setup)
        {
            if (setup != null) setup(this.spanLabelBuilder);
            return this;
        }

        public CheckBoxListBuilder DataBind(IEnumerable<CheckBoxListItem> dataSource, IEnumerable<string> selectedList, bool toOrder)
        {
            if (dataSource != null)
            {
                this.Component.SourceItems.Clear();
                this.Component.SourceItems.AddRange(dataSource);
                if (selectedList != null)
                {
                    this.Component.SourceItems.Where(y => selectedList.Contains(y.Value)).ToList().ForEach(x => x.Selected = true);
                }
                if (toOrder)
                {
                    this.Component.SourceItems = this.Component.SourceItems.OrderBy(o => !o.Selected).ThenBy(o => o.Text).ToList();
                }
            }
            return this;
        }
    }
}
