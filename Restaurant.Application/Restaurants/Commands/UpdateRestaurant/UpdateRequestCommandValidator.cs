using FluentValidation;

namespace Restaurant.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRequestCommandValidator:AbstractValidator<UpdateRestaurantCommand>
{

    public UpdateRequestCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(3, 50)
            .NotEmpty();

        RuleFor(c => c.Description)
             .NotEmpty();
    }
}
