namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommunicatorBuilder : ComponentBuilderBase<CommunicatorComponent, CommunicatorBuilder>
    {
        public CommunicatorBuilder(CommunicatorComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
