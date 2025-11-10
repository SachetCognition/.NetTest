using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests
{
    public class CheckBoxComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public CheckBoxComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_ShouldInitializeWithDefaults()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object);

            component.Should().NotBeNull();
            component.HtmlHelper.Should().Be(_mockHtmlHelper.Object);
            component.IsChecked.Should().BeFalse();
            component.IsDisabled.Should().BeFalse();
        }

        [Fact]
        public void IsChecked_ShouldBeSettable()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object);

            component.IsChecked = true;

            component.IsChecked.Should().BeTrue();
        }

        [Fact]
        public void IsDisabled_ShouldBeSettable()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object);

            component.IsDisabled = true;

            component.IsDisabled.Should().BeTrue();
        }

        [Fact]
        public void OnClick_ShouldBeSettable()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object);

            component.OnClick = "handleClick()";

            component.OnClick.Should().Be("handleClick()");
        }

        [Fact]
        public void OnChange_ShouldBeSettable()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object);

            component.OnChange = "handleChange()";

            component.OnChange.Should().Be("handleChange()");
        }

        [Fact]
        public void WriteHtml_ShouldGenerateCheckboxMarkup()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object)
            {
                Id = "chk-agree",
                IsChecked = true
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("checkbox");
            html.Should().Contain("chk-agree");
        }

        [Fact]
        public void WriteHtml_WhenChecked_ShouldIncludeCheckedAttribute()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object)
            {
                Id = "chk-agree",
                IsChecked = true
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("checked");
        }

        [Fact]
        public void WriteHtml_WhenDisabled_ShouldIncludeDisabledAttribute()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object)
            {
                Id = "chk-agree",
                IsDisabled = true
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("disabled");
        }

        [Fact]
        public void WriteInitScript_WhenOnClickSet_ShouldGenerateScript()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object)
            {
                Id = "chk-agree",
                OnClick = "handleClick()"
            };
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            var script = writer.ToString();
            script.Should().Contain("chk-agree");
            script.Should().Contain("handleClick");
        }

        [Fact]
        public void JsResources_ShouldReturnCheckBoxJavaScript()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            resources.Should().NotBeNull();
            resources.Should().HaveCount(1);
            resources[0].Name.Should().Be("JsCheckBox");
        }
    }
}
