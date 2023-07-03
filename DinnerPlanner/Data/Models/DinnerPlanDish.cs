namespace DinnerPlanner.Data.Models;

public class DinnerPlanDish : BaseEntity
{
    public int DinnerPlanId { get; set; }
    public DinnerPlan DinnerPlan { get; set; } = null!;
    public int DishId { get; set; }
    public Dish Dish { get; set; } = null!;
}