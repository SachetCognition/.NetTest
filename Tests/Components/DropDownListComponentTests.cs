using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
using Equant.SAV2000.ComponentLibrary.MVC.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Components;

public class DropDownListComponentTests
{
    [Fact]
    public void DropDownListComponent_ShouldInitializeWithDefaults()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();

        // Act
        var dropdown = new DropDownListComponent(htmlHelper);

        // Assert
        Assert.NotNull(dropdown);
        Assert.NotNull(dropdown.HtmlAttributes);
    }

    [Fact]
    public void DropDownListBuilder_ShouldSetId()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        var builder = new DropDownListBuilder(dropdown, null);

        // Act
        builder.Id("testDropdown");

        // Assert
        Assert.Equal("testDropdown", dropdown.Id);
    }

    [Fact]
    public void DropDownListBuilder_ShouldSetName()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        var builder = new DropDownListBuilder(dropdown, null);

        // Act
        builder.Name("countrySelect");

        // Assert
        Assert.Equal("countrySelect", dropdown.Name);
    }

    [Fact]
    public void DropDownListBuilder_ShouldSetCssClass()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        var builder = new DropDownListBuilder(dropdown, null);

        // Act
        builder.CssClass("form-select");

        // Assert
        Assert.Equal("form-select", dropdown.CssClass);
    }

    [Fact]
    public void DropDownListBuilder_ShouldSetOnChange()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        var builder = new DropDownListBuilder(dropdown, null);

        // Act
        builder.OnChange("handleSelectionChange()");

        // Assert
        Assert.Equal("handleSelectionChange()", dropdown.OnChange);
    }

    [Fact]
    public void DropDownListBuilder_ShouldSetDataBind()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        var builder = new DropDownListBuilder(dropdown, null);
        var items = new List<SelectListItem>
        {
            new SelectListItem { Text = "Option 1", Value = "1" },
            new SelectListItem { Text = "Option 2", Value = "2" }
        };
        var selectList = new SelectList(items, "Value", "Text");

        // Act
        builder.DataBind(selectList);

        // Assert
        Assert.NotNull(dropdown.Items);
        Assert.Equal(2, dropdown.Items.Count);
    }

    [Fact]
    public void DropDownListBuilder_ShouldSupportFluentChaining()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        var builder = new DropDownListBuilder(dropdown, null);

        // Act
        builder
            .Id("myDropdown")
            .Name("selectCountry")
            .CssClass("form-control")
            .OnChange("updateSelection()");

        // Assert
        Assert.Equal("myDropdown", dropdown.Id);
        Assert.Equal("selectCountry", dropdown.Name);
        Assert.Equal("form-control", dropdown.CssClass);
        Assert.Equal("updateSelection()", dropdown.OnChange);
    }

    [Fact]
    public void DropDownListComponent_ToHtml_ShouldReturnIHtmlContent()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        new DropDownListBuilder(dropdown, null)
            .Id("testDdl")
            .Name("testDropdown");

        // Act
        var html = dropdown.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("testDdl", htmlString);
    }

    [Fact]
    public void DropDownListComponent_ToHtml_ShouldContainSelectTag()
    {
        // Arrange
        var htmlHelper = TestHtmlHelper.Create();
        var dropdown = new DropDownListComponent(htmlHelper);
        new DropDownListBuilder(dropdown, null)
            .Id("testDdl")
            .Name("testDropdown");

        // Act
        var html = dropdown.ToHtml();

        // Assert
        Assert.NotNull(html);
        var htmlString = TestHtmlHelper.RenderToString(html);
        Assert.Contains("<select", htmlString);
    }
}
