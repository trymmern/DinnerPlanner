using DinnerPlanner.Data.DataAccess;
using DinnerPlanner.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Controllers;

[ApiController]
[Route("[controller]")]
public class DishController : ControllerBase
{
    private readonly IDishRepo _dishRepo;

    public DishController(IDishRepo dishRepo)
    {
        _dishRepo = dishRepo;
    }

    [HttpGet("")]
    public async Task<IEnumerable<Dish>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dishRepo.GetAsync(cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<Dish> GetAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await _dishRepo.GetAsync(id, cancellationToken);
    }

    [HttpGet("{name}")]
    public async Task<Dish> GetByNameAsync([FromRoute] string name, CancellationToken cancellationToken)
    {
        return await _dishRepo.GetAsync(name, cancellationToken);
    }
    
    /// <summary>
    /// hello
    /// </summary>
    /// <param name="dishes"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("/batch")]
    public async Task<IActionResult> PostAsync([FromBody] IEnumerable<Dish> dishes, CancellationToken cancellationToken)
    {
        await _dishRepo.PostAsync(dishes, cancellationToken);
        return Ok();
    }

    [HttpPost("")]
    public async Task<IActionResult> PostAsync([FromBody] Dish dish, CancellationToken cancellationToken)
    {
        await _dishRepo.PostAsync(dish, cancellationToken);
        return Ok();
    }
}