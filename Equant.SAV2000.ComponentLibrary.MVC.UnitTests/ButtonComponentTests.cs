using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Button;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests
{
    public class ButtonComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ButtonComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_ShouldInitializeWithDefaults()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object);

            component.Should().NotBeNull();
            component.HtmlHelper.Should().Be(_mockHtmlHelper.Object);
            component.IsVisible.Should().BeTrue();
        }

        [Fact]
        public void Value_ShouldBeSettable()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object);

            component.Value = "Submit";

            component.Value.Should().Be("Submit");
        }

        [Fact]
        public void OnClick_ShouldBeSettable()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object);

            component.OnClick = "handleClick()";

            component.OnClick.Should().Be("handleClick()");
        }

        [Fact]
        public void IsDisabled_ShouldBeSettable()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object);

            component.IsDisabled = true;

            component.IsDisabled.Should().BeTrue();
        }

        [Fact]
        public void WriteHtml_ShouldGenerateButtonMarkup()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-submit",
                Value = "Submit"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("type=\"submit\"");
            html.Should().Contain("btn-submit");
            html.Should().Contain("Submit");
        }

        [Fact]
        public void WriteInitScript_WhenOnClickSet_ShouldGenerateScript()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-submit",
                OnClick = "handleClick()"
            };
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            var script = writer.ToString();
            script.Should().Contain("btn-submit");
            script.Should().Contain("handleClick");
        }

        [Fact]
        public void JsResources_ShouldReturnButtonJavaScript()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            resources.Should().NotBeNull();
            resources.Should().HaveCount(1);
            resources[0].Name.Should().Be("JsButton");
        }
    }
}
