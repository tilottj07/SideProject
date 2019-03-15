using System;
using System.Collections.Generic;

namespace Scheduler.Domain
{
    public class Schedule : ChangeTracker
    {
        public string ScheduleId { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public int SupportLevel { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Team Team { get; set; }
        public User User { get; set; }
        public List<ScheduleNote> ScheduleNotes { get; set; }
    }
}
