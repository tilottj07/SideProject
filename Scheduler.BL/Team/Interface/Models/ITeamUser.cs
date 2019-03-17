using System;
namespace Scheduler.BL.Team.Interface.Models
{
    public interface ITeamUser
    {

        Guid TeamUserId { get; }
        Guid TeamId { get; }
        Guid UserId { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdateDate { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

    }
}
