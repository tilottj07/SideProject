using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

namespace SchedulerApp.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService CategoryService;

        public CategoryController()
        {
            CategoryService = new CategoryService();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Categories");
        }


        public IActionResult Categories()
        {
            Models.Category.Categories vm = new Models.Category.Categories();
            return View(vm);
        }

        public IActionResult _getCategoriesGridData()
        {
            List<Models.Category.CategoriesGridRow> rows = new List<Models.Category.CategoriesGridRow>();

            foreach (var item in CategoryService.GetCategories())
                rows.Add(new Models.Category.CategoriesGridRow(item));

            return Json(new { data = rows });
        }


        public IActionResult EditCategoryModal(string id)
        {
            return View();
        }


    }
}