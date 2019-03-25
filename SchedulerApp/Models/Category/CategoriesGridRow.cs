using Scheduler.BL.Team.Interface.Models;
using System;

namespace SchedulerApp.Models.Category
{
    public class CategoriesGridRow
    {
        public CategoriesGridRow(ICategory category)
        {
            CategoryId = category.CategoryId;
            CategoryName = category.CategoryName;
            CategoryDescription = category.CategoryDescription;
            CategoryEmail = category.CategoryEmail;
        }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryEmail { get; set; }

    }
}
