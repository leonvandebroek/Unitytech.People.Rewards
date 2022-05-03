namespace Unitytech.People.Rewards.Data.Entities
{
    public class Reward : BaseEntity
    {
        public Reward() : base()
        {

        }
        public Reward(string name, int intervalInMonths, string description = "", string image = "", string awardDocument = "") : base()
        {
            this.Name = name;
            this.IntervalInMonths = intervalInMonths;
            this.Description = description;
            this.Image = image;
            this.AwardDocument = awardDocument;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AwardDocument { get; set; }

        public int IntervalInMonths { get; set; }

        public bool IsUpForReward(Person person)
        {
            TimeSpan membership = DateTime.Now - person.MemberSince;
            if (membership.Days > 0)
            {
                var monthsInInterval = membership.Days / 30;
                if (IntervalInMonths <= monthsInInterval)
                {
                    return true;
                }
            }
            return false;
        }
    }
}