using MediatR;
using Restaurant.Application.Restaurants.DTOs;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery:IRequest<IEnumerable<RestaurantDTO>>
{


}
