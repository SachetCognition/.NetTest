using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

public class ErrorBuilder : ComponentBuilderBase<ErrorComponents, ErrorBuilder>
{
    public ErrorBuilder(ErrorComponents component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    public ErrorBuilder Message(string value)
    {
        Component.Message = value;
        return this;
    }

    public ErrorBuilder CssClass(string value)
    {
        Component.CssClass = value;
        return this;
    }
}
