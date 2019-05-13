using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Warranty
{
    public class EditWarranty
    {
        public EditWarranty() { IsAddNew = true; }

        public EditWarranty(List<ITeam> teams)
        {
            FillTeamsSelectList(teams);
            UserSelectList = new List<SelectListItem>();
            IsAddNew = true;
        }

        public EditWarranty(IWarranty warranty, List<ITeam> teams)
        {
            if (warranty != null)
            {
                WarrantyId = warranty.WarrantyId;
                WarrantyName = warranty.WarrantyName;
                WarrentyDescription = warranty.WarrentyDescription;
                TeamId = warranty.TeamId;
                UserId = warranty.UserId;
                StartDate = warranty.StartDate.ToLocalTime();
                EndDate = warranty.EndDate.ToLocalTime();
                UserIdPlaceholder = warranty.UserId;
            }

            FillTeamsSelectList(teams);
            UserSelectList = new List<SelectListItem>();
            IsAddNew = false;
        }

        public Guid WarrantyId { get; set; }

        [Required]
        [Display(Name = "Warranty Name")]
        [StringLength(100)]
        public string WarrantyName { get; set; }

        [Required]
        [Display(Name = "Warranty Description")]
        [StringLength(500)]
        public string WarrentyDescription { get; set; }

        [Required]
        [Display(Name = "Team")]
        public Guid TeamId { get; set; }

        [Required]
        [Display(Name = "User")]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public bool IsAddNew { get; set; }
        public Guid UserIdPlaceholder { get; set; }



        public List<SelectListItem> TeamSelectList { get; set; }
        public List<SelectListItem> UserSelectList { get; set; }


        public void FillTeamsSelectList(List<ITeam> teams)
        {
            TeamSelectList = new List<SelectListItem>();

            foreach (var item in teams.OrderBy(x => x.TeamName))
                TeamSelectList.Add(new SelectListItem { Selected = item.TeamId == TeamId, Text = item.TeamName, Value = item.TeamId.ToString() });
        }


        public ChangeResult Result { get; set; }

        public string ModalTitle
        {
            get
            {
                if (IsAddNew) return "Add Warranty";
                return "Update Warranty";
            }
        }
    }
}
