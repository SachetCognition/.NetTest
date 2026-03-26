namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ListBoxBuilder : ComponentBuilderBase<ListBoxComponent, ListBoxBuilder>
    {
        public ListBoxBuilder(ListBoxComponent component) : base(component) { }
        public ListBoxBuilder(ListBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
