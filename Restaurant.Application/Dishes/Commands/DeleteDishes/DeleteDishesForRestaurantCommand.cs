using MediatR;

namespace Restaurant.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommand(Guid id):IRequest
{
    public Guid RestaurantId { get; } = id;
}
