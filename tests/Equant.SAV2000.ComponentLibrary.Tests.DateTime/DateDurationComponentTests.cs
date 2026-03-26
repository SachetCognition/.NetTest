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

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateDuration;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers;

    /// <summary>
    /// US-DT-005: DateDuration component tests (1 test)
    /// </summary>
    public class DateDurationComponentTests
    {
        /// <summary>
        /// TC-DT-005-U01: DateDurationComponent renders date with duration
        /// Verifies the component renders date input along with duration fields (days, hours, minutes).
        /// </summary>
        [Fact]
        public void TC_DT_005_U01_DateDurationComponent_RendersDateWithDuration()
        {
            // Arrange
            var component = new DateDurationComponent();
            component.Id = "dateDur";
            component.Name = "DateDur";
            component.IsVisible = true;
            var value = new DateDuration();
            value.DateValue = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            value.DateValue.DateText = "03/26/2026";
            value.DurationDays = 5;
            value.DurationHours = 8;
            value.DurationMinutes = 30;
            component.Value = value;

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Main div
            var mainDiv = document.QuerySelector("#dateDurMainDiv");
            mainDiv.Should().NotBeNull("Main div should exist");

            // Date container
            var dateContainer = document.QuerySelector("#dateDurDateContainer");
            dateContainer.Should().NotBeNull("Date container should exist");
            dateContainer.ClassList.Should().Contain("date-container");

            // Date input within container
            var dateInput = document.QuerySelector("#dateDurTxtDate");
            dateInput.Should().NotBeNull("Date input should exist");
            dateInput.GetAttribute("value").Should().Be("03/26/2026");

            // Calendar icon
            var calIcon = document.QuerySelector("#dateDurCalendarIcon");
            calIcon.Should().NotBeNull("Calendar icon should exist");

            // Duration container
            var durationContainer = document.QuerySelector("#dateDurDurationContainer");
            durationContainer.Should().NotBeNull("Duration container should exist");
            durationContainer.ClassList.Should().Contain("duration-container");

            // Duration Days input
            var daysInput = document.QuerySelector("#dateDurDurationDays");
            daysInput.Should().NotBeNull("Duration Days input should exist");
            daysInput.GetAttribute("value").Should().Be("5");
            daysInput.ClassList.Should().Contain("durationTextbox");

            // Duration Hours input
            var hoursInput = document.QuerySelector("#dateDurDurationHours");
            hoursInput.Should().NotBeNull("Duration Hours input should exist");
            hoursInput.GetAttribute("value").Should().Be("8");

            // Duration Minutes input
            var minutesInput = document.QuerySelector("#dateDurDurationMinutes");
            minutesInput.Should().NotBeNull("Duration Minutes input should exist");
            minutesInput.GetAttribute("value").Should().Be("30");

            // Verify DateDuration model
            var duration = value.GetDuration();
            duration.Should().NotBeNull("Duration should not be null");
            duration.Value.Days.Should().Be(5);
            duration.Value.Hours.Should().Be(8);
            duration.Value.Minutes.Should().Be(30);
        }
    }
}
