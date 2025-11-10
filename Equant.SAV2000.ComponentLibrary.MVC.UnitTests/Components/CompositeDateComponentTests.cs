using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System;
using System.IO;
using System.Collections.Generic;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class CompositeDateComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public CompositeDateComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        #region Constructor and Basic Properties Tests

        [Fact]
        public void Constructor_InitializesWithDefaults()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
            Assert.True(component.DisplayTime);
            Assert.True(component.DisplayEraseButton);
            Assert.NotNull(component.CustomLabel);
            Assert.NotNull(component.InformationIcon);
        }

        [Fact]
        public void JsResources_NotEmpty_AlwaysIncludesCompositeScript()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
            Assert.NotEmpty(resources);
            Assert.True(resources.Count >= 1);
        }

        #endregion

        #region Builder Fluent API Tests

        [Fact]
        public void Builder_CssMainDiv_SetsProperty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);

            builder.CssMainDiv("custom-css-class");

            Assert.Equal("custom-css-class", component.CssMainDiv);
        }

        [Fact]
        public void Builder_DisplayTime_SetsProperty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);

            builder.DisplayTime(false);

            Assert.False(component.DisplayTime);
        }

        [Fact]
        public void Builder_DisplayEraseButton_SetsProperty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);

            builder.DisplayEraseButton(false);

            Assert.False(component.DisplayEraseButton);
        }

        [Fact]
        public void Builder_DateTypesIncluded_SetsProperty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);
            var dateTypes = new List<EnumDateTypes> { EnumDateTypes.Between, EnumDateTypes.Equal };

            builder.DateTypesIncluded(dateTypes);

            Assert.Equal(dateTypes, component.DateTypesIncluded);
        }

        [Fact]
        public void Builder_DateTypes_SetsAllProperties()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);
            var dateTypes = new List<EnumDateTypes> { EnumDateTypes.Between };

            builder.DateTypes("DateTypesName", "Between", dateTypes);

            Assert.Equal("DateTypesName", component.DateTypesName);
            Assert.Equal("Between", component.DateTypesValue);
            Assert.Equal(dateTypes, component.DateTypesIncluded);
        }

        [Fact]
        public void Builder_CssClassLabelDiv_SetsProperty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);

            builder.CssClassLabelDiv("label-div-css");

            Assert.Equal("label-div-css", component.CssClassLabelDiv);
        }

        [Fact]
        public void Builder_CssClassDateTypesSelectDiv_SetsProperty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);

            builder.CssClassDateTypesSelectDiv("select-div-css");

            Assert.Equal("select-div-css", component.CssClassDateTypesSelectDiv);
        }

        [Fact]
        public void Builder_FluentApi_ReturnsBuilderForChaining()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);

            var result = builder.CssMainDiv("css1")
                               .DisplayTime(true)
                               .DisplayEraseButton(false)
                               .CssClassLabelDiv("css2");

            Assert.IsType<CompositeDateBuilder>(result);
            Assert.Equal("css1", component.CssMainDiv);
            Assert.True(component.DisplayTime);
            Assert.False(component.DisplayEraseButton);
            Assert.Equal("css2", component.CssClassLabelDiv);
        }

        #endregion

        #region WriteHtml Precondition Tests

        [Fact]
        public void WriteHtml_Throws_When_DateTypesIncluded_Null()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            component.Id = "comp1";
            component.DateTypesName = "DateTypes";
            component.DateTypesValue = "Between";
            component.DateTypesIncluded = null!;

            using var writer = new StringWriter();

            Assert.Throws<NullReferenceException>(() => component.WriteHtml(writer));
        }

        [Fact]
        public void WriteHtml_Throws_When_DateTypesIncluded_Empty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            component.Id = "comp1";
            component.DateTypesName = "DateTypes";
            component.DateTypesValue = "Between";
            component.DateTypesIncluded = new List<EnumDateTypes>();

            using var writer = new StringWriter();

            var exception = Assert.Throws<ArgumentException>(() => component.WriteHtml(writer));
            Assert.Contains("is not in the given DateTypesIncluded list", exception.Message);
        }

        [Fact]
        public void WriteHtml_Throws_When_DateTypesName_Empty()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            component.Id = "comp1";
            component.DateTypesName = "";
            component.DateTypesValue = "Between";
            component.DateTypesIncluded = new List<EnumDateTypes> { EnumDateTypes.Between };

            using var writer = new StringWriter();

            var exception = Assert.Throws<ArgumentException>(() => component.WriteHtml(writer));
            Assert.Contains("DateTypesName cannot be null or empty", exception.Message);
        }

        [Fact]
        public void WriteHtml_Throws_When_DateTypesValue_Null()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            component.Id = "comp1";
            component.DateTypesName = "DateTypes";
            component.DateTypesValue = null!;
            component.DateTypesIncluded = new List<EnumDateTypes> { EnumDateTypes.Between };

            using var writer = new StringWriter();

            // When null, TryParse fails and throws with "is not a valid EnumDateTypes"
            var exception = Assert.Throws<ArgumentException>(() => component.WriteHtml(writer));
            Assert.Contains("is not a valid EnumDateTypes", exception.Message);
        }

        [Fact]
        public void WriteHtml_Throws_When_DateTypesValue_InvalidEnum()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            component.Id = "comp1";
            component.DateTypesName = "DateTypes";
            component.DateTypesValue = "NotAValidType";
            component.DateTypesIncluded = new List<EnumDateTypes> { EnumDateTypes.Between };

            using var writer = new StringWriter();

            var exception = Assert.Throws<ArgumentException>(() => component.WriteHtml(writer));
            Assert.Contains("is not a valid EnumDateTypes", exception.Message);
        }

        [Fact]
        public void WriteHtml_Throws_When_SelectedType_NotInIncluded()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            component.Id = "comp1";
            component.DateTypesName = "DateTypes";
            component.DateTypesValue = "Between";
            component.DateTypesIncluded = new List<EnumDateTypes> { EnumDateTypes.Equal };

            using var writer = new StringWriter();

            var exception = Assert.Throws<ArgumentException>(() => component.WriteHtml(writer));
            Assert.Contains("is not in the given DateTypesIncluded list", exception.Message);
        }

        #endregion

        #region WriteHtml Happy Path Tests

        [Fact]
        public void WriteHtml_WritesMarkup_For_ValidBetweenScenario()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);
            
            builder.Id("comp1")
                   .DateTypes("DateTypes", "Between", new List<EnumDateTypes> { EnumDateTypes.Between })
                   .CustomLabel(c => c.Text("Date Range"));

            using var writer = new StringWriter();
            
            var exception = Record.Exception(() => component.WriteHtml(writer));
            Assert.Null(exception);
            
            var html = writer.ToString();
            Assert.NotEmpty(html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_WritesNoOutput()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);
            
            builder.Id("comp1")
                   .DateTypes("DateTypes", "Between", new List<EnumDateTypes> { EnumDateTypes.Between });
            
            component.IsVisible = false;

            using var writer = new StringWriter();
            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        #endregion

        #region WriteInitScript Tests

        [Fact]
        public void WriteInitScript_Throws_OnNullWriter()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);

            var exception = Assert.Throws<ArgumentException>(() => component.WriteInitScript(null!));
            Assert.Contains("The parameter writer cannot be null", exception.Message);
        }

        [Fact]
        public void WriteInitScript_NoThrow_When_Configured()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);
            
            builder.Id("comp1")
                   .DateTypes("DateTypes", "Between", new List<EnumDateTypes> { EnumDateTypes.Between });

            using var writer = new StringWriter();
            
            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("comp1", script);
            Assert.Contains("compositeDate", script);
        }

        [Fact]
        public void WriteInitScript_IncludesComponentId_InOutput()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var builder = new CompositeDateBuilder(component, null);
            
            builder.Id("myCompositeDate")
                   .DateTypes("DateTypes", "Between", new List<EnumDateTypes> { EnumDateTypes.Between });

            using var writer = new StringWriter();
            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("myCompositeDate", script);
        }

        #endregion

        #region Date and Week Format Tests

        [Fact]
        public void FirstDateAndFormat_GetSet_WorksCorrectly()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var dateFormat = component.FirstDateAndFormat;

            Assert.NotNull(dateFormat);
            Assert.NotNull(dateFormat.Format);
        }

        [Fact]
        public void SecondDateAndFormat_GetSet_WorksCorrectly()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var dateFormat = component.SecondDateAndFormat;

            Assert.NotNull(dateFormat);
            Assert.NotNull(dateFormat.Format);
        }

        [Fact]
        public void FirstWeekAndFormat_GetSet_WorksCorrectly()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var weekFormat = new WeekYearWithFormat();

            component.FirstWeekAndFormat = weekFormat;

            Assert.Equal(weekFormat, component.FirstWeekAndFormat);
        }

        [Fact]
        public void SecondWeekAndFormat_GetSet_WorksCorrectly()
        {
            var component = new CompositeDateComponent(_mockHtmlHelper.Object);
            var weekFormat = new WeekYearWithFormat();

            component.SecondWeekAndFormat = weekFormat;

            Assert.Equal(weekFormat, component.SecondWeekAndFormat);
        }

        #endregion
    }
}
