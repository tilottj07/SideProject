using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Warranty
{
    public class EditWarranty
    {
        public EditWarranty() { }

        public EditWarranty(List<ITeam> teams)
        {
            FillTeamsSelectList(teams);
            UserSelectList = new List<SelectListItem>();
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
            }

            FillTeamsSelectList(teams);
            UserSelectList = new List<SelectListItem>();
        }

        public Guid WarrantyId { get; set; }

        [Required]
        [Display(Name = "Warranty Name")]
        public string WarrantyName { get; set; }

        [Required]
        [Display(Name = "Warranty Description")]
        public string WarrentyDescription { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }



        public List<SelectListItem> TeamSelectList { get; set; }
        public List<SelectListItem> UserSelectList { get; set; }


        private void FillTeamsSelectList(List<ITeam> teams)
        {
            TeamSelectList = new List<SelectListItem>();

            foreach (var item in teams.OrderBy(x => x.TeamName))
                TeamSelectList.Add(new SelectListItem { Selected = item.TeamId == TeamId, Text = item.TeamName, Value = item.TeamId.ToString() });
        }
    }
}
