namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox;

    /// <summary>
    /// US-FC-007: TextBox Component Tests
    /// </summary>
    public class TextBoxTests
    {
        /// <summary>TC-FC-007-U01: TextBoxComponent renders input[type=text]</summary>
        [Fact]
        public void TC_FC_007_U01_TextBoxComponent_RendersTextInput()
        {
            var component = new TextBoxComponent
            {
                Id = "txt1",
                Name = "username",
                Value = "John",
                CssClass = "form-control"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input[type='text']");

            input.Should().NotBeNull("should render an input element with type=text");
            input.GetAttribute("id").Should().Be("txt1");
            input.GetAttribute("name").Should().Be("username");
            input.GetAttribute("value").Should().Be("John");
        }

        /// <summary>TC-FC-007-U02: TextBox readonly and disabled states</summary>
        [Fact]
        public void TC_FC_007_U02_TextBox_ReadonlyAndDisabledStates()
        {
            // Test readonly
            var readonlyComponent = new TextBoxComponent
            {
                Id = "txt1",
                Name = "txt1",
                IsReadOnly = true,
                CssClassReadOnly = "readonly-class"
            };

            var readonlyHtml = readonlyComponent.WriteHtml();
            var readonlyDoc = TestHelper.ParseHtml(readonlyHtml);
            var readonlyInput = readonlyDoc.QuerySelector("input");
            readonlyInput.HasAttribute("readonly").Should().BeTrue("readonly input should have readonly attribute");

            // Test disabled
            var disabledComponent = new TextBoxComponent
            {
                Id = "txt2",
                Name = "txt2",
                IsDisabled = true,
                CssClassReadOnly = "disabled-class"
            };

            var disabledHtml = disabledComponent.WriteHtml();
            var disabledDoc = TestHelper.ParseHtml(disabledHtml);
            var disabledInput = disabledDoc.QuerySelector("input");
            disabledInput.HasAttribute("disabled").Should().BeTrue("disabled input should have disabled attribute");
        }

        /// <summary>TC-FC-007-U03: TextBox value preservation on postback</summary>
        [Fact]
        public void TC_FC_007_U03_TextBox_ValuePreservationOnPostback()
        {
            var component = new TextBoxComponent
            {
                Id = "txtEmail",
                Name = "Email",
                Value = "test@example.com",
                MaxLength = 100
            };

            var htmlString = TestHelper.GetHtmlString(component.WriteHtml());

            htmlString.Should().Contain("value=\"test@example.com\"");
            htmlString.Should().Contain("name=\"Email\"");
            htmlString.Should().Contain("maxlength=\"100\"");
        }
    }
}
