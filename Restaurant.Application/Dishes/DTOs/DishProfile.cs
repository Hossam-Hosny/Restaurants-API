using AutoMapper;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes.DTOs;

public class DishProfile:Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishDTO>();
        CreateMap<DishDTO, Dish>();

        CreateMap<CreateDishCommand, Dish>();
    }
}
