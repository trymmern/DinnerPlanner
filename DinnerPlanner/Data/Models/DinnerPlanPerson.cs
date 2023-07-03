namespace DinnerPlanner.Data.Models;

public class DinnerPlanPerson : BaseEntity
{
    public int DinnerPlanId { get; set; }
    public DinnerPlan DinnerPlan { get; set; } = null!;
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
}