using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurants.DTOs;

namespace Restaurant.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery:IRequest<PageResult<RestaurantDTO>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SrotDirection SortDirection { get; set; }

}
