using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

public class MenuBuilder : ComponentBuilderBase<MenuComponent, MenuBuilder>
{
    public MenuBuilder(MenuComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public MenuBuilder Items(IEnumerable<MenuItem> items)
    {
        Component.Items = items.ToList();
        return this;
    }

    public MenuBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public MenuBuilder CssClassItem(string value)
    {
        Component.CssClassItem = value;
        return this;
    }

    public MenuBuilder CssClassSelected(string value)
    {
        Component.CssClassSelected = value;
        return this;
    }
}
