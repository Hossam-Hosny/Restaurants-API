using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.DTOs;

public class RestaurantDTO
{


    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDTO> Dishes { get; set; } = new();



}
