using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Xunit;

using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
using Equant.SAV2000.ComponentLibrary.MVC.Components.PopupImage;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-009: Image/PopupImage Tests
    /// </summary>
    public class ImagePopupImageTests
    {
        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// TC-DD-009-U01: ImageComponent renders img tag with src and alt
        /// </summary>
        [Fact]
        public void TC_DD_009_U01_ImageComponent_RendersImgTag_WithSrcAndAlt()
        {
            // Arrange
            var component = new ImageComponent();
            component.Id = "testImage";
            component.Src = "/images/photo.jpg";
            component.Alt = "Test Photo";
            component.Title = "My Photo";
            component.Width = 200;
            component.Height = 150;

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                html.Should().NotBeNullOrEmpty("ImageComponent should produce HTML output");

                var img = doc.QuerySelector("img#testImage");
                img.Should().NotBeNull("an <img> element with id 'testImage' should be rendered");
                img.GetAttribute("src").Should().Be("/images/photo.jpg", "src should match");
                img.GetAttribute("alt").Should().Be("Test Photo", "alt should match");
                img.GetAttribute("title").Should().Be("My Photo", "title should match");
                img.GetAttribute("width").Should().Be("200", "width should match");
                img.GetAttribute("height").Should().Be("150", "height should match");
            }
        }

        /// <summary>
        /// TC-DD-009-U02: PopupImageComponent renders with popup trigger
        /// </summary>
        [Fact]
        public void TC_DD_009_U02_PopupImageComponent_RendersWithPopupTrigger()
        {
            // Arrange
            var component = new PopupImageComponent();
            component.Id = "popupImg";
            component.Src = "/images/thumb.jpg";
            component.Alt = "Thumbnail";
            component.Title = "Small Image";
            component.PopupUrl = "/images/full.jpg";
            component.PopupTitle = "Full Size Image";
            component.PopupWidth = 1024;
            component.PopupHeight = 768;

            // Act - Test HTML rendering
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert - HTML
                html.Should().NotBeNullOrEmpty("PopupImageComponent should produce HTML output");

                var anchor = doc.QuerySelector("a#popupImg");
                anchor.Should().NotBeNull("an <a> element with id 'popupImg' should be rendered");
                anchor.GetAttribute("href").Should().Be("/images/full.jpg", "href should be the popup URL");
                anchor.GetAttribute("title").Should().Be("Full Size Image", "title should be the popup title");
                anchor.ClassList.Should().Contain("popup-image-trigger", "should have popup-image-trigger class");

                var img = doc.QuerySelector("a#popupImg img");
                img.Should().NotBeNull("an <img> inside the anchor should be rendered");
                img.GetAttribute("src").Should().Be("/images/thumb.jpg", "img src should be the thumbnail");
                img.GetAttribute("alt").Should().Be("Thumbnail", "img alt should match");
            }

            // Act - Test init script
            using (var sw = new StringWriter())
            {
                component.WriteInitScript(sw);
                var initScript = sw.ToString();

                // Assert - Init script
                initScript.Should().Contain("$('#popupImg')", "init script should target the popup image element");
                initScript.Should().Contain("window.open", "init script should open a popup window");
                initScript.Should().Contain("/images/full.jpg", "init script should contain popup URL");
                initScript.Should().Contain("1024", "init script should contain popup width");
                initScript.Should().Contain("768", "init script should contain popup height");
            }
        }
    }
}
