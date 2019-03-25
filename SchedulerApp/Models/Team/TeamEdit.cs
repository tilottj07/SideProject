using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;
using Scheduler.BL.User.Interface.Models;

namespace SchedulerApp.Models.Team
{
    public class TeamEdit
    {
        public TeamEdit()
        {
            IsAddNew = true;
        }

        public TeamEdit(List<ILocation> locations, List<IUser> users)
        {
            FillLocationSelectList(locations);
            FillTeamLeaderSelectList(users);
            IsAddNew = true;
        }

        public TeamEdit(ITeam team, List<ILocation> locations, List<IUser> users)
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
            FillTeamLeaderSelectList(users);
        }


        public void FillLocationSelectList(List<ILocation> locations)
        {
            LocationsSelectList = new List<SelectListItem>();
            foreach (var location in locations.OrderBy(x => x.LocationName))
            {
                LocationsSelectList.Add(new SelectListItem()
                { Text = $"{location.LocationName} - {location.Description}", Value = location.LocationId.ToString(), Selected = location.LocationId == LocationId });
            }
        }

        public void FillTeamLeaderSelectList(List<IUser> users)
        {
            TeamLeaderSelectList = new List<SelectListItem>
            {
                new SelectListItem() { Text = "None", Value = string.Empty, Selected = TeamLeaderId == null }
            };

            foreach (var user in users)
                TeamLeaderSelectList.Add(new SelectListItem() { Text = user.DisplayName, Value = user.UserId.ToString(), Selected = user.UserId == TeamLeaderId });
        }

        public bool IsAddNew { get; set; }

        public Guid TeamId { get; set; }

        [Display(Name = "Location")]
        public Guid LocationId { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        [MaxLength(100)]
        public string TeamName { get; set; }

        [Display(Name = "Team Leader")]
        public Guid? TeamLeaderId { get; set; }

        [Display(Name = "Team Email")]
        [MaxLength(100)]
        public string TeamEmail { get; set; }

        [Display(Name = "Team Description")]
        [MaxLength(500)]
        public string TeamDescription { get; set; }

        public List<SelectListItem> LocationsSelectList { get; set; }
        public List<SelectListItem> TeamLeaderSelectList { get; set; }

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
