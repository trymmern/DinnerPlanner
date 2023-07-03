using System.Collections;
using DinnerPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DinnerPlanner.Data.DataAccess;

public interface IDinnerPlanRepo
{
    public Task<IEnumerable<DinnerPlan>> GetAsync(CancellationToken cancellationToken);
    public Task<DinnerPlan> GetAsync(int id, CancellationToken cancellationToken);
    public Task<DinnerPlan> GetAsync(string name, CancellationToken cancellationToken);
    public Task PostAsync(DinnerPlan dinnerPlan, CancellationToken cancellationToken);
    public Task UpdateAsync(DinnerPlan dinnerPlan, CancellationToken cancellationToken);
}

public class DinnerPlanRepo : IDinnerPlanRepo
{
    private readonly DinnerPlannerContext _db;

    public DinnerPlanRepo(DinnerPlannerContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<DinnerPlan>> GetAsync(CancellationToken cancellationToken)
    {
        return await _db.DinnerPlans.Select(dp => dp)
            .Include(dp => dp.Dishes)
            .Include(dp => dp.Persons)
            .ToListAsync(cancellationToken);
    }

    public async Task<DinnerPlan> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.DinnerPlans.FindAsync(new object?[] { id }, cancellationToken) ?? 
               throw new InvalidOperationException($"Dinner plan with id {id} does not exist");
    }

    public async Task<DinnerPlan> GetAsync(string name, CancellationToken cancellationToken)
    {
        return await _db.DinnerPlans.FirstOrDefaultAsync(d => d.Name == name, cancellationToken) ?? 
               throw new InvalidOperationException($"Dinner plan with name {name} does not exist");
    }

    public async Task PostAsync(DinnerPlan dinnerPlan, CancellationToken cancellationToken)
    {
        var existingDinnerPlan = await _db.DinnerPlans.FirstOrDefaultAsync(d => d.Name == dinnerPlan.Name, cancellationToken);
        if (existingDinnerPlan != null)
        {
            throw new InvalidOperationException($"Dinner plan with name {dinnerPlan.Name} already exists");
        }

        await ValidateDishes(dinnerPlan, cancellationToken);
        _db.DinnerPlans.Add(dinnerPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DinnerPlan dinnerPlan, CancellationToken cancellationToken)
    {
        var existingDinnerPlan = await _db.DinnerPlans.FirstOrDefaultAsync(d => d.Id == dinnerPlan.Id, cancellationToken);
        if (existingDinnerPlan == null)
        {
            throw new InvalidOperationException($"Dinner plan with id {dinnerPlan.Id} does not exist");
        }
        _db.Entry(existingDinnerPlan).CurrentValues.SetValues(dinnerPlan);
        await _db.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Check if Dish dishes already exists. If they do set the database object instead of data from request.
    /// If not use the request data and save the new dish
    /// </summary>
    /// <param name="dinnerPlan">Dinner plan to validate</param>
    /// <param name="cancellationToken"></param>
    private async Task ValidateDishes(DinnerPlan dinnerPlan, CancellationToken cancellationToken)
    {
        var dishes = dinnerPlan.Dishes;
        foreach (var dish in dishes)
        {
            var existingDish = await _db.Dishes.FirstOrDefaultAsync(d => d.Name == dish.Name, cancellationToken);
            if (existingDish == null) continue;
            dinnerPlan.Dishes.Remove(dish);
            dinnerPlan.Dishes.Add(existingDish);
        }
        
    }
}