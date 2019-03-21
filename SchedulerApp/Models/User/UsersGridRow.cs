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
            FirstName = user.FirstName;
            MiddleInitial = user.MiddleInitial;
            LastName = user.LastName;
        }


        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }



    }
}
