using System;
namespace Scheduler.Domain
{
    public class TeamCategory : ChangeTracker
    {
        public string TeamCategoryId { get; set; }
        public string TeamId { get; set; }
        public string CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Team Team { get; set; }
        public Category Category { get; set; }
    }
}
