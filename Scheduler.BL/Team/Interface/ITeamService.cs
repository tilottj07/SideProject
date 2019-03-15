using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Interface
{
    public interface ITeamService
    {
        ITeam GetTeam(Guid teamId);
        List<ITeam> GetLocationTeams(Guid locationId);
        List<ITeam> GetTeams();

        ChangeResult AddTeam(ITeam team);
        ChangeResult AddTeam(List<ITeam> teams);

        ChangeResult UpdateTeam(ITeam team);
        ChangeResult UpdateTeam(List<ITeam> teams);

        ChangeResult DeleteTeam(Guid teamId);
    }
}
