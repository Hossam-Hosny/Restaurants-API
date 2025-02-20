using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.DbContexts;

namespace Restaurant.Infrastructure.Repositories;

internal class DishesRepository(AppDbContext _db) : IDishRepository
{
    public async Task<int> Create(Dish entity)
    {
        await _db.Dishes.AddAsync(entity);
        await _db.SaveChangesAsync();   
        return entity.Id;
    }

    public async Task Delete(IEnumerable<Dish> dishes)
    {
         _db.Dishes.RemoveRange(dishes);
        await _db.SaveChangesAsync();
    }
}
