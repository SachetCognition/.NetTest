namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton;

    /// <summary>
    /// US-FC-005: RadioButton Component Tests
    /// </summary>
    public class RadioButtonTests
    {
        /// <summary>TC-FC-005-U01: RadioButtonComponent renders input[type=radio]</summary>
        [Fact]
        public void TC_FC_005_U01_RadioButtonComponent_RendersRadio()
        {
            var component = new RadioButtonComponent
            {
                Id = "radio1",
                GroupName = "gender",
                Value = "male"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input[type='radio']");

            input.Should().NotBeNull("should render an input element with type=radio");
            input.GetAttribute("id").Should().Be("radio1");
            input.GetAttribute("name").Should().Be("gender");
            input.GetAttribute("value").Should().Be("male");
        }

        /// <summary>TC-FC-005-U02: RadioButton selected value model binding</summary>
        [Fact]
        public void TC_FC_005_U02_RadioButton_SelectedValueModelBinding()
        {
            var component = new RadioButtonComponent
            {
                Id = "radio1",
                GroupName = "color",
                Value = "red",
                IsChecked = true
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input[type='radio']");

            input.Should().NotBeNull();
            input.HasAttribute("checked").Should().BeTrue("selected radio should have checked attribute");
            input.GetAttribute("name").Should().Be("color");
            input.GetAttribute("value").Should().Be("red");
        }
    }
}
