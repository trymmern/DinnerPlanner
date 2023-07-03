using System.ComponentModel.DataAnnotations;

namespace DinnerPlanner.Data.Models;

public class Person : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int Age { get; set; }
    
    public List<DinnerPlan>? DinnerPlans { get; set; }
}