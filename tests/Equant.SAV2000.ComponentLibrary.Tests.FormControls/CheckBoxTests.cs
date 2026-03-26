namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;

    /// <summary>
    /// US-FC-003: CheckBox Component Tests
    /// </summary>
    public class CheckBoxTests
    {
        /// <summary>TC-FC-003-U01: CheckBoxComponent renders input[type=checkbox]</summary>
        [Fact]
        public void TC_FC_003_U01_CheckBoxComponent_RendersCheckbox()
        {
            var component = new CheckBoxComponent
            {
                Id = "chk1",
                Name = "chk1",
                CssClass = "form-check"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input[type='checkbox']");

            input.Should().NotBeNull("should render an input element with type=checkbox");
            input.GetAttribute("id").Should().Be("chk1");
            input.GetAttribute("name").Should().Be("chk1");
        }

        /// <summary>TC-FC-003-U02: CheckBox onClick/onChange event handlers</summary>
        [Fact]
        public void TC_FC_003_U02_CheckBox_EventHandlers()
        {
            var component = new CheckBoxComponent
            {
                Id = "chk1",
                OnClick = "handleClick",
                OnChange = "handleChange"
            };

            var script = component.WriteInitScript();

            script.Should().Contain("$('#chk1')");
            script.Should().Contain(".checkBox(");
            script.Should().Contain("handleClick");
            script.Should().Contain("handleChange");
        }

        /// <summary>TC-FC-003-U03: CheckBox checked state persistence</summary>
        [Fact]
        public void TC_FC_003_U03_CheckBox_CheckedStatePersistence()
        {
            var component = new CheckBoxComponent
            {
                Id = "chk1",
                Name = "chk1",
                IsChecked = true
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input[type='checkbox']");

            input.Should().NotBeNull();
            // CheckBox stores checked in HtmlAttributes and renders value="true"
            input.GetAttribute("value").Should().Be("true");
            input.HasAttribute("checked").Should().BeTrue("checked checkbox should have checked attribute");
        }

        /// <summary>TC-FC-003-I01: CheckBox model binding roundtrip</summary>
        [Fact]
        public void TC_FC_003_I01_CheckBox_ModelBindingRoundtrip()
        {
            var component = new CheckBoxComponent
            {
                Id = "chkActive",
                Name = "IsActive",
                IsChecked = true
            };

            var htmlString = TestHelper.GetHtmlString(component.WriteHtml());

            // Should have the checkbox input
            htmlString.Should().Contain("type=\"checkbox\"");
            htmlString.Should().Contain("name=\"IsActive\"");

            // Should include hidden field for model binding
            htmlString.Should().Contain("type=\"hidden\"");
        }
    }
}
