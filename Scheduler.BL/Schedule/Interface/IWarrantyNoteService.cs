using System;
using System.Collections.Generic;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;

namespace Scheduler.BL.Schedule.Interface
{
    public interface IWarrantyNoteService
    {
        IWarrantyNote GetWarrantyNote(Guid warrantyNoteId);
        List<IWarrantyNote> GetWarrantyNotes(Guid warrantyId, DateTime? startDate = null, DateTime? endDate = null);
        List<IWarrantyNote> GetScheduleNotesByTeam(Guid teamId, DateTime startDate, DateTime endDate);

        ChangeResult AddWarrantyNote(IWarrantyNote note);
        ChangeResult AddWarrantyNote(List<IWarrantyNote> notes);

        ChangeResult UpdateWarrantyNote(IWarrantyNote note);
        ChangeResult UpdateWarrantyNote(List<IWarrantyNote> notes);

        ChangeResult DeleteWarrantyNote(Guid warrantyNoteId);

    }
}
