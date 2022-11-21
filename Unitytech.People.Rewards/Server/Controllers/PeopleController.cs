using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet("GetById")]
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
        try
        {
            context.People.Add(person);
            context.SaveChanges();
        }
        catch (Exception exc1)
        {
            _logger.LogError(exc1, "Exception on create");
        }
    }
    [HttpPut]
    public void Update(Person person)
    {
        try
        {
            context.People.Update(person);
            context.SaveChanges();
        }
        catch (Exception exc1)
        {
            _logger.LogError(exc1, "Exception on update");
        }
    }
    [HttpDelete]
    public void Delete(Guid id)
    {
        var person = context.People.FirstOrDefault(p => p.Id == id);
        if (person != null)
        {
            person.DateDeleted = DateTime.Now;
            Update(person);
        }
    }
}

