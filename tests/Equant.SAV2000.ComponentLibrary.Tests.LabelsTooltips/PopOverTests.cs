namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver;

    /// <summary>
    /// Test cases for PopOver component (US-LT-004)
    /// </summary>
    public class PopOverTests
    {
        private readonly IHtmlParser _parser;

        public PopOverTests()
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
        /// TC-LT-004-U01: PopOverComponent renders popover trigger
        /// </summary>
        [Fact]
        public void TC_LT_004_U01_PopOverComponent_Renders_Popover_Trigger()
        {
            // Arrange
            var component = new PopOverComponent
            {
                Id = "popover1",
                Text = "Popover content here",
                Title = "Popover Title",
                CssClass = "popover-trigger",
                Placement = "top"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var trigger = doc.QuerySelector("span");
            trigger.Should().NotBeNull("popover trigger element should be rendered");
            trigger.GetAttribute("id").Should().Be("popover1");
            trigger.GetAttribute("data-toggle").Should().Be("popover");
            trigger.GetAttribute("data-title").Should().Be("Popover Title");
            trigger.GetAttribute("data-content").Should().Be("Popover content here");
            trigger.GetAttribute("data-placement").Should().Be("top");
            trigger.ClassList.Should().Contain("popover-trigger");

            // Verify builder fluent API
            var builderComponent = new PopOverComponent();
            var builder = new PopOverBuilder(builderComponent);
            var result = builder
                .Id("pop2")
                .Text("Content")
                .Title("Title")
                .CssClass("css")
                .Placement("bottom");
            result.Should().BeSameAs(builder);
            builderComponent.Id.Should().Be("pop2");
            builderComponent.Text.Should().Be("Content");
        }
    }
}
