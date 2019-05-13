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
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class WarrantyController : Controller
    {
        private IWarrantyService WarrantyService;
        private ITeamService TeamService;
        private IUserService UserService;

        public WarrantyController()
        {
            WarrantyService = new WarrantyService();
            TeamService = new TeamService();
            UserService = new UserService();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Warranties");
        }

        public IActionResult Warranties()
        {
            Models.Warranty.Warranties vm = new Models.Warranty.Warranties(TeamService.GetTeams());
            return View(vm);
        }

        public IActionResult GetWarrantiesGridData(DateTime startDate, DateTime endDate, Guid? teamId)
        {
            List<Models.Warranty.WarrantiesGridRow> rows = new List<Models.Warranty.WarrantiesGridRow>();
            List<IWarrantyDisplay> displays;

            if (teamId.HasValue) displays = WarrantyService.GetWarranyDisplaysByTeamId(teamId.Value, startDate, endDate);
            else displays = WarrantyService.GetWarrantyDisplays(startDate, endDate);

            foreach (var item in displays)
                rows.Add(new Models.Warranty.WarrantiesGridRow(item));

            return Json(new { data = rows });
        }

        public IActionResult EditWarrantyModal(string id)
        {
            Guid? warrantyId = Helper.ConvertToGuid(id);
            var teams = TeamService.GetTeams();

            Models.Warranty.EditWarranty model;
            if (warrantyId.HasValue)
                model = new Models.Warranty.EditWarranty(WarrantyService.GetWarranty(warrantyId.Value), teams);
            else
                model = new Models.Warranty.EditWarranty(teams);

            return PartialView("_WarrantyEditPartial", model);
        }

        [HttpPost]
        public IActionResult EditWarrantyModal(Models.Warranty.EditWarranty model)
        {
            if (ModelState.IsValid)
            {
                WarrantyDto dto = new WarrantyDto
                {
                    WarrantyId = model.WarrantyId,
                    EndDate = model.EndDate.ToUniversalTime(),
                    StartDate = model.StartDate.ToUniversalTime(),
                    TeamId = model.TeamId,
                    UserId = model.UserId,
                    WarrantyName = model.WarrantyName,
                    WarrentyDescription = model.WarrentyDescription
                };

                if (model.IsAddNew)
                    model.Result = WarrantyService.AddWarranty(dto);
                else
                    model.Result = WarrantyService.UpdateWarranty(dto);
            }
            else
            {
                model.FillTeamsSelectList(TeamService.GetTeams());
                model.UserSelectList = new List<SelectListItem>();
            }

            return PartialView("_WarrantyEditPartial", model);
        }

        public IActionResult GetTeamUsersSelectList(Guid teamId, Guid? userId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var user in UserService.GetTeamUsers(teamId).OrderBy(x => x.LastName).ThenBy(x => x.FirstName))
                list.Add(new SelectListItem { Selected = userId == user.UserId, Text = user.DisplayName, Value = user.UserId.ToString() });

            return Json(list);
        }
    }
}
