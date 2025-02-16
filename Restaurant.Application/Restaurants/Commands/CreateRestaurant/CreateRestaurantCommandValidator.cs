using FluentValidation;
using Restaurant.Application.Restaurants.DTOs;

namespace Restaurant.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = new List<string>{
        "Egyption",
        "Italian" ,
        "Indian",
        "American" };

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 50)
            .NotEmpty();

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required.");



        RuleFor(dto => dto.Category)
            //.Must(validCategories.Contains)
            //.WithMessage("Invalid category. Please Choose from the valid categories.");
            .Custom((value, context) =>
            {
                var isValidCategory = validCategories.Contains(value);
                if (!isValidCategory)
                {
                    context.AddFailure("Category", "Invalid category. Please Choose from the valid categories.");
                }
            });

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Enter a vaild email.");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please Provide a valide postal code (xx-xxx).");


    }

}
