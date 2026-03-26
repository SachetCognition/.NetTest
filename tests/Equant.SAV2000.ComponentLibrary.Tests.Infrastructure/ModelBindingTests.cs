namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Xunit;
    using FluentAssertions;
    using Moq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Http;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    /// <summary>
    /// US-INF-006: Model Binding tests
    /// </summary>
    public class ModelBindingTests
    {
        #region Helpers

        private static ModelBindingContext CreateBindingContext(string modelName, Dictionary<string, string> formValues)
        {
            var valueProvider = new Mock<IValueProvider>();

            foreach (var kvp in formValues)
            {
                valueProvider.Setup(v => v.GetValue(kvp.Key))
                    .Returns(new ValueProviderResult(new Microsoft.Extensions.Primitives.StringValues(kvp.Value), CultureInfo.InvariantCulture));
            }

            // Return None for keys not in the dictionary
            valueProvider.Setup(v => v.GetValue(It.Is<string>(s => !formValues.ContainsKey(s))))
                .Returns(ValueProviderResult.None);

            var modelState = new ModelStateDictionary();

            var bindingContext = new Mock<ModelBindingContext>();
            bindingContext.SetupProperty(c => c.Result);
            bindingContext.Setup(c => c.ModelName).Returns(modelName);
            bindingContext.Setup(c => c.ValueProvider).Returns(valueProvider.Object);
            bindingContext.Setup(c => c.ModelState).Returns(modelState);

            return bindingContext.Object;
        }

        #endregion

        /// <summary>
        /// TC-INF-006-U01: DateTimeWithFormatBinder parses French date format.
        /// Verifies that the binder correctly parses a date in dd/MM/yyyy (French) format.
        /// </summary>
        [Fact]
        public async Task TC_INF_006_U01_DateTimeWithFormatBinder_Parses_French_Date_Format()
        {
            // Arrange
            var binder = new DateTimeWithFormatBinder();
            var formValues = new Dictionary<string, string>
            {
                { "MyDate.Date", "25/12/2024" },  // d/M/yyyy format
                { "MyDate.Format", DateTimeConstants.FrenchFormat },  // "d/M/yyyy"
                { "MyDate.Type", DateTimeConstants.StandardFormat },
                { "MyDate.TimeOffset", "0" },
                { "MyDate.Utc", DateTimeConstants.IsNonUtc },
                { "MyDate.DropDownHours", "14" },
                { "MyDate.DropDownMins", "30" }
            };
            var bindingContext = CreateBindingContext("MyDate", formValues);

            // Act
            await binder.BindModelAsync(bindingContext);

            // Assert
            bindingContext.Result.IsModelSet.Should().BeTrue();
            var result = bindingContext.Result.Model as DateTimeWithFormat;
            result.Should().NotBeNull();
            result.Format.Should().Be(DateTimeConstants.FrenchFormat);
            result.DateText.Should().Be("25/12/2024");
            result.HourValue.Should().Be("14");
            result.MinuteValue.Should().Be("30");
            result.Date.Should().NotBeNull();
            result.Date.Value.Year.Should().Be(2024);
            result.Date.Value.Month.Should().Be(12);
            result.Date.Value.Day.Should().Be(25);
            result.Date.Value.Hour.Should().Be(14);
            result.Date.Value.Minute.Should().Be(30);
        }

        /// <summary>
        /// TC-INF-006-U02: DateTimeWithFormatBinder parses English date format.
        /// Verifies that the binder correctly parses a date in MM/dd/yyyy (English) format.
        /// </summary>
        [Fact]
        public async Task TC_INF_006_U02_DateTimeWithFormatBinder_Parses_English_Date_Format()
        {
            // Arrange
            var binder = new DateTimeWithFormatBinder();
            var formValues = new Dictionary<string, string>
            {
                { "MyDate.Date", "12/25/2024" },  // M/d/yyyy format
                { "MyDate.Format", DateTimeConstants.EnglishFormat },  // "M/d/yyyy"
                { "MyDate.Type", DateTimeConstants.StandardFormat },
                { "MyDate.TimeOffset", "0" },
                { "MyDate.Utc", DateTimeConstants.IsNonUtc },
                { "MyDate.DropDownHours", "10" },
                { "MyDate.DropDownMins", "15" }
            };
            var bindingContext = CreateBindingContext("MyDate", formValues);

            // Act
            await binder.BindModelAsync(bindingContext);

            // Assert
            bindingContext.Result.IsModelSet.Should().BeTrue();
            var result = bindingContext.Result.Model as DateTimeWithFormat;
            result.Should().NotBeNull();
            result.Format.Should().Be(DateTimeConstants.EnglishFormat);
            result.DateText.Should().Be("12/25/2024");
            result.HourValue.Should().Be("10");
            result.MinuteValue.Should().Be("15");
            result.Date.Should().NotBeNull();
            result.Date.Value.Year.Should().Be(2024);
            result.Date.Value.Month.Should().Be(12);
            result.Date.Value.Day.Should().Be(25);
            result.Date.Value.Hour.Should().Be(10);
            result.Date.Value.Minute.Should().Be(15);
        }

        /// <summary>
        /// TC-INF-006-U03: WeekYearWithFormatBinder parses week/year form data.
        /// Verifies that the binder correctly parses week and year values and computes the date.
        /// </summary>
        [Fact]
        public async Task TC_INF_006_U03_WeekYearWithFormatBinder_Parses_WeekYear_FormData()
        {
            // Arrange
            var binder = new WeekYearWithFormatBinder();
            var formValues = new Dictionary<string, string>
            {
                { "MyWeek.WeekText", "10" },
                { "MyWeek.YearText", "2024" },
                { "MyWeek.Format", "English" },
                { "MyWeek.TimeOffset", "0" },
                { "MyWeek.IsUtcMode", "false" }
            };
            var bindingContext = CreateBindingContext("MyWeek", formValues);

            // Act
            await binder.BindModelAsync(bindingContext);

            // Assert
            bindingContext.Result.IsModelSet.Should().BeTrue();
            var result = bindingContext.Result.Model as WeekYearWithFormat;
            result.Should().NotBeNull();
            result.WeekText.Should().Be("10");
            result.YearText.Should().Be("2024");
            result.Format.Should().Be(WeekFormat.English);
            result.Date.Should().NotBeNull();

            // Week 10 of 2024 should be early March 2024
            // The first day of week 10 in 2024 (ISO week) should be a Monday in March
            result.Date.Value.Year.Should().Be(2024);
            result.Date.Value.DayOfWeek.Should().Be(DayOfWeek.Monday);
        }

        /// <summary>
        /// TC-INF-006-U04: DataTableContextModelBinder binds DataTable request context.
        /// Verifies that the binder deserializes a JSON context string into a DataTableContext object.
        /// </summary>
        [Fact]
        public async Task TC_INF_006_U04_DataTableContextModelBinder_Binds_DataTable_Request_Context()
        {
            // Arrange
            var binder = new DataTableContextModelBinder();
            var jsonContext = "{\"Sorting\":[[\"Name\",\"asc\"],[\"Date\",\"desc\"]],\"DisplayStart\":20,\"SelectState\":{\"row1\":true,\"row2\":false}}";
            var formValues = new Dictionary<string, string>
            {
                { "MyTable.Context", jsonContext }
            };
            var bindingContext = CreateBindingContext("MyTable", formValues);

            // Act
            await binder.BindModelAsync(bindingContext);

            // Assert
            bindingContext.Result.IsModelSet.Should().BeTrue();
            var result = bindingContext.Result.Model as DataTableContext;
            result.Should().NotBeNull();
            result.DisplayStart.Should().Be(20);

            // Verify sorting
            result.Sorting.Should().NotBeNull();
            result.Sorting.Should().HaveCount(2);
            result.Sorting[0].Key.Should().Be("Name");
            result.Sorting[0].Value.Should().Be(SortDirection.Asc);
            result.Sorting[1].Key.Should().Be("Date");
            result.Sorting[1].Value.Should().Be(SortDirection.Desc);

            // Verify select state
            result.SelectState.Should().NotBeNull();
            result.SelectState.Should().HaveCount(2);
            result.SelectState["row1"].Should().BeTrue();
            result.SelectState["row2"].Should().BeFalse();
        }

        /// <summary>
        /// TC-INF-006-R01: Model binding output regression vs .NET FW 4.8.
        /// Verifies that the model binders produce identical outputs to the .NET Framework versions.
        /// Tests DateTimeWithFormat construction, parsing, and the Format property behavior.
        /// </summary>
        [Fact]
        public void TC_INF_006_R01_Model_Binding_Regression()
        {
            // Test 1: DateTimeWithFormat construction and format validation
            // In .NET Framework, invalid format throws ArgumentException - same in .NET 8+
            Action invalidFormat = () => new DateTimeWithFormat("invalid", false);
            invalidFormat.Should().Throw<ArgumentException>();

            // Test 2: DateTimeWithFormat with French format
            var frenchDt = new DateTimeWithFormat(DateTimeConstants.FrenchFormat, false);
            frenchDt.Format.Should().Be(DateTimeConstants.FrenchFormat); // "d/M/yyyy"
            frenchDt.IsModel.Should().BeFalse();
            frenchDt.IsEmpty.Should().BeTrue();

            // Test 3: DateTimeWithFormat with English format
            var englishDt = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
            englishDt.Format.Should().Be(DateTimeConstants.EnglishFormat); // "M/d/yyyy"
            englishDt.IsModel.Should().BeFalse();

            // Test 4: Date parsing regression - French format "d/M/yyyy" parses 25/12/2024 -> Dec 25, 2024
            frenchDt.DateText = "25/12/2024";
            frenchDt.HourValue = "0";
            frenchDt.MinuteValue = "0";
            frenchDt.Date.Should().NotBeNull();
            frenchDt.Date.Value.Should().Be(new DateTime(2024, 12, 25, 0, 0, 0));

            // Test 5: Date parsing regression - English format "M/d/yyyy" parses 12/25/2024 -> Dec 25, 2024
            englishDt.DateText = "12/25/2024";
            englishDt.HourValue = "0";
            englishDt.MinuteValue = "0";
            englishDt.Date.Should().NotBeNull();
            englishDt.Date.Value.Should().Be(new DateTime(2024, 12, 25, 0, 0, 0));

            // Test 6: Both formats produce the same date for the same calendar date
            frenchDt.Date.Value.Should().Be(englishDt.Date.Value);

            // Test 7: WeekAndYear regression
            var weekAndYear = WeekHelper.GetWeekYearFromDateTime(new DateTime(2024, 3, 4)); // Monday of week 10
            weekAndYear.Week.Should().Be(10);
            weekAndYear.Year.Should().Be(2024);

            // Test 8: DataTableContext default values regression
            var context = new DataTableContext();
            context.DisplayStart.Should().Be(-1);
            context.Sorting.Should().BeNull();
            context.SelectState.Should().BeNull();
        }
    }
}
