namespace Equant.SAV2000.ComponentLibrary.Tests.DateTime
{
    using System;
    using System.IO;
    using System.Text.Encodings.Web;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Microsoft.AspNetCore.Html;
    using Xunit;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers;

    /// <summary>
    /// US-DT-001: DateTime component tests (7 tests)
    /// </summary>
    public class DateTimeComponentTests
    {
        /// <summary>
        /// TC-DT-001-U01: DateTimeComponent default configuration
        /// Verifies that a new DateTimeComponent has correct default property values.
        /// </summary>
        [Fact]
        public void TC_DT_001_U01_DateTimeComponent_DefaultConfiguration()
        {
            // Arrange & Act
            var component = new DateTimeComponent();
            component.Id = "testDate";
            component.Name = "TestDate";

            // Assert
            component.DisplayTime.Should().BeTrue("DisplayTime should default to true");
            component.DisplayEraseButton.Should().BeFalse("DisplayEraseButton should default to false");
            component.DisplayInformationIcon.Should().BeFalse("DisplayInformationIcon should default to false");
            component.DisplayCurrentDateSelector.Should().BeFalse("DisplayCurrentDateSelector should default to false");
            component.StartFromCurrentDate.Should().BeFalse("StartFromCurrentDate should default to false");
            component.OnDateChange.Should().Be("null", "OnDateChange should default to 'null'");
            component.Value.Should().NotBeNull("Value should not be null");
            component.Value.Format.Should().Be(DateTimeConstants.EnglishFormat, "Default format should be English");
            component.Value.IsModel.Should().BeFalse("IsModel should default to false");
            component.IsVisible.Should().BeTrue("IsVisible should default to true");
            component.IsUpdatable.Should().BeTrue("IsUpdatable should default to true");
            component.CustomLabel.Should().NotBeNull("CustomLabel should not be null");
            component.InformationIcon.Should().NotBeNull("InformationIcon should not be null");
        }

        /// <summary>
        /// TC-DT-001-U02: DateTimeHtmlBuilder renders date input with calendar icon
        /// Verifies the HTML output contains a text input for date and a calendar icon span.
        /// </summary>
        [Fact]
        public void TC_DT_001_U02_DateTimeHtmlBuilder_RendersDateInputWithCalendarIcon()
        {
            // Arrange
            var component = new DateTimeComponent();
            component.Id = "myDate";
            component.Name = "MyDate";
            component.IsVisible = true;
            component.IsUpdatable = true;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            htmlString.Should().NotBeNullOrEmpty("HTML output should not be empty");

            // Should have main div
            var mainDiv = document.QuerySelector("#myDateMainDiv");
            mainDiv.Should().NotBeNull("Main div with correct ID should exist");

            // Should have date text input
            var dateInput = document.QuerySelector("#myDateTxtDate");
            dateInput.Should().NotBeNull("Date text input should exist");
            dateInput.GetAttribute("type").Should().Be("text");
            dateInput.GetAttribute("name").Should().Be("MyDate.Date");

            // Should have calendar icon
            var calIcon = document.QuerySelector("#myDateCalendarIcon");
            calIcon.Should().NotBeNull("Calendar icon span should exist");
            calIcon.ClassList.Should().Contain("calendar-icon");
        }

        /// <summary>
        /// TC-DT-001-U03: DateTime French format dd/MM/yyyy
        /// Verifies the component correctly handles French date format.
        /// </summary>
        [Fact]
        public void TC_DT_001_U03_DateTime_FrenchFormat()
        {
            // Arrange
            var component = new DateTimeComponent();
            component.Id = "frDate";
            component.Name = "FrDate";
            component.Value = new DateTimeWithFormat(DateTimeConstants.FrenchFormat, false);
            component.Value.DateText = "25/03/2026";
            component.Value.HourValue = "14";
            component.Value.MinuteValue = "30";

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            var dateInput = document.QuerySelector("#frDateTxtDate");
            dateInput.Should().NotBeNull("Date input should exist");
            dateInput.GetAttribute("value").Should().Be("25/03/2026", "Date value should be in French format dd/MM/yyyy");

            // Verify format hidden field
            var formatHidden = document.QuerySelector("#frDateHdnFormat");
            formatHidden.Should().NotBeNull("Hidden format field should exist");
            formatHidden.GetAttribute("value").Should().Be(DateTimeConstants.FrenchFormat, "Format should be French");

            // Verify init script uses French JS format
            var script = component.RenderInitScript();
            script.Should().Contain("dd/mm/yy", "Init script should use French JS date format");
        }

        /// <summary>
        /// TC-DT-001-U04: DateTime English format MM/dd/yyyy
        /// Verifies the component correctly handles English date format.
        /// </summary>
        [Fact]
        public void TC_DT_001_U04_DateTime_EnglishFormat()
        {
            // Arrange
            var component = new DateTimeComponent();
            component.Id = "enDate";
            component.Name = "EnDate";
            component.Value = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            component.Value.DateText = "03/25/2026";
            component.Value.HourValue = "2";
            component.Value.MinuteValue = "30";

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            var dateInput = document.QuerySelector("#enDateTxtDate");
            dateInput.Should().NotBeNull("Date input should exist");
            dateInput.GetAttribute("value").Should().Be("03/25/2026", "Date value should be in English format MM/dd/yyyy");

            // Verify format hidden field
            var formatHidden = document.QuerySelector("#enDateHdnFormat");
            formatHidden.Should().NotBeNull("Hidden format field should exist");
            formatHidden.GetAttribute("value").Should().Be(DateTimeConstants.EnglishFormat, "Format should be English");

            // Verify init script uses English JS format
            var script = component.RenderInitScript();
            script.Should().Contain("mm/dd/yy", "Init script should use English JS date format");
        }

        /// <summary>
        /// TC-DT-001-U05: DateTime UTC mode configuration
        /// Verifies the component correctly handles UTC mode.
        /// </summary>
        [Fact]
        public void TC_DT_001_U05_DateTime_UtcModeConfiguration()
        {
            // Arrange
            var component = new DateTimeComponent();
            component.Id = "utcDate";
            component.Name = "UtcDate";
            var value = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            value.IsUtcMode = true;
            value.TimeOffset = 0;
            component.Value = value;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Verify UTC hidden field
            var utcHidden = document.QuerySelector("#utcDateHdnUtc");
            utcHidden.Should().NotBeNull("Hidden UTC field should exist");
            utcHidden.GetAttribute("value").Should().Be(DateTimeConstants.IsUtc, "UTC mode should be enabled");

            // Verify init script contains utcMode
            var script = component.RenderInitScript();
            script.Should().Contain("utcMode:true", "Init script should contain utcMode:true");
        }

        /// <summary>
        /// TC-DT-001-I01: DateTime jQuery UI datepicker integration script
        /// Verifies the component generates correct jQuery plugin initialization script.
        /// </summary>
        [Fact]
        public void TC_DT_001_I01_DateTime_JQueryDatepickerIntegrationScript()
        {
            // Arrange
            var component = new DateTimeComponent();
            component.Id = "dpDate";
            component.Name = "DpDate";
            component.DisplayTime = true;
            component.IsUpdatable = true;
            component.DisplayEraseButton = true;
            component.DisplayCurrentDateSelector = true;
            component.OnDateChange = "myCallback";

            // Act
            var script = component.RenderInitScript();

            // Assert
            script.Should().NotBeNullOrEmpty("Init script should not be empty");
            script.Should().Contain("<script type=\"text/javascript\">", "Should contain script tag");
            script.Should().Contain("$('#dpDateTxtDate').dateTime({", "Should initialize dateTime jQuery plugin");
            script.Should().Contain("dateFormat:'mm/dd/yy'", "Should include date format");
            script.Should().Contain("displayTime:true", "Should include displayTime option");
            script.Should().Contain("isUpdatable:true", "Should include isUpdatable option");
            script.Should().Contain("onDateChange:myCallback", "Should include onDateChange callback");
            script.Should().Contain("eraseButtonId:'dpDateLnkErase'", "Should include erase button ID");
            script.Should().Contain("currentDateImageId:'dpDateLnkCurrentDate'", "Should include current date image ID");
            script.Should().Contain("</script>", "Should close script tag");
        }

        /// <summary>
        /// TC-DT-001-R01: DateTime HTML output regression
        /// Verifies the complete HTML structure rendered by the DateTime component.
        /// </summary>
        [Fact]
        public void TC_DT_001_R01_DateTime_HtmlOutputRegression()
        {
            // Arrange
            var component = new DateTimeComponent();
            component.Id = "regDate";
            component.Name = "RegDate";
            component.IsVisible = true;
            component.IsUpdatable = true;
            component.DisplayTime = true;
            component.DisplayEraseButton = true;
            component.DisplayCurrentDateSelector = true;
            component.Value = new DateTimeWithFormat(DateTimeConstants.FrenchFormat, false);
            component.Value.DateText = "26/03/2026";
            component.Value.HourValue = "10";
            component.Value.MinuteValue = "15";

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert - Verify complete HTML structure
            // Main div
            var mainDiv = document.QuerySelector("#regDateMainDiv");
            mainDiv.Should().NotBeNull("Main div should exist");

            // Date input
            var dateInput = document.QuerySelector("#regDateTxtDate");
            dateInput.Should().NotBeNull("Date input should exist");
            dateInput.GetAttribute("value").Should().Be("26/03/2026");
            dateInput.GetAttribute("maxlength").Should().Be("10");

            // Hidden fields
            document.QuerySelector("#regDateHdnFormat").Should().NotBeNull("Format hidden field should exist");
            document.QuerySelector("#regDateHdnTimeOffset").Should().NotBeNull("TimeOffset hidden field should exist");
            document.QuerySelector("#regDateHdnUtc").Should().NotBeNull("UTC hidden field should exist");
            document.QuerySelector("#regDateHdnType").Should().NotBeNull("Type hidden field should exist");

            // Calendar icon
            document.QuerySelector("#regDateCalendarIcon").Should().NotBeNull("Calendar icon should exist");

            // Erase button
            var eraseLink = document.QuerySelector("#regDateLnkErase");
            eraseLink.Should().NotBeNull("Erase button should exist");
            eraseLink.ClassList.Should().Contain("cleanImage");

            // Current date selector
            var currentDateLink = document.QuerySelector("#regDateLnkCurrentDate");
            currentDateLink.Should().NotBeNull("Current date selector should exist");

            // Hour dropdown
            var hourDropDown = document.QuerySelector("#regDateDdlHour");
            hourDropDown.Should().NotBeNull("Hour dropdown should exist");
            hourDropDown.GetAttribute("name").Should().Be("RegDate.Hour");

            // Minute dropdown
            var minDropDown = document.QuerySelector("#regDateDdlMinute");
            minDropDown.Should().NotBeNull("Minute dropdown should exist");
            minDropDown.GetAttribute("name").Should().Be("RegDate.Minute");

            // Validation span
            var valSpan = document.QuerySelector("span[data-valmsg-for='RegDate.Date']");
            valSpan.Should().NotBeNull("Validation span should exist");
        }
    }
}
