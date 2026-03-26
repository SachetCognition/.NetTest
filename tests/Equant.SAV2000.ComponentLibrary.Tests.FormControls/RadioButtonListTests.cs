namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList;

    /// <summary>
    /// US-FC-006: RadioButtonList Component Tests
    /// </summary>
    public class RadioButtonListTests
    {
        /// <summary>TC-FC-006-U01: RadioButtonListComponent renders group</summary>
        [Fact]
        public void TC_FC_006_U01_RadioButtonListComponent_RendersGroup()
        {
            var component = new RadioButtonListComponent
            {
                Id = "rbList1",
                GroupName = "priority",
                Items = new List<RadioButtonListItem>
                {
                    new RadioButtonListItem { Text = "Low", Value = "low" },
                    new RadioButtonListItem { Text = "Medium", Value = "medium" },
                    new RadioButtonListItem { Text = "High", Value = "high" }
                },
                SelectedValue = "medium"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var radios = doc.QuerySelectorAll("input[type='radio']");

            radios.Should().HaveCount(3, "should render one radio per item");
            radios.All(r => r.GetAttribute("name") == "priority").Should().BeTrue("all radios should share the same group name");

            var selected = doc.QuerySelector("input[type='radio'][checked]");
            selected.Should().NotBeNull("medium should be checked");
            selected.GetAttribute("value").Should().Be("medium");
        }

        /// <summary>TC-FC-006-U02: RadioButtonList configurable layout</summary>
        [Fact]
        public void TC_FC_006_U02_RadioButtonList_ConfigurableLayout()
        {
            var component = new RadioButtonListComponent
            {
                Id = "rbList1",
                GroupName = "layout",
                Layout = RadioButtonListLayout.Horizontal,
                Items = new List<RadioButtonListItem>
                {
                    new RadioButtonListItem { Text = "A", Value = "a" },
                    new RadioButtonListItem { Text = "B", Value = "b" }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var container = doc.QuerySelector("ul");

            container.Should().NotBeNull();
            container.ClassList.Should().Contain("horizontal", "horizontal layout should add 'horizontal' class");
        }
    }
}
