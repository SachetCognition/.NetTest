using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Xunit;

using Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-006: TreeGrid Tests
    /// </summary>
    public class TreeGridTests
    {
        private static DataTable CreateHierarchicalDataTable()
        {
            var dt = new DataTable("TreeData");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ParentID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("HasChild", typeof(string));
            dt.Columns.Add("Depth", typeof(int));
            dt.Columns.Add("Created", typeof(bool));

            dt.Rows.Add(1, 0, "Root Node 1", "Yes", 0, false);
            dt.Rows.Add(2, 1, "Child Node 1.1", "No", 1, false);
            dt.Rows.Add(3, 1, "Child Node 1.2", "No", 1, false);
            dt.Rows.Add(4, 0, "Root Node 2", "No", 0, false);

            return dt;
        }

        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument("<html><body>" + html + "</body></html>");
        }

        /// <summary>
        /// TC-DD-006-U01: TreeGrid renders hierarchical data
        /// </summary>
        [Fact]
        public void TC_DD_006_U01_TreeGrid_RendersHierarchicalData()
        {
            // Arrange
            var dt = CreateHierarchicalDataTable();
            var component = new TreeGridComponent();
            component.Id = "treeGrid";
            component.Data = dt;
            component.KeyColumnName = "ID";
            component.Columns = new List<TreeViewColumn>
            {
                new TreeViewColumn { PropertyName = "Name", HeaderText = "Name", TreeColumnType = ColumnType.Text, IsHeaderVisible = true }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert
                html.Should().NotBeNullOrEmpty("TreeGrid should produce HTML output");

                var treeDiv = doc.QuerySelector("div#treeGrid");
                treeDiv.Should().NotBeNull("a div with id 'treeGrid' should be rendered");
                treeDiv.ClassList.Should().Contain("tree", "tree div should have 'tree' class");
                treeDiv.ClassList.Should().Contain("row", "tree div should have 'row' class");

                // Check for hierarchical structure
                var ulElements = doc.QuerySelectorAll("div#treeGrid ul");
                ulElements.Length.Should().BeGreaterThan(0, "there should be ul elements for tree structure");

                // Check for parent nodes with expand/collapse
                var parentLi = doc.QuerySelectorAll("li.parent_li");
                parentLi.Length.Should().BeGreaterThan(0, "there should be parent_li elements for expandable nodes");

                // Check for header
                var headerLi = doc.QuerySelector("li.tree-head");
                headerLi.Should().NotBeNull("a tree header should be rendered");

                // Check for node content
                html.Should().Contain("Root Node 1", "root node text should be rendered");
                html.Should().Contain("Child Node 1.1", "child node text should be rendered");
                html.Should().Contain("Root Node 2", "second root node text should be rendered");
            }
        }

        /// <summary>
        /// TC-DD-006-U02: TreeGrid checkbox and image column types
        /// </summary>
        [Fact]
        public void TC_DD_006_U02_TreeGrid_CheckboxAndImageColumnTypes()
        {
            // Arrange
            var dt = new DataTable("TreeData");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ParentID", typeof(int));
            dt.Columns.Add("HasChild", typeof(string));
            dt.Columns.Add("Depth", typeof(int));
            dt.Columns.Add("Created", typeof(bool));
            dt.Columns.Add("CheckCol", typeof(object));
            dt.Columns.Add("ImageCol", typeof(object));

            var checkboxData = new CheckBoxTextColumn
            {
                Text = "Check Item 1",
                AccessText = "Select Item 1",
                IsVisible = true,
                IsUpdatable = true
            };

            var imageData = new ImageTextColumn
            {
                Text = "Image Label",
                LstImage = new List<TreeGridimage>
                {
                    new TreeGridimage
                    {
                        Source = "/images/icon.png",
                        Title = "Icon",
                        ActionUrl = "/action/1",
                        IsVisible = true
                    }
                }
            };

            dt.Rows.Add(1, 0, "No", 0, false, checkboxData, imageData);

            var component = new TreeGridComponent();
            component.Id = "treeGridTypes";
            component.Name = "treeGridTypes";
            component.Data = dt;
            component.KeyColumnName = "ID";
            component.Columns = new List<TreeViewColumn>
            {
                new TreeViewColumn
                {
                    PropertyName = "CheckCol",
                    HeaderText = "Selection",
                    TreeColumnType = ColumnType.CheckBox,
                    IsHeaderVisible = true
                },
                new TreeViewColumn
                {
                    PropertyName = "ImageCol",
                    HeaderText = "Image",
                    TreeColumnType = ColumnType.Image,
                    IsHeaderVisible = true
                }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();
                var doc = ParseHtml(html);

                // Assert - Checkbox column
                var checkboxInput = doc.QuerySelector("input[type='checkbox']");
                checkboxInput.Should().NotBeNull("checkbox input should be rendered for CheckBox column");

                // Assert - Image column
                var imgElement = doc.QuerySelector("img");
                imgElement.Should().NotBeNull("img element should be rendered for Image column");
                imgElement.GetAttribute("src").Should().Be("/images/icon.png");

                // Assert - Image link
                var imgLink = doc.QuerySelector("a img");
                imgLink.Should().NotBeNull("image should be wrapped in anchor");

                // Assert column headers
                html.Should().Contain("Selection", "checkbox column header should be rendered");
                html.Should().Contain("Image", "image column header should be rendered");
            }
        }
    }
}
