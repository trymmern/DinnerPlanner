using DinnerPlanner.Data.DataAccess;
using DinnerPlanner.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Controllers;

[ApiController]
[Route("[controller]")]
public class DishCategoryController : ControllerBase
{
    private readonly IDishCategoryRepo _dishCategoryRepo;

    public DishCategoryController(IDishCategoryRepo dishCategoryRepo)
    {
        _dishCategoryRepo = dishCategoryRepo;
    }

    [HttpPost("")]
    public async Task<IActionResult> PostAsync([FromBody] DishCategory dishCategory,
        CancellationToken cancellationToken)
    {
        await _dishCategoryRepo.PostAsync(dishCategory, cancellationToken);
        return Ok();
    }
}