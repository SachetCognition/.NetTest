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

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Duration;
    using Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers;

    /// <summary>
    /// US-DT-006: Duration component tests (2 tests)
    /// </summary>
    public class DurationComponentTests
    {
        /// <summary>
        /// TC-DT-006-U01: DurationComponent renders H/M/S/MS inputs
        /// Verifies the component renders hours, minutes, seconds, and milliseconds input fields.
        /// </summary>
        [Fact]
        public void TC_DT_006_U01_DurationComponent_RendersHMSMSInputs()
        {
            // Arrange
            var component = new DurationComponent();
            component.Id = "dur";
            component.Name = "Dur";
            component.IsVisible = true;
            component.DisplaySeconds = true;
            component.DisplayMilliseconds = true;
            component.Value = new DurationEntity(2, 30, 45, 500);

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Main div
            var mainDiv = document.QuerySelector("#durMainDiv");
            mainDiv.Should().NotBeNull("Main div should exist");

            // Hours input
            var hoursInput = document.QuerySelector("#durTxtHours");
            hoursInput.Should().NotBeNull("Hours input should exist");
            hoursInput.GetAttribute("type").Should().Be("text");
            hoursInput.GetAttribute("name").Should().Be("Dur.Hours");
            hoursInput.GetAttribute("value").Should().Be("2", "Hours value should be 2");
            hoursInput.ClassList.Should().Contain("durationTextbox");

            // Hours label
            var hoursLabel = document.QuerySelector("label[for='durTxtHours']");
            hoursLabel.Should().NotBeNull("Hours label should exist");

            // Minutes input
            var minutesInput = document.QuerySelector("#durTxtMinutes");
            minutesInput.Should().NotBeNull("Minutes input should exist");
            minutesInput.GetAttribute("name").Should().Be("Dur.Minutes");
            minutesInput.GetAttribute("value").Should().Be("30", "Minutes value should be 30");

            // Seconds input (should be visible when DisplaySeconds = true)
            var secondsInput = document.QuerySelector("#durTxtSeconds");
            secondsInput.Should().NotBeNull("Seconds input should exist when DisplaySeconds is true");
            secondsInput.GetAttribute("name").Should().Be("Dur.Seconds");
            secondsInput.GetAttribute("value").Should().Be("45", "Seconds value should be 45");

            // Milliseconds input (should be visible when DisplayMilliseconds = true)
            var msInput = document.QuerySelector("#durTxtMilliseconds");
            msInput.Should().NotBeNull("Milliseconds input should exist when DisplayMilliseconds is true");
            msInput.GetAttribute("name").Should().Be("Dur.Milliseconds");
            msInput.GetAttribute("value").Should().Be("500", "Milliseconds value should be 500");

            // Separators
            var separators = document.QuerySelectorAll(".duration-separator");
            separators.Length.Should().BeGreaterOrEqualTo(2, "Should have at least 2 separators (: between H:M and M:S)");
        }

        /// <summary>
        /// TC-DT-006-U02: DurationEntity TimeSpan conversion
        /// Verifies the DurationEntity correctly converts to/from TimeSpan.
        /// </summary>
        [Fact]
        public void TC_DT_006_U02_DurationEntity_TimeSpanConversion()
        {
            // Arrange
            var duration = new DurationEntity(2, 30, 45, 500);

            // Act
            var timeSpan = duration.ToTimeSpan();

            // Assert
            timeSpan.Hours.Should().Be(2, "Hours should be 2");
            timeSpan.Minutes.Should().Be(30, "Minutes should be 30");
            timeSpan.Seconds.Should().Be(45, "Seconds should be 45");
            timeSpan.Milliseconds.Should().Be(500, "Milliseconds should be 500");
            timeSpan.TotalHours.Should().BeApproximately(2.5125 + (500.0 / 3600000.0), 0.001, "TotalHours should be approximately correct");

            // Test FromTimeSpan
            var fromTimeSpan = DurationEntity.FromTimeSpan(new TimeSpan(0, 1, 15, 30, 250));
            fromTimeSpan.Hours.Should().Be(1, "Hours from TimeSpan should be 1");
            fromTimeSpan.Minutes.Should().Be(15, "Minutes from TimeSpan should be 15");
            fromTimeSpan.Seconds.Should().Be(30, "Seconds from TimeSpan should be 30");
            fromTimeSpan.Milliseconds.Should().Be(250, "Milliseconds from TimeSpan should be 250");

            // Test IsEmpty
            var emptyDuration = new DurationEntity();
            emptyDuration.IsEmpty.Should().BeTrue("Default DurationEntity should be empty");
            duration.IsEmpty.Should().BeFalse("Non-zero DurationEntity should not be empty");

            // Test roundtrip
            var original = new DurationEntity(5, 45, 12, 100);
            var roundTrip = DurationEntity.FromTimeSpan(original.ToTimeSpan());
            roundTrip.Hours.Should().Be(original.Hours);
            roundTrip.Minutes.Should().Be(original.Minutes);
            roundTrip.Seconds.Should().Be(original.Seconds);
            roundTrip.Milliseconds.Should().Be(original.Milliseconds);
        }
    }
}
