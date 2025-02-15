using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Entities;
using System.ComponentModel;

namespace Restaurant.Application.Restaurants
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurants();
        Task<RestaurantDTO?> GetById(Guid Id);
        Task<Guid> Create(CreateRestaurantDTO dto);

        
    }
}