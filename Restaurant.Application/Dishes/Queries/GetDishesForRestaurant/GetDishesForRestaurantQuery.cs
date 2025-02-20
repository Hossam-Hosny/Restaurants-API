using MediatR;
using Restaurant.Application.Dishes.DTOs;

namespace Restaurant.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(Guid restaurantId):IRequest<IEnumerable<DishDTO>>
{
    public Guid RestaurantId { get; } = restaurantId;
}
