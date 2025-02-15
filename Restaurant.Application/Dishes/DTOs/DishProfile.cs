using AutoMapper;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes.DTOs;

public class DishProfile:Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDTO>();
        CreateMap<DishDTO, Dish>();
    }
}
