using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

public class DropDownListBuilder : ComponentBuilderBase<DropDownListComponent, DropDownListBuilder>
{
    public DropDownListBuilder(DropDownListComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public DropDownListBuilder Items(IEnumerable<SelectListItem> items)
    {
        Component.Items = items.ToList();
        return this;
    }

    public DropDownListBuilder SelectedValue(string value)
    {
        Component.SelectedValue = value;
        return this;
    }

    public DropDownListBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public DropDownListBuilder CssClassReadOnly(string value)
    {
        Component.CssClassReadOnly = value;
        return this;
    }

    public DropDownListBuilder OnChange(string value)
    {
        Component.OnChange = value;
        return this;
    }

    public DropDownListBuilder Title(string value)
    {
        Component.Title = value;
        return this;
    }

    public DropDownListBuilder AccessText(string value)
    {
        Component.AccessText = value;
        return this;
    }
}
