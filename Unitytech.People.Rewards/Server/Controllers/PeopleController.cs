using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitytech.People.Rewards.Data.Entities;
using Unitytech.People.Rewards.Data.Repository;

namespace Unitytech.People.Rewards.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{

    private readonly ILogger<PeopleController> _logger;
    private readonly DatabaseContext context;

    public PeopleController(ILogger<PeopleController> logger, DatabaseContext context)
    {
        _logger = logger;
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<Person> Get()
    {
        return context.People.ToList();
    }
    [HttpGet]
    public Person? GetById(Guid id)
    {
        var person = context.People.FirstOrDefault(p => p.Id == id);
        if (person != null)
        {
            return person;
        }
        return null;
    }
    [HttpPost]
    public void Create(Person person)
    {
        context.People.Add(person);
        context.SaveChanges();
    }
    [HttpPut]
    public void Update(Person person)
    {
        context.People.Update(person);
        context.SaveChanges();
    }
    [HttpDelete]
    public void Delete(Guid id)
    {
        var person = context.People.FirstOrDefault(p => p.Id == id);
        if(person != null)
        {
            person.DateDeleted = DateTime.Now;
            Update(person);
        }
    }
}

