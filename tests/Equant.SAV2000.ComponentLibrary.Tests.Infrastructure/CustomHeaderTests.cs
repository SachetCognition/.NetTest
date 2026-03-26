namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System.IO;
    using Xunit;
    using FluentAssertions;
    using AngleSharp;
    using AngleSharp.Html.Parser;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader;

    /// <summary>
    /// US-INF-005: CustomHeader tests
    /// </summary>
    public class CustomHeaderTests
    {
        /// <summary>
        /// TC-INF-005-U01: CustomHeader renders header section.
        /// Verifies that CustomHeaderComponent renders a proper header div
        /// with the sav-custom-header class, title, subtitle, icon, and id.
        /// </summary>
        [Fact]
        public void TC_INF_005_U01_CustomHeader_Renders_Header_Section()
        {
            // Arrange
            var component = new CustomHeaderComponent
            {
                Id = "mainHeader",
                Title = "Dashboard",
                SubTitle = "Overview of system status",
                CssClass = "header-primary",
                IconCssClass = "fa fa-dashboard"
            };

            // Act
            var htmlContent = component.BuildHtml();
            var writer = new StringWriter();
            htmlContent.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            var html = writer.ToString();

            // Assert - Parse with AngleSharp
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument("<html><body>" + html + "</body></html>");

            // Verify header div with correct classes
            var headerDiv = document.QuerySelector("div.sav-custom-header");
            headerDiv.Should().NotBeNull("div with sav-custom-header class should exist");
            headerDiv.GetAttribute("id").Should().Be("mainHeader");
            headerDiv.ClassList.Should().Contain("header-primary");

            // Verify icon
            var iconSpan = headerDiv.QuerySelector("span.fa.fa-dashboard");
            iconSpan.Should().NotBeNull("icon span with FontAwesome classes should exist");

            // Verify title
            var titleElement = headerDiv.QuerySelector("h3.sav-header-title");
            titleElement.Should().NotBeNull("h3 with sav-header-title class should exist");
            titleElement.TextContent.Should().Be("Dashboard");

            // Verify subtitle
            var subtitleElement = headerDiv.QuerySelector("span.sav-header-subtitle");
            subtitleElement.Should().NotBeNull("span with sav-header-subtitle class should exist");
            subtitleElement.TextContent.Should().Be("Overview of system status");
        }
    }
}
