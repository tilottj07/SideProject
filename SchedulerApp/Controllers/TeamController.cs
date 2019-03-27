using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Interface.Models;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class TeamController : Controller
    {
        private ITeamService TeamService;
        private ILocationService LocationService;
        private IUserService UserService;
        private ITeamUserService TeamUserService;

        public TeamController()
        {
            TeamService = new TeamService();
            LocationService = new LocationService();
            UserService = new UserService();
            TeamUserService = new TeamUserService();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Teams");
        }

        public IActionResult Teams()
        {
            Models.Team.Teams vm = new Models.Team.Teams();
            return View(vm);
        }

        public IActionResult _getTeamsGridData()
        {
            List<Models.Team.TeamsGridRow> rows = new List<Models.Team.TeamsGridRow>();

            foreach (var team in TeamService.GetTeamDisplays())
                rows.Add(new Models.Team.TeamsGridRow(team));

            return Json(new { data = rows });
        }


        public IActionResult EditTeamModal(string id)
        {
            Guid? teamId = Helper.ConvertToGuid(id);
            var locations = LocationService.GetLocations();
            var users = UserService.GetUsers();
            List<ITeamUser> teamUsers = new List<ITeamUser>();

            Models.Team.TeamEdit model = new Models.Team.TeamEdit();
            if (teamId.HasValue)
            {
                var team = TeamService.GetTeam(teamId.Value);
                teamUsers = TeamUserService.GetTeamUsersByTeamId(teamId.Value);
                model = new Models.Team.TeamEdit(team, locations, users, teamUsers);
            }
            else model = new Models.Team.TeamEdit(locations, users, teamUsers);

            return PartialView("_TeamEditPartial", model);
        }

        [HttpPost]
        public IActionResult EditTeamModal(Models.Team.TeamEdit model)
        {
            if (ModelState.IsValid)
            {
                TeamDto dto = new TeamDto()
                {
                    LocationId = model.LocationId,
                    TeamDescription = model.TeamDescription,
                    TeamEmail = model.TeamEmail,
                    TeamId = model.TeamId,
                    TeamLeaderId = model.TeamLeaderId,
                    TeamName = model.TeamName
                };

                if (model.IsAddNew) model.Result = TeamService.AddTeam(dto);
                else model.Result = TeamService.UpdateTeam(dto);

                if (model.Result.Ids.Count == 1)
                    model.TeamId = model.Result.Ids.FirstOrDefault();

                if (model.Result.IsSuccess && model.TeamUserIds != null && model.TeamUserIds.Any())
                    model.Result = TeamUserService.SaveTeamUsers(model.TeamId, model.TeamUserIds.ToList());
            }
            else
            {
                var users = UserService.GetUsers();

                model.FillLocationSelectList(LocationService.GetLocations());
                model.FillTeamLeaderSelectList(users);
                model.FillTeamUsersSelectList(users, TeamUserService.GetTeamUsersByTeamId(model.TeamId));
            }

            return PartialView("_TeamEditPartial", model);
        }

        [HttpPost]
        public IActionResult _deleteTeam(string id)
        {
            ChangeResult result = new ChangeResult();
            Guid? teamId = Helper.ConvertToGuid(id);
            if (teamId.HasValue)
            {
                var team = TeamService.GetTeam(teamId.Value);
                if (team != null)
                {
                    TeamDto dto = new TeamDto()
                    {
                        LocationId = team.LocationId,
                        TeamDescription = team.TeamDescription,
                        TeamEmail = team.TeamEmail,
                        TeamId = team.TeamId,
                        TeamLeaderId = team.TeamLeaderId,
                        TeamName = team.TeamName,
                        DeleteDate = DateTime.UtcNow
                    };
                    result = TeamService.UpdateTeam(dto);
                }
            }

            return new JsonResult(result);
        }
    }
}
