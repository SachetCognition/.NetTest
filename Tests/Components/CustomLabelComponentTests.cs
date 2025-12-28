using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class CustomLabelComponentTests
{
    [Fact]
    public void CustomLabelComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var label = new CustomLabelComponent(htmlHelper);

        // Assert
        Assert.NotNull(label);
        Assert.NotNull(label.HtmlAttributes);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder.Id("testCustomLabel");

        // Assert
        Assert.Equal("testCustomLabel", label.Id);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSetText()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder.Text("Custom Label Text");

        // Assert
        Assert.Equal("Custom Label Text", label.Text);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSetDisplayStar()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder.DisplayStar(true);

        // Assert
        Assert.True(label.DisplayStar);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSetDisplayColon()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder.DisplayColon(true);

        // Assert
        Assert.True(label.DisplayColon);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSetSuperScriptText()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder.SuperScriptText("(required)");

        // Assert
        Assert.Equal("(required)", label.SuperscriptText);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSetAssociatedControlId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder.AssociatedControlId("inputField");

        // Assert
        Assert.Equal("inputField", label.AssociatedControlId);
    }

    [Fact]
    public void CustomLabelBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        var builder = new CustomLabelBuilder(label, null);

        // Act
        builder
            .Id("myCustomLabel")
            .Text("Username")
            .DisplayStar(true)
            .DisplayColon(true)
            .AssociatedControlId("usernameInput");

        // Assert
        Assert.Equal("myCustomLabel", label.Id);
        Assert.Equal("Username", label.Text);
        Assert.True(label.DisplayStar);
        Assert.True(label.DisplayColon);
        Assert.Equal("usernameInput", label.AssociatedControlId);
    }

    [Fact]
    public void CustomLabelComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        new CustomLabelBuilder(label, null)
            .Id("testLbl")
            .Text("Test Custom Label")
            .AssociatedControlId("inputField");

        // Act
        var html = label.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("Test Custom Label", htmlString);
    }

    [Fact]
    public void CustomLabelComponent_ToHtml_WithStar_ShouldContainStarIndicator()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new CustomLabelComponent(htmlHelper);
        new CustomLabelBuilder(label, null)
            .Id("testLbl")
            .Text("Required Field")
            .DisplayStar(true)
            .AssociatedControlId("requiredInput");

        // Act
        var html = label.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("*", htmlString);
    }
}
