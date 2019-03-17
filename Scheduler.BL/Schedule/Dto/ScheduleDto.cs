using System;
using Scheduler.BL.Schedule.Interface.Models;
using static Scheduler.BL.Schedule.Implementation.ScheduleService;

namespace Scheduler.BL.Schedule.Dto
{
    public class ScheduleDto : ISchedule
    {
        public Guid ScheduleId { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public SupportLevelType SupportLevel { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
