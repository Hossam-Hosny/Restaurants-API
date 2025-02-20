using MediatR;
using Restaurant.Application.Dishes.DTOs;

namespace Restaurant.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery(Guid restaurantId , int dishId):IRequest<DishDTO>
{

    public Guid RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;

}
