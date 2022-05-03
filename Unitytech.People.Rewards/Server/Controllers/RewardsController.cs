using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitytech.People.Rewards.Data.Repository;
using Unitytech.People.Rewards.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Unitytech.People.Rewards.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RewardsController : ControllerBase
{

    private readonly ILogger<RewardsController> _logger;
    private readonly DatabaseContext context;

    public RewardsController(ILogger<RewardsController> logger, DatabaseContext context)
    {
        _logger = logger;
        this.context = context;
    }

    [HttpGet]
    public IEnumerable<Reward> Get()
    {
        return context.Rewards.ToList();
    }
    [HttpGet("GetById")]
    public Reward? GetById(Guid id)
    {
        var Reward = context.Rewards.FirstOrDefault(p => p.Id == id);
        if (Reward != null)
        {
            return Reward;
        }
        return null;
    }
    [HttpGet("GetByPersonId")]
    public Reward? GetByPersonId(Guid id)
    {
        var Reward = context.PeopleRewards.Include(p => p.Person).Include(p => p.Reward).FirstOrDefault(p => p.PersonId == id)?.Reward;
        if (Reward != null)
        {
            return Reward;
        }
        return null;
    }
    [HttpPost]
    public void Create(Reward Reward)
    {
        context.Rewards.Add(Reward);
        context.SaveChanges();
    }
    [HttpPost("AwardReceived")]
    public void Create([FromQuery] Guid rewardId, [FromQuery] Guid personId)
    {
        var person = context.People.FirstOrDefault(p => p.Id == personId);
        var reward = context.Rewards.FirstOrDefault(p => p.Id == rewardId);

        if (person != null && reward != null)
        {
            context.PeopleRewards.Add(new PersonReward(rewardId, personId, "Manually created"));
            context.SaveChanges();
        }
    }
    [HttpPut]
    public void Update(Reward Reward)
    {
        context.Rewards.Update(Reward);
        context.SaveChanges();
    }
    [HttpDelete]
    public void Delete(Guid id)
    {
        var Reward = context.Rewards.FirstOrDefault(p => p.Id == id);
        if (Reward != null)
        {
            context.Remove(Reward);
            context.SaveChanges();
        }
    }
}

