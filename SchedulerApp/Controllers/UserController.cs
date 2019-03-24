using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataTables;
using Scheduler.BL.User.Interface;
using Scheduler.BL.User.Implementation;
using SchedulerApp.Models.User;
using Scheduler.BL.User.Dto;
using Scheduler.BL.User.Interface.Models;
using Newtonsoft.Json;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class UserController : Controller
    {
        private IUserService UserService;

        public UserController()
        {
            UserService = new UserService();
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
            Models.User.UserEdit vm = new UserEdit();

            Guid? userId = Helper.ConvertToGuid(id);
            if (userId.HasValue)
            {
                vm = new UserEdit(UserService.GetUser(userId.Value));
            }
           
            return PartialView("_UserEditPartial", vm);
        }

        [HttpPost]
        public IActionResult EditUserModal(Models.User.UserEdit model)
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
            }
            return PartialView("_UserEditPartial", model);
        }

    }
}
