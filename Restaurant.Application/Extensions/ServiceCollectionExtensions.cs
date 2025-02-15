using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurants;
using Restaurant.Domain.Entities;
using System.ComponentModel.Design;

namespace Restaurant.Application.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantService,RestaurantService>();



    }

}
