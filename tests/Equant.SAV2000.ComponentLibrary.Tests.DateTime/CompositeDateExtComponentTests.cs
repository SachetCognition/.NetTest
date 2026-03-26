namespace Equant.SAV2000.ComponentLibrary.Tests.DateTime
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.Encodings.Web;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Microsoft.AspNetCore.Html;
    using Xunit;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers;

    /// <summary>
    /// US-DT-003: CompositeDateExt component tests (2 tests)
    /// </summary>
    public class CompositeDateExtComponentTests
    {
        /// <summary>
        /// TC-DT-003-U01: CompositeDateExtComponent renders with depth controls
        /// Verifies the extended composite date renders with depth Add/Subtract controls.
        /// </summary>
        [Fact]
        public void TC_DT_003_U01_CompositeDateExtComponent_RendersWithDepthControls()
        {
            // Arrange
            var component = new CompositeDateExtComponent();
            component.Id = "extDate";
            component.Name = "ExtDate";
            component.IsVisible = true;
            var viewModel = new CompositeDateExtViewModel();
            viewModel.SelectedDateType = EnumDateTypes.Equal;
            viewModel.DepthAdd = 5;
            viewModel.DepthSubtract = 3;
            component.ViewModel = viewModel;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Main div exists
            var mainDiv = document.QuerySelector("[id$='MainDiv']");
            mainDiv.Should().NotBeNull("Main div should exist");

            // Depth container exists
            var depthContainer = document.QuerySelector("#extDateDepthContainer");
            depthContainer.Should().NotBeNull("Depth container should exist");

            // Depth Add input
            var depthAddInput = document.QuerySelector("#extDateDepthAdd");
            depthAddInput.Should().NotBeNull("Depth Add input should exist");
            depthAddInput.GetAttribute("value").Should().Be("5", "Depth Add value should be 5");

            // Depth Subtract input
            var depthSubInput = document.QuerySelector("#extDateDepthSubtract");
            depthSubInput.Should().NotBeNull("Depth Subtract input should exist");
            depthSubInput.GetAttribute("value").Should().Be("3", "Depth Subtract value should be 3");

            // Date type dropdown should also exist (inherited from CompositeDate pattern)
            var dropdown = document.QuerySelector("#extDateDdlDateType");
            dropdown.Should().NotBeNull("Date type dropdown should exist");
            dropdown.QuerySelectorAll("option").Length.Should().Be(16, "Should have 16 date type options");
        }

        /// <summary>
        /// TC-DT-003-U02: CompositeDateExt Add/Subtract day depth fields
        /// Verifies the depth fields render correctly with labels and inputs.
        /// </summary>
        [Fact]
        public void TC_DT_003_U02_CompositeDateExt_AddSubtractDayDepthFields()
        {
            // Arrange
            var component = new CompositeDateExtComponent();
            component.Id = "depthDate";
            component.Name = "DepthDate";
            component.IsVisible = true;
            var viewModel = new CompositeDateExtViewModel();
            viewModel.DepthAdd = 10;
            viewModel.DepthSubtract = 7;
            component.ViewModel = viewModel;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Depth container
            var depthContainer = document.QuerySelector("#depthDateDepthContainer");
            depthContainer.Should().NotBeNull("Depth container should exist");
            depthContainer.ClassList.Should().Contain("depth-container", "Depth container should have correct CSS class");

            // Verify labels exist within depth container
            var labels = depthContainer.QuerySelectorAll("label");
            labels.Length.Should().BeGreaterOrEqualTo(2, "Should have at least 2 labels (Add and Subtract)");

            // Verify Add input
            var addInput = document.QuerySelector("#depthDateDepthAdd");
            addInput.Should().NotBeNull("Depth Add input should exist");
            addInput.GetAttribute("value").Should().Be("10");
            addInput.ClassList.Should().Contain("depthTextbox");

            // Verify Subtract input
            var subInput = document.QuerySelector("#depthDateDepthSubtract");
            subInput.Should().NotBeNull("Depth Subtract input should exist");
            subInput.GetAttribute("value").Should().Be("7");
            subInput.ClassList.Should().Contain("depthTextbox");

            // Verify init script includes depth container reference
            var script = component.RenderInitScript();
            script.Should().Contain("depthContainer:'#depthDateDepthContainer'", "Init script should reference depth container");
            script.Should().Contain("compositeDateExt", "Should initialize compositeDateExt jQuery plugin");
        }
    }
}
