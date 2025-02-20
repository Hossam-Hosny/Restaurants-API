using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants.GetById;

public class GetByIdHandler(ILogger<GetByIdHandler> _Logger , IMapper _mapper , IRestaurantsRepository _restaurantsRepository) : IRequestHandler<GetByIdQuery, RestaurantDTO>
{
    public async Task<RestaurantDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {


        _Logger.LogInformation($"Getting Restaurant with Id : {request.id}");
        var restaurant = await _restaurantsRepository.GeyRestaurantById(request.id)??
             throw new NotFoundException($"Restaurant with that id: {request.id} does not exist  "); 

        var resturnatDto = _mapper.Map<RestaurantDTO>(restaurant);

        return resturnatDto;

    }
}
