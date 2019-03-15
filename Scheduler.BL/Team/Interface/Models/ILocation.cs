using System;
namespace Scheduler.BL.Team.Interface.Models
{
    public interface ILocation
    {

        Guid LocationId { get; }
        string LocationName { get; }
        string Description { get; }
        string Address { get; }
        string City { get; }
        string StateRegion { get; }
        string Country { get; }
        string ZipCode { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdateDate { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}
