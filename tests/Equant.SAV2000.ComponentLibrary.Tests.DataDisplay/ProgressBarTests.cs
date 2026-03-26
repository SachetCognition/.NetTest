using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Xunit;

using Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-008: ProgressBar Tests
    /// </summary>
    public class ProgressBarTests
    {
        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// TC-DD-008-U01: ProgressBar renders progress element
        /// </summary>
        [Fact]
        public void TC_DD_008_U01_ProgressBar_RendersProgressElement()
        {
            // Arrange
            var component = new ProgressBarComponent();
            component.Id = "progressBar1";
            component.Value = 75;
            component.Max = 100;
            component.CssClass = "custom-progress";

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                html.Should().NotBeNullOrEmpty("ProgressBar should produce HTML output");

                var progress = doc.QuerySelector("progress#progressBar1");
                progress.Should().NotBeNull("a <progress> element with id 'progressBar1' should be rendered");
                progress.GetAttribute("value").Should().Be("75", "value should be 75");
                progress.GetAttribute("max").Should().Be("100", "max should be 100");
                progress.ClassList.Should().Contain("custom-progress", "custom CSS class should be applied");
                progress.TextContent.Should().Contain("75%", "fallback text should show percentage");
            }
        }

        /// <summary>
        /// Additional test: ProgressBar with HTML attributes
        /// </summary>
        [Fact]
        public void TC_DD_008_R01_ProgressBar_WithHtmlAttributes()
        {
            // Arrange
            var component = new ProgressBarComponent();
            component.Id = "progressBar2";
            component.Value = 50;
            component.Max = 200;
            component.HtmlAttributes.Add("data-step", "10");

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                var progress = doc.QuerySelector("progress#progressBar2");
                progress.Should().NotBeNull();
                progress.GetAttribute("value").Should().Be("50");
                progress.GetAttribute("max").Should().Be("200");
                progress.GetAttribute("data-step").Should().Be("10");
            }
        }
    }
}
