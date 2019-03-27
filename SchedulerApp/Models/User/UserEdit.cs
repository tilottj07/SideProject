using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;
using Scheduler.BL.User.Interface.Models;
using static Scheduler.BL.User.Implementation.UserService;

namespace SchedulerApp.Models.User
{
    public class UserEdit
    {
        public UserEdit() { IsAddNew = true; }

        public UserEdit(List<ITeam> teams, List<ITeamUser> teamUsers)
        {
            FillUserTeamsSelectList(teams, teamUsers);
            IsAddNew = true;
        }

        public UserEdit(IUser user, List<ITeam> teams, List<ITeamUser> teamUsers)
        {
            if (user != null)
            {
                UserId = user.UserId;
                UserName = user.UserName;
                FirstName = user.FirstName;
                MiddleInitial = user.MiddleInitial;
                LastName = user.LastName;
                PrimaryPhoneNumber = user.PrimaryPhoneNumber;
                BackupPhoneNumber = user.BackupPhoneNumber;
                PrimaryEmail = user.PrimaryEmail;
                BackupEmail = user.BackupEmail;

                IsAddNew = false;
            }

            FillUserTeamsSelectList(teams, teamUsers);
        }

        public bool IsAddNew { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        public string MiddleInitial { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

  
        [Required]
        [Display(Name = "Primary Phone #")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PrimaryPhoneNumber { get; set; }

        [Display(Name = "Backup Phone #")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string BackupPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Primary Email")]
        [EmailAddress]
        public string PrimaryEmail { get; set; }

        [EmailAddress]
        [Display(Name = "Backup Email")]
        public string BackupEmail { get; set; }

        public List<SelectListItem> UserTeamsSelectList { get; set; }

        [Display(Name = "User Teams (hold cntrl to select)")]
        public IEnumerable<Guid> UserTeamIds { get; set; }


      


        public string ModalTitle
        {
            get
            {
                if (IsAddNew) return "Add User";
                return $"Edit {UserName}";
            }
        }


        public ChangeResult Result { get; set; }



        public void FillUserTeamsSelectList(List<ITeam> teams, List<ITeamUser> teamUsers)
        {
            UserTeamsSelectList = new List<SelectListItem>();
            List<Guid> teamIds = new List<Guid>();

            foreach (var tu in teamUsers)
            {
                if (!teamIds.Contains(tu.TeamId))
                {
                    var team = teams.FirstOrDefault(x => x.TeamId == tu.TeamId);
                    if (team != null)
                    {
                        UserTeamsSelectList.Add(new SelectListItem() { Text = team.TeamName, Value = tu.TeamId.ToString(), Selected = true });
                        teamIds.Add(tu.TeamId);
                    }
                }
            }

            if (UserTeamIds != null)
            {
                foreach(Guid teamId in UserTeamIds)
                {
                    if (!teamIds.Contains(teamId))
                    {
                        var team = teams.FirstOrDefault(x => x.TeamId == teamId);
                        if (team != null)
                        {
                            UserTeamsSelectList.Add(new SelectListItem() { Text = team.TeamName, Value = teamId.ToString(), Selected = true });
                            teamIds.Add(teamId);
                        }
                    }
                }
            }

            foreach(var team in teams.OrderBy(x => x.TeamName))
            {
                if (!teamIds.Contains(team.TeamId))
                    UserTeamsSelectList.Add(new SelectListItem() { Text = team.TeamName, Value = team.TeamId.ToString(), Selected = false });
            }
        }


    }
}
