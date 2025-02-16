using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantHandler(ILogger<DeleteRestaurantHandler> _logger, IRestaurantsRepository _restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand,bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting Restaurant with Id : {request.id}");

        var restaurant = await _restaurantsRepository.GeyRestaurantById(request.id);
        if (restaurant == null) { return false; }

        await _restaurantsRepository.DeleteRestaurant(restaurant);
        return true;



    }
}
