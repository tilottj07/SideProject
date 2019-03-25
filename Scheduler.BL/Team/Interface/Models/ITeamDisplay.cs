using System;
namespace Scheduler.BL.Team.Interface.Models
{
    public interface ITeamDisplay
    {

        Guid TeamId { get; set; }
        Guid LocationId { get; set; }
        string TeamName { get; set; }
        string TeamDescription { get; set; }
        Guid? TeamLeaderId { get; set; }
        string TeamEmail { get; set; }

        string LocationName { get; set; }
        string LeaderDisplayName { get; set; }

    }
}
