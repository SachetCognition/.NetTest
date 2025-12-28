using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

public class ImageToolTipBuilder : ComponentBuilderBase<ImageToolTipComponent, ImageToolTipBuilder>
{
    public ImageToolTipBuilder(ImageToolTipComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public ImageToolTipBuilder Text(string value)
    {
        Component.Text = value;
        return this;
    }

    public ImageToolTipBuilder ImageUrl(string value)
    {
        Component.ImageUrl = value;
        return this;
    }

    public ImageToolTipBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public ImageToolTipBuilder ToolTipId(string value)
    {
        Component.ToolTipId = value;
        return this;
    }

    public ImageToolTipBuilder Title(string value)
    {
        Component.Title = value;
        return this;
    }

    public ImageToolTipBuilder Alt(string value)
    {
        Component.Alt = value;
        return this;
    }

    public ImageToolTipBuilder CssClassImage(string value)
    {
        Component.CssClassImage = value;
        return this;
    }

    public ImageToolTipBuilder PersistanceMode(PersistanceMode value)
    {
        Component.PersistanceMode = value;
        return this;
    }

    public ImageToolTipBuilder AlternateText(string value)
    {
        Component.Alt = value;
        return this;
    }

    public ImageToolTipBuilder Css(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public ImageToolTipBuilder CssClassSpan(string value)
    {
        Component.CssClassSpan = value;
        return this;
    }

    public ImageToolTipBuilder CssClassInnerSpan(string value)
    {
        Component.CssClassInnerSpan = value;
        return this;
    }
}

public enum PersistanceMode
{
    None,
    InnerDefaultProperty,
    InnerProperty,
    Attribute,
    EncodedInnerDefaultProperty,
    Click
}
