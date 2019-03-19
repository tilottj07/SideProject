using System;
using System.Collections.Generic;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;

namespace Scheduler.BL.Schedule.Interface
{
    public interface IScheduleNoteService
    {
        IScheduleNote GetScheduleNote(Guid scheduleNoteId);
        List<IScheduleNote> GetScheduleNotes(Guid scheduleId, DateTime? startDate = null, DateTime? endDate = null);
        List<IScheduleNote> GetScheduleNotesByTeam(Guid teamId, DateTime startDate, DateTime endDate);

        ChangeResult AddScheduleNote(IScheduleNote scheduleNote);
        ChangeResult AddScheduleNote(List<IScheduleNote> scheduleNotes);

        ChangeResult UpdateScheduleNote(IScheduleNote scheduleNote);
        ChangeResult UpdateScheduleNote(List<IScheduleNote> scheduleNotes);

        ChangeResult DeleteScheduleNote(Guid scheduleNoteId);

    }
}
