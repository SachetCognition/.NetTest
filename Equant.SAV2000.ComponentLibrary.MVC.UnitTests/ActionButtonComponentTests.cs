using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests
{
    public class ActionButtonComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ActionButtonComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_ShouldInitializeWithDefaults()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            component.Should().NotBeNull();
            component.HtmlHelper.Should().Be(_mockHtmlHelper.Object);
            component.IsVisible.Should().BeTrue();
        }

        [Fact]
        public void Value_ShouldBeSettable()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            component.Value = "Save";

            component.Value.Should().Be("Save");
        }

        [Fact]
        public void OnClick_ShouldBeSettable()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            component.OnClick = "saveData()";

            component.OnClick.Should().Be("saveData()");
        }

        [Fact]
        public void DialogBoxId_ShouldBeSettable()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            component.DialogBoxId = "confirm-dialog";

            component.DialogBoxId.Should().Be("confirm-dialog");
        }

        [Fact]
        public void IsDisabled_ShouldBeSettable()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            component.IsDisabled = true;

            component.IsDisabled.Should().BeTrue();
        }

        [Fact]
        public void WriteHtml_ShouldGenerateButtonMarkup()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-save",
                Value = "Save"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("button");
            html.Should().Contain("btn-save");
        }

        [Fact]
        public void WriteHtml_WhenDialogBoxIdSet_ShouldIncludeDataToggle()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-save",
                DialogBoxId = "confirm-dialog"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("data-toggle");
            html.Should().Contain("confirm-dialog");
        }

        [Fact]
        public void WriteInitScript_WhenOnClickSet_ShouldGenerateScript()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-save",
                OnClick = "saveData()"
            };
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            var script = writer.ToString();
            script.Should().Contain("btn-save");
            script.Should().Contain("saveData");
        }

        [Fact]
        public void JsResources_ShouldReturnActionButtonJavaScript()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            resources.Should().NotBeNull();
            resources.Should().HaveCount(1);
            resources[0].Name.Should().Be("JsActionButton");
        }

        [Fact]
        public void AccessText_ShouldBeSettable()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object);

            component.AccessText = "Save button for screen readers";

            component.AccessText.Should().Be("Save button for screen readers");
        }
    }
}
