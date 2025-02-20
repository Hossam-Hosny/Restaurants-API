using MediatR;

namespace Restaurant.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(Guid Id):IRequest
{
    public Guid id { get; } = Id;
}
