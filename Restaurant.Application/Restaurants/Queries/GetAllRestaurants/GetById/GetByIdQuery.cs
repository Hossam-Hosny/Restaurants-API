using MediatR;
using Restaurant.Application.Restaurants.DTOs;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants.GetById;

public class GetByIdQuery(Guid Id):IRequest<RestaurantDTO?>
{
    public Guid id { get; }  = Id;
  
}
