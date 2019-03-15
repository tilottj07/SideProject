using System;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Dto
{
    public class TeamDto : ITeam
    {
        public Guid TeamId { get; set; }
        public Guid LocationId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Guid? TeamLeaderId { get; set; }
        public string TeamEmail { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
