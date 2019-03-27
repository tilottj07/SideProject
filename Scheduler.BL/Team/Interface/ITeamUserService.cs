using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Interface
{
    public interface ITeamUserService
    {

        ITeamUser GetTeamUser(Guid teamUserId);
        ITeamUser GetTeamUser(Guid teamId, Guid userId);
        List<ITeamUser> GetTeamUsersByTeamId(Guid teamId);
        List<ITeamUser> GetTeamUsersByUserId(Guid userId);
        List<ITeamUser> GetTeamUsers();

        ChangeResult SaveTeamUsers(Guid teamId, List<Guid> userIds, Guid? changeUserId = null);

        ChangeResult AddTeamUser(ITeamUser teamUser);
        ChangeResult AddTeamUser(List<ITeamUser> teamUsers);

        ChangeResult UpdateTeamUser(ITeamUser teamUser);
        ChangeResult UpdateTeamUser(List<ITeamUser> teamUsers);

        ChangeResult DeleteTeamUser(Guid teamUserId);

    }
}
