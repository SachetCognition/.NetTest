namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice;

    /// <summary>
    /// Test cases for ClickToVoice component (US-LT-006)
    /// </summary>
    public class ClickToVoiceTests
    {
        private readonly IHtmlParser _parser;

        public ClickToVoiceTests()
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
        /// TC-LT-006-U01: ClickToVoice renders telephone number with qtip
        /// </summary>
        [Fact]
        public void TC_LT_006_U01_ClickToVoice_Renders_Telephone_Number_With_Qtip()
        {
            // Arrange
            var component = new ClickToVoiceComponent
            {
                Id = "ctv1",
                TelephoneNumber = "+33 1 23 45 67 89",
                IsNumberSpanVisible = true,
                CssTelephoneNumberSpan = "tel-number",
                CssAnchorTag = "ctv-anchor",
                CssClassImage = "ctv-image",
                ImageUrl = "/images/phone.png",
                AlternateText = "Call"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert - Anchor tag
            var anchor = doc.QuerySelector("a");
            anchor.Should().NotBeNull("an anchor element should be rendered");
            anchor.GetAttribute("id").Should().Be("ctv1");
            anchor.GetAttribute("href").Should().Be("###");
            anchor.ClassList.Should().Contain("ctv-anchor");

            // Assert - Image inside anchor
            var img = doc.QuerySelector("a img");
            img.Should().NotBeNull("an image should be rendered inside the anchor");
            img.GetAttribute("src").Should().Be("/images/phone.png");
            img.GetAttribute("alt").Should().Be("Call");
            img.ClassList.Should().Contain("ctv-image");

            // Assert - Telephone number span
            var telSpan = doc.QuerySelector("a span.tel-number");
            telSpan.Should().NotBeNull("telephone number span should be rendered when IsNumberSpanVisible is true");
            telSpan.TextContent.Should().Be("+33 1 23 45 67 89");

            // Assert - Init script contains jQuery plugin call
            var initScript = component.BuildInitScript();
            initScript.Should().Contain("$('#ctv1').clickToVoice(");
            initScript.Should().Contain("telephoneNumber");
            initScript.Should().Contain("33 1 23 45 67 89");

            // Verify builder fluent API
            var builderComponent = new ClickToVoiceComponent();
            var builder = new ClickToVoiceBuilder(builderComponent, null);
            builder.TelephoneNumber("+1 555 0123")
                   .IsNumberSpanVisible(true)
                   .CssAnchorTag("anchor-css");
            builderComponent.TelephoneNumber.Should().Be("+1 555 0123");
            builderComponent.IsNumberSpanVisible.Should().BeTrue();
        }
    }
}
