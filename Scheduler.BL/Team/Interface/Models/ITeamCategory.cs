using System;
namespace Scheduler.BL.Team.Interface.Models
{
    public interface ITeamCategory
    {
        Guid TeamCategoryId { get; }
        Guid TeamId { get; }
        Guid CategoryId { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdateDate { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }
    }
}
