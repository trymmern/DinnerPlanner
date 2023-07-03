using DinnerPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DinnerPlanner.Data;

public class DinnerPlannerContext : DbContext
{
    public DinnerPlannerContext(DbContextOptions options) : base(options) { }
    
    public DbSet<DinnerPlan> DinnerPlans { get; set; } = null!;
    public DbSet<Dish> Dishes { get; set; } = null!;
    public DbSet<DishCategory> DishCategories { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<DinnerPlanPerson> DinnerPlanPersons { get; set; }
    public DbSet<DinnerPlanDish> DinnerPlanDishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).Updated = DateTimeOffset.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).Created = DateTimeOffset.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}