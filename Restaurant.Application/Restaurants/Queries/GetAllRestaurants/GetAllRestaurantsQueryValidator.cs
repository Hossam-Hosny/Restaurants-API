using FluentValidation;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowPageSize = [ 5, 10, 15, 30 ];
    private string[] allowedSortByColumnNames = [nameof(RestaurantDTO.Name)
        ,nameof(RestaurantDTO.Category)
        ,nameof(RestaurantDTO.Description)];

    public GetAllRestaurantsQueryValidator()
    {

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSize.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",",allowPageSize)}]");


        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
           .WithMessage($"Sort By is optional, or must be in [{string.Join(",",allowedSortByColumnNames)}] ");



        

            
    }
}
