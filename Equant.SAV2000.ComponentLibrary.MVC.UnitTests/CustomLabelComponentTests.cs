using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests
{
    public class CustomLabelComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public CustomLabelComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_ShouldInitializeWithDefaults()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.Should().NotBeNull();
            component.HtmlHelper.Should().Be(_mockHtmlHelper.Object);
            component.DisplayColon.Should().BeTrue();
            component.DisplayStar.Should().BeFalse();
            component.IsHtmlEncode.Should().BeFalse();
            component.IsOnlyForAccess.Should().BeFalse();
        }

        [Fact]
        public void Text_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.Text = "Username";

            component.Text.Should().Be("Username");
        }

        [Fact]
        public void DisplayStar_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.DisplayStar = true;

            component.DisplayStar.Should().BeTrue();
        }

        [Fact]
        public void DisplayColon_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.DisplayColon = false;

            component.DisplayColon.Should().BeFalse();
        }

        [Fact]
        public void SuperscriptText_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.SuperscriptText = "Required";

            component.SuperscriptText.Should().Be("Required");
        }

        [Fact]
        public void SuperscriptCssClass_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.SuperscriptCssClass = "required-field";

            component.SuperscriptCssClass.Should().Be("required-field");
        }

        [Fact]
        public void SuperscriptToolTip_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.SuperscriptToolTip = "This field is required";

            component.SuperscriptToolTip.Should().Be("This field is required");
        }

        [Fact]
        public void IsHtmlEncode_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.IsHtmlEncode = true;

            component.IsHtmlEncode.Should().BeTrue();
        }

        [Fact]
        public void IsOnlyForAccess_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.IsOnlyForAccess = true;

            component.IsOnlyForAccess.Should().BeTrue();
        }

        [Fact]
        public void AssociatedControlId_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.AssociatedControlId = "txt-username";

            component.AssociatedControlId.Should().Be("txt-username");
            component.HtmlAttributes["for"].Should().Be("txt-username");
        }

        [Fact]
        public void WriteHtml_ShouldGenerateLabelMarkup()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object)
            {
                Id = "lbl-username",
                Text = "Username",
                AssociatedControlId = "txt-username"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("label");
            html.Should().Contain("Username");
        }

        [Fact]
        public void WriteHtml_WhenDisplayStarTrue_ShouldIncludeAsterisk()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object)
            {
                Id = "lbl-username",
                Text = "Username",
                DisplayStar = true,
                AssociatedControlId = "txt-username"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("*");
            html.Should().Contain("abbr");
        }

        [Fact]
        public void WriteHtml_WhenDisplayColonTrue_ShouldIncludeColon()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object)
            {
                Id = "lbl-username",
                Text = "Username",
                DisplayColon = true,
                AssociatedControlId = "txt-username"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain(":");
        }

        [Fact]
        public void WriteHtml_WhenSuperscriptTextSet_ShouldIncludeSuperscript()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object)
            {
                Id = "lbl-username",
                Text = "Username",
                SuperscriptText = "Required",
                AssociatedControlId = "txt-username"
            };
            var writer = new StringWriter();

            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("Required");
        }

        [Fact]
        public void WriteInitScript_ShouldNotWriteAnything()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            writer.ToString().Should().BeEmpty();
        }

        [Fact]
        public void JsResources_ShouldReturnEmptyCollection()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            resources.Should().NotBeNull();
            resources.Should().BeEmpty();
        }

        [Fact]
        public void AccessText_ShouldBeSettable()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object);

            component.AccessText = "Username label for screen readers";

            component.AccessText.Should().Be("Username label for screen readers");
        }
    }
}
