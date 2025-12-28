using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Button;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class ButtonComponentTests
{
    [Fact]
    public void ButtonComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var button = new ButtonComponent(htmlHelper);

        // Assert
        Assert.NotNull(button);
        Assert.NotNull(button.HtmlAttributes);
    }

    [Fact]
    public void ButtonBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        var builder = new ButtonBuilder(button, null);

        // Act
        builder.Id("testButton");

        // Assert
        Assert.Equal("testButton", button.Id);
    }

    [Fact]
    public void ButtonBuilder_ShouldSetName()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        var builder = new ButtonBuilder(button, null);

        // Act
        builder.Name("submitBtn");

        // Assert
        Assert.Equal("submitBtn", button.Name);
    }

    [Fact]
    public void ButtonBuilder_ShouldSetValue()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        var builder = new ButtonBuilder(button, null);

        // Act
        builder.Value("Click Me");

        // Assert
        Assert.Equal("Click Me", button.Value);
    }

    [Fact]
    public void ButtonBuilder_ShouldSetCssClass()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        var builder = new ButtonBuilder(button, null);

        // Act
        builder.CssClass("btn-primary");

        // Assert
        Assert.Equal("btn-primary", button.CssClass);
    }

    [Fact]
    public void ButtonBuilder_ShouldSetOnClick()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        var builder = new ButtonBuilder(button, null);

        // Act
        builder.OnClick("handleClick()");

        // Assert
        Assert.Equal("handleClick()", button.OnClick);
    }

    [Fact]
    public void ButtonBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        var builder = new ButtonBuilder(button, null);

        // Act
        builder
            .Id("myButton")
            .Name("submitButton")
            .Value("Submit")
            .CssClass("btn btn-success")
            .OnClick("submitForm()");

        // Assert
        Assert.Equal("myButton", button.Id);
        Assert.Equal("submitButton", button.Name);
        Assert.Equal("Submit", button.Value);
        Assert.Equal("btn btn-success", button.CssClass);
        Assert.Equal("submitForm()", button.OnClick);
    }

    [Fact]
    public void ButtonComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        new ButtonBuilder(button, null)
            .Id("testBtn")
            .Value("Test Button");

        // Act
        var html = button.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("testBtn", htmlString);
    }

    [Fact]
    public void ButtonComponent_ToInitScript_ShouldReturnJavaScript()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var button = new ButtonComponent(htmlHelper);
        new ButtonBuilder(button, null)
            .Id("testBtn")
            .OnClick("handleClick()");

        // Act
        var script = button.ToInitScript();

        // Assert
        Assert.NotNull(script);
        Assert.Contains("testBtn", script);
    }
}
