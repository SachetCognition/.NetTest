using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class CheckBoxComponentTests
{
    [Fact]
    public void CheckBoxComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var checkbox = new CheckBoxComponent(htmlHelper);

        // Assert
        Assert.NotNull(checkbox);
        Assert.NotNull(checkbox.HtmlAttributes);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.Id("testCheckbox");

        // Assert
        Assert.Equal("testCheckbox", checkbox.Id);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetName()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.Name("agreeTerms");

        // Assert
        Assert.Equal("agreeTerms", checkbox.Name);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetIsChecked()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.IsChecked(true);

        // Assert
        Assert.True(checkbox.IsChecked);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetIsDisabled()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.Disabled(true);

        // Assert
        Assert.True(checkbox.IsDisabled);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetCssClass()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.CssClass("custom-checkbox");

        // Assert
        Assert.Equal("custom-checkbox", checkbox.CssClass);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetOnClick()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.OnClick("toggleSelection()");

        // Assert
        Assert.Equal("toggleSelection()", checkbox.OnClick);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSetOnChange()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder.OnChange("handleChange()");

        // Assert
        Assert.Equal("handleChange()", checkbox.OnChange);
    }

    [Fact]
    public void CheckBoxBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        var builder = new CheckBoxBuilder(checkbox, null);

        // Act
        builder
            .Id("myCheckbox")
            .Name("acceptTerms")
            .IsChecked(true)
            .CssClass("form-check-input")
            .OnChange("validateForm()");

        // Assert
        Assert.Equal("myCheckbox", checkbox.Id);
        Assert.Equal("acceptTerms", checkbox.Name);
        Assert.True(checkbox.IsChecked);
        Assert.Equal("form-check-input", checkbox.CssClass);
        Assert.Equal("validateForm()", checkbox.OnChange);
    }

    [Fact]
    public void CheckBoxComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        new CheckBoxBuilder(checkbox, null)
            .Id("testChk")
            .Name("testCheckbox");

        // Act
        var html = checkbox.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("testChk", htmlString);
    }

    [Fact]
    public void CheckBoxComponent_ToHtml_ShouldIncludeHiddenField()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var checkbox = new CheckBoxComponent(htmlHelper);
        new CheckBoxBuilder(checkbox, null)
            .Id("testChk")
            .Name("testCheckbox")
            .IsChecked(true);

        // Act
        var html = checkbox.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("hidden", htmlString);
    }
}
