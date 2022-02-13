using System;
using Microsoft.EntityFrameworkCore;
using Unitytech.People.Rewards.Data.Entities;

namespace Unitytech.People.Rewards.Data.Repository
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext()
		{

		}

		public virtual DbSet<Person> People { get; set; }
		public virtual DbSet<PersonReward> PeopleRewards { get; set; }
		public virtual DbSet<Reward> Rewards { get; set; }
	}
}

