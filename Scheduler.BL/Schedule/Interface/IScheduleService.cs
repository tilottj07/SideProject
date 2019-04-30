using System;
using System.Collections.Generic;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;
using static Scheduler.BL.Schedule.Implementation.ScheduleService;

namespace Scheduler.BL.Schedule.Interface
{
    public interface IScheduleService
    {

        ISchedule GetSchedule(Guid scheduleId);
        List<ISchedule> GetSchedulesByTeamId(Guid teamId, DateTime startDate, DateTime endDate);
        List<ISchedule> GetSchedulesByUserId(Guid userId, DateTime startDate, DateTime endDate);

        List<IScheduleDisplay> GetTeamScheduleByInterval(Guid teamId, DateTime startDate, DateTime endDate, TimeInterval interval);
        List<IScheduleDisplay> GetUserScheduleByInterval(Guid userId, DateTime startDate, DateTime endDate, TimeInterval interval);
        List<IScheduleDisplay> GetAllSchedulesByInterval(DateTime startDate, DateTime endDate, TimeInterval interval);

        List<ISchedule> GetSchedules(DateTime startDate, DateTime endDate);

        ChangeResult SaveSchedule(ISchedule schedule);
        ChangeResult SaveSchedule(List<ISchedule> schedules);

        ChangeResult DeleteSchedule(Guid scheduleId);
        ChangeResult MarkScheduleDeleted(Guid scheduleId);

    }
}
    