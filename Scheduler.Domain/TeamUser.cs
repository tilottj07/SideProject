using System;
namespace Scheduler.Domain
{
    public class TeamUser : ChangeTracker
    {
        public string TeamUserId { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Team Team { get; set; }
        public User User { get; set; }
    }
}
