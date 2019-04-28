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
            StartDate = scheduleDisplay.StartDate.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt");
            EndDate = scheduleDisplay.EndDate.ToLocalTime().ToString("MM/dd/yyyy hh:mm tt");
            SupportLevel = scheduleDisplay.SupportLevel.ToString();

            IsSelected |= (DateTime.UtcNow >= scheduleDisplay.StartDate && DateTime.UtcNow <= scheduleDisplay.EndDate);
        }

        public Guid ScheduleId { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public string DisplayName { get; set; }
        public string TeamName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SupportLevel { get; set; }

        public bool IsSelected { get; set; }
    }
}
