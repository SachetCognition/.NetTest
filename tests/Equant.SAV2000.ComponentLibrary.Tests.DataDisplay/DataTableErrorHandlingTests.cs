using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-005: DataTable Error Handling Tests
    /// </summary>
    public class DataTableErrorHandlingTests
    {
        private static IHtmlHelper CreateMockHtmlHelper(string pathBase = "", string numFen = "1", string cookieName = "TestCookie")
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var mockViewContext = new Mock<ViewContext>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.PathBase = new PathString(pathBase);
            httpContext.Request.QueryString = new QueryString($"?NumFen={numFen}&COOKIENAME={cookieName}");

            mockViewContext.Object.HttpContext = httpContext;

            // Use a real ViewContext with mocked HttpContext
            var viewContext = new ViewContext
            {
                HttpContext = httpContext
            };

            mockHtmlHelper.Setup(h => h.ViewContext).Returns(viewContext);
            return mockHtmlHelper.Object;
        }

        /// <summary>
        /// TC-DD-005-U01: ErrorPageUrl generation from HttpContext
        /// </summary>
        [Fact]
        public void TC_DD_005_U01_ErrorPageUrl_GenerationFromHttpContext()
        {
            // Arrange
            var htmlHelper = CreateMockHtmlHelper(pathBase: "/myapp", numFen: "42", cookieName: "MyCookie");
            var component = new DataTableComponent(htmlHelper);
            component.Id = "errorUrlTable";
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteInitScript(sw);
                var initScript = sw.ToString();

                // Assert
                component.ErrorPageUrl.Should().NotBeNullOrEmpty("ErrorPageUrl should be generated");
                component.ErrorPageUrl.Should().Contain("/myapp/Home/SubErr", "ErrorPageUrl should contain the path base");
                component.ErrorPageUrl.Should().Contain("NUMFEN=42", "ErrorPageUrl should contain NumFen parameter");
                component.ErrorPageUrl.Should().Contain("COOKIENAME=MyCookie", "ErrorPageUrl should contain COOKIENAME parameter");
            }
        }

        /// <summary>
        /// TC-DD-005-U02: TotalRecordsLimit message display
        /// </summary>
        [Fact]
        public void TC_DD_005_U02_TotalRecordsLimit_MessageDisplay()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "limitTable";
            component.TotalRecordsLimit = 1000;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            var serializer = new DataTableOptionsSerializer(component);
            var json = serializer.Serialize();
            var jObj = JObject.Parse(json);

            // Assert
            var totalRecordLimit = jObj.SelectToken("totalRecordLimit");
            totalRecordLimit.Should().NotBeNull("totalRecordLimit should be in JSON");
            totalRecordLimit.Value<int>().Should().Be(1000, "total record limit should be 1000");

            // Verify error message template is rendered in HTML
            using (var sw = new StringWriter())
            {
                component.WriteHtml(sw);
                var html = sw.ToString();

                html.Should().Contain("limitTable_ErrorId", "error div should be rendered");
                html.Should().Contain("limitTable_Message", "error message span should be rendered");
            }
        }
    }
}
