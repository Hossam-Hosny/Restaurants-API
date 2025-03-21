using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Repositories;

internal class RestaurantsRepository(AppDbContext _db) : IRestaurantsRepository
{
    public async Task<IEnumerable<RestaurantModel>> GetAllAsync()
    {
        var restaurants = await _db.Restaurants.Include("Dishes").ToListAsync();
        return restaurants;
    }

    public async Task <(IEnumerable<RestaurantModel>,int)> GetAllMatchingAsync(string? SearchPhrase, int PageNumber, int PageSize 
        , string? sortBy
        , SrotDirection sortDirection)
    {
        var searchPhraseLower = SearchPhrase?.ToLower();

        var baseQuery = _db
            .Restaurants
            .Where(r => searchPhraseLower == null ||
            (r.Name.ToLower().Contains(searchPhraseLower)
            || r.Description.ToLower().Contains(searchPhraseLower)));


        var totalCount =await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<RestaurantModel, object>>>
            {
                { nameof(RestaurantModel.Name), r => r.Name },
                { nameof(RestaurantModel.Category), r => r.Category },
                { nameof(RestaurantModel.Description), r => r.Description }
            };
            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SrotDirection.Ascending
             ? baseQuery.OrderBy(selectedColumn)
             : baseQuery.OrderByDescending(selectedColumn);

        }


        var restaurants = await baseQuery
            .Skip(PageSize * (PageNumber-1))
            .Take(PageSize) 

            .ToListAsync();
        return (restaurants , totalCount );
    }

    public async Task<RestaurantModel?> GeyRestaurantById(Guid id)
    {
        RestaurantModel? Restaurant = await _db.Restaurants
                                .Include("Dishes")
                                .FirstOrDefaultAsync(r => r.Id == id);
        return Restaurant;
    }

    public async Task<Guid> CreateRestaurant(RestaurantModel restaurant)
    {
      await  _db.Restaurants.AddAsync(restaurant);
      await  _db.SaveChangesAsync();

        



        return restaurant.Id;


    }

  

    public async Task DeleteRestaurant(RestaurantModel model)
    {
        _db.Restaurants.Remove(model);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateRestaurant(RestaurantModel model)
    {
        _db.Restaurants.Update(model);
        await _db.SaveChangesAsync();
    }
}
