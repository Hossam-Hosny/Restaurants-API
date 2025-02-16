using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantHandler(ILogger<UpdateRestaurantHandler> _logger ,IMapper _mapper, IRestaurantsRepository _repository) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating Restaurant with Id :{request.Id}");

        RestaurantModel? restaurant = await _repository.GeyRestaurantById(request.Id);
        if (restaurant == null) { return false; }

        _mapper.Map(request, restaurant);


       
        await _repository.UpdateRestaurant(restaurant);
        return true;
    }
}
