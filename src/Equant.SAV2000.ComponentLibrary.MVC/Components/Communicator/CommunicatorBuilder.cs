namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommunicatorBuilder : ComponentBuilderBase<CommunicatorComponent, CommunicatorBuilder>
    {
        public CommunicatorBuilder(CommunicatorComponent component) : base(component) { }
        public CommunicatorBuilder(CommunicatorComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CommunicatorBuilder Text(string text) { Component.Text = text; return this; }
        public CommunicatorBuilder CssClass(string cssClass) { Component.CssClass = cssClass; return this; }
        public CommunicatorBuilder Channel(string channel) { Component.Channel = channel; return this; }
    }
}
