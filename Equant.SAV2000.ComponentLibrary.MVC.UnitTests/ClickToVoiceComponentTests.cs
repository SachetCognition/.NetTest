using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests
{
    public class ClickToVoiceComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ClickToVoiceComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_ShouldInitializeWithDefaults()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.Should().NotBeNull();
            component.HtmlHelper.Should().Be(_mockHtmlHelper.Object);
            component.Title.Should().Be(string.Empty);
            component.TelephoneNumber.Should().Be(string.Empty);
            component.ActionUrl.Should().Be(string.Empty);
            component.ImageUrl.Should().Be(string.Empty);
            component.AlternateText.Should().Be(string.Empty);
        }

        [Fact]
        public void TelephoneNumber_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.TelephoneNumber = "+1234567890";

            component.TelephoneNumber.Should().Be("+1234567890");
        }

        [Fact]
        public void ActionUrl_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.ActionUrl = "/api/call";

            component.ActionUrl.Should().Be("/api/call");
        }

        [Fact]
        public void ImageUrl_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.ImageUrl = "/images/phone.png";

            component.ImageUrl.Should().Be("/images/phone.png");
        }

        [Fact]
        public void AlternateText_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.AlternateText = "Click to call";

            component.AlternateText.Should().Be("Click to call");
        }

        [Fact]
        public void Title_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.Title = "Call customer";

            component.Title.Should().Be("Call customer");
        }

        [Fact]
        public void IsNumberSpanVisible_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.IsNumberSpanVisible = true;

            component.IsNumberSpanVisible.Should().BeTrue();
        }

        [Fact]
        public void TelephoneControlId_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.TelephoneControlId = "txt-phone";

            component.TelephoneControlId.Should().Be("txt-phone");
        }

        [Fact]
        public void RootService_ShouldBeSettable()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            component.RootService = "CustomerService";

            component.RootService.Should().Be("CustomerService");
        }

        [Fact]
        public void WriteHtml_ShouldGenerateAnchorMarkup()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object)
            {
                Id = "ctv-call",
                TelephoneNumber = "+1234567890"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("<a");
            html.Should().Contain("ctv-call");
        }

        [Fact]
        public void WriteHtml_WhenIsNumberSpanVisibleTrue_ShouldIncludePhoneNumber()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object)
            {
                Id = "ctv-call",
                TelephoneNumber = "+1234567890",
                IsNumberSpanVisible = true
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("+1234567890");
        }

        [Fact]
        public void WriteInitScript_ShouldGenerateJavaScript()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object)
            {
                Id = "ctv-call",
                TelephoneNumber = "+1234567890",
                ActionUrl = "/api/call"
            };
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            var script = writer.ToString();
            script.Should().Contain("ctv-call");
            script.Should().Contain("clickToVoice");
        }

        [Fact]
        public void WriteInitScript_WhenRootServiceSet_ShouldEncodeInUrl()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object)
            {
                Id = "ctv-call",
                TelephoneNumber = "+1234567890",
                ActionUrl = "/api/call/{1}",
                RootService = "CustomerService"
            };
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            var script = writer.ToString();
            script.Should().Contain("CustomerService");
        }

        [Fact]
        public void JsResources_ShouldReturnClickToVoiceAndQtipJavaScript()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            resources.Should().NotBeNull();
            resources.Should().HaveCount(2);
            resources[0].Name.Should().Be("jsQtip");
            resources[1].Name.Should().Be("JsClickToVoice");
        }

        [Fact]
        public void CssResources_ShouldReturnQtipCss()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object);

            var resources = component.CssResources;

            resources.Should().NotBeNull();
            resources.Should().HaveCount(1);
            resources[0].Name.Should().Be("Cssjqueryqtip");
        }
    }
}
