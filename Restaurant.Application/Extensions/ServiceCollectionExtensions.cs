using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurants;
using Restaurant.Application.User;
using Restaurant.Domain.Entities;
using System.ComponentModel.Design;

namespace Restaurant.Application.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddApplicationServices(this IServiceCollection services)
    {
        var ApplicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        // Adding MediatR to the IOC
        services.AddMediatR(config=>config.RegisterServicesFromAssembly(ApplicationAssembly));

        // Adding AutoMapper to the IOC
        services.AddAutoMapper(ApplicationAssembly);

        // Adding the Fluent Validation to the IOC
        services.AddValidatorsFromAssembly(ApplicationAssembly)
            .AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        services.AddHttpContextAccessor();


    }

}
