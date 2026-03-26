namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ClientDependencyStyleRenderComponent : IStyleRendererComponent
    {
        public ClientDependencyStyleRenderComponent() { }
        public ClientDependencyStyleRenderComponent(IHtmlHelper htmlHelper) { }
    }
}
