using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.User.Interface.Models;

namespace Scheduler.BL.User.Interface
{
    public interface IUserService
    {

        IUser GetUser(Guid userId);
        IUser GetUser(string userName);
        List<IUser> GetUsers();
        List<IUser> GetTeamUsers(Guid teamId);

        ChangeResult AddUser(IUser user);
        ChangeResult AddUser(List<IUser> users);

        ChangeResult UpdateUser(IUser user);
        ChangeResult UpdateUser(List<IUser> users);

        ChangeResult DeleteUser(Guid userId);
        ChangeResult DeleteUser(string userName);

    }
}
