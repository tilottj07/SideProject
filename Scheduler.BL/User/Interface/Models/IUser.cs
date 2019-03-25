using System;
using static Scheduler.BL.User.Implementation.UserService;

namespace Scheduler.BL.User.Interface.Models
{
    public interface IUser
    {

        Guid UserId { get; }
        string UserName { get; }
        string FirstName { get; }
        string MiddleInitial { get; }
        string LastName { get; }

        string PrimaryPhoneNumber { get; }
        string BackupPhoneNumber { get; }
        string PrimaryEmail { get; }
        string BackupEmail { get; }
        PreferredContactMehodType? PreferredContactMethod { get; }

        DateTime CreateDate { get; }
        Guid CreateUserId { get; }

        DateTime LastUpdateDate { get; }
        Guid LastUpdateUserId { get; }

        DateTime ChangeDate { get; }
        DateTime? DeleteDate { get; }

        string DisplayName { get; }

    }
}
