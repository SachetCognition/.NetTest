namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

    /// <summary>
    /// Test cases for ImageToolTip component (US-LT-003)
    /// </summary>
    public class ImageToolTipTests
    {
        private readonly IHtmlParser _parser;

        public ImageToolTipTests()
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
        /// TC-LT-003-U01: ImageToolTip renders image with tooltip
        /// </summary>
        [Fact]
        public void TC_LT_003_U01_ImageToolTip_Renders_Image_With_Tooltip()
        {
            // Arrange
            var component = new ImageToolTipComponent
            {
                Id = "imgTip1",
                ImageUrl = "/images/info.png",
                ImageAlt = "Information",
                Text = "This is tooltip text",
                ToolTipId = "tip1",
                CssClass = "tooltip-image"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var outerSpan = doc.QuerySelector("span");
            outerSpan.Should().NotBeNull("outer span wrapper should be rendered");
            outerSpan.GetAttribute("id").Should().Be("imgTip1");

            var img = doc.QuerySelector("span img");
            img.Should().NotBeNull("image element should be rendered");
            img.GetAttribute("src").Should().Be("/images/info.png");
            img.GetAttribute("alt").Should().Be("Information");
            img.ClassList.Should().Contain("tooltip-image");

            var tooltipSpan = doc.QuerySelector("span span");
            tooltipSpan.Should().NotBeNull("tooltip content span should be rendered");
            tooltipSpan.GetAttribute("id").Should().Be("tip1");
        }

        /// <summary>
        /// TC-LT-003-U02: ImageToolTip click vs hover persistence mode
        /// </summary>
        [Fact]
        public void TC_LT_003_U02_ImageToolTip_Click_Vs_Hover_PersistenceMode()
        {
            // Arrange - Click mode
            var clickComponent = new ImageToolTipComponent
            {
                Id = "imgClick",
                ImageUrl = "/images/help.png",
                PersistanceMode = PersistanceMode.Click
            };

            // Arrange - Hover mode
            var hoverComponent = new ImageToolTipComponent
            {
                Id = "imgHover",
                ImageUrl = "/images/help.png",
                PersistanceMode = PersistanceMode.Hover
            };

            // Act
            var clickHtml = clickComponent.BuildHtml().ToHtmlString();
            var hoverHtml = hoverComponent.BuildHtml().ToHtmlString();

            var clickDoc = ParseHtml(clickHtml);
            var hoverDoc = ParseHtml(hoverHtml);

            // Assert
            var clickSpan = clickDoc.QuerySelector("span");
            clickSpan.GetAttribute("data-persistence-mode").Should().Be("click");

            var hoverSpan = hoverDoc.QuerySelector("span");
            hoverSpan.GetAttribute("data-persistence-mode").Should().Be("hover");

            // Verify builder sets the mode correctly
            var builderComponent = new ImageToolTipComponent();
            var builder = new ImageToolTipBuilder(builderComponent);
            builder.PersistanceMode(PersistanceMode.Click);
            builderComponent.PersistanceMode.Should().Be(PersistanceMode.Click);

            builder.PersistanceMode(PersistanceMode.Hover);
            builderComponent.PersistanceMode.Should().Be(PersistanceMode.Hover);
        }
    }
}
