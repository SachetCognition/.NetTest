using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-001: DataTable Client-Side Tests
    /// </summary>
    public class DataTableClientSideTests
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

        private static string RenderHtml(DataTableComponent component)
        {
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                return sw.ToString();
            }
        }

        private static string RenderInitScript(DataTableComponent component)
        {
            using (var sw = new StringWriter())
            {
                component.WriteInitScript(sw);
                return sw.ToString();
            }
        }

        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// TC-DD-001-U01: DataTableComponent default configuration
        /// Verifies default page size 10, horizontal scroll, CSS class
        /// </summary>
        [Fact]
        public void TC_DD_001_U01_DataTableComponent_DefaultConfiguration()
        {
            // Arrange & Act
            var component = new DataTableComponent();

            // Assert
            component.PageSize.Should().Be(10, "default page size should be 10");
            component.IsHorizontalScrolling.Should().BeTrue("horizontal scrolling should be enabled by default");
            component.CssClass.Should().Be("display table dataTable", "default CSS class should be set");
            component.IsPaginate.Should().BeTrue("pagination should be enabled by default");
            component.IsReorder.Should().BeTrue("column reorder should be enabled by default");
            component.IsSelect.Should().BeFalse("select should be disabled by default");
            component.IsDeleteNeeded.Should().BeFalse("delete should be disabled by default");
            component.IsModifyNeeded.Should().BeFalse("modify should be disabled by default");
            component.IsShowHeader.Should().BeTrue("header should be shown by default");
            component.IsSelectAll.Should().BeTrue("select all should be enabled by default");
            component.SelectColumnIndex.Should().Be(0);
            component.DeleteColumnIndex.Should().Be(1);
            component.ModifyColumnnIndex.Should().Be(2);
        }

        /// <summary>
        /// TC-DD-001-U02: DataTableHtmlBuilder renders table from System.Data.DataTable
        /// Verifies table with 3 cols, 5 rows is rendered correctly
        /// </summary>
        [Fact]
        public void TC_DD_001_U02_DataTableHtmlBuilder_RendersTable_3Cols5Rows()
        {
            // Arrange
            var dt = CreateSampleDataTable(3, 5);
            var component = new DataTableComponent();
            component.Id = "testTable";
            component.Data = dt;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" },
                new DataTableColumn { PropertyName = "Col1", HeaderText = "Column 1" },
                new DataTableColumn { PropertyName = "Col2", HeaderText = "Column 2" }
            };

            // Act
            var html = RenderHtml(component);
            var doc = ParseHtml(html);

            // Assert
            html.Should().NotBeNullOrEmpty();
            var table = doc.QuerySelector("table#testTable");
            table.Should().NotBeNull("a <table> with id 'testTable' should be rendered");

            var thElements = doc.QuerySelectorAll("table#testTable thead th");
            thElements.Length.Should().Be(3, "there should be 3 column headers");

            var tbody = doc.QuerySelector("table#testTable tbody");
            tbody.Should().NotBeNull("a <tbody> should be rendered");
        }

        /// <summary>
        /// TC-DD-001-U03: DataTable pagination type SwanPagination in options JSON
        /// </summary>
        [Fact]
        public void TC_DD_001_U03_DataTable_PaginationType_SwanPagination()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "testTable";
            component.IsPaginate = true;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            var serializer = new DataTableOptionsSerializer(component);
            var json = serializer.Serialize();
            var jObj = JObject.Parse(json);

            // Assert
            var paginationType = jObj.SelectToken("dataTableInit.sPaginationType");
            paginationType.Should().NotBeNull("pagination type should be in the JSON");
            paginationType.Value<string>().Should().Be("SwanPagination", "pagination type should be SwanPagination");
        }

        /// <summary>
        /// TC-DD-001-U04: DataTable column reorder script loading
        /// </summary>
        [Fact]
        public void TC_DD_001_U04_DataTable_ColumnReorder_ScriptLoading()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "testTable";
            component.IsReorder = true;

            // Act
            var jsResources = component.JsResources;

            // Assert
            jsResources.Should().NotBeNull();
            jsResources.Any(r => r.Name == "JsjquerydataTablescolMoveResize").Should().BeTrue(
                "column reorder script should be loaded when IsReorder=true");
        }

        /// <summary>
        /// TC-DD-001-I01: DataTable full init script with jQuery plugin JSON
        /// </summary>
        [Fact]
        public void TC_DD_001_I01_DataTable_FullInitScript_jQueryPlugin()
        {
            // Arrange
            var dt = CreateSampleDataTable(2, 3);
            var component = new DataTableComponent();
            component.Id = "myDataTable";
            component.Data = dt;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" },
                new DataTableColumn { PropertyName = "Col1", HeaderText = "Column 1" }
            };

            // Act
            var initScript = RenderInitScript(component);

            // Assert
            initScript.Should().Contain("$('#myDataTable').dataTableCore(", "init script should contain jQuery plugin call");
            initScript.Should().Contain("dataTableInit", "init script should contain dataTableInit options");
            initScript.Should().Contain("\"sScrollX\"", "init script should contain scroll options");

            // Verify the JSON options string contains key properties
            // Note: The serialized output contains JRaw JS expressions, so we verify via string contains
            initScript.Should().Contain("\"iDisplayLength\":10", "display length should be 10");
            initScript.Should().Contain("\"sPaginationType\":\"SwanPagination\"", "pagination type should be SwanPagination");
            initScript.Should().Contain("\"bServerSide\":false", "server-side should be false for client-side mode");
        }

        /// <summary>
        /// TC-DD-001-R01: DataTable HTML output regression
        /// </summary>
        [Fact]
        public void TC_DD_001_R01_DataTable_HtmlOutput_Regression()
        {
            // Arrange
            var dt = CreateSampleDataTable(2, 2);
            var component = new DataTableComponent();
            component.Id = "regressionTable";
            component.Name = "regressionTable";
            component.Data = dt;
            component.Caption = "Test Caption";
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Header 0" },
                new DataTableColumn { PropertyName = "Col1", HeaderText = "Header 1" }
            };

            // Act
            var html = RenderHtml(component);
            var doc = ParseHtml(html);

            // Assert - Verify structural elements
            var table = doc.QuerySelector("table#regressionTable");
            table.Should().NotBeNull("table should be rendered");
            table.GetAttribute("name").Should().Be("regressionTable");

            var caption = doc.QuerySelector("table#regressionTable caption");
            caption.Should().NotBeNull("caption should be rendered");
            caption.TextContent.Should().Contain("Test Caption");

            var thead = doc.QuerySelector("table#regressionTable thead");
            thead.Should().NotBeNull("thead should be rendered");

            var ths = doc.QuerySelectorAll("table#regressionTable thead th");
            ths.Length.Should().Be(2);
            ths[0].TextContent.Should().Contain("Header 0");
            ths[1].TextContent.Should().Contain("Header 1");

            var tbody = doc.QuerySelector("table#regressionTable tbody");
            tbody.Should().NotBeNull("tbody should be rendered");

            // Verify error message template is also rendered
            html.Should().Contain("_ErrorId", "error message template should be rendered");
        }
    }
}
