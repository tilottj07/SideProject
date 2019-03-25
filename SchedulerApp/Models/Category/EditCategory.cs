using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulerApp.Models.Category
{
    public class EditCategory
    {
        public EditCategory() { IsAddNew = true; }

        public EditCategory(ICategory category)
        {
            if (category != null)
            {
                CategoryId = category.CategoryId;
                CategoryName = category.CategoryName;
                CategoryDescription = category.CategoryDescription;
                CategoryEmail = category.CategoryEmail;

                IsAddNew = false;
            }
        }

        public bool IsAddNew { get; set; }

        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [StringLength(500)]
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Category Email")]
        public string CategoryEmail { get; set; }


        public string ModalTitle
        {
            get
            {
                if (IsAddNew) return "Add Category";
                return $"Edit {CategoryName}";
            }
        }


        public ChangeResult Result { get; set; }

    }
}
