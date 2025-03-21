using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<RestaurantModel>> GetAllAsync();
    Task<RestaurantModel?> GeyRestaurantById(Guid id);
    Task<Guid> CreateRestaurant(RestaurantModel dto);
    Task DeleteRestaurant(RestaurantModel model);
    Task UpdateRestaurant(RestaurantModel model);
    Task<(IEnumerable<RestaurantModel>,int)> GetAllMatchingAsync(string? SearchPhrase , int PageNumber, int PageSize , string? SortBy , SrotDirection sortDirection );
}
