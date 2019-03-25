using System;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Dto
{
    public class TeamDisplayDto : ITeamDisplay
    {
        public Guid TeamId { get; set; }
        public Guid LocationId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public Guid? TeamLeaderId { get; set; }
        public string TeamEmail { get; set; }

        public string LocationName { get; set; }
        public string LeaderDisplayName { get; set; }
    }
}
