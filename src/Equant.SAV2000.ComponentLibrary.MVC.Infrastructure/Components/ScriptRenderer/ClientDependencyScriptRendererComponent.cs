namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// Stub replacement for ClientDependency-based script renderer.
    /// In ASP.NET Core, scripts are served as static files.
    /// </summary>
    public class ClientDependencyScriptRendererComponent : IScriptRendererComponent
    {
        public ClientDependencyScriptRendererComponent() { }
        public ClientDependencyScriptRendererComponent(IHtmlHelper htmlHelper) { }
    }
}
