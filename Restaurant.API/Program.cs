using Restaurant.API.Middlewares;
using Restaurant.Application.Extensions;
using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adding Services of Infrastructure Layer 

builder.Services.AddInfrastrucdure(builder.Configuration);

// Adding Services of Application Layer 
builder.Services.AddApplicationServices();

builder.Host.UseSerilog((context,configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

// registering the  handle middleware
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();



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

app.UseAuthorization();

app.MapControllers();

app.Run();
