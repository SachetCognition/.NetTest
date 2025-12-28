using Microsoft.AspNetCore.Html;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public interface IStyleRendererComponent
{
    void RegisterStyle(CssResource resource);
    IHtmlContent RenderStyles();
}
