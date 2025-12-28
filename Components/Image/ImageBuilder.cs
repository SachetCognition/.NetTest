using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Image;

public class ImageBuilder : ComponentBuilderBase<ImageComponent, ImageBuilder>
{
    public ImageBuilder(ImageComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public ImageBuilder Src(string value)
    {
        Component.Src = value;
        return this;
    }

    public ImageBuilder Alt(string value)
    {
        Component.Alt = value;
        return this;
    }

    public ImageBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }

    public ImageBuilder Title(string value)
    {
        Component.Title = value;
        return this;
    }

    public ImageBuilder Width(int value)
    {
        Component.Width = value;
        return this;
    }

    public ImageBuilder Height(int value)
    {
        Component.Height = value;
        return this;
    }
}
