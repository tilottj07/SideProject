using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;
using static Scheduler.BL.Schedule.Implementation.ScheduleService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class ScheduleController : Controller
    {
        private IScheduleService ScheduleService;
        private ITeamService TeamService;
        private IUserService UserService;

        public ScheduleController()
        {
            ScheduleService = new ScheduleService();
            TeamService = new TeamService();
            UserService = new UserService();
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Schedules");
        }


        public IActionResult Schedules()
        {
            Models.Schedule.Schedules vm = new Models.Schedule.Schedules(TeamService.GetTeams());
            return View(vm);
        }

        public IActionResult _getSchedulesGridData(DateTime startDate, DateTime endDate, Guid? teamId, Guid? userId)
        {
            List<Models.Schedule.SchedulesGridRow> rows = new List<Models.Schedule.SchedulesGridRow>();
            List<IScheduleDisplay> displays = new List<IScheduleDisplay>();

            startDate = startDate.ToUniversalTime();
            endDate = endDate.ToUniversalTime();

            if (teamId.HasValue) displays = ScheduleService.GetTeamScheduleByInterval(teamId.Value, startDate, endDate, TimeInterval.Day);
            else if (userId.HasValue) displays = ScheduleService.GetUserScheduleByInterval(userId.Value, startDate, endDate, TimeInterval.Day);
            else displays = ScheduleService.GetAllSchedulesByInterval(startDate, endDate, TimeInterval.Day);

            foreach (var item in displays)
                rows.Add(new Models.Schedule.SchedulesGridRow(item));

            return Json(new { data = rows });
        }

        public IActionResult EditScheduleModal(string id)
        {
            Guid? scheduleId = Helper.ConvertToGuid(id);
            var teams = TeamService.GetTeams();

            Models.Schedule.EditSchedule model = new Models.Schedule.EditSchedule(teams);
            if (scheduleId.HasValue)
            {
                var schedule = ScheduleService.GetSchedule(scheduleId.Value);
                model = new Models.Schedule.EditSchedule(schedule, teams);
            }

            return PartialView("_ScheduleEditPartial", model);
        }

        [HttpPost]
        public IActionResult EditScheduleModal(Models.Schedule.EditSchedule model)
        {
            ChangeResult result = new ChangeResult();
            if (ModelState.IsValid)
            {
                ScheduleDto dto = new ScheduleDto
                {
                    EndDate = model.EndDate.ToUniversalTime(),
                    ScheduleId = model.ScheduleId,
                    StartDate = model.StartDate.ToUniversalTime(),
                    SupportLevel = model.SupportLevel,
                    TeamId = model.TeamId,
                    UserId = model.UserId
                };
                result = ScheduleService.SaveSchedule(dto);
            }
            else
            {
                model.FillTeamSelectList(TeamService.GetTeams());
                model.FillSupportLevelSelectList();
                model.UserSelectList = new List<SelectListItem>();
            }

            return PartialView("_ScheduleEditPartial", model);
        }

        public IActionResult GetTeamUsersSelectList(Guid teamId, Guid? userId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var user in UserService.GetTeamUsers(teamId).OrderBy(x => x.LastName).ThenBy(x => x.FirstName))
                list.Add(new SelectListItem() { Selected = userId == user.UserId, Text = user.DisplayName, Value = user.UserId.ToString() });

            return Json(list);
        }

        [HttpPost]
        public IActionResult DeleteSchedule(string id)
        {
            ChangeResult result = new ChangeResult();

            Guid? scheduleId = Helper.ConvertToGuid(id);
            if (scheduleId.HasValue)          
                result = ScheduleService.DeleteSchedule(scheduleId.Value);

            return Json(result);
        }
    }
}
