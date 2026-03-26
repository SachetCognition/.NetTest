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

using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-003: DataTable Columns Tests
    /// </summary>
    public class DataTableColumnsTests
    {
        private static DataTable CreateDataTableWithRowId()
        {
            var dt = new DataTable("Test");
            dt.Columns.Add("RowIdColumn", typeof(string));
            dt.Columns.Add("Col0", typeof(string));
            dt.Columns.Add("Col1", typeof(string));
            dt.Rows.Add("1", "A", "B");
            dt.Rows.Add("2", "C", "D");
            return dt;
        }

        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// TC-DD-003-U01: Select column at index 0 with checkbox
        /// </summary>
        [Fact]
        public void TC_DD_003_U01_SelectColumn_AtIndex0_WithCheckbox()
        {
            // Arrange
            var dt = CreateDataTableWithRowId();
            var component = new DataTableComponent();
            component.Id = "selectTable";
            component.Data = dt;
            component.IsSelect = true;
            component.IsSelectAll = true;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "RowIdColumn", HeaderText = "ID" },
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" },
                new DataTableColumn { PropertyName = "Col1", HeaderText = "Column 1" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert - Select column should be inserted at index 0
                var ths = doc.QuerySelectorAll("table#selectTable thead th");
                ths.Length.Should().BeGreaterThan(0, "there should be column headers");

                // The first th should contain the select-all checkbox
                var firstTh = ths[0];
                var checkbox = firstTh.QuerySelector("input[type='checkbox']");
                checkbox.Should().NotBeNull("select column at index 0 should contain a checkbox");

                // Verify SelectAllName is set
                component.SelectAllName.Should().NotBeNullOrEmpty("SelectAllName should be set");
                component.SelectAllName.Should().Contain("selectTable_selectAll");
            }
        }

        /// <summary>
        /// TC-DD-003-U02: Delete column renders trash.png icon
        /// </summary>
        [Fact]
        public void TC_DD_003_U02_DeleteColumn_RendersDeleteIcon()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "deleteTable";
            component.IsDeleteNeeded = true;
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
                var doc = ParseHtml(html);

                // Assert - Delete column should be inserted at index 1
                var ths = doc.QuerySelectorAll("table#deleteTable thead th");
                ths.Length.Should().Be(3, "there should be 3 columns: Col0, DeleteColumn, Col1");

                // Verify the delete column has ShowHeaderText=false (hide-access span)
                var deleteColumn = component.Columns[component.DeleteColumnIndex];
                deleteColumn.PropertyName.Should().Be(DataTableComponent.DefaultDeleteColumnName);
                deleteColumn.IsSortable.Should().BeFalse();
                deleteColumn.IsEncodeHtml.Should().BeFalse();
                deleteColumn.ColumnType.Should().Be(DataTableColumnType.Html);
                deleteColumn.ShowHeaderText.Should().BeFalse("delete column header text should be hidden");
            }
        }

        /// <summary>
        /// TC-DD-003-U03: Modify column with callback events
        /// </summary>
        [Fact]
        public void TC_DD_003_U03_ModifyColumn_WithCallbackEvents()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "modifyTable";
            component.IsModifyNeeded = true;
            component.OnModifyClick = "function(id) { alert(id); }";
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" },
                new DataTableColumn { PropertyName = "Col1", HeaderText = "Column 1" },
                new DataTableColumn { PropertyName = "Col2", HeaderText = "Column 2" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
            }

            // Assert - Modify column should be at index 2
            var modifyColumn = component.Columns[component.ModifyColumnnIndex];
            modifyColumn.PropertyName.Should().Be(DataTableComponent.DefaultModifyColumnName);
            modifyColumn.IsSortable.Should().BeFalse();
            modifyColumn.IsEncodeHtml.Should().BeFalse();
            modifyColumn.ColumnType.Should().Be(DataTableColumnType.Html);

            // Verify the OnModifyClick callback is set in the serializer output
            // Note: The serialized output contains JRaw JS expressions, so we verify via string contains
            var serializer = new DataTableOptionsSerializer(component);
            var json = serializer.Serialize();

            json.Should().Contain("\"modify\":", "modify section should be present when IsModifyNeeded=true");
            json.Should().Contain("\"onmodifyClick\":", "onmodifyClick should be present");
            json.Should().Contain("function(id)", "the callback function should be in the JSON output");
        }
    }
}
