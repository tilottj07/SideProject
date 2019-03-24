using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.Shared;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class TeamController : Controller
    {
        private ITeamService TeamService;
        private ILocationService LocationService;

        public TeamController()
        {
            TeamService = new TeamService();
            LocationService = new LocationService();
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

            foreach (var team in TeamService.GetTeams())
                rows.Add(new Models.Team.TeamsGridRow(team));

            return Json(new { data = rows });
        }


        public IActionResult EditTeamModal(string id)
        {
            Guid? teamId = Helper.ConvertToGuid(id);
            var locations = LocationService.GetLocations();

            Models.Team.TeamEdit model = new Models.Team.TeamEdit();
            if (teamId.HasValue)
            {
                var team = TeamService.GetTeam(teamId.Value);
                model = new Models.Team.TeamEdit(team, locations);
            }
            else model = new Models.Team.TeamEdit(locations);

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
            }

            return PartialView("_TeamEditPartial", model);
        }
    }
}
