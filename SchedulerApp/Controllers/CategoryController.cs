﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
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
            Guid? categoryId = Helper.ConvertToGuid(id);
            Models.Category.EditCategory model = new Models.Category.EditCategory();

            if (categoryId.HasValue)
                model = new Models.Category.EditCategory(CategoryService.GetCategory(categoryId.Value));

            return PartialView("_CategoryEditPartial", model);
        }

        [HttpPost]
        public IActionResult EditCategoryModal(Models.Category.EditCategory model)
        {
            if (ModelState.IsValid)
            {
                CategoryDto dto = new CategoryDto()
                {
                    CategoryDescription = model.CategoryDescription,
                    CategoryEmail = model.CategoryEmail,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName
                };

                if (model.IsAddNew)
                    model.Result = CategoryService.AddCategory(dto);
                else
                    model.Result = CategoryService.UpdateCategory(dto);
            }
            return PartialView("_CategoryEditPartial", model);
        }

        [HttpPost]
        public IActionResult _deleteCategory(string id)
        {
            Guid? categoryId = Helper.ConvertToGuid(id);
            ChangeResult result = new ChangeResult();

            if (categoryId.HasValue)
            {
                var model = CategoryService.GetCategory(categoryId.Value);
                if (model != null)
                {
                    CategoryDto dto = new CategoryDto()
                    {
                        CategoryDescription = model.CategoryDescription,
                        CategoryEmail = model.CategoryEmail,
                        CategoryId = model.CategoryId,
                        CategoryName = model.CategoryName,
                        DeleteDate = model.DeleteDate
                    };

                    result = CategoryService.UpdateCategory(dto);
                }
            }

            return new JsonResult(result);
        }


    }
}