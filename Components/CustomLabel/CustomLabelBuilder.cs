using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

/// <summary>
/// The custom label builder.
/// </summary>
public class CustomLabelBuilder : ComponentBuilderBase<CustomLabelComponent, CustomLabelBuilder>
{
    public CustomLabelBuilder(CustomLabelComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public CustomLabelBuilder IsOnlyForAccess(bool value)
    {
        Component.IsOnlyForAccess = value;
        return this;
    }

    public CustomLabelBuilder CssClassLabel(string value)
    {
        Component.CssClassLabel = value;
        return this;
    }

    public CustomLabelBuilder AssociatedControlId(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Component.AssociatedControlId = value;
        return this;
    }

    public CustomLabelBuilder Text(string value)
    {
        Component.Text = value;
        return this;
    }

    public CustomLabelBuilder SuperScriptText(string value)
    {
        Component.SuperscriptText = value;
        return this;
    }

    public CustomLabelBuilder SuperscriptCssClass(string value)
    {
        Component.SuperscriptCssClass = value;
        return this;
    }

    public CustomLabelBuilder SuperscriptToolTip(string value)
    {
        Component.SuperscriptToolTip = value;
        return this;
    }

    public CustomLabelBuilder DisplayStar(bool value)
    {
        Component.DisplayStar = value;
        return this;
    }

    public CustomLabelBuilder DisplayColon(bool value)
    {
        Component.DisplayColon = value;
        return this;
    }

    public CustomLabelBuilder IsHtmlEncode(bool value)
    {
        Component.IsHtmlEncode = value;
        return this;
    }

    public CustomLabelBuilder AccessText(string value)
    {
        Component.AccessText = value;
        return this;
    }

    public new CustomLabelBuilder Id(string value)
    {
        Component.Id = value;
        return this;
    }
}
