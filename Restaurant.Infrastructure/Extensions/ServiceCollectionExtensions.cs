﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.User;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization;
using Restaurant.Infrastructure.Authorization.Requirements;
using Restaurant.Infrastructure.Authorization.Services;
using Restaurant.Infrastructure.DbContexts;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Seeders;

namespace Restaurant.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static void AddInfrastructure(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DevelopmentConnectionString"))
                .EnableSensitiveDataLogging();
            });

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishRepository, DishesRepository>();

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimPrincipleFactory>()
                .AddEntityFrameworkStores<AppDbContext>();


            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.HasNationality))
                .AddPolicy(PolicyNames.AtLeast20, builder => builder.AddRequirements(
                    new MinimumAgeRequirement(20)))

                .AddPolicy(PolicyNames.CreatedAtLeast2Restaurants,
                builder => builder.AddRequirements(new CreateMultipleRestaurantsRequirement(2)));
                

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreateMultipleRestaurantsRequirementHandler>();
            // services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
            services.AddScoped<UserContext>();
        }

    }
}
