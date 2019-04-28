using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Schedule
{
    public class Schedules
    {
        public Schedules(List<ITeam> teams)
        {
            StartDate = DateTime.Today;
            EndDate = StartDate.AddDays(8).AddMinutes(-1);

            FillTeamSelectList(teams);
        }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        public DateTime EndDate { get; set; }

        public Guid TeamIdParam { get; set; }


        public List<SelectListItem> TeamSelectList { get; set; }
        


        private void FillTeamSelectList(List<ITeam> teams)
        {
            TeamSelectList = new List<SelectListItem>();

            foreach (var team in teams.OrderBy(x => x.TeamName))
                TeamSelectList.Add(new SelectListItem() { Text = team.TeamName, Value = team.TeamId.ToString() });

        }

    }
}
