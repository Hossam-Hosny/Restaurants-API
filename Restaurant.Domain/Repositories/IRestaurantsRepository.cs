using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<RestaurantModel>> GetAllAsync();
    Task<RestaurantModel?> GeyRestaurantById(Guid id);

}
