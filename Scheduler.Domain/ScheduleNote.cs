using System;
namespace Scheduler.Domain
{
    public class ScheduleNote : ChangeTracker
    {
        public string ScheduleNoteId { get; set; }
        public string ScheduleId { get; set; }
        public string Note { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdated { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
