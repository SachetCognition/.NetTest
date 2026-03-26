namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

    /// <summary>
    /// US-FC-009: DropDownList Component Tests
    /// </summary>
    public class DropDownListTests
    {
        /// <summary>TC-FC-009-U01: DropDownListComponent renders select with options</summary>
        [Fact]
        public void TC_FC_009_U01_DropDownListComponent_RendersSelectWithOptions()
        {
            var component = new DropDownListComponent
            {
                Id = "ddl1",
                Name = "country",
                DataSource = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "USA", Value = "us" },
                        new DataCollectionItem { Text = "UK", Value = "uk" },
                        new DataCollectionItem { Text = "Canada", Value = "ca" }
                    }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var select = doc.QuerySelector("select");
            var options = doc.QuerySelectorAll("option");

            select.Should().NotBeNull("should render a select element");
            select.GetAttribute("id").Should().Be("ddl1");
            select.GetAttribute("name").Should().Be("country");
            options.Should().HaveCount(3, "should render one option per data item");
        }

        /// <summary>TC-FC-009-U02: DropDownList default/placeholder option</summary>
        [Fact]
        public void TC_FC_009_U02_DropDownList_PlaceholderOption()
        {
            var component = new DropDownListComponent
            {
                Id = "ddl1",
                Name = "country",
                PlaceholderText = "-- Select --",
                DataSource = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "USA", Value = "us" }
                    }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var options = doc.QuerySelectorAll("option");

            options.Should().HaveCount(2, "should have placeholder + 1 data item");
            options[0].GetAttribute("value").Should().Be("", "placeholder should have empty value");
            options[0].TextContent.Should().Be("-- Select --");
        }

        /// <summary>TC-FC-009-U03: DropDownList selected value binding</summary>
        [Fact]
        public void TC_FC_009_U03_DropDownList_SelectedValueBinding()
        {
            var component = new DropDownListComponent
            {
                Id = "ddl1",
                Name = "country",
                SelectedValue = "uk",
                DataSource = new DataCollection
                {
                    Items = new List<DataCollectionItem>
                    {
                        new DataCollectionItem { Text = "USA", Value = "us" },
                        new DataCollectionItem { Text = "UK", Value = "uk" },
                        new DataCollectionItem { Text = "Canada", Value = "ca" }
                    }
                }
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var selectedOption = doc.QuerySelector("option[selected]");

            selectedOption.Should().NotBeNull("UK should be selected");
            selectedOption.GetAttribute("value").Should().Be("uk");
        }

        /// <summary>TC-FC-009-U04: DropDownList onChange cascading support</summary>
        [Fact]
        public void TC_FC_009_U04_DropDownList_OnChangeCascadingSupport()
        {
            var component = new DropDownListComponent
            {
                Id = "ddlCountry",
                Name = "country",
                CascadeFrom = "ddlRegion",
                OnChange = "handleCountryChange"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var select = doc.QuerySelector("select");

            select.Should().NotBeNull();
            select.GetAttribute("data-cascade-from").Should().Be("ddlRegion");

            var script = component.WriteInitScript();
            script.Should().Contain("handleCountryChange");
        }
    }
}
