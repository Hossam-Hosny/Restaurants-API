﻿using MediatR;

namespace Restaurant.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommand:IRequest
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public bool HasDelivery { get; set; }

}
