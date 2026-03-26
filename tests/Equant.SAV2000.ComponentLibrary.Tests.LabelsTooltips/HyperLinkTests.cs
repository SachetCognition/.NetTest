namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;

    /// <summary>
    /// Test cases for HyperLink component (US-LT-005)
    /// </summary>
    public class HyperLinkTests
    {
        private readonly IHtmlParser _parser;

        public HyperLinkTests()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            _parser = context.GetService<IHtmlParser>();
        }

        private IDocument ParseHtml(string html)
        {
            return _parser.ParseDocument($"<html><body>{html}</body></html>");
        }

        /// <summary>
        /// TC-LT-005-U01: HyperLink renders anchor with href and attributes
        /// </summary>
        [Fact]
        public void TC_LT_005_U01_HyperLink_Renders_Anchor_With_Href_And_Attributes()
        {
            // Arrange
            var component = new HyperLinkComponent
            {
                Id = "lnk1",
                Text = "Click Here",
                Href = "https://example.com",
                CssClass = "link-style",
                Target = "_blank",
                Title = "Go to Example"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var anchor = doc.QuerySelector("a");
            anchor.Should().NotBeNull("an anchor element should be rendered");
            anchor.GetAttribute("id").Should().Be("lnk1");
            anchor.GetAttribute("href").Should().Be("https://example.com");
            anchor.GetAttribute("target").Should().Be("_blank");
            anchor.GetAttribute("title").Should().Be("Go to Example");
            anchor.ClassList.Should().Contain("link-style");
            anchor.TextContent.Should().Be("Click Here");

            // Verify builder fluent API
            var builderComponent = new HyperLinkComponent();
            var builder = new HyperLinkBuilder(builderComponent);
            var result = builder
                .Id("lnk2")
                .Href("https://test.com")
                .Text("Test Link")
                .CssClass("test-css")
                .Target("_self")
                .Title("Test");

            result.Should().BeSameAs(builder);
            builderComponent.Href.Should().Be("https://test.com");
            builderComponent.Text.Should().Be("Test Link");
        }
    }
}
