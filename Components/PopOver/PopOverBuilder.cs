namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopOverBuilder : ComponentBuilderBase<PopOverComponent, PopOverBuilder>
    {
        public PopOverBuilder(PopOverComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
