using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Interface.Models;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;
using Scheduler.BL.User.Interface.Models;

namespace Scheduler.BL.Team.Implementation
{
    public class TeamService : ITeamService
    {
        private IMapper Mapper;

        public TeamService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Team, TeamDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public ITeam GetTeam(Guid teamId)
        {
            ITeam team = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Teams.FirstOrDefault(x => x.TeamId == teamId.ToString());
                if (item != null) team = Mapper.Map<TeamDto>(item);
            }
            return team;
        }

        public List<ITeam> GetTeams(List<Guid> teamIds)
        {
            List<ITeam> teams = new List<ITeam>();

            List<string> ids = new List<string>();
            foreach (Guid id in teamIds.Distinct()) ids.Add(id.ToString());

            using(var context = new Data.ScheduleContext())
            {
                var items = context.Teams.Where(x => ids.Contains(x.TeamId) && !x.DeleteDate.HasValue);
                foreach (var item in items) teams.Add(Mapper.Map<TeamDto>(item));
            }

            return teams;
        }

        public List<ITeam> GetLocationTeams(Guid locationId)
        {
            List<ITeam> teams = new List<ITeam>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Teams.Where(x => x.LocationId == locationId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) teams.Add(Mapper.Map<TeamDto>(item));
            }
            return teams;
        }

        public List<ITeam> GetTeams()
        {
            List<ITeam> teams = new List<ITeam>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Teams.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) teams.Add(Mapper.Map<TeamDto>(item));
            }
            return teams;
        }

        public List<ITeamDisplay> GetTeamDisplays()
        {
            List<ITeamDisplay> teamDisplays = new List<ITeamDisplay>();

            if (UserService == null) UserService = new UserService();
            if (LocationService == null) LocationService = new LocationService();

            var teams = GetTeams();
            var leaderDict = UserService.GetUsers(teams.Where(x => x.TeamLeaderId.HasValue).Select(x => x.TeamLeaderId.Value).Distinct().ToList()).ToDictionary(x => x.UserId);
            var locationDict = LocationService.GetLocations(teams.Select(x => x.LocationId).Distinct().ToList()).ToDictionary(x => x.LocationId);

            foreach(var team in teams)
            {
                IUser user = null;
                ILocation loc = null;

                if (team.TeamLeaderId.HasValue && leaderDict.ContainsKey(team.TeamLeaderId.Value)) user = leaderDict[team.TeamLeaderId.Value];
                if (locationDict.ContainsKey(team.LocationId)) loc = locationDict[team.LocationId];

                TeamDisplayDto dto = new TeamDisplayDto()
                {
                    TeamId = team.TeamId,
                    LocationId = team.LocationId,
                    TeamDescription = team.TeamDescription,
                    TeamEmail = team.TeamEmail,
                    TeamName = team.TeamName
                };

                if (user != null)
                    dto.LeaderDisplayName = Helper.GetDisplayName(user.UserName, user.FirstName, user.MiddleInitial, user.LastName);

                if (loc != null) dto.LocationName = loc.LocationName;

                teamDisplays.Add(dto);
            }

            return teamDisplays;
        }

        public ChangeResult AddTeam(ITeam team)
        {
            return AddTeam(new List<ITeam> { team });
        }
        public ChangeResult AddTeam(List<ITeam> teams)
        {
            var result = Validate(teams, isAddNew: true);
            if (result.IsSuccess)
            {
                using(var context = new Data.ScheduleContext())
                {
                    foreach(var item in teams)
                    {
                        Guid id = item.TeamId == Guid.Empty ? Guid.NewGuid() : item.TeamId;
                        context.Teams.Add(new Domain.Team()
                        {
                            TeamId = id.ToString(),
                            TeamName = Helper.CleanString(item.TeamName),
                            TeamDescription = Helper.CleanString(item.TeamDescription),
                            TeamLeaderId = item.TeamLeaderId.HasValue ? item.TeamLeaderId.Value.ToString() : null,
                            LocationId = item.LocationId.ToString(),
                            TeamEmail = Helper.CleanString(item.TeamEmail),
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = item.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                        result.Ids.Add(id);
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        public ChangeResult UpdateTeam(ITeam team)
        {
            return UpdateTeam(new List<ITeam> { team });
        }
        public ChangeResult UpdateTeam(List<ITeam> teams)
        {
            var result = Validate(teams);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in teams)
                    {
                        context.Teams.Update(new Domain.Team()
                        {
                            TeamId = item.TeamId.ToString(),
                            TeamName = Helper.CleanString(item.TeamName),
                            TeamDescription = Helper.CleanString(item.TeamDescription),
                            TeamLeaderId = item.TeamLeaderId.HasValue ? item.TeamLeaderId.Value.ToString() : null,
                            LocationId = item.LocationId.ToString(),    
                            TeamEmail = Helper.CleanString(item.TeamEmail),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                        result.Ids.Add(item.TeamId);
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        private ChangeResult Validate(List<ITeam> teams, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            UserService = new UserService();

            foreach(var item in teams)
            {
                if (!isAddNew)
                {
                    if (item.TeamId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid TeamId");
                    }
                }

                if (item.TeamLeaderId.HasValue)
                {
                    if (item.TeamLeaderId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Team Leader Id");
                    }
                    else
                    {
                        var user = UserService.GetUser(item.TeamLeaderId.Value);
                        if (user == null)
                        {
                            result.IsSuccess = false;
                            result.ErrorMessages.Add($"Invalid Team Leader Id: {item.TeamLeaderId}");
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(item.TeamName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Team Name is required");
                }
                else if (item.TeamName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Team Name cannot be longer than 100 characters");
                }

                if (!string.IsNullOrWhiteSpace(item.TeamDescription) && item.TeamDescription.Trim().Length > 500)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Team Description cannot be longer than 500 characters");
                }

                if (!string.IsNullOrWhiteSpace(item.TeamEmail) && !Helper.IsValidEmail(item.TeamEmail))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid Team Email: {item.TeamEmail}");
                }
            }
            return result;
        }

        private IUserService UserService;
        private ILocationService LocationService;


        /// <summary>
        /// Removes the team record, doesn't just set the delete date.
        /// </summary>
        /// <returns>The team.</returns>
        /// <param name="teamId">Team identifier.</param>
        public ChangeResult DeleteTeam(Guid teamId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Teams.FirstOrDefault(x => x.TeamId == teamId.ToString());
                if (item != null)
                {
                    context.Teams.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }
    }
}
