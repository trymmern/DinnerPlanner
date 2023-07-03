using System.Collections;
using DinnerPlanner.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DinnerPlanner.Data.DataAccess;

public interface IPersonRepo
{
    public Task<IEnumerable<Person>> GetAsync(CancellationToken cancellationToken);
    public Task PostAsync(IEnumerable<Person> persons, CancellationToken cancellationToken);
}

public class PersonRepo : IPersonRepo
{
    private readonly DinnerPlannerContext _db;

    public PersonRepo(DinnerPlannerContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Person>> GetAsync(CancellationToken cancellationToken)
    {
        return await _db.Persons.Select(p => p).ToListAsync(cancellationToken);
    }

    public async Task PostAsync(IEnumerable<Person> persons, CancellationToken cancellationToken)
    {
        foreach (var person in persons)
        {
            if (await _db.Persons.AnyAsync(p => p.Name == person.Name && p.Age == person.Age, cancellationToken))
            {
                throw new InvalidOperationException(
                    $"A person of age {person.Age} and with the name {person.Name} already exists. Aborting batch operation");
            }
            _db.Persons.Add(person);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
}