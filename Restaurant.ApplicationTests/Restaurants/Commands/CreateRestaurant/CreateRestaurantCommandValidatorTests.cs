using FluentValidation.TestHelper;
using Xunit;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommands_ShouldNotHaveValidErrors()
    {

        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "test",
            Category = "Egyption",
            
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
            Description="This is Description"
        };



        var validator = new CreateRestaurantCommandValidator();

        // Act 

        var result = validator.TestValidate(command);

        // Assert 

        result.ShouldNotHaveAnyValidationErrors();





    }


    [Fact()]
    public void Validator_ForInValidCommands_ShouldHaveValidErrors()
    {

        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "te",
            Category = "Egypt",

            ContactEmail = "@test.com",
            PostalCode = "12345"
         
        };



        var validator = new CreateRestaurantCommandValidator();

        // Act 

        var result = validator.TestValidate(command);

        // Assert 

        result.ShouldHaveValidationErrorFor( r=> r.Name);
        result.ShouldHaveValidationErrorFor( r=> r.Description);
        result.ShouldHaveValidationErrorFor( r=> r.PostalCode);
        result.ShouldHaveValidationErrorFor( r=> r.ContactEmail);





    }

    [Theory()]
    [InlineData("American")]
    [InlineData("Indian")]
    [InlineData("Italian")]
    [InlineData("Egyption")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // Arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category};

        // Act 
        var result = validator.TestValidate(command);

        // Assert 

        result.ShouldNotHaveValidationErrorFor(r => r.Category);



    }

    [Theory()]
    [InlineData("123-33")]
    [InlineData("123-3")]
    [InlineData("123 -3")]
    [InlineData("12-23")]
    [InlineData("12-3")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorForPostalCodeProperty(string code)
    {
        // Arrange 
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { PostalCode = code};

        // Act 
        var result = validator.TestValidate(command);

        // Assert 
        result.ShouldHaveValidationErrorFor(r => r.PostalCode);
    }

}