using System;
namespace Scheduler.BL.Schedule.Interface.Models
{
    public interface IScheduleNote
    {

        Guid ScheduleNoteId { get; }
        Guid ScheduleId { get; }
        string Note { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdated { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}
