namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CommunicatorBuilder : ComponentBuilderBase<CommunicatorComponent, CommunicatorBuilder>
    {
        public CommunicatorBuilder(CommunicatorComponent component) : base(component) { }
        public CommunicatorBuilder(CommunicatorComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
