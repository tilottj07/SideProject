using System;
using Scheduler.BL.User.Interface.Models;
using static Scheduler.BL.User.Implementation.UserService;

namespace Scheduler.BL.User.Dto
{
    public class UserDto : IUser
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        public string PrimaryPhoneNumber { get; set; }
        public string BackupPhoneNumber { get; set; }
        public string PrimaryEmail { get; set; }
        public string BackupEmail { get; set; }
        public PreferredContactMehodType? PreferredContactMethod { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }


        public string DisplayName
        {
            get
            {
                return Shared.Helper.GetDisplayName(
                    UserName, FirstName, MiddleInitial, LastName);
            }
        }

    }
}
