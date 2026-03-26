using System.Collections.Generic;
using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Xunit;

using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-007: ErrorComponent Tests
    /// </summary>
    public class ErrorComponentTests
    {
        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// TC-DD-007-U01: ErrorComponent renders error messages with styling
        /// </summary>
        [Fact]
        public void TC_DD_007_U01_ErrorComponent_RendersErrorMessages_WithStyling()
        {
            // Arrange
            var component = new ErrorComponent();
            component.Id = "errorComp";
            component.CssClass = "custom-error-class";
            component.ErrorMessages = new List<ErrorMessageModel>
            {
                new ErrorMessageModel { ErrorHtml = "<strong>Error 1:</strong> Something went wrong" },
                new ErrorMessageModel { ErrorHtml = "Error 2: Another issue" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                html.Should().NotBeNullOrEmpty("ErrorComponent should produce HTML output");

                var container = doc.QuerySelector("div#errorComp");
                container.Should().NotBeNull("a div with id 'errorComp' should be rendered");
                container.ClassList.Should().Contain("custom-error-class", "custom CSS class should be applied");

                var errorDivs = doc.QuerySelectorAll("div#errorComp .error-message");
                errorDivs.Length.Should().Be(2, "there should be 2 error message divs");

                // Check content
                html.Should().Contain("Error 1:", "first error message should be rendered");
                html.Should().Contain("Something went wrong", "first error detail should be rendered");
                html.Should().Contain("Error 2:", "second error message should be rendered");
            }
        }

        /// <summary>
        /// Additional test: ErrorComponent not rendered when no messages
        /// </summary>
        [Fact]
        public void TC_DD_007_R01_ErrorComponent_NotRendered_WhenNoMessages()
        {
            // Arrange
            var component = new ErrorComponent();
            component.Id = "emptyError";
            component.ErrorMessages = new List<ErrorMessageModel>();

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();

                // Assert
                html.Should().BeEmpty("ErrorComponent should not render when there are no error messages");
            }
        }
    }
}
