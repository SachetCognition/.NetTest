using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using System.IO;
using System.Text.Encodings.Web;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

/// <summary>
/// Helper class to create mock IHtmlHelper instances for testing.
/// </summary>
public static class TestHtmlHelper
{
    /// <summary>
    /// Creates a mock IHtmlHelper for testing components.
    /// </summary>
    public static IHtmlHelper Create()
    {
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(
            httpContext,
            new RouteData(),
            new ActionDescriptor());

        var viewContext = new ViewContext(
            actionContext,
            Mock.Of<IView>(),
            new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()),
            Mock.Of<ITempDataDictionary>(),
            TextWriter.Null,
            new HtmlHelperOptions());

        var mockHtmlHelper = new Mock<IHtmlHelper>();
        mockHtmlHelper.Setup(h => h.ViewContext).Returns(viewContext);

        return mockHtmlHelper.Object;
    }

    /// <summary>
    /// Renders IHtmlContent to a string for assertion.
    /// </summary>
    public static string RenderToString(Microsoft.AspNetCore.Html.IHtmlContent content)
    {
        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}
