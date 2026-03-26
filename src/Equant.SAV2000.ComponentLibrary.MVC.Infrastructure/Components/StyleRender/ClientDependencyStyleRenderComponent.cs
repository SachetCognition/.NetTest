namespace Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// Stub replacement for ClientDependency-based style renderer.
    /// In ASP.NET Core, styles are served as static files.
    /// </summary>
    public class ClientDependencyStyleRenderComponent : IStyleRendererComponent
    {
        public ClientDependencyStyleRenderComponent() { }
        public ClientDependencyStyleRenderComponent(IHtmlHelper htmlHelper) { }
    }
}
