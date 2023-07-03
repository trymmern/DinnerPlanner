using DinnerPlanner.Data.DataAccess;
using DinnerPlanner.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace DinnerPlanner.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepo _personRepo;
    
    public PersonController(IPersonRepo personRepo)
    {
        _personRepo = personRepo;
    }

    [HttpGet("")]
    public async Task<IEnumerable<Person>> GetAsync(CancellationToken cancellationToken)
    {
        return await _personRepo.GetAsync(cancellationToken);
    }

    [HttpPost("")]
    public async Task<IActionResult> PostAsync(IEnumerable<Person> persons, CancellationToken cancellationToken)
    {
        await _personRepo.PostAsync(persons, cancellationToken);
        return Ok();
    }
}