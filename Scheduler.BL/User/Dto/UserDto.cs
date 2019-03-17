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
        public byte[] Photo { get; set; }

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
                string val = string.Empty;
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(MiddleInitial) && !string.IsNullOrWhiteSpace(LastName))
                {
                    val = $"{FirstName} {MiddleInitial}. {LastName}";
                }
                else if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                {
                    val = $"{FirstName} {LastName}";
                }
                else if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(MiddleInitial))
                {
                    val = $"{FirstName} {MiddleInitial}.";
                }
                else if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    val = FirstName;
                }
                else if (!string.IsNullOrWhiteSpace(LastName))
                {
                    val = LastName;
                }
                else
                {
                    val = UserName;
                }

                return val.Trim();
            }
        }

    }
}
