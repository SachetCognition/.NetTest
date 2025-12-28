using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class LabelComponentTests
{
    [Fact]
    public void LabelComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var label = new LabelComponent(htmlHelper);

        // Assert
        Assert.NotNull(label);
        Assert.NotNull(label.HtmlAttributes);
    }

    [Fact]
    public void LabelBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new LabelComponent(htmlHelper);
        var builder = new LabelBuilder(label, null);

        // Act
        builder.Id("testLabel");

        // Assert
        Assert.Equal("testLabel", label.Id);
    }

    [Fact]
    public void LabelBuilder_ShouldSetText()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new LabelComponent(htmlHelper);
        var builder = new LabelBuilder(label, null);

        // Act
        builder.Text("Hello World");

        // Assert
        Assert.Equal("Hello World", label.Text);
    }

    [Fact]
    public void LabelBuilder_ShouldSetCssClassLabel()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new LabelComponent(htmlHelper);
        var builder = new LabelBuilder(label, null);

        // Act
        builder.CssClassLabel("form-label");

        // Assert
        Assert.Equal("form-label", label.CssClass);
    }

    [Fact]
    public void LabelBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new LabelComponent(htmlHelper);
        var builder = new LabelBuilder(label, null);

        // Act
        builder
            .Id("myLabel")
            .Text("Username")
            .CssClassLabel("control-label");

        // Assert
        Assert.Equal("myLabel", label.Id);
        Assert.Equal("Username", label.Text);
        Assert.Equal("control-label", label.CssClass);
    }

    [Fact]
    public void LabelComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new LabelComponent(htmlHelper);
        new LabelBuilder(label, null)
            .Id("testLbl")
            .Text("Test Label");

        // Act
        var html = label.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("Test Label", htmlString);
    }

    [Fact]
    public void LabelComponent_ToHtml_ShouldContainLabelTag()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var label = new LabelComponent(htmlHelper);
        new LabelBuilder(label, null)
            .Id("testLbl")
            .Text("My Label");

        // Act
        var html = label.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("<label", htmlString);
    }
}
