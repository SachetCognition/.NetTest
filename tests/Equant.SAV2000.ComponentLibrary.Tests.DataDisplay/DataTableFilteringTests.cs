using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Xunit;

using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-004: DataTable Filtering Tests
    /// </summary>
    public class DataTableFilteringTests
    {
        /// <summary>
        /// TC-DD-004-U01: Column filter script loaded when IsFilter=true
        /// </summary>
        [Fact]
        public void TC_DD_004_U01_ColumnFilterScript_Loaded_WhenIsFilterTrue()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "filterTable";
            component.IsFilter = true;

            // Act
            var jsResources = component.JsResources;

            // Assert
            jsResources.Any(r => r.Name == "JsjquerydataTablesdatatablescolumnFilter").Should().BeTrue(
                "column filter script should be loaded when IsFilter=true");
        }

        /// <summary>
        /// TC-DD-004-U02: Filter not loaded when IsFilter=false
        /// </summary>
        [Fact]
        public void TC_DD_004_U02_FilterNotLoaded_WhenIsFilterFalse()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "noFilterTable";
            component.IsFilter = false;

            // Act
            var jsResources = component.JsResources;

            // Assert
            jsResources.Any(r => r.Name == "JsjquerydataTablesdatatablescolumnFilter").Should().BeFalse(
                "column filter script should NOT be loaded when IsFilter=false");
        }

        /// <summary>
        /// Additional test: Filter row rendered in HTML when IsFilter=true
        /// </summary>
        [Fact]
        public void TC_DD_004_R01_FilterRow_Rendered_WhenIsFilterTrue()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "filterHtmlTable";
            component.IsFilter = true;
            component.IsShowHeader = true;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" },
                new DataTableColumn { PropertyName = "Col1", HeaderText = "Column 1" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();

                var context = BrowsingContext.New(Configuration.Default);
                var parser = context.GetService<IHtmlParser>();
                var doc = parser.ParseDocument("<html><body>" + html + "</body></html>");

                // Assert
                var filterRow = doc.QuerySelector("table#filterHtmlTable thead tr.filter-search");
                filterRow.Should().NotBeNull("filter row should be rendered when IsFilter=true");

                var filterCells = doc.QuerySelectorAll("table#filterHtmlTable thead tr.filter-search td");
                filterCells.Length.Should().Be(2, "filter row should have cells for each column");
            }
        }
    }
}
