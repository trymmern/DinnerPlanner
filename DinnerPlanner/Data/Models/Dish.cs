using System.ComponentModel.DataAnnotations;

namespace DinnerPlanner.Data.Models;

public class Dish : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public DishCategory Category { get; set; } = null!;
    [Required]
    public string Ingredients { get; set; } = null!;
    public List<DinnerPlan>? DinnerPlans { get; set; }
}