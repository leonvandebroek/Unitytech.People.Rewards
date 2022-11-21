using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitytech.People.Rewards.Data.Repository;
using Unitytech.People.Rewards.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Unitytech.People.Rewards.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonRewardsController : ControllerBase
{

    private readonly ILogger<RewardsController> _logger;
    private readonly DatabaseContext context;

    public PersonRewardsController(ILogger<RewardsController> logger, DatabaseContext context)
    {
        _logger = logger;
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<PersonReward> Get()
    {
        return context.PeopleRewards.Include(p => p.Reward).ToList();
    }
    [HttpGet("GetById")]
    public PersonReward? GetById(Guid id)
    {
        var Reward = context.PeopleRewards.Include(p => p.Reward).FirstOrDefault(p => p.Id == id);
        if (Reward != null)
        {
            return Reward;
        }
        return null;
    }
    [HttpGet("ByPersonId")]
    public PersonReward[]? GetByPersonId(Guid id)
    {
        var rewards = context.Rewards.ToArray();
        var person = context.People.Include(p => p.ReceivedRewards).FirstOrDefault(p => p.Id == id);

        if (person != null)
        {
            var result = CalculateLogicalRewards(person, rewards);
            if (result != null)
            {
                return result.ToArray();
            }
        }

        return null;
    }
    [HttpPost]
    public void Create(PersonReward personReward)
    {
        personReward.Person = default;
        personReward.Reward = default;

        if (context.PeopleRewards.Any(p => p.Id == personReward.Id))
        {
            context.PeopleRewards.Update(personReward);
        }
        else
        {
            context.PeopleRewards.Add(personReward);
        }
        context.SaveChanges();
    }

    [HttpPut]
    public void Update(PersonReward personReward)
    {
        context.PeopleRewards.Update(personReward);
        context.SaveChanges();
    }
    [HttpDelete]
    public void Delete(Guid id)
    {
        var Reward = context.PeopleRewards.FirstOrDefault(p => p.Id == id);
        if (Reward != null)
        {
            context.PeopleRewards.Remove(Reward);
            context.SaveChanges();
        }
    }

    private ICollection<PersonReward> CalculateLogicalRewards(Person person, Reward[] rewards)
    {
        List<PersonReward> personRewards = new List<PersonReward>();

        foreach (var reward in rewards)
        {
            if (person.ReceivedRewards?.Any(p => p.RewardId == reward.Id) == false)
            {
                if (reward.IsUpForReward(person))
                {
                    personRewards.Add(new PersonReward(reward, person, "Automatically added"));
                }
            }
            else
            {
                if (person.ReceivedRewards?.Any(p => p.RewardId == reward.Id) == true)
                {
                    personRewards.Add(person.ReceivedRewards?.FirstOrDefault(p => p.RewardId == reward.Id));
                }
            }

        }
        return personRewards;
    }
}

