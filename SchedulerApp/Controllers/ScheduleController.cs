using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using static Scheduler.BL.Schedule.Implementation.ScheduleService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class ScheduleController : Controller
    {
        private IScheduleService ScheduleService;
        private ITeamService TeamService;

        public ScheduleController()
        {
            ScheduleService = new ScheduleService();
            TeamService = new TeamService();
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

            if (teamId.HasValue) displays = ScheduleService.GetTeamScheduleByInterval(teamId.Value, startDate, endDate, TimeInterval.Day);
            else if (userId.HasValue) displays = ScheduleService.GetUserScheduleByInterval(userId.Value, startDate, endDate, TimeInterval.Day);
            else displays = ScheduleService.GetAllSchedulesByInterval(startDate, endDate, TimeInterval.Day);

            foreach (var item in displays)
                rows.Add(new Models.Schedule.SchedulesGridRow(item));

            return Json(new { data = rows });
        }
    }
}
