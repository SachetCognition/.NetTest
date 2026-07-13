namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ListBoxBuilder : ComponentBuilderBase<ListBoxComponent, ListBoxBuilder>
    {
        public ListBoxBuilder(ListBoxComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
