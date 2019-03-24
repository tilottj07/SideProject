using Scheduler.BL.User.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerApp.Models.User
{
    public class UsersGridRow
    {
        public UsersGridRow(IUser user)
        {
            UserId = user.UserId;
            UserName = user.UserName;
            DisplayName = user.DisplayName;
            PrimaryPhone = user.PrimaryPhoneNumber;
            Email = user.PrimaryEmail;
            BackupPhone = user.BackupPhoneNumber;
        }


        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
        public string BackupPhone { get; set; }



    }
}
