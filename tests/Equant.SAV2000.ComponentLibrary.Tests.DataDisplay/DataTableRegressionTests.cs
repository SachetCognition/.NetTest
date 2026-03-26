using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;

using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// DataTable Regression Tests (5 additional tests)
    /// </summary>
    public class DataTableRegressionTests
    {
        private static DataTable CreateSampleDataTable(int cols, int rows)
        {
            var dt = new DataTable("TestTable");
            for (int c = 0; c < cols; c++)
            {
                dt.Columns.Add("Col" + c, typeof(string));
            }
            for (int r = 0; r < rows; r++)
            {
                var row = dt.NewRow();
                for (int c = 0; c < cols; c++)
                {
                    row["Col" + c] = "Cell_" + r + "_" + c;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// Regression: DataTable with no header should add NoHeader CSS class
        /// </summary>
        [Fact]
        public void TC_DD_R01_DataTable_NoHeader_CssClass()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "noHeaderTable";
            component.IsShowHeader = false;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                var table = doc.QuerySelector("table#noHeaderTable");
                table.Should().NotBeNull();
                table.ClassList.Should().Contain("NoHeader", "table should have NoHeader class when IsShowHeader=false");
            }
        }

        /// <summary>
        /// Regression: DataTable with header should add HeaderSpace CSS class
        /// </summary>
        [Fact]
        public void TC_DD_R02_DataTable_WithHeader_HeaderSpaceCssClass()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "headerTable";
            component.IsShowHeader = true;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                var table = doc.QuerySelector("table#headerTable");
                table.Should().NotBeNull();
                table.ClassList.Should().Contain("HeaderSpace", "table should have HeaderSpace class when IsShowHeader=true");
            }
        }

        /// <summary>
        /// Regression: DataTable CalculateDisplayStart boundary conditions
        /// </summary>
        [Fact]
        public void TC_DD_R03_DataTable_CalculateDisplayStart_Boundary()
        {
            // When displayStart >= totalRecords, should return last page start
            var result1 = DataTableComponent.CalculateDisplayStart(100, 50, 10);
            result1.Should().Be(40, "when displayStart >= totalRecords, should return last page start ((50-1)/10)*10 = 40");

            // When displayStart < totalRecords, should return displayStart as-is
            var result2 = DataTableComponent.CalculateDisplayStart(20, 50, 10);
            result2.Should().Be(20, "when displayStart < totalRecords, should return displayStart as-is");

            // Edge case: displayStart exactly at totalRecords
            var result3 = DataTableComponent.CalculateDisplayStart(50, 50, 10);
            result3.Should().Be(40, "when displayStart == totalRecords, should return last page start");
        }

        /// <summary>
        /// Regression: DataTable builder fluent API preserves all properties
        /// </summary>
        [Fact]
        public void TC_DD_R04_DataTable_BuilderFluentApi_PreservesProperties()
        {
            // Arrange & Act
            var component = new DataTableComponent();
            var builder = new DataTableBuilder(component);

            builder
                .Id("fluentTable")
                .Name("fluentTableName")
                .Caption("Fluent Caption")
                .PageSize(25)
                .IsFilter(true)
                .IsHorizontalScrolling(false)
                .IsReorder(false)
                .IsPaginate(false)
                .ServiceUri("/api/test")
                .TotalRecordsLimit(500)
                .IsEncryptionRequired(true);

            // Assert
            component.Id.Should().Be("fluentTable");
            component.Name.Should().Be("fluentTableName");
            component.Caption.Should().Be("Fluent Caption");
            component.PageSize.Should().Be(25);
            component.IsFilter.Should().BeTrue();
            component.IsHorizontalScrolling.Should().BeFalse();
            component.IsReorder.Should().BeFalse();
            component.IsPaginate.Should().BeFalse();
            component.ServiceUri.Should().Be("/api/test");
            component.TotalRecordsLimit.Should().Be(500);
            component.IsEncryptionRequired.Should().BeTrue();
        }

        /// <summary>
        /// Regression: DataTable DomLayout includes R prefix when reorder and header shown
        /// </summary>
        [Fact]
        public void TC_DD_R05_DataTable_DomLayout_RPrefix_WhenReorderAndHeader()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "domLayoutTable";
            component.IsReorder = true;
            component.IsShowHeader = true;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            var serializer = new DataTableOptionsSerializer(component);
            var json = serializer.Serialize();
            var jObj = JObject.Parse(json);

            // Assert
            var dom = jObj.SelectToken("dataTableInit.sDom");
            dom.Should().NotBeNull("sDom should be present");
            dom.Value<string>().Should().StartWith("R", "sDom should start with 'R' when IsReorder=true and IsShowHeader=true");

            // Now test without reorder
            component.IsReorder = false;
            var serializer2 = new DataTableOptionsSerializer(component);
            var json2 = serializer2.Serialize();
            var jObj2 = JObject.Parse(json2);
            var dom2 = jObj2.SelectToken("dataTableInit.sDom");
            dom2.Should().NotBeNull();
            dom2.Value<string>().Should().NotStartWith("R", "sDom should NOT start with 'R' when IsReorder=false");
        }
    }
}
