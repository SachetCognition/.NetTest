using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers;

/// <summary>
/// The error helper.
/// </summary>
public static class ErrorHelper
{
    public static ErrorComponents? CreateErrorComponent(ComponentBase componentBase)
    {
        return componentBase == null ? null : CreateErrorComponent(componentBase, null, componentBase.ValidationString.ToString());
    }

    public static ErrorComponents CreateErrorComponent(ComponentBase componentBase, string? accessText)
    {
        if (componentBase == null)
        {
            throw new ArgumentNullException(nameof(componentBase));
        }

        var errorComponent = new ErrorComponents(componentBase.HtmlHelper);
        var errorMessages = new List<ErrorMessageModel> { new ErrorMessageModel { ErrorHtml = componentBase.ValidationString.ToString() } };
        new ErrorBuilder(errorComponent, componentBase.ModelMetadata).ErrorCollection(errorMessages).Id(componentBase.Id + "_Error").AccessibleText(accessText).DivCssClass("error-input");
        return errorComponent;
    }

    public static ErrorComponents CreateErrorComponent(ComponentBase componentBase, string? accessText, string errorHtml)
    {
        if (componentBase == null)
        {
            throw new ArgumentNullException(nameof(componentBase));
        }

        var errorComponent = new ErrorComponents(componentBase.HtmlHelper);
        var errorMessages = new List<ErrorMessageModel> { new ErrorMessageModel { ErrorHtml = errorHtml } };
        new ErrorBuilder(errorComponent, componentBase.ModelMetadata).ErrorCollection(errorMessages).Id(componentBase.Id + "_Error").AccessibleText(accessText).DivCssClass("error-input");
        return errorComponent;
    }
}
