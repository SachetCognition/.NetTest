namespace Equant.SAV2000.ComponentLibrary.Tests.DateTime
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Moq;
    using Xunit;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.Tests.DateTime.Helpers;

    /// <summary>
    /// US-DT-004: WeekYear component tests (3 tests)
    /// </summary>
    public class WeekYearComponentTests
    {
        /// <summary>
        /// TC-DT-004-U01: WeekYearComponent renders week/year inputs
        /// Verifies the component renders week and year input fields.
        /// </summary>
        [Fact]
        public void TC_DT_004_U01_WeekYearComponent_RendersWeekYearInputs()
        {
            // Arrange
            var component = new WeekYearComponent();
            component.Id = "weekYear";
            component.Name = "WeekYear";
            component.Value = new WeekYearWithFormat(12, 2026, "English");

            // Act
            var html = component.RenderHtml();
            var htmlString = HtmlTestHelper.GetHtmlString(html);
            var document = HtmlTestHelper.ParseHtml(htmlString);

            // Assert
            // Main div
            var mainDiv = document.QuerySelector("#weekYearMainDiv");
            mainDiv.Should().NotBeNull("Main div should exist");

            // Week input
            var weekInput = document.QuerySelector("#weekYearTxtWeek");
            weekInput.Should().NotBeNull("Week input should exist");
            weekInput.GetAttribute("type").Should().Be("text");
            weekInput.GetAttribute("name").Should().Be("WeekYear.Week");
            weekInput.GetAttribute("value").Should().Be("12", "Week value should be 12");
            weekInput.GetAttribute("maxlength").Should().Be("2", "Week maxlength should be 2");
            weekInput.ClassList.Should().Contain("weekTextbox");

            // Year input
            var yearInput = document.QuerySelector("#weekYearTxtYear");
            yearInput.Should().NotBeNull("Year input should exist");
            yearInput.GetAttribute("type").Should().Be("text");
            yearInput.GetAttribute("name").Should().Be("WeekYear.Year");
            yearInput.GetAttribute("value").Should().Be("2026", "Year value should be 2026");
            yearInput.GetAttribute("maxlength").Should().Be("4", "Year maxlength should be 4");
            yearInput.ClassList.Should().Contain("yearTextbox");

            // Hidden format field
            var formatHidden = document.QuerySelector("#weekYearHdnFormat");
            formatHidden.Should().NotBeNull("Hidden format field should exist");
            formatHidden.GetAttribute("value").Should().Be("English");

            // Week label
            var weekLabel = document.QuerySelector(".week-label");
            weekLabel.Should().NotBeNull("Week label should exist");
        }

        /// <summary>
        /// TC-DT-004-U02: WeekYearWithFormat model binding
        /// Verifies the WeekYearWithFormat class correctly stores week/year/format data.
        /// </summary>
        [Fact]
        public void TC_DT_004_U02_WeekYearWithFormat_ModelBinding()
        {
            // Arrange & Act
            var weekYearFormat = new WeekYearWithFormat(25, 2026, "French");

            // Assert
            weekYearFormat.Value.Should().NotBeNull("Value should not be null");
            weekYearFormat.Value.Week.Should().Be(25, "Week should be 25");
            weekYearFormat.Value.Year.Should().Be(2026, "Year should be 2026");
            weekYearFormat.Format.Should().Be("French", "Format should be French");
            weekYearFormat.IsEmpty.Should().BeFalse("Should not be empty when week and year are set");

            // Test empty state
            var emptyWeekYear = new WeekYearWithFormat();
            emptyWeekYear.IsEmpty.Should().BeTrue("Should be empty when no values set");
            emptyWeekYear.Value.Week.Should().BeNull("Week should be null");
            emptyWeekYear.Value.Year.Should().BeNull("Year should be null");
            emptyWeekYear.Format.Should().Be("English", "Default format should be English");

            // Test WeekAndYear directly
            var weekAndYear = new WeekAndYear(1, 2026);
            weekAndYear.Week.Should().Be(1);
            weekAndYear.Year.Should().Be(2026);
            weekAndYear.IsEmpty.Should().BeFalse();

            var emptyWeekAndYear = new WeekAndYear();
            emptyWeekAndYear.IsEmpty.Should().BeTrue();
        }

        /// <summary>
        /// TC-DT-004-U03: WeekYearWithFormatBinder parses form data correctly
        /// Verifies the model binder correctly parses week, year, and format from form values.
        /// </summary>
        [Fact]
        public async Task TC_DT_004_U03_WeekYearWithFormatBinder_ParsesFormDataCorrectly()
        {
            // Arrange
            var binder = new WeekYearWithFormatBinder();

            var formValues = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "MyWeek.Week", "15" },
                { "MyWeek.Year", "2026" },
                { "MyWeek.Format", "French" }
            };

            var valueProvider = new FormValueProvider(
                BindingSource.Form,
                new Microsoft.AspNetCore.Http.FormCollection(formValues),
                CultureInfo.InvariantCulture);

            var modelMetadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(WeekYearWithFormat));

            var bindingContext = new DefaultModelBindingContext
            {
                ModelName = "MyWeek",
                ModelMetadata = modelMetadata,
                ValueProvider = valueProvider
            };

            // Act
            await binder.BindModelAsync(bindingContext);

            // Assert
            bindingContext.Result.IsModelSet.Should().BeTrue("Model should be set after binding");
            var result = bindingContext.Result.Model as WeekYearWithFormat;
            result.Should().NotBeNull("Result should be WeekYearWithFormat");
            result.Value.Week.Should().Be(15, "Week should be parsed as 15");
            result.Value.Year.Should().Be(2026, "Year should be parsed as 2026");
            result.Format.Should().Be("French", "Format should be French");
        }
    }
}
