﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsHandler(ILogger<GetAllRestaurantsHandler> _Logger ,IMapper _mapper, IRestaurantsRepository _restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDTO>>
{
    public async Task<PageResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _Logger.LogInformation("Getting all Restaurants (we are in Restaurant Service )");
         (IEnumerable<RestaurantModel> restaurants,int totalCount) = await _restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageNumber
            ,request.PageSize
            ,request.SortBy
            ,request.SortDirection);

        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

        var result = new PageResult<RestaurantDTO>(restaurantsDto, totalCount , request.PageSize,request.PageNumber);

        return result;
    }
}
