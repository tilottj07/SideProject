using System;
namespace Scheduler.BL.Team.Interface.Models
{
    public interface ITeam
    {
        Guid TeamId { get; }
        Guid LocationId { get; }
        string TeamName { get; }
        string TeamDescription { get; }
        Guid? TeamLeaderId { get; }
        string TeamEmail { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdateDate { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }
    }
}
