using System.ComponentModel.DataAnnotations;

namespace DinnerPlanner.Data.Models;

public class DishCategory : BaseEntity
{
    [Required]
    public string Value { get; set; } = null!;
}