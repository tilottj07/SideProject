using System;
namespace Scheduler.BL.Schedule.Interface.Models
{
    public interface IWarrantyDisplay
    {
        Guid WarrantyId { get; }
        string WarrantyName { get; }
        string WarrentyDescription { get; }

        Guid TeamId { get; }
        Guid UserId { get; }

        string TeamName { get; }
        string UserDisplayName { get; }

        DateTime StartDate { get; }
        DateTime EndDate { get; }

    }
}
