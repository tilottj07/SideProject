using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.User.Interface.Models;

namespace Scheduler.BL.User.Interface
{
    public interface IUserDetailService
    {

        IUserDetail GetUserDetail(Guid userDetailId);
        List<IUserDetail> GetUserDetails(Guid userId);
        List<IUserDetail> GetUserDetails();

        ChangeResult AddUserDetail(IUserDetail userDetail);
        ChangeResult AddUserDetail(List<IUserDetail> userDetails);

        ChangeResult UpdateUserDetail(IUserDetail userDetail);
        ChangeResult UpdateUserDetail(List<IUserDetail> userDetails);

        ChangeResult DeleteUserDetail(Guid userDetailId);

    }
}
