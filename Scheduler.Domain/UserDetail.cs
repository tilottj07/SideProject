using System;
namespace Scheduler.Domain
{
    public class UserDetail : ChangeTracker
    {
        public string UserDetailId { get; set; }
        public string UserId { get; set; }
        public string Characteristic { get; set; }
        public string Description { get; set; }
        public int ProficiencyLevel { get; set; }

        public string CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public string LastUpdateUserId { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
