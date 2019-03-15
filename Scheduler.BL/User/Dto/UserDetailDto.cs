using System;
using Scheduler.BL.User.Interface.Models;
using static Scheduler.BL.User.Implementation.UserDetailService;

namespace Scheduler.BL.User.Dto
{
    public class UserDetailDto : IUserDetail
    {
        public Guid UserDetailId { get; set; }
        public Guid UserId { get; set; }
        public string Characteristic { get; set; }
        public string Description { get; set; }
        public ProficiencyLevelType ProficiencyLevel { get; set; }

        public Guid CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid LastUpdateUserId { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
