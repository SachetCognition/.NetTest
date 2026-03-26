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
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers;

    /// <summary>
    /// US-DT-002: CompositeDate component tests (6 tests)
    /// </summary>
    public class CompositeDateComponentTests
    {
        /// <summary>
        /// TC-DT-002-U01: CompositeDateComponent renders date type dropdown with 16 options
        /// Verifies the dropdown contains all 16 date type options.
        /// </summary>
        [Fact]
        public void TC_DT_002_U01_CompositeDateComponent_RendersDateTypeDropdownWith16Options()
        {
            // Arrange
            var component = new CompositeDateComponent();
            component.Id = "compDate";
            component.Name = "CompDate";

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            var dropdown = document.QuerySelector("#compDateDdlDateType");
            dropdown.Should().NotBeNull("Date type dropdown should exist");

            var options = dropdown.QuerySelectorAll("option");
            options.Length.Should().Be(16, "Dropdown should have exactly 16 date type options");

            // Verify all 16 enum values are present
            var optionValues = options.Select(o => o.GetAttribute("value")).ToList();
            for (int i = 0; i <= 15; i++)
            {
                optionValues.Should().Contain(i.ToString(), $"Option value {i} ({(EnumDateTypes)i}) should be present");
            }

            // Verify option texts
            var optionTexts = options.Select(o => o.TextContent).ToList();
            optionTexts.Should().Contain("Between");
            optionTexts.Should().Contain("Equal");
            optionTexts.Should().Contain("Greater Than");
            optionTexts.Should().Contain("Less Than");
            optionTexts.Should().Contain("Less Than or Equal");
            optionTexts.Should().Contain("Greater Than or Equal");
            optionTexts.Should().Contain("Empty");
            optionTexts.Should().Contain("Current Date");
            optionTexts.Should().Contain("Model Between");
            optionTexts.Should().Contain("Model Equal");
            optionTexts.Should().Contain("Model Greater Than");
            optionTexts.Should().Contain("Model Greater Than or Equal");
            optionTexts.Should().Contain("Model Less Than");
            optionTexts.Should().Contain("Model Less Than or Equal");
            optionTexts.Should().Contain("Week");
            optionTexts.Should().Contain("Week Between");
        }

        /// <summary>
        /// TC-DT-002-U02: CompositeDate 'Between' type shows dual date fields
        /// Verifies the Between type renders both DateFrom and DateTo containers.
        /// </summary>
        [Fact]
        public void TC_DT_002_U02_CompositeDate_BetweenType_ShowsDualDateFields()
        {
            // Arrange
            var component = new CompositeDateComponent();
            component.Id = "betDate";
            component.Name = "BetDate";
            var viewModel = new CompositeDateViewModel();
            viewModel.SelectedDateType = EnumDateTypes.Between;
            viewModel.DateFrom = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            viewModel.DateFrom.DateText = "03/01/2026";
            viewModel.DateTo = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            viewModel.DateTo.DateText = "03/31/2026";
            component.ViewModel = viewModel;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Verify both date containers exist
            var dateFromContainer = document.QuerySelector("#betDateDateFromContainer");
            dateFromContainer.Should().NotBeNull("DateFrom container should exist for Between type");

            var dateToContainer = document.QuerySelector("#betDateDateToContainer");
            dateToContainer.Should().NotBeNull("DateTo container should exist for Between type");

            // Verify date inputs within containers
            var dateFromInput = document.QuerySelector("#betDateDateFrom");
            dateFromInput.Should().NotBeNull("DateFrom input should exist");
            dateFromInput.GetAttribute("value").Should().Be("03/01/2026");

            var dateToInput = document.QuerySelector("#betDateDateTo");
            dateToInput.Should().NotBeNull("DateTo input should exist");
            dateToInput.GetAttribute("value").Should().Be("03/31/2026");

            // Verify Between is selected in dropdown
            var dropdown = document.QuerySelector("#betDateDdlDateType");
            var selectedOption = dropdown.QuerySelector("option[selected]");
            selectedOption.Should().NotBeNull("Between should be selected");
            selectedOption.GetAttribute("value").Should().Be("0", "Between enum value is 0");
        }

        /// <summary>
        /// TC-DT-002-U03: CompositeDate 'Week' type shows week fields
        /// Verifies the Week type renders week/year input containers.
        /// </summary>
        [Fact]
        public void TC_DT_002_U03_CompositeDate_WeekType_ShowsWeekFields()
        {
            // Arrange
            var component = new CompositeDateComponent();
            component.Id = "wkDate";
            component.Name = "WkDate";
            var viewModel = new CompositeDateViewModel();
            viewModel.SelectedDateType = EnumDateTypes.Week;
            viewModel.WeekFrom = "12";
            viewModel.YearFrom = "2026";
            component.ViewModel = viewModel;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Verify week containers exist
            var weekFromContainer = document.QuerySelector("#wkDateWeekFromContainer");
            weekFromContainer.Should().NotBeNull("WeekFrom container should exist for Week type");

            // Verify week input
            var weekInput = document.QuerySelector("#wkDateWeekFrom");
            weekInput.Should().NotBeNull("Week input should exist");
            weekInput.GetAttribute("value").Should().Be("12");

            // Verify year input
            var yearInput = document.QuerySelector("#wkDateYearFrom");
            yearInput.Should().NotBeNull("Year input should exist");
            yearInput.GetAttribute("value").Should().Be("2026");

            // Verify Week type is selected in dropdown
            var dropdown = document.QuerySelector("#wkDateDdlDateType");
            var selectedOption = dropdown.QuerySelector("option[selected]");
            selectedOption.Should().NotBeNull("Week should be selected");
            selectedOption.GetAttribute("value").Should().Be("14", "Week enum value is 14");
        }

        /// <summary>
        /// TC-DT-002-U04: CompositeDate model date types (D+5, J-3 format)
        /// Verifies the component correctly renders model date type containers.
        /// </summary>
        [Fact]
        public void TC_DT_002_U04_CompositeDate_ModelDateTypes()
        {
            // Arrange
            var component = new CompositeDateComponent();
            component.Id = "modDate";
            component.Name = "ModDate";
            var viewModel = new CompositeDateViewModel();
            viewModel.SelectedDateType = EnumDateTypes.ModelEqual;
            viewModel.ModelValue = "D+5";
            component.ViewModel = viewModel;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Verify model container exists
            var modelContainer = document.QuerySelector("#modDateModelContainer");
            modelContainer.Should().NotBeNull("Model container should exist for Model date types");

            // Verify model input
            var modelInput = document.QuerySelector("#modDateModelValue");
            modelInput.Should().NotBeNull("Model value input should exist");
            modelInput.GetAttribute("value").Should().Be("D+5", "Model value should be D+5");
            modelInput.GetAttribute("maxlength").Should().Be("5", "Model input maxlength should be 5");

            // Verify ModelEqual is selected in dropdown
            var dropdown = document.QuerySelector("#modDateDdlDateType");
            var selectedOption = dropdown.QuerySelector("option[selected]");
            selectedOption.Should().NotBeNull("ModelEqual should be selected");
            selectedOption.GetAttribute("value").Should().Be("9", "ModelEqual enum value is 9");
        }

        /// <summary>
        /// TC-DT-002-U05: CompositeDate JavaScript show/hide logic generation
        /// Verifies the component generates correct JavaScript for show/hide logic.
        /// </summary>
        [Fact]
        public void TC_DT_002_U05_CompositeDate_JavaScriptShowHideLogicGeneration()
        {
            // Arrange
            var component = new CompositeDateComponent();
            component.Id = "jsDate";
            component.Name = "JsDate";
            component.OnDateTypeChange = "myHandler";

            // Act
            var script = component.RenderInitScript();

            // Assert
            script.Should().NotBeNullOrEmpty("Init script should not be empty");
            script.Should().Contain("<script type=\"text/javascript\">", "Should contain script tag");
            script.Should().Contain("$('#jsDateDdlDateType').compositeDate({", "Should initialize compositeDate jQuery plugin");
            script.Should().Contain("dateFromContainer:'#jsDateDateFromContainer'", "Should reference date from container");
            script.Should().Contain("dateToContainer:'#jsDateDateToContainer'", "Should reference date to container");
            script.Should().Contain("weekFromContainer:'#jsDateWeekFromContainer'", "Should reference week from container");
            script.Should().Contain("weekToContainer:'#jsDateWeekToContainer'", "Should reference week to container");
            script.Should().Contain("modelContainer:'#jsDateModelContainer'", "Should reference model container");
            script.Should().Contain("onDateTypeChange:myHandler", "Should include onDateTypeChange callback");
            script.Should().Contain("</script>", "Should close script tag");
        }

        /// <summary>
        /// TC-DT-002-I01: CompositeDate full rendering pipeline
        /// Verifies the complete rendering pipeline from component to HTML.
        /// </summary>
        [Fact]
        public void TC_DT_002_I01_CompositeDate_FullRenderingPipeline()
        {
            // Arrange
            var component = new CompositeDateComponent();
            component.Id = "fullDate";
            component.Name = "FullDate";
            component.IsVisible = true;
            component.IsUpdatable = true;
            var viewModel = new CompositeDateViewModel();
            viewModel.SelectedDateType = EnumDateTypes.Between;
            viewModel.DateFrom = new DateTimeWithFormat(DateTimeConstants.FrenchFormat, false);
            viewModel.DateFrom.DateText = "01/03/2026";
            viewModel.DateTo = new DateTimeWithFormat(DateTimeConstants.FrenchFormat, false);
            viewModel.DateTo.DateText = "31/03/2026";
            component.ViewModel = viewModel;

            // Act - Full pipeline: component -> htmlBuilder -> IHtmlContent -> string
            var htmlContent = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(htmlContent);
            var initScript = component.RenderInitScript();
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert - HTML structure
            htmlString.Should().NotBeNullOrEmpty("HTML output should not be empty");

            var mainDiv = document.QuerySelector("[id$='MainDiv']");
            mainDiv.Should().NotBeNull("Main container div should exist");

            // Date type dropdown
            var dropdown = document.QuerySelector("#fullDateDdlDateType");
            dropdown.Should().NotBeNull("Date type dropdown should exist");
            dropdown.QuerySelectorAll("option").Length.Should().Be(16);

            // Date containers
            document.QuerySelector("#fullDateDateFromContainer").Should().NotBeNull();
            document.QuerySelector("#fullDateDateToContainer").Should().NotBeNull();

            // Week containers
            document.QuerySelector("#fullDateWeekFromContainer").Should().NotBeNull();
            document.QuerySelector("#fullDateWeekToContainer").Should().NotBeNull();

            // Model container
            document.QuerySelector("#fullDateModelContainer").Should().NotBeNull();

            // Init script
            initScript.Should().NotBeNullOrEmpty("Init script should not be empty");
            initScript.Should().Contain("compositeDate");
        }
    }
}
