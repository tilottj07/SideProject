using System;
namespace Scheduler.BL.Schedule.Interface.Models
{
    public interface IScheduleDisplay : ISchedule
    {

        string DisplayName { get; }
        string TeamName { get; }

    }
}
