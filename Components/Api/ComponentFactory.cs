using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public class ComponentFactory<TModel>
{
    private readonly IHtmlHelper<TModel> _htmlHelper;
    private readonly ScriptRendererBuilder _scriptRendererBuilder;
    private readonly StyleRendererBuilder _styleRendererBuilder;

    public ComponentFactory(
        IHtmlHelper<TModel> htmlHelper,
        ScriptRendererBuilder scriptRendererBuilder,
        StyleRendererBuilder styleRendererBuilder)
    {
        _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
        _scriptRendererBuilder = scriptRendererBuilder ?? throw new ArgumentNullException(nameof(scriptRendererBuilder));
        _styleRendererBuilder = styleRendererBuilder ?? throw new ArgumentNullException(nameof(styleRendererBuilder));
    }

    public IHtmlHelper<TModel> HtmlHelper => _htmlHelper;

    public ScriptRendererBuilder ScriptRenderer => _scriptRendererBuilder;

    public StyleRendererBuilder StyleRenderer => _styleRendererBuilder;
}
