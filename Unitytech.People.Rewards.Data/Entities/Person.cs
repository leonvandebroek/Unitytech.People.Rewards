using System;
using System.Text.Json.Serialization;

namespace Unitytech.People.Rewards.Data.Entities
{
    public class Person : BaseEntity
    {
        public Person() : base()
        {
            ReceivedRewards = new HashSet<PersonReward>();
        }

        public Person(string firstName, string lastName,
            string email, string memberNumber,
            DateTime birthDate, DateTime memberSince,
            string initials = "", string middleName = "",
            string phone = "", string department = "",
            string street = "", string streetNumber = "",
            string zipcode = "", string city = "",
            DateTime? memberUntil = null) : base()
        {
            ReceivedRewards = new HashSet<PersonReward>();
            this.Initials = initials;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.Email = email;
            this.Phone = phone;
            this.MemberNumber = memberNumber;
            this.Department = department;
            this.Street = street;
            this.StreetNumber = streetNumber;
            this.Zipcode = zipcode;
            this.City = city;

            this.MemberSince = memberSince;
            this.MemberUntil = !memberUntil.HasValue ? DateTime.MinValue : memberUntil.Value;

            this.BirthDate = birthDate;
        }

        public string Initials { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        public string Department { get; set; }

        public string MemberNumber { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime MemberUntil { get; set; }

        public virtual ICollection<PersonReward>? ReceivedRewards { get; set; }

    }

    public class PersonReward : BaseEntity
    {
        public PersonReward() : base()
        {
            IssuedDate = DateTime.MinValue;
            ReceivedDate = DateTime.MinValue;
        }

        public PersonReward(Guid rewardId, Guid personId, string note = "") : base()
        {
            IssuedDate = DateTime.MinValue;
            ReceivedDate = DateTime.MinValue;
            this.RewardId = rewardId;
            this.PersonId = personId;
            this.Note = note;
        }

        public PersonReward(Reward reward, Person person, string note = "") : base()
        {
            IssuedDate = DateTime.MinValue;
            ReceivedDate = DateTime.MinValue;
            this.RewardId = reward.Id;
            this.PersonId = person.Id;
            this.Reward = reward;
            this.Person = person;
            this.Note = note;
        }

        public Guid RewardId { get; set; }
        public virtual Reward? Reward { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person? Person { get; set; }

        public DateTime IssuedDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool Received
        {
            get
            {
                return ReceivedDate != DateTime.MinValue;
            }
        }

        public string Note { get; set; }
    }
}

