using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

public class ErrorMessageModel
{
    public string? ErrorHtml { get; set; }
    public string? ErrorMessage { get; set; }
}

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

    public ErrorBuilder ErrorCollection(IEnumerable<ErrorMessageModel> errors)
    {
        Component.ErrorCollection = errors.ToList();
        return this;
    }

    public ErrorBuilder AccessibleText(string? value)
    {
        Component.AccessibleText = value;
        return this;
    }

    public ErrorBuilder DivCssClass(string value)
    {
        Component.DivCssClass = value;
        return this;
    }
}
