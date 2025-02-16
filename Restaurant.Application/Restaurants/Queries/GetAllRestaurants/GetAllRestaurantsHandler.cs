using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsHandler(ILogger<GetAllRestaurantsHandler> _Logger ,IMapper _mapper, IRestaurantsRepository _restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
{
    public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _Logger.LogInformation("Getting all Restaurants (we are in Restaurant Service )");
        var restaurants = await _restaurantsRepository.GetAllAsync();

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);



        return restaurantsDto;
    }
}
