using System;
namespace Scheduler.BL.Schedule.Interface.Models
{
    public interface IWarranty
    {
        Guid WarrantyId { get; }
        string WarrantyName { get; }
        string WarrentyDescription { get; }
        Guid TeamId { get; }
        Guid UserId { get; }

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
