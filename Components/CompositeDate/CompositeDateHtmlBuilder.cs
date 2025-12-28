using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;

public class CompositeDateHtmlBuilder : HtmlBuilderBase<CompositeDateComponent>
{
    public CompositeDateHtmlBuilder(CompositeDateComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        EnumDateTypes parsedDateType;
        var isDateType = Enum.TryParse(Component.DateTypesValue, true, out parsedDateType);
        if (!isDateType)
        {
            throw new ArgumentException("The given selected DateTypesValue :".AppendWithBuilder(Component.DateTypesValue,
                "is not a valid EnumDateTypes"));
        }

        var isSelectedDateTypeInGivenList = Component.DateTypesIncluded.Contains(parsedDateType);
        if (!isSelectedDateTypeInGivenList)
        {
            throw new ArgumentException("The given selected DateTypesValue :".AppendWithBuilder(Component.DateTypesValue,
                " is not in the given DateTypesIncluded list"));
        }

        var tagBuilderMainDiv = new TagBuilder("div");
        if (!string.IsNullOrEmpty(Component.CssMainDiv))
        {
            tagBuilderMainDiv.AddCssClass(Component.CssMainDiv);
        }

        tagBuilderMainDiv.MergeAttribute("id", Component.Id);

        Component.DatesToRender = CheckWhichDateToRender();
        Component.WeeksToRender = CheckWhichWeekToRender();
        Component.RenderAndLabel = RenderAndLabel(Component.DatesToRender, Component.WeeksToRender);

        var weeksToShow = CheckWhichWeekToShow(parsedDateType);
        var datesToShow = CheckWhichDateToShow(parsedDateType);
        var showAndLabel = ShowAndLabel(datesToShow, weeksToShow);

        ConsistencyCheckOfProvidedValues(datesToShow, weeksToShow);

        Component.CustomLabel.AssociatedControlId = GetForAttribute(datesToShow, weeksToShow);
        var tagBuilderLabelDiv = new TagBuilder("div");
        tagBuilderLabelDiv.InnerHtml.AppendHtml(Component.CustomLabel.ToHtml());

        if (!string.IsNullOrEmpty(Component.CssClassLabelDiv))
        {
            tagBuilderLabelDiv.AddCssClass(Component.CssClassLabelDiv);
        }

        var sbTagMainDivInnerHtml = new StringBuilder();
        sbTagMainDivInnerHtml.Append(RenderTagBuilder(tagBuilderLabelDiv));

        sbTagMainDivInnerHtml.Append(DateTypesHtml());

        if (Component.DatesToRender != DateRenderer.SkipBothDates)
        {
            Component.FirstDateBuilder.DisplayTime(Component.DisplayTime)
                .DisplayEraseButton(Component.DisplayEraseButton)
                .DisplayInformationIcon(false)
                .StartFromCurrentDate(false)
                .AssociatedDateHtmlId(Component.SecondDate.Id)
                .CssClassDateDiv(Component.CssClassFirstDateDateDiv)
                .CssMainDiv(Component.CssFirstDateMainDiv)
                .OnDateChange(Component.OnFirstDateChange).ExternalLabelText(Component.CustomLabel.Text);
            if (datesToShow == DateVisibility.HideBothDates)
            {
                Component.FirstDateBuilder.CssMainDiv("displaynone");
            }

            sbTagMainDivInnerHtml.Append(RenderHtmlContent(Component.FirstDate.ToHtml()));
        }

        if (Component.WeeksToRender != WeekRenderer.SkipBothWeeks)
        {
            Component.FirstWeekBuilder.DisplayInformationIcon(false)
                .AssociatedWeekYearHtmlId(Component.SecondWeek.Id)
                .CssClassWeekDiv(Component.CssClassFirstWeekWeekDiv)
                .CssMainDiv(Component.CssFirstWeekMainDiv)
                .OnWeekChange(Component.OnFirstWeekChange);
            if (weeksToShow == WeekVisibility.HideBothWeeks)
            {
                Component.FirstWeekBuilder.CssMainDiv("displaynone");
            }

            sbTagMainDivInnerHtml.Append(RenderHtmlContent(Component.FirstWeek.ToHtml()));
        }

        if (Component.RenderAndLabel)
        {
            if (Component.CssClassAndLabel.IsNull())
            {
                Component.CssClassAndLabel = "andLabel";
            }

            if (!showAndLabel)
            {
                Component.CssClassAndLabel = "displaynone ".AppendWithBuilder(Component.CssClassAndLabel);
            }

            new LabelBuilder(Component.AndLabel, Component.AndLabel.ModelMetadata).HtmlAttributes(new Dictionary<string, object> { { "Id", Component.AndLabelId } })
                .Text(ApplicationStrings.LBL000385)
                .CssClassLabel(Component.CssClassAndLabel);
            sbTagMainDivInnerHtml.Append(RenderHtmlContent(Component.AndLabel.ToHtml()));
        }

        if (Component.DatesToRender == DateRenderer.RenderBothDates)
        {
            Component.SecondDateBuilder.DisplayTime(Component.DisplayTime)
                .DisplayEraseButton(Component.DisplayEraseButton)
                .DisplayInformationIcon(false)
                .StartFromCurrentDate(false)
                .AssociatedDateHtmlId(Component.FirstDate.Id)
                .CssClassDateDiv(Component.CssClassSecondDateDateDiv)
                .CssMainDiv(Component.CssSecondDateMainDiv)
                .OnDateChange(Component.OnSecondDateChange).ExternalLabelText(Component.CustomLabel.Text);

            if (datesToShow == DateVisibility.HideBothDates || datesToShow == DateVisibility.HideSecondDate)
            {
                Component.SecondDateBuilder.CssMainDiv("displaynone");
            }

            sbTagMainDivInnerHtml.Append(RenderHtmlContent(Component.SecondDate.ToHtml()));
        }

        if (Component.WeeksToRender == WeekRenderer.RenderBothWeeks)
        {
            Component.SecondWeekBuilder.DisplayInformationIcon(false)
                .AssociatedWeekYearHtmlId(Component.FirstWeek.Id)
                .CssClassWeekDiv(Component.CssClassSecondWeekWeekDiv)
                .CssMainDiv(Component.CssSecondWeekMainDiv)
                .OnWeekChange(Component.OnSecondWeekChange);
            if (weeksToShow == WeekVisibility.HideSecondWeek || weeksToShow == WeekVisibility.HideBothWeeks)
            {
                Component.SecondWeekBuilder.CssMainDiv("displaynone");
            }

            sbTagMainDivInnerHtml.Append(RenderHtmlContent(Component.SecondWeek.ToHtml()));
        }

        if (!string.IsNullOrEmpty(Component.InformationIcon.Text))
        {
            Component.InformationIcon.Name = Component.Id.AppendWithBuilder("Name", "InformationIcon");
            Component.InformationIcon.Id = Component.Id.AppendWithBuilder("InformationIcon");
            Component.InformationIcon.ToolTipId = Component.Id.AppendWithBuilder("InformationIconToolTip");
            sbTagMainDivInnerHtml.Append(RenderHtmlContent(Component.InformationIcon.ToHtml()));
        }

        tagBuilderMainDiv.InnerHtml.AppendHtml(sbTagMainDivInnerHtml.ToString());
        return tagBuilderMainDiv;
    }

    private static string RenderTagBuilder(TagBuilder tagBuilder)
    {
        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private static string RenderHtmlContent(IHtmlContent content)
    {
        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private WeekRenderer CheckWhichWeekToRender()
    {
        var doubleWeekTypes = DoubleWeekTypes();
        var renderSecondWeek = Component.DateTypesIncluded.Any(x => doubleWeekTypes.Any(y => y == x));

        if (renderSecondWeek)
        {
            return WeekRenderer.RenderBothWeeks;
        }

        var singleWeekTypes = SingleWeekTypes();
        var renderFirstDate = Component.DateTypesIncluded.Any(x => singleWeekTypes.Any(y => y == x));

        if (renderFirstDate)
        {
            return WeekRenderer.SkipSecondWeek;
        }

        return WeekRenderer.SkipBothWeeks;
    }

    private DateRenderer CheckWhichDateToRender()
    {
        var doubleDateTypes = DoubleDateTypes();
        var renderSecondDate = Component.DateTypesIncluded.Any(x => doubleDateTypes.Any(y => y == x));

        if (renderSecondDate)
        {
            return DateRenderer.RenderBothDates;
        }

        var singleDateTypes = SingleDateTypes();
        var renderFirstDate = Component.DateTypesIncluded.Any(x => singleDateTypes.Any(y => y == x));

        if (renderFirstDate)
        {
            return DateRenderer.SkipSecondDate;
        }

        return DateRenderer.SkipBothDates;
    }

    private static bool RenderAndLabel(DateRenderer dateToRender, WeekRenderer weekToRender)
    {
        return (dateToRender == DateRenderer.RenderBothDates) || (weekToRender == WeekRenderer.RenderBothWeeks);
    }

    private static WeekVisibility CheckWhichWeekToShow(EnumDateTypes parsedDateType)
    {
        var singleWeekTypes = SingleWeekTypes();
        var singleWeekTypesIsSelected = singleWeekTypes.Contains(parsedDateType);
        if (singleWeekTypesIsSelected)
        {
            return WeekVisibility.HideSecondWeek;
        }

        var doubleWeekTypes = DoubleWeekTypes();
        var doubleWeekTypesIsSelected = doubleWeekTypes.Contains(parsedDateType);
        if (doubleWeekTypesIsSelected)
        {
            return WeekVisibility.ShowBothWeeks;
        }

        return WeekVisibility.HideBothWeeks;
    }

    private static DateVisibility CheckWhichDateToShow(EnumDateTypes parsedDateType)
    {
        var singleDateTypes = SingleDateTypes();
        var singleDateTypesIsSelected = singleDateTypes.Contains(parsedDateType);
        if (singleDateTypesIsSelected)
        {
            return DateVisibility.HideSecondDate;
        }

        var doubleDateTypes = DoubleDateTypes();
        var doubleDateTypesIsSelected = doubleDateTypes.Contains(parsedDateType);
        if (doubleDateTypesIsSelected)
        {
            return DateVisibility.ShowBothDates;
        }

        return DateVisibility.HideBothDates;
    }

    private static bool ShowAndLabel(DateVisibility dateToShow, WeekVisibility weekToShow)
    {
        return (dateToShow == DateVisibility.ShowBothDates) || (weekToShow == WeekVisibility.ShowBothWeeks);
    }

    private string GetForAttribute(DateVisibility dateToShow, WeekVisibility weekToShow)
    {
        if (dateToShow == DateVisibility.HideBothDates && weekToShow == WeekVisibility.HideBothWeeks)
        {
            return Component.DateTypesId;
        }

        return dateToShow != DateVisibility.HideBothDates ? Component.FirstDate.GetDateTextId : Component.FirstWeek.WeekTextId;
    }

    private void ConsistencyCheckOfProvidedValues(DateVisibility dateToShow, WeekVisibility weekToShow)
    {
        if (dateToShow == DateVisibility.HideBothDates)
        {
            if (!Component.SecondDate.Value.IsEmpty || !Component.FirstDate.Value.IsEmpty)
            {
                throw new ArgumentException(
                    "The value provided for both date and format should be empty for the selected date type value:".AppendWithBuilder(
                        Component.DateTypesValue));
            }
        }

        if (dateToShow == DateVisibility.HideSecondDate)
        {
            if (!Component.SecondDate.Value.IsEmpty)
            {
                throw new ArgumentException(
                    "The value provided for second date and format should be empty for the selected date type value:".AppendWithBuilder(
                        Component.DateTypesValue));
            }
        }

        if (weekToShow == WeekVisibility.HideBothWeeks)
        {
            if (!Component.SecondWeek.Value.IsEmpty || !Component.FirstWeek.Value.IsEmpty)
            {
                throw new ArgumentException(
                    "The value provided for both week and format should be empty for the selected date type value:".AppendWithBuilder(
                        Component.DateTypesValue));
            }
        }

        if (weekToShow == WeekVisibility.HideSecondWeek)
        {
            if (!Component.SecondWeek.Value.IsEmpty)
            {
                throw new ArgumentException(
                    "The value provided for second week and format should be empty for the selected date type value:".AppendWithBuilder(
                        Component.DateTypesValue));
            }
        }

        DateTypeSameAsModel();
    }

    private void DateTypeSameAsModel()
    {
        var standardTypes = DateComponentHelper.StandardDateTypes();

        if (standardTypes.Contains(Component.DateTypesValue))
        {
            if (Component.FirstDateAndFormat.IsModel || Component.SecondDateAndFormat.IsModel)
            {
                throw new ArgumentException("The selected DateTypesValue is not Model but one or both of the provided dates are of Model Type");
            }
        }

        var modelTypes = DateComponentHelper.ModelDateTypes();
        if (modelTypes.Contains(Component.DateTypesValue))
        {
            if (!Component.FirstDateAndFormat.IsModel || !Component.SecondDateAndFormat.IsModel)
            {
                throw new ArgumentException("The selected DateTypesValue is Model but the one or both of the provided dates are of non-Model Type");
            }
        }
    }

    private string DateTypesHtml()
    {
        if (Component.DateTypesIncluded == null || Component.DateTypesIncluded.Count == 0)
        {
            throw new ArgumentException("DateTypesIncluded cannot be null or empty");
        }

        if (string.IsNullOrEmpty(Component.DateTypesName))
        {
            throw new ArgumentException("DateTypesName cannot be null or empty");
        }

        if (Component.DateTypesValue.IsNull())
        {
            throw new ArgumentException("DateTypesValue cannot be null");
        }
        var ddlDropDownDateTypesId = Component.DateTypesId;
        var dateTypes = GetDateTypeItems();
        var dateTypesSelectList = new SelectList(dateTypes, "Value", "Text", Component.DateTypesValue);
        var accessText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.ACCESS000007, Component.CustomLabel.Text);
        new DropDownListBuilder(Component.DropDownDateTypes, Component.DropDownDateTypes.ModelMetadata).Name(Component.DateTypesName)
                                                                 .Id(Component.DateTypesId)
                                                                 .DataBind(dateTypesSelectList)
                                                                 .CssClassSelectDiv(Component.CssClassDateTypesSelectDiv)
                                                                 .CustomLabel(m => m.Text(accessText)
                                                                 .AssociatedControlId(Component.DateTypesId)
                                                                 .HtmlAttributes(new Dictionary<string, object> { { "Id", ddlDropDownDateTypesId + "lbl" } })
                                                                 .IsOnlyForAccess(true));

        return RenderHtmlContent(Component.DropDownDateTypes.ToHtml());
    }

    private IEnumerable<SelectListItem> GetDateTypeItems()
    {
        var dateTypes = new List<SelectListItem>();
        CheckDateTypesForNullAndDuplicates();
        var textForDateTypes = DateComponentHelper.TextValuesForDateTypes();

        foreach (var dateTypeItem in Component.DateTypesIncluded)
        {
            switch (dateTypeItem)
            {
                case EnumDateTypes.Between:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.Between], Value = EnumDateTypes.Between.ToString() });
                    break;
                case EnumDateTypes.Empty:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.Empty], Value = EnumDateTypes.Empty.ToString() });
                    break;
                case EnumDateTypes.Equal:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.Equal], Value = EnumDateTypes.Equal.ToString() });
                    break;
                case EnumDateTypes.EqualCurrentDate:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.EqualCurrentDate], Value = EnumDateTypes.EqualCurrentDate.ToString() });
                    break;
                case EnumDateTypes.LessThan:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.LessThan], Value = EnumDateTypes.LessThan.ToString() });
                    break;
                case EnumDateTypes.GreaterThan:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.GreaterThan], Value = EnumDateTypes.GreaterThan.ToString() });
                    break;
                case EnumDateTypes.LessThanOrEqual:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.LessThanOrEqual], Value = EnumDateTypes.LessThanOrEqual.ToString() });
                    break;
                case EnumDateTypes.ModelBetween:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.ModelBetween], Value = EnumDateTypes.ModelBetween.ToString() });
                    break;
                case EnumDateTypes.ModelEqual:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.ModelEqual], Value = EnumDateTypes.ModelEqual.ToString() });
                    break;
                case EnumDateTypes.ModelGreaterThan:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.ModelGreaterThan], Value = EnumDateTypes.ModelGreaterThan.ToString() });
                    break;
                case EnumDateTypes.ModelGreaterThanOrEqual:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.ModelGreaterThanOrEqual], Value = EnumDateTypes.ModelGreaterThanOrEqual.ToString() });
                    break;
                case EnumDateTypes.ModelLessThan:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.ModelLessThan], Value = EnumDateTypes.ModelLessThan.ToString() });
                    break;
                case EnumDateTypes.ModelLessThanOrEqual:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.ModelLessThanOrEqual], Value = EnumDateTypes.ModelLessThanOrEqual.ToString() });
                    break;
                case EnumDateTypes.GreaternThanOrEqual:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.GreaternThanOrEqual], Value = EnumDateTypes.GreaternThanOrEqual.ToString() });
                    break;
                case EnumDateTypes.Week:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.Week], Value = EnumDateTypes.Week.ToString() });
                    break;
                case EnumDateTypes.WeekBetween:
                    dateTypes.Add(new SelectListItem { Text = textForDateTypes[EnumDateTypes.WeekBetween], Value = EnumDateTypes.WeekBetween.ToString() });
                    break;
            }
        }

        return dateTypes;
    }

    private void CheckDateTypesForNullAndDuplicates()
    {
        if (Component.DateTypesIncluded == null || Component.DateTypesIncluded.Count == 0)
        {
            throw new ArgumentException("The provided DateTypes is empty");
        }

        var checkForDuplicates = Component.DateTypesIncluded.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        if (checkForDuplicates.Count >= 1)
        {
            throw new ArgumentException("The provided DateTypes have got duplicate values");
        }
    }

    private static IEnumerable<EnumDateTypes> SingleDateTypes()
    {
        return new List<EnumDateTypes>
        {
            EnumDateTypes.Empty,
            EnumDateTypes.Equal,
            EnumDateTypes.GreaterThan,
            EnumDateTypes.GreaternThanOrEqual,
            EnumDateTypes.LessThan,
            EnumDateTypes.LessThanOrEqual,
            EnumDateTypes.ModelEqual,
            EnumDateTypes.ModelGreaterThan,
            EnumDateTypes.ModelGreaterThanOrEqual,
            EnumDateTypes.ModelLessThan,
            EnumDateTypes.ModelLessThanOrEqual,
        };
    }

    private static IEnumerable<EnumDateTypes> DoubleDateTypes()
    {
        return new List<EnumDateTypes>
        {
            EnumDateTypes.Between,
            EnumDateTypes.ModelBetween
        };
    }

    private static IEnumerable<EnumDateTypes> SingleWeekTypes()
    {
        return new List<EnumDateTypes>
        {
            EnumDateTypes.Week
        };
    }

    private static IEnumerable<EnumDateTypes> DoubleWeekTypes()
    {
        return new List<EnumDateTypes>
        {
            EnumDateTypes.WeekBetween
        };
    }
}
