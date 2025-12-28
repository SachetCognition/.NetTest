using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class HyperLinkComponentTests
{
    [Fact]
    public void HyperLinkComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var hyperlink = new HyperLinkComponent(htmlHelper);

        // Assert
        Assert.NotNull(hyperlink);
        Assert.NotNull(hyperlink.HtmlAttributes);
    }

    [Fact]
    public void HyperLinkBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        var builder = new HyperLinkBuilder(hyperlink, null);

        // Act
        builder.Id("testLink");

        // Assert
        Assert.Equal("testLink", hyperlink.Id);
    }

    [Fact]
    public void HyperLinkBuilder_ShouldSetActionUrl()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        var builder = new HyperLinkBuilder(hyperlink, null);

        // Act
        builder.ActionUrl("https://example.com");

        // Assert
        Assert.Equal("https://example.com", hyperlink.Href);
    }

    [Fact]
    public void HyperLinkBuilder_ShouldSetTitle()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        var builder = new HyperLinkBuilder(hyperlink, null);

        // Act
        builder.Title("Click here");

        // Assert
        Assert.Equal("Click here", hyperlink.Title);
    }

    [Fact]
    public void HyperLinkBuilder_ShouldSetImageUrl()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        var builder = new HyperLinkBuilder(hyperlink, null);

        // Act
        builder.ImageUrl("/images/icon.png");

        // Assert
        Assert.Equal("/images/icon.png", hyperlink.ImageUrl);
    }

    [Fact]
    public void HyperLinkBuilder_ShouldSetCss()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        var builder = new HyperLinkBuilder(hyperlink, null);

        // Act
        builder.Css("link-primary");

        // Assert
        Assert.Equal("link-primary", hyperlink.CssClass);
    }

    [Fact]
    public void HyperLinkBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        var builder = new HyperLinkBuilder(hyperlink, null);

        // Act
        builder
            .Id("myLink")
            .ActionUrl("https://example.com/page")
            .Title("Visit Example")
            .Css("btn btn-link");

        // Assert
        Assert.Equal("myLink", hyperlink.Id);
        Assert.Equal("https://example.com/page", hyperlink.Href);
        Assert.Equal("Visit Example", hyperlink.Title);
        Assert.Equal("btn btn-link", hyperlink.CssClass);
    }

    [Fact]
    public void HyperLinkComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        new HyperLinkBuilder(hyperlink, null)
            .Id("testLnk")
            .ActionUrl("https://example.com");

        // Act
        var html = hyperlink.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("testLnk", htmlString);
    }

    [Fact]
    public void HyperLinkComponent_ToHtml_ShouldContainAnchorTag()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        new HyperLinkBuilder(hyperlink, null)
            .Id("testLnk")
            .ActionUrl("https://example.com");

        // Act
        var html = hyperlink.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("<a", htmlString);
    }

    [Fact]
    public void HyperLinkComponent_ToHtml_ShouldContainHref()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var hyperlink = new HyperLinkComponent(htmlHelper);
        new HyperLinkBuilder(hyperlink, null)
            .Id("testLnk")
            .ActionUrl("https://example.com");

        // Act
        var html = hyperlink.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("href", htmlString);
    }
}
