using Restaurant.Domain.Entities;
using System.ComponentModel;

namespace Restaurant.Application.Restaurants
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantModel>> GetAllRestaurants();
        Task<RestaurantModel?> GetById(Guid Id);
    }
}