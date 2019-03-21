using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataTables;
using Scheduler.BL.User.Interface;
using Scheduler.BL.User.Implementation;
using SchedulerApp.Models.User;

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
            Models.User.Users vm = new Models.User.Users();
            return View(vm);
        }


        [HttpPost]
        public IActionResult _getUserGridData()
        {
            List<UsersGridRow> rows = new List<UsersGridRow>();

            foreach (var user in UserService.GetUsers())
                rows.Add(new UsersGridRow(user));

            return Json(rows);
        }


    }
}
