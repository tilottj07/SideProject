using System;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Team
{
    public class TeamsGridRow
    {
        public TeamsGridRow(ITeam team)
        {
            TeamId = team.TeamId;
            LocationId = team.LocationId;
            TeamName = team.TeamName;
            TeamDescription = team.TeamDescription;
            TeamLeaderId = team.TeamLeaderId;
            TeamEmail = team.TeamEmail;
            TeamLocation = string.Empty;
            TeamLeader = string.Empty;
        }

        public Guid TeamId { get; set; }
        public Guid LocationId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public string TeamLocation { get; set; }
        public Guid? TeamLeaderId { get; set; }
        public string TeamLeader { get; set; }
        public string TeamEmail { get; set; }

       
    }
}
