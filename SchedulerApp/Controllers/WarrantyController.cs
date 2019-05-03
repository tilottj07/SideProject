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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class WarrantyController : Controller
    {
        private IWarrantyService WarrantyService;
        private ITeamService TeamService;

        public WarrantyController()
        {
            WarrantyService = new WarrantyService();
            TeamService = new TeamService();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Warranties");
        }

        public ActionResult Warranties()
        {
            Models.Warranty.Warranties vm = new Models.Warranty.Warranties(TeamService.GetTeams());
            return View(vm);
        }

        public ActionResult GetWarrantiesGridData(DateTime startDate, DateTime endDate, Guid? teamId)
        {
            List<Models.Warranty.WarrantiesGridRow> rows = new List<Models.Warranty.WarrantiesGridRow>();
            List<IWarrantyDisplay> displays;

            if (teamId.HasValue) displays = WarrantyService.GetWarranyDisplaysByTeamId(teamId.Value, startDate, endDate);
            else displays = WarrantyService.GetWarrantyDisplays(startDate, endDate);

            foreach (var item in displays)
                rows.Add(new Models.Warranty.WarrantiesGridRow(item));

            return Json(new { data = rows });
        }
    }
}
