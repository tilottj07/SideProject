using System;
using System.Collections.Generic;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;

namespace Scheduler.BL.Schedule.Interface
{
    public interface IScheduleService
    {

        ISchedule GetSchedule(Guid scheduleId);
        List<ISchedule> GetSchedulesByTeamId(Guid teamId, DateTime startDate, DateTime endDate);
        List<ISchedule> GetSchedulesByUserId(Guid userId, DateTime startDate, DateTime endDate);
        List<ISchedule> GetSchedules(DateTime startDate, DateTime endDate);

        ChangeResult SaveSchedule(ISchedule schedule);
        ChangeResult SaveSchedule(List<ISchedule> schedules);

        ChangeResult DeleteSchedule(Guid scheduleId);

    }
}
    