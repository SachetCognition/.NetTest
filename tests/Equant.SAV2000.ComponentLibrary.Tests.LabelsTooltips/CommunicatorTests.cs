namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator;

    /// <summary>
    /// Test cases for Communicator component (US-LT-007)
    /// </summary>
    public class CommunicatorTests
    {
        private readonly IHtmlParser _parser;

        public CommunicatorTests()
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
        /// TC-LT-007-U01: Communicator renders component structure
        /// </summary>
        [Fact]
        public void TC_LT_007_U01_Communicator_Renders_Component_Structure()
        {
            // Arrange
            var component = new CommunicatorComponent
            {
                Id = "comm1",
                Name = "communicator1",
                Text = "Communication Channel",
                CssClass = "comm-style",
                Channel = "email"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert - Main div structure
            var div = doc.QuerySelector("div");
            div.Should().NotBeNull("a div element should be rendered");
            div.GetAttribute("id").Should().Be("comm1");
            div.GetAttribute("name").Should().Be("communicator1");
            div.ClassList.Should().Contain("communicator");
            div.ClassList.Should().Contain("comm-style");
            div.GetAttribute("data-channel").Should().Be("email");

            // Assert - Inner text span
            var textSpan = doc.QuerySelector("div span.communicator-text");
            textSpan.Should().NotBeNull("text span should be rendered");
            textSpan.TextContent.Should().Be("Communication Channel");

            // Assert - Init script
            var initScript = component.BuildInitScript();
            initScript.Should().Contain("$('#comm1').communicator()");

            // Verify builder fluent API
            var builderComponent = new CommunicatorComponent();
            var builder = new CommunicatorBuilder(builderComponent);
            var result = builder
                .Id("comm2")
                .Text("Chat")
                .CssClass("chat-css")
                .Channel("chat");
            result.Should().BeSameAs(builder);
            builderComponent.Channel.Should().Be("chat");
        }
    }
}
