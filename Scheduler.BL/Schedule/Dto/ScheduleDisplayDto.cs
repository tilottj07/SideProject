using System;
using Scheduler.BL.Schedule.Interface.Models;

namespace Scheduler.BL.Schedule.Dto
{
    public class ScheduleDisplayDto : ScheduleDto, IScheduleDisplay
    {
        public string DisplayName { get; set; }
        public string TeamName { get; set; }
    }
}
