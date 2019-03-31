using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("MaintenanceHub");
        }

        public IActionResult MaintenanceHub()
        {
            Models.Admin.MaintenanceHub vm = new Models.Admin.MaintenanceHub();
            return View(vm);
        }
    }
}
