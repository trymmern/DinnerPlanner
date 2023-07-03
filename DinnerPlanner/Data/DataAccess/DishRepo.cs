using DinnerPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DinnerPlanner.Data.DataAccess;

public interface IDishRepo
{
    public Task<IEnumerable<Dish>> GetAsync(CancellationToken cancellationToken);
    public Task<Dish> GetAsync(int id, CancellationToken cancellationToken);
    public Task<Dish> GetAsync(string name, CancellationToken cancellationToken);
    public Task PostAsync(IEnumerable<Dish> dishes, CancellationToken cancellationToken);
    public Task PostAsync(Dish dish, CancellationToken cancellationToken);
    public Task UpdateAsync(Dish dish, CancellationToken cancellationToken);
}

public class DishRepo : IDishRepo
{
    private readonly DinnerPlannerContext _db;

    public DishRepo(DinnerPlannerContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Dish>> GetAsync(CancellationToken cancellationToken)
    {
        return await _db.Dishes.Select(d => d)
            .Include(d => d.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<Dish> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Dishes.FindAsync(new object?[] { id }, cancellationToken) ?? 
               throw new InvalidOperationException($"Dish with id {id} does not exist");
    }

    public async Task<Dish> GetAsync(string name, CancellationToken cancellationToken)
    {
        return await _db.Dishes.FirstOrDefaultAsync(d => d.Name == name, cancellationToken) ?? 
               throw new InvalidOperationException($"Dish with name {name} does not exist");
    }

    public async Task PostAsync(IEnumerable<Dish> dishes, CancellationToken cancellationToken)
    {
        foreach (var dish in dishes)
        {
            if (await _db.Dishes.AnyAsync(d => d.Name == dish.Name, cancellationToken))
            {
                throw new InvalidOperationException(
                    $"A dish with the name {dish.Name} already exists. Aborting operation");
            }

            await ValidateCategory(dish, cancellationToken);
            _db.Dishes.Add(dish);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
    
    public async Task PostAsync(Dish dish, CancellationToken cancellationToken)
    {
        var existingDish = await _db.Dishes.FirstOrDefaultAsync(d => d.Name == dish.Name, cancellationToken);
        if (existingDish != null)
        {
            throw new InvalidOperationException($"Dish with name {dish.Name} already exists");
        }
        await ValidateCategory(dish, cancellationToken);
        _db.Dishes.Add(dish);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Dish dish, CancellationToken cancellationToken)
    {
        var existingDish = await _db.Dishes.FirstOrDefaultAsync(d => d.Id == dish.Id, cancellationToken);
        if (existingDish == null)
        {
            throw new InvalidOperationException($"Dish with id {dish.Id} does not exist");
        }
        await ValidateCategory(dish, cancellationToken);
        _db.Entry(existingDish).CurrentValues.SetValues(dish);
        await _db.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateCategory(Dish dish, CancellationToken cancellationToken)
    {
        var category =
            await _db.DishCategories.FirstOrDefaultAsync(dc => dc.Value == dish.Category.Value, cancellationToken);
        if (category != null)
        {
            dish.Category = category;
        }
    }
}