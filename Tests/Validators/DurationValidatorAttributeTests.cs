using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Validators;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Validators;

public class DurationValidatorAttributeTests
{
    [Fact]
    public void DurationValidatorAttribute_ShouldBeValidationAttribute()
    {
        // Arrange & Act
        var attribute = new DurationValidatorAttribute(
            "StartDate", "EndDate", "startDateId", "endDateId", "durationId", "Invalid duration");

        // Assert
        Assert.IsAssignableFrom<ValidationAttribute>(attribute);
    }

    [Fact]
    public void DurationValidatorAttribute_ShouldStorePropertyNames()
    {
        // Arrange & Act
        var attribute = new DurationValidatorAttribute(
            "StartDate", "EndDate", "startDateId", "endDateId", "durationId", "Invalid duration");

        // Assert
        Assert.Equal("StartDate", attribute.FirstPropertyName);
        Assert.Equal("EndDate", attribute.SecondPropertyName);
    }

    [Fact]
    public void DurationValidatorAttribute_ShouldStoreHtmlIds()
    {
        // Arrange & Act
        var attribute = new DurationValidatorAttribute(
            "StartDate", "EndDate", "startDateId", "endDateId", "durationId", "Invalid duration");

        // Assert
        Assert.Equal("startDateId", attribute.FirstPropertyHtmlId);
        Assert.Equal("endDateId", attribute.SecondPropertyHtmlId);
        Assert.Equal("durationId", attribute.ThirdPropertyHtmlId);
    }

    [Fact]
    public void DurationValidatorAttribute_ShouldWorkWithoutErrorMessage()
    {
        // Arrange & Act
        var attribute = new DurationValidatorAttribute(
            "StartDate", "EndDate", "startDateId", "endDateId", "durationId");

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal("StartDate", attribute.FirstPropertyName);
    }
}
