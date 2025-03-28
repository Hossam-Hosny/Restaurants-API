﻿using MediatR;

namespace Restaurant.Application.Dishes.Commands.CreateDish;

public class CreateDishCommand:IRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public Guid RestaurantId { get; set; }

   
}
