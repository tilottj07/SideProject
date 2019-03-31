using System;
using Scheduler.BL.Schedule.Interface.Models;

namespace SchedulerApp.Models.Schedule
{
    public class SchedulesGridRow
    {
        public SchedulesGridRow(IScheduleDisplay scheduleDisplay)
        {
            ScheduleId = scheduleDisplay.ScheduleId;
            TeamId = scheduleDisplay.TeamId;
            UserId = scheduleDisplay.UserId;
            DisplayName = scheduleDisplay.DisplayName;
            TeamName = scheduleDisplay.TeamName;
            StartDate = new DateTime(scheduleDisplay.StartDate.Ticks, DateTimeKind.Utc);
            EndDate = new DateTime(scheduleDisplay.EndDate.Ticks, DateTimeKind.Utc);
            SupportLevel = scheduleDisplay.SupportLevel.ToString();
        }

        public Guid ScheduleId { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public string DisplayName { get; set; }
        public string TeamName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SupportLevel { get; set; }
    }
}
