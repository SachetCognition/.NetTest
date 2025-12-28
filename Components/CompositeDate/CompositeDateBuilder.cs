using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;

public class CompositeDateBuilder : ComponentBuilderBase<CompositeDateComponent, CompositeDateBuilder>
{
    private readonly CustomLabelBuilder _customLabelBuilder;
    private readonly ImageToolTipBuilder _informationIconBuilder;

    public CompositeDateBuilder(CompositeDateComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
        _customLabelBuilder = new CustomLabelBuilder(Component.CustomLabel, modelMetadata);
        _informationIconBuilder = new ImageToolTipBuilder(Component.InformationIcon, modelMetadata);
        _informationIconBuilder.ImageUrl("/Images/picto-information.png").CssClassImage("img15")
            .PersistanceMode(PersistanceMode.Click).Text(ApplicationStrings.MSG000491)
            .Title(ApplicationStrings.TIP000024).AlternateText(ApplicationStrings.TIP000024).Css("fortooltipclick")
            .CssClassSpan("help").CssClassInnerSpan("tooltip");
    }

    public CompositeDateBuilder CustomLabel(Action<CustomLabelBuilder> setup)
    {
        if (setup == null)
        {
            return this;
        }

        setup(_customLabelBuilder);
        return this;
    }

    public CompositeDateBuilder InformationIcon(Action<ImageToolTipBuilder> setup)
    {
        if (setup == null)
        {
            return this;
        }

        setup(_informationIconBuilder);
        return this;
    }

    public CompositeDateBuilder CssMainDiv(string value)
    {
        Component.CssMainDiv = value;
        return this;
    }

    public CompositeDateBuilder FirstDateId(string value)
    {
        Component.FirstDate.Id = value;
        return this;
    }

    public CompositeDateBuilder SecondDateId(string value)
    {
        Component.SecondDate.Id = value;
        return this;
    }

    public CompositeDateBuilder DateTypesIncluded(List<EnumDateTypes> value)
    {
        Component.DateTypesIncluded = value;
        return this;
    }

    public CompositeDateBuilder FirstDateAndFormat(DateTimeWithFormat value)
    {
        Component.FirstDateAndFormat = value;
        return this;
    }

    public CompositeDateBuilder SecondDateAndFormat(DateTimeWithFormat value)
    {
        Component.FirstDateAndFormat = value;
        return this;
    }

    public CompositeDateBuilder FirstWeekAndFormat(WeekYearWithFormat value)
    {
        Component.FirstWeekAndFormat = value;
        return this;
    }

    public CompositeDateBuilder SecondWeekAndFormat(WeekYearWithFormat value)
    {
        Component.SecondWeekAndFormat = value;
        return this;
    }

    public CompositeDateBuilder FirstWeekId(string value)
    {
        Component.FirstWeek.Id = value;
        return this;
    }

    public CompositeDateBuilder SecondWeekId(string value)
    {
        Component.SecondWeek.Id = value;
        return this;
    }

    public CompositeDateBuilder DateTypes(string dateTypesName, string dateTypesValue, List<EnumDateTypes> value)
    {
        Component.DateTypesName = dateTypesName;
        Component.DateTypesValue = dateTypesValue;
        Component.DateTypesIncluded = value;
        return this;
    }

    public CompositeDateBuilder DisplayTime(bool value)
    {
        Component.DisplayTime = value;
        return this;
    }

    public CompositeDateBuilder DisplayEraseButton(bool value)
    {
        Component.DisplayEraseButton = value;
        return this;
    }

    public CompositeDateBuilder CssClassFirstDateDateDiv(string value)
    {
        Component.CssClassFirstDateDateDiv = value;
        return this;
    }

    public CompositeDateBuilder CssFirstDateMainDiv(string value)
    {
        Component.CssFirstDateMainDiv = value;
        return this;
    }

    public CompositeDateBuilder OnFirstDateChange(string value)
    {
        Component.OnFirstDateChange = value;
        return this;
    }

    public CompositeDateBuilder CssClassSecondDateDateDiv(string value)
    {
        Component.CssClassSecondDateDateDiv = value;
        return this;
    }

    public CompositeDateBuilder CssSecondDateMainDiv(string value)
    {
        Component.CssSecondDateMainDiv = value;
        return this;
    }

    public CompositeDateBuilder OnSecondDateChange(string value)
    {
        Component.OnSecondDateChange = value;
        return this;
    }

    public CompositeDateBuilder CssClassFirstWeekWeekDiv(string value)
    {
        Component.CssClassFirstWeekWeekDiv = value;
        return this;
    }

    public CompositeDateBuilder CssFirstWeekMainDiv(string value)
    {
        Component.CssFirstWeekMainDiv = value;
        return this;
    }

    public CompositeDateBuilder OnFirstWeekChange(string value)
    {
        Component.OnFirstWeekChange = value;
        return this;
    }

    public CompositeDateBuilder CssClassSecondWeekWeekDiv(string value)
    {
        Component.CssClassSecondWeekWeekDiv = value;
        return this;
    }

    public CompositeDateBuilder CssSecondWeekMainDiv(string value)
    {
        Component.CssSecondWeekMainDiv = value;
        return this;
    }

    public CompositeDateBuilder CssClassDateTypesSelectDiv(string value)
    {
        Component.CssClassDateTypesSelectDiv = value;
        return this;
    }

    public CompositeDateBuilder CssClassLabelDiv(string value)
    {
        Component.CssClassLabelDiv = value;
        return this;
    }

    public CompositeDateBuilder OnSecondWeekChange(string value)
    {
        Component.OnSecondWeekChange = value;
        return this;
    }
}
