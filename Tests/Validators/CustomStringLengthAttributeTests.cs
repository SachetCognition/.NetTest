using Xunit;
using Equant.SAV2000.ComponentLibrary.MVC.Validators;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Tests.Validators;

public class CustomStringLengthAttributeTests
{
    [Fact]
    public void CustomStringLengthAttribute_ShouldBeValidationAttribute()
    {
        // Arrange & Act
        var attribute = new CustomStringLengthAttribute(10);

        // Assert
        Assert.IsAssignableFrom<ValidationAttribute>(attribute);
    }

    [Fact]
    public void CustomStringLengthAttribute_ShouldReturnValidForStringWithinLength()
    {
        // Arrange
        var attribute = new CustomStringLengthAttribute(10);
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult("Hello", validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void CustomStringLengthAttribute_ShouldReturnErrorForStringExceedingLength()
    {
        // Arrange
        var attribute = new CustomStringLengthAttribute(5);
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult("Hello World", validationContext);

        // Assert
        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void CustomStringLengthAttribute_ShouldReturnValidForNull()
    {
        // Arrange
        var attribute = new CustomStringLengthAttribute(10);
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult(null, validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void CustomStringLengthAttribute_ShouldReturnValidForEmptyString()
    {
        // Arrange
        var attribute = new CustomStringLengthAttribute(10);
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult(string.Empty, validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void CustomStringLengthAttribute_ShouldReturnValidForExactLength()
    {
        // Arrange
        var attribute = new CustomStringLengthAttribute(5);
        var validationContext = new ValidationContext(new object());

        // Act
        var result = attribute.GetValidationResult("Hello", validationContext);

        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }
}
