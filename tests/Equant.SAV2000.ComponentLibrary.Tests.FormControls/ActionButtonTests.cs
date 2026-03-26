namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;

    /// <summary>
    /// US-FC-002: ActionButton Component Tests
    /// </summary>
    public class ActionButtonTests
    {
        /// <summary>TC-FC-002-U01: ActionButtonComponent default values</summary>
        [Fact]
        public void TC_FC_002_U01_ActionButtonComponent_DefaultValues()
        {
            var component = new ActionButtonComponent();

            component.Id.Should().BeNull();
            component.Name.Should().BeNull();
            component.IsVisible.Should().BeTrue();
            component.IsDisabled.Should().BeFalse();
            component.DialogBoxId.Should().BeNull();
        }

        /// <summary>TC-FC-002-U02: ActionButtonHtmlBuilder renders span-wrapped text</summary>
        [Fact]
        public void TC_FC_002_U02_ActionButtonHtmlBuilder_RendersSpanWrappedText()
        {
            var component = new ActionButtonComponent
            {
                Id = "actBtn1",
                Text = "Click Me",
                CssClass = "action-btn"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var button = doc.QuerySelector("button");

            button.Should().NotBeNull("should render a button element");
            button.GetAttribute("id").Should().Be("actBtn1");

            var span = button.QuerySelector("span");
            span.Should().NotBeNull("text should be wrapped in a span");
            span.TextContent.Should().Be("Click Me");
        }

        /// <summary>TC-FC-002-U03: ActionButton dialog box integration</summary>
        [Fact]
        public void TC_FC_002_U03_ActionButton_DialogBoxIntegration()
        {
            var component = new ActionButtonComponent
            {
                Id = "actBtn1",
                Text = "Open",
                DialogBoxId = "editDialog"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var button = doc.QuerySelector("button");

            button.Should().NotBeNull();
            button.GetAttribute("data-toggle").Should().Be("modal");
            button.GetAttribute("data-target").Should().Be("#editDialog");
        }
    }
}
