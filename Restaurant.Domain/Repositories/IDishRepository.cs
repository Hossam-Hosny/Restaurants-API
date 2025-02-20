using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IDishRepository
{
    Task<int> Create(Dish entity);
    Task Delete(IEnumerable<Dish> dishes);
}
