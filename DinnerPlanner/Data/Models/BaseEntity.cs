using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinnerPlanner.Data.Models;

public class BaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateTimeOffset Created { get; set; }
    [Required]
    public DateTimeOffset Updated { get; set; }
}