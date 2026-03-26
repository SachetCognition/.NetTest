namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DualList
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    public class DualListBuilder : ComponentBuilderBase<DualListComponent, DualListBuilder>
    {
        public DualListBuilder(DualListComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public DualListBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public DualListBuilder Size(int value) { Component.Size = value; return this; }
        public DualListBuilder AvailableLabel(string value) { Component.AvailableLabel = value; return this; }
        public DualListBuilder SelectedLabel(string value) { Component.SelectedLabel = value; return this; }
        public DualListBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }

        public DualListBuilder AvailableItems(DataCollection data)
        {
            if (data != null) Component.AvailableItems = data;
            return this;
        }

        public DualListBuilder SelectedItems(DataCollection data)
        {
            if (data != null) Component.SelectedItems = data;
            return this;
        }
    }
}
