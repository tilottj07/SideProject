using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Warranty
{
    public class Warranties
    {
        public Warranties(List<ITeam> teams)
        {
            StartDateParam = DateTime.Today;
            EndDateParam = DateTime.Today.AddMonths(1);

            FillTeamSelectList(teams);
        }

        [DataType(DataType.Date)]
        [Display(Name = "Start")]
        public DateTime StartDateParam { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End")]
        public DateTime EndDateParam { get; set; }

        public List<SelectListItem> TeamSelectList { get; set; }
        public Guid TeamIdParam { get; set; }


        private void FillTeamSelectList(List<ITeam> teams)
        {
            TeamSelectList = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "All", Value = string.Empty }
            };

            foreach (var item in teams)
                TeamSelectList.Add(new SelectListItem { Selected = false, Text = item.TeamName, Value = item.TeamId.ToString() });
        }
    }
}
