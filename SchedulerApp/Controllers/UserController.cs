using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.User.Interface;
using Scheduler.BL.User.Implementation;
using SchedulerApp.Models.User;
using Scheduler.BL.User.Dto;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Implementation;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class UserController : Controller
    {
        private IUserService UserService;
        private ITeamService TeamService;
        private ITeamUserService TeamUserService;

        public UserController()
        {
            UserService = new UserService();
            TeamService = new TeamService();
            TeamUserService = new TeamUserService();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }


        public IActionResult Users()
        {
            Users vm = new Users();
            return View(vm);
        }

        public IActionResult _getUserGridData()
        {
            List<UsersGridRow> rows = new List<UsersGridRow>();

            foreach (var user in UserService.GetUsers())
                rows.Add(new UsersGridRow(user));

            return Json(new { data = rows });
        }


        public IActionResult EditUserModal(string id)
        {
            UserEdit vm;
            List<ITeam> teams = TeamService.GetTeams();
            List<ITeamUser> teamUsers = new List<ITeamUser>();

            Guid? userId = Helper.ConvertToGuid(id);
            if (userId.HasValue)
            {
                teamUsers = TeamUserService.GetTeamUsersByUserId(userId.Value);
                vm = new UserEdit(UserService.GetUser(userId.Value), teams, teamUsers);
            }
            else vm = new UserEdit(teams, teamUsers);
           
            return PartialView("_UserEditPartial", vm);
        }

        [HttpPost]
        public IActionResult EditUserModal(UserEdit model)
        {
            if (ModelState.IsValid)
            {
                UserDto dto = new UserDto()
                {
                    BackupEmail = model.BackupEmail,
                    BackupPhoneNumber = model.BackupPhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleInitial = model.MiddleInitial,
                    PrimaryEmail = model.PrimaryEmail,
                    PrimaryPhoneNumber = model.PrimaryPhoneNumber,
                    UserId = model.UserId,
                    UserName = model.UserName
                };

                if (model.IsAddNew)
                    model.Result = UserService.AddUser(dto);
                else
                    model.Result = UserService.UpdateUser(dto);

                if (model.Result.IsSuccess && model.Result.Ids.Count == 1)
                    model.UserId = model.Result.Ids.FirstOrDefault();

                if (model.UserTeamIds != null && model.UserTeamIds.Any())
                    model.Result = TeamUserService.SaveUserTeams(model.UserId, model.UserTeamIds.ToList());
            }
            else
                model.FillUserTeamsSelectList(TeamService.GetTeams(), TeamUserService.GetTeamUsersByUserId(model.UserId));

            return PartialView("_UserEditPartial", model);
        }


        [HttpPost]
        public IActionResult _deleteUser(string id)
        {
            ChangeResult result = new ChangeResult();

            Guid? userId = Helper.ConvertToGuid(id);
            if (userId.HasValue)
            {
                var user = UserService.GetUser(userId.Value);
                if (user != null)
                {
                    UserDto dto = new UserDto()
                    {
                        BackupEmail = user.BackupEmail,
                        BackupPhoneNumber = user.BackupPhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleInitial = user.MiddleInitial,
                        PrimaryEmail = user.PrimaryEmail,
                        PrimaryPhoneNumber = user.PrimaryPhoneNumber,
                        UserId = user.UserId,
                        UserName = user.UserName,
                        DeleteDate = DateTime.UtcNow
                    };

                    result = UserService.UpdateUser(dto);
                }
            }

            return new JsonResult(result);
        }

    }
}
