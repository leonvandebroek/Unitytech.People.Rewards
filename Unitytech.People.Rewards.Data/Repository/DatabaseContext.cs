using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Unitytech.People.Rewards.Data.Entities;

namespace Unitytech.People.Rewards.Data.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(
        DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonReward> PeopleRewards { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetDefaultData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected void SetDefaultData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new Person("Léon", "Broek", "lvdh@me.com", "1258257", new DateTime(1992, 9, 23), new DateTime(1997, 9, 1), "L.G.E.", "van de", "06-12345678", "Scouts", "Teststraat", "1", "7546HR", "Teststad") {Id = Guid.NewGuid() });
            modelBuilder.Entity<Person>().HasData(new Person("Test", "Test", "tvdt@me.com", "1258258", new DateTime(1994, 12, 6), new DateTime(1999, 9, 8), "T.E.S.T.", "van der", "06-87654321", "Roverscouts", "Ganzenlaan", "21", "7544HO", "Duckstad") { Id = Guid.NewGuid() });
            modelBuilder.Entity<Person>().HasData(new Person("Pietje", "Puk", "pp@me.com", "1258259", new DateTime(1991, 6, 3), new DateTime(1996, 9, 15), "P.P", "", "06-87651234", "Plusscouts", "Motherload avenue", "5", "1234DH", "Simcity") { Id = Guid.NewGuid() });
        }
    }
}