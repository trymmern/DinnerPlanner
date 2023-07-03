using DinnerPlanner.Data.DataAccess;
using DinnerPlanner.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Controllers;

[ApiController]
[Route("[controller]")]
public class DinnerPlanController : ControllerBase
{
    private readonly IDinnerPlanRepo _dinnerPlanRepo;

    public DinnerPlanController(IDinnerPlanRepo dinnerPlanRepo)
    {
        _dinnerPlanRepo = dinnerPlanRepo;
    }

    [HttpGet("")]
    public async Task<IEnumerable<DinnerPlan>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dinnerPlanRepo.GetAsync(cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<DinnerPlan> GetAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await _dinnerPlanRepo.GetAsync(id, cancellationToken);
    }

    [HttpGet("{name}")]
    public async Task<DinnerPlan> GetByNameAsync([FromRoute] string name, CancellationToken cancellationToken)
    {
        return await _dinnerPlanRepo.GetAsync(name, cancellationToken);
    }

    [HttpPost("")]
    public async Task<IActionResult> PostAsync([FromBody] DinnerPlan dinnerPlan, CancellationToken cancellationToken)
    {
        await _dinnerPlanRepo.PostAsync(dinnerPlan, cancellationToken);
        return Ok();
    }
}