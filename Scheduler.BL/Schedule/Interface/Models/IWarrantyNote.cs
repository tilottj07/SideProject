using System;
namespace Scheduler.BL.Schedule.Interface.Models
{
    public interface IWarrantyNote
    {
        Guid WarrantyNoteId { get; }
        Guid WarrantyId { get; }
        string Note { get; }

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
