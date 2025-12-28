using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Validators;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Validators;

public class DateRequiredAttributeTests
{
    [Fact]
    public void DateRequiredAttribute_ShouldBeValidationAttribute()
    {
        // Arrange & Act
        var attribute = new DateRequiredAttribute();

        // Assert
        Assert.IsAssignableFrom<ValidationAttribute>(attribute);
    }

    [Fact]
    public void DateRequiredAttribute_ShouldReturnValidForNonEmptyDate()
    {
        // Arrange
        var attribute = new DateRequiredAttribute();
        var dateWithFormat = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
        dateWithFormat.DateText = "01/01/2024";
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult(dateWithFormat, validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void DateRequiredAttribute_ShouldReturnErrorForEmptyDateTimeWithFormat()
    {
        // Arrange
        var attribute = new DateRequiredAttribute("Date is required");
        var emptyDate = new DateTimeWithFormat(DateTimeConstants.EnglishFormat, false);
        emptyDate.DateText = "";
        emptyDate.HourValue = "";
        emptyDate.MinuteValue = "";
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult(emptyDate, validationContext);

        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void DateRequiredAttribute_ShouldReturnValidForNull()
    {
        // Arrange - DateRequiredAttribute returns Success for null values
        var attribute = new DateRequiredAttribute();
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult(null, validationContext);

        // Assert - null is considered valid (not required)
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void DateRequiredAttribute_ShouldAcceptErrorMessage()
    {
        // Arrange
        var errorMessage = "Date is required";
        var attribute = new DateRequiredAttribute(errorMessage);

        // Act & Assert
        Assert.NotNull(attribute);
    }
}
