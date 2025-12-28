using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class ActionButtonComponentTests
{
    [Fact]
    public void ActionButtonComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var button = new ActionButtonComponent(htmlHelper);

        // Assert
        Assert.NotNull(button);
        Assert.NotNull(button.HtmlAttributes);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder.Id("testActionButton");

        // Assert
        Assert.Equal("testActionButton", button.Id);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSetName()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder.Name("submitAction");

        // Assert
        Assert.Equal("submitAction", button.Name);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSetValue()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder.Value("Submit Form");

        // Assert
        Assert.Equal("Submit Form", button.Value);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSetCssClass()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder.CssClass("btn-action");

        // Assert
        Assert.Equal("btn-action", button.CssClass);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSetOnClick()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder.OnClick("performAction()");

        // Assert
        Assert.Equal("performAction()", button.OnClick);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSetDialogBoxId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder.DialogBoxId("confirmDialog");

        // Assert
        Assert.Equal("confirmDialog", button.DialogBoxId);
    }

    [Fact]
    public void ActionButtonBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        var builder = new ActionButtonBuilder(button, null);

        // Act
        builder
            .Id("myActionButton")
            .Name("actionBtn")
            .Value("Confirm")
            .CssClass("btn btn-warning")
            .OnClick("confirmAction()")
            .DialogBoxId("confirmModal");

        // Assert
        Assert.Equal("myActionButton", button.Id);
        Assert.Equal("actionBtn", button.Name);
        Assert.Equal("Confirm", button.Value);
        Assert.Equal("btn btn-warning", button.CssClass);
        Assert.Equal("confirmAction()", button.OnClick);
        Assert.Equal("confirmModal", button.DialogBoxId);
    }

    [Fact]
    public void ActionButtonComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        new ActionButtonBuilder(button, null)
            .Id("testBtn")
            .Value("Test Action");

        // Act
        var html = button.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("testBtn", htmlString);
    }

    [Fact]
    public void ActionButtonComponent_ToHtml_ShouldContainButtonTag()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ActionButtonComponent(htmlHelper);
        new ActionButtonBuilder(button, null)
            .Id("testBtn")
            .Value("Test Action");

        // Act
        var html = button.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("<button", htmlString);
    }
}
