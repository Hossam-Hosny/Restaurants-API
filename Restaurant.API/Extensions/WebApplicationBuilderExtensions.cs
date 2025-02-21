using Microsoft.OpenApi.Models;
using Restaurant.API.Middlewares;
using Serilog;

namespace Restaurant.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentaion(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(); 
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Enter 'Bearer {your_token}' in the text input below."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }

            },new List<string>()
        }
    });
        });


     

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });

        // registering the  handle middleware
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

    }
}
