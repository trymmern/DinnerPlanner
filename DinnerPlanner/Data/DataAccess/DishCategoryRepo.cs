using DinnerPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DinnerPlanner.Data.DataAccess;

public interface IDishCategoryRepo
{
    public Task PostAsync(DishCategory dishCategory, CancellationToken cancellationToken);
}

public class DishCategoryRepo : IDishCategoryRepo
{
    private readonly DinnerPlannerContext _db;

    public DishCategoryRepo (DinnerPlannerContext db)
    {
        _db = db;
    }

    public async Task PostAsync(DishCategory dishCategory, CancellationToken cancellationToken)
    {
        var existingCategory = await _db.DishCategories.FirstOrDefaultAsync(d => d.Value == dishCategory.Value, cancellationToken);
        if (existingCategory != null)
        {
            throw new InvalidOperationException($"Dish category with id {dishCategory.Id} already exists");
        }

        _db.DishCategories.Add(dishCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }
}