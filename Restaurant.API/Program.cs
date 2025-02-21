using Microsoft.OpenApi.Models;
using Restaurant.API.Extensions;
using Restaurant.API.Middlewares;
using Restaurant.Application.Extensions;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding Services of Infrastructure Layer 

builder.Services.AddInfrastrucdure(builder.Configuration);

// Adding Services of Application Layer 
builder.Services.AddApplicationServices();

// Adding Peresentation to the IOC
builder.AddPresentaion();

var app = builder.Build();
// Seeding data if Any() => is false (Restaurants table is empty) 
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();



// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.MapGroup("api/Identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
