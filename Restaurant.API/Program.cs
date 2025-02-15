using Restaurant.Application.Extensions;
using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;

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




var app = builder.Build();
// Seeding data if Any() => is false (Restaurants table is empty) 
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
