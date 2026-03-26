namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DualList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    /// <summary>
    /// US-FC-011: DualList Component Tests
    /// </summary>
    public class DualListTests
    {
        /// <summary>TC-FC-011-U01: DualListComponent renders two list boxes</summary>
        [Fact]
        public void TC_FC_011_U01_DualListComponent_RendersTwoListBoxes()
        {
            var component = new DualListComponent
            {
                Id = "dl1",
                Name = "selectedItems",
                AvailableItems = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "A", Value = "a" },
                        new DataCollectionItem { Text = "B", Value = "b" }
                    }
                },
                SelectedItems = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "C", Value = "c" }
                    }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var selects = doc.QuerySelectorAll("select");

            selects.Should().HaveCount(2, "should render two select elements (available + selected)");

            var container = doc.QuerySelector("div.dual-list");
            container.Should().NotBeNull("should have a dual-list container");
            container.GetAttribute("id").Should().Be("dl1");

            // Verify add/remove buttons
            var buttons = doc.QuerySelectorAll("button");
            buttons.Should().HaveCountGreaterOrEqualTo(2, "should have add and remove buttons");
        }

        /// <summary>TC-FC-011-U02: DualList item transfer JavaScript</summary>
        [Fact]
        public void TC_FC_011_U02_DualList_ItemTransferJavaScript()
        {
            var component = new DualListComponent
            {
                Id = "dl1"
            };

            var script = component.WriteInitScript();

            script.Should().Contain("$('#dl1')");
            script.Should().Contain(".savDualList(");
        }
    }
}
