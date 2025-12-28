using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
{
    public WeekYearBuilder(WeekYearComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public WeekYearBuilder Value(WeekYearWithFormat value)
    {
        Component.Value = value;
        return this;
    }

    public WeekYearBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public WeekYearBuilder CssClassReadOnly(string value)
    {
        Component.CssClassReadOnly = value;
        return this;
    }

    public WeekYearBuilder OnChange(string value)
    {
        Component.OnChange = value;
        return this;
    }

    public WeekYearBuilder Title(string value)
    {
        Component.Title = value;
        return this;
    }

    public WeekYearBuilder AccessText(string value)
    {
        Component.AccessText = value;
        return this;
    }

    public WeekYearBuilder WeekLabel(string value)
    {
        Component.WeekLabel = value;
        return this;
    }

    public WeekYearBuilder YearLabel(string value)
    {
        Component.YearLabel = value;
        return this;
    }

    public WeekYearBuilder DisplayInformationIcon(bool value)
    {
        Component.DisplayInformationIcon = value;
        return this;
    }

    public WeekYearBuilder CssMainDiv(string value)
    {
        Component.CssMainDiv = value;
        return this;
    }

    public WeekYearBuilder WeekTextId(string value)
    {
        Component.WeekTextId = value;
        return this;
    }

    public WeekYearBuilder AssociatedWeekYearHtmlId(string value)
    {
        Component.AssociatedWeekYearHtmlId = value;
        return this;
    }

    public WeekYearBuilder CssClassWeekDiv(string value)
    {
        Component.CssClassWeekDiv = value;
        return this;
    }

    public WeekYearBuilder OnWeekChange(string value)
    {
        Component.OnChange = value;
        return this;
    }
}
