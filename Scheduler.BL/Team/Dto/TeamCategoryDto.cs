using System;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Dto
{
    public class TeamCategoryDto : ITeamCategory
    {
        public Guid TeamCategoryId { get; set; }
        public Guid TeamId { get; set; }
        public Guid CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
