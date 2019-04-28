using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Team.Interface.Models;
using static Scheduler.BL.Schedule.Implementation.ScheduleService;

namespace SchedulerApp.Models.Schedule
{
    public class EditSchedule
    {
        public EditSchedule() { }

        public EditSchedule(List<ITeam> teams)
        {
            FillTeamSelectList(teams);
            FillSupportLevelSelectList();
            UserSelectList = new List<SelectListItem>();

            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(7);
        }

        public EditSchedule(ISchedule schedule, List<ITeam> teams)
        {
            if (schedule != null)
            {
                ScheduleId = schedule.ScheduleId;
                TeamId = schedule.TeamId;
                UserId = schedule.UserId;
                SupportLevel = schedule.SupportLevel;
                StartDate = schedule.StartDate.ToLocalTime();
                EndDate = schedule.EndDate.ToLocalTime(); 
            }

            FillTeamSelectList(teams);
            FillSupportLevelSelectList();
            UserSelectList = new List<SelectListItem>();
        }

        public Guid ScheduleId { get; set; }

        [Display(Name = "Team")]
        public Guid TeamId { get; set; }
        [Display(Name = "User")]
        public Guid UserId { get; set; }
        [Display(Name = "Support Level")]
        public SupportLevelType SupportLevel { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        public DateTime EndDate { get; set; }

        public List<SelectListItem> TeamSelectList { get; set; }
        public List<SelectListItem> UserSelectList { get; set; }
        public List<SelectListItem> SupportLevelSelectList { get; set; }

        public string ModalTitle
        {
            get
            {
                return "Modify Schedule";
            }
        }


        public void FillTeamSelectList(List<ITeam> teams)
        {
            TeamSelectList = new List<SelectListItem>();

            foreach (var team in teams.OrderBy(x => x.TeamName))
                TeamSelectList.Add(new SelectListItem() { Selected = TeamId == team.TeamId, Text = team.TeamName, Value = team.TeamId.ToString() });
        }

        public void FillSupportLevelSelectList()
        {
            SupportLevelSelectList = new List<SelectListItem>();
            foreach(var type in Enum.GetValues(typeof(SupportLevelType)))
            {
                var st = (SupportLevelType)type;
                SupportLevelSelectList.Add(new SelectListItem() { Selected = st == SupportLevel, Text = st.ToString(), Value = ((int)st).ToString() });
            }
        }
    }
}
