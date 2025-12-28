using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

public class DateTimeBuilder : ComponentBuilderBase<DateTimeComponent, DateTimeBuilder>
{
    private readonly CustomLabelBuilder _customLabelBuilder;
    private readonly ImageToolTipBuilder _informationIconBuilder;

    public DateTimeBuilder(DateTimeComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
        _customLabelBuilder = new CustomLabelBuilder(Component.CustomLabel, modelMetadata);
        _informationIconBuilder = new ImageToolTipBuilder(Component.InformationIcon, modelMetadata)
            .ImageUrl("/Images/picto-information.png")
            .CssClassImage("img15")
            .PersistanceMode(PersistanceMode.Click)
            .Text("Date information tooltip")
            .Title("Information")
            .AlternateText("Information")
            .Css("fortooltipclick")
            .CssClassSpan("help")
            .CssClassInnerSpan("tooltip");
    }

    public DateTimeBuilder CustomLabel(Action<CustomLabelBuilder> setup)
    {
        if (setup == null) return this;
        setup(_customLabelBuilder);
        return this;
    }

    public DateTimeBuilder ConsumingAppAreaId(string appAreaId)
    {
        Component.ConsumingAppAreaId = appAreaId;
        return this;
    }

    public DateTimeBuilder InformationIcon(Action<ImageToolTipBuilder> setup)
    {
        if (setup == null) return this;
        setup(_informationIconBuilder);
        return this;
    }

    public DateTimeBuilder CssMainDiv(string value)
    {
        Component.CssMainDiv = value;
        return this;
    }

    public DateTimeBuilder OnDateChange(string value)
    {
        Component.OnDateChange = value;
        return this;
    }

    public DateTimeBuilder Value(DateTimeWithFormat date)
    {
        Component.Value = date;
        return this;
    }

    public DateTimeBuilder DisplayTime(bool value)
    {
        Component.DisplayTime = value;
        return this;
    }

    public DateTimeBuilder EraseButtonText(string value)
    {
        Component.EraseButtonText = value;
        return this;
    }

    public DateTimeBuilder DisplayEraseButton(bool value)
    {
        Component.DisplayEraseButton = value;
        return this;
    }

    public DateTimeBuilder DisplayInformationIcon(bool value)
    {
        Component.DisplayInformationIcon = value;
        return this;
    }

    public DateTimeBuilder CssClassLabelDiv(string value)
    {
        Component.CssClassLabelDiv = value;
        return this;
    }

    public DateTimeBuilder CssClassDateDiv(string value)
    {
        Component.CssClassDateDiv = value;
        return this;
    }

    public DateTimeBuilder CssClassDateInput(string value)
    {
        Component.CssClassDateInput = value;
        return this;
    }

    public DateTimeBuilder LabelCssReadOnly(string value)
    {
        Component.LabelCssReadOnly = value;
        return this;
    }

    public DateTimeBuilder StartFromCurrentDate(bool value)
    {
        Component.StartFromCurrentDate = value;
        return this;
    }

    public DateTimeBuilder AssociatedDateHtmlId(string value)
    {
        Component.AssociatedDateHtmlId = value;
        return this;
    }

    public DateTimeBuilder CalendarImagePath(string value)
    {
        Component.CalendarImagePath = value;
        return this;
    }

    public DateTimeBuilder ExternalLabelText(string value)
    {
        Component.ExternalLabelText = value;
        return this;
    }

    public DateTimeBuilder EraseImagePath(string value)
    {
        Component.EraseImagePath = value;
        return this;
    }

    public DateTimeBuilder InformationIconPath(string value)
    {
        Component.InformationIconPath = value;
        return this;
    }

    public DateTimeBuilder ConditionalAnnotations(Dictionary<string, string> value)
    {
        Component.ConditionalAnnotations = value;
        return this;
    }

    public DateTimeBuilder Mandatory(bool value)
    {
        Component.IsMandatory = value;
        return this;
    }

    public DateTimeBuilder MandatoryMessage(string value)
    {
        Component.MandatoryMessage = value;
        return this;
    }

    public DateTimeBuilder AccessHrText(string value)
    {
        Component.AccessHrText = value;
        return this;
    }

    public DateTimeBuilder AccessMinText(string value)
    {
        Component.AccessMinText = value;
        return this;
    }

    public DateTimeBuilder CurrentDateSelectionImageUrl(string value)
    {
        Component.CurrentDateSelectionImageUrl = value;
        return this;
    }

    public DateTimeBuilder CurrentDateSelectionTitle(string value)
    {
        Component.CurrentDateSelectionTitle = value;
        return this;
    }

    public DateTimeBuilder DisplayCurrentDateSelector(bool value)
    {
        Component.DisplayCurrentDateSelector = value;
        return this;
    }
}
