using System.ComponentModel.DataAnnotations;

namespace DinnerPlanner.Data.Models;

public class DinnerPlan : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public List<Dish>? Dishes { get; set; }
    
    public List<Person>? Persons { get; set; }
    
    [Required]
    public DateTimeOffset Date { get; set; }
}