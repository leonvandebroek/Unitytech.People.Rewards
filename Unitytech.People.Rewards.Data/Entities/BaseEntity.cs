using System;
namespace Unitytech.People.Rewards.Data.Entities
{
	public class BaseEntity
	{
		public BaseEntity()
		{
			DateCreated = DateTime.Now;
			DateDeleted = DateTime.MaxValue;
		}

		public Guid Id { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateDeleted { get; set; }
	}
}

