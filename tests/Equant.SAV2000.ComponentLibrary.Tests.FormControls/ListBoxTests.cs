namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    /// <summary>
    /// US-FC-010: ListBox Component Tests
    /// </summary>
    public class ListBoxTests
    {
        /// <summary>TC-FC-010-U01: ListBoxComponent renders select[multiple]</summary>
        [Fact]
        public void TC_FC_010_U01_ListBoxComponent_RendersSelectMultiple()
        {
            var component = new ListBoxComponent
            {
                Id = "lb1",
                Name = "items",
                Size = 5,
                DataSource = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "Item 1", Value = "1" },
                        new DataCollectionItem { Text = "Item 2", Value = "2" },
                        new DataCollectionItem { Text = "Item 3", Value = "3" }
                    }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var select = doc.QuerySelector("select");

            select.Should().NotBeNull("should render a select element");
            select.HasAttribute("multiple").Should().BeTrue("should have multiple attribute");
            select.GetAttribute("size").Should().Be("5");
            select.GetAttribute("id").Should().Be("lb1");

            var options = doc.QuerySelectorAll("option");
            options.Should().HaveCount(3);
        }

        /// <summary>TC-FC-010-U02: ListBox multiple selection binding</summary>
        [Fact]
        public void TC_FC_010_U02_ListBox_MultipleSelectionBinding()
        {
            var component = new ListBoxComponent
            {
                Id = "lb1",
                Name = "items",
                SelectedValues = new List<string> { "1", "3" },
                DataSource = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "Item 1", Value = "1" },
                        new DataCollectionItem { Text = "Item 2", Value = "2" },
                        new DataCollectionItem { Text = "Item 3", Value = "3" }
                    }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var selectedOptions = doc.QuerySelectorAll("option[selected]");

            selectedOptions.Should().HaveCount(2, "items 1 and 3 should be selected");
            selectedOptions.Select(o => o.GetAttribute("value")).Should().BeEquivalentTo(new[] { "1", "3" });
        }
    }
}
