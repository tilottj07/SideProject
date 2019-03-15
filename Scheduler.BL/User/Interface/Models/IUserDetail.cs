using System;
using static Scheduler.BL.User.Implementation.UserDetailService;

namespace Scheduler.BL.User.Interface.Models
{
    public interface IUserDetail
    {
        Guid UserDetailId { get; }
        Guid UserId { get; }
        string Characteristic { get; }
        string Description { get; }
        ProficiencyLevelType ProficiencyLevel { get; }

        Guid CreateUserId { get; }
        DateTime CreateDate { get; }

        Guid LastUpdateUserId { get; }
        DateTime LastUpdateDate { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }
    }
}
