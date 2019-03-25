using System;
using System.Collections.Generic;

namespace Scheduler.Domain
{
    public class User : ChangeTracker
    {

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        public string PrimaryPhoneNumber { get; set; }
        public string BackupPhoneNumber { get; set; }
        public string PrimaryEmail { get; set; }
        public string BackupEmail { get; set; }
        public int? PreferredContactMethod { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public List<TeamUser> TeamUsers { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Warranty> Warranties { get; set; }
        public List<UserDetail> UserDetails { get; set; }

    }
}
