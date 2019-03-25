using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Team
{
    public class TeamEdit
    {
        public TeamEdit()
        {
            IsAddNew = true;
        }

        public TeamEdit(List<ILocation> locations)
        {
            FillLocationSelectList(locations);
            IsAddNew = true;
        }

        public TeamEdit(ITeam team, List<ILocation> locations)
        {
            if (team != null)
            {
                TeamId = team.TeamId;
                LocationId = team.LocationId;
                TeamName = team.TeamName;
                TeamLeaderId = team.TeamLeaderId;
                TeamEmail = team.TeamEmail;
                TeamDescription = team.TeamDescription;

                IsAddNew = false;
            }

            FillLocationSelectList(locations);
        }


        private void FillLocationSelectList(List<ILocation> locations)
        {
            LocationsSelectList = new List<SelectListItem>();
            foreach (var location in locations.OrderBy(x => x.LocationName))
            {
                LocationsSelectList.Add(new SelectListItem()
                { Text = location.LocationName, Value = location.LocationId.ToString(), Selected = location.LocationId == LocationId });
            }
        }

        public bool IsAddNew { get; set; }

        public Guid TeamId { get; set; }

        [Display(Name = "Location")]
        public Guid LocationId { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        [MaxLength(100)]
        public string TeamName { get; set; }
        public Guid? TeamLeaderId { get; set; }

        [Display(Name = "Team Email")]
        [MaxLength(100)]
        public string TeamEmail { get; set; }

        [Display(Name = "Team Description")]
        [MaxLength(500)]
        public string TeamDescription { get; set; }

        public List<SelectListItem> LocationsSelectList { get; set; }

        public string ModalTitle
        {
            get
            {
                if (IsAddNew) return "Add Team";
                return $"Edit {TeamName}";
            }
        }


        public ChangeResult Result { get; set; }
    }
}
