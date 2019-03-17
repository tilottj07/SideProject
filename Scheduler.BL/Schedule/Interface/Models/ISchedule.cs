using System;
using static Scheduler.BL.Schedule.Implementation.ScheduleService;

namespace Scheduler.BL.Schedule.Interface.Models
{
    public interface ISchedule
    {

        Guid ScheduleId { get; }
        Guid TeamId { get; }
        Guid UserId { get; }
        SupportLevelType SupportLevel { get; }

        DateTime StartDate { get; }
        DateTime EndDate { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdateDate { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}
