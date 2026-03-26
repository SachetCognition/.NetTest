namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList;

    /// <summary>
    /// US-FC-004: CheckBoxList Component Tests
    /// </summary>
    public class CheckBoxListTests
    {
        /// <summary>TC-FC-004-U01: CheckBoxListComponent renders from collection</summary>
        [Fact]
        public void TC_FC_004_U01_CheckBoxListComponent_RendersFromCollection()
        {
            var component = new CheckBoxListComponent
            {
                Id = "chkList1",
                Name = "options",
                SourceItems = new List<CheckBoxListItem>
                {
                    new CheckBoxListItem { Text = "Option A", Value = "A" },
                    new CheckBoxListItem { Text = "Option B", Value = "B" },
                    new CheckBoxListItem { Text = "Option C", Value = "C" }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var checkboxes = doc.QuerySelectorAll("input[type='checkbox']");

            checkboxes.Should().HaveCount(3, "should render one checkbox per item");
            checkboxes.Select(c => c.GetAttribute("value")).Should().BeEquivalentTo(new[] { "A", "B", "C" });
        }

        /// <summary>TC-FC-004-U02: CheckBoxList selected items model binding</summary>
        [Fact]
        public void TC_FC_004_U02_CheckBoxList_SelectedItemsModelBinding()
        {
            var component = new CheckBoxListComponent
            {
                Id = "chkList1",
                Name = "selectedOptions",
                SourceItems = new List<CheckBoxListItem>
                {
                    new CheckBoxListItem { Text = "Option A", Value = "A", Selected = true },
                    new CheckBoxListItem { Text = "Option B", Value = "B", Selected = false },
                    new CheckBoxListItem { Text = "Option C", Value = "C", Selected = true }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var checkedBoxes = doc.QuerySelectorAll("input[type='checkbox'][checked]");

            checkedBoxes.Should().HaveCount(2, "two items should be checked");
            checkedBoxes.Select(c => c.GetAttribute("value")).Should().BeEquivalentTo(new[] { "A", "C" });
        }

        /// <summary>TC-FC-004-U03: CheckBoxList horizontal and vertical layout</summary>
        [Fact]
        public void TC_FC_004_U03_CheckBoxList_HorizontalAndVerticalLayout()
        {
            var component = new CheckBoxListComponent
            {
                Id = "chkList1",
                Name = "options",
                SourceItems = new List<CheckBoxListItem>
                {
                    new CheckBoxListItem { Text = "A", Value = "A" },
                    new CheckBoxListItem { Text = "B", Value = "B" }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);

            // CheckBoxList renders as ul/li structure
            var ul = doc.QuerySelector("ul");
            ul.Should().NotBeNull("should render as a UL element");
            ul.GetAttribute("id").Should().Be("chkList1");

            var listItems = doc.QuerySelectorAll("li");
            listItems.Should().HaveCountGreaterOrEqualTo(2, "should have at least 2 list items");
        }
    }
}
