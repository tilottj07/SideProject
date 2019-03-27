using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Interface.Models;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

namespace Scheduler.BL.Team.Implementation
{
    public class TeamUserService : ITeamUserService
    {
        private IMapper Mapper;

        public TeamUserService()
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Domain.TeamUser, TeamUserDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public ITeamUser GetTeamUser(Guid teamUserId)
        {
            ITeamUser teamUser = null;
            using (var context = new Data.ScheduleContext())
            {
                var item = context.TeamUsers.FirstOrDefault(x => x.TeamUserId == teamUserId.ToString());
                if (item != null) teamUser = Mapper.Map<TeamUserDto>(item);
            }
            return teamUser;
        }

        public ITeamUser GetTeamUser(Guid teamId, Guid userId)
        {
            ITeamUser teamUser = null;
            using (var context = new Data.ScheduleContext())
            {
                var item = context.TeamUsers.FirstOrDefault(x => x.TeamId == teamId.ToString() && x.UserId == userId.ToString());
                if (item != null) teamUser = Mapper.Map<TeamUserDto>(item);
            }
            return teamUser;
        }

        public List<ITeamUser> GetTeamUsersByTeamId(Guid teamId)
        {
            List<ITeamUser> teamUsers = new List<ITeamUser>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.TeamUsers.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) teamUsers.Add(Mapper.Map<TeamUserDto>(item));
            }
            return teamUsers;
        }

        public List<ITeamUser> GetTeamUsersByUserId(Guid userId)
        {
            List<ITeamUser> teamUsers = new List<ITeamUser>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.TeamUsers.Where(x => x.UserId == userId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) teamUsers.Add(Mapper.Map<TeamUserDto>(item));
            }
            return teamUsers;
        }

        public List<ITeamUser> GetTeamUsers()
        {
            List<ITeamUser> teamUsers = new List<ITeamUser>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.TeamUsers.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) teamUsers.Add(Mapper.Map<TeamUserDto>(item));
            }
            return teamUsers;
        }


        public ChangeResult SaveTeamUsers(Guid teamId, List<Guid> userIds, Guid? changeUserId = null)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var existingUsers = context.TeamUsers.Where(x => x.TeamId == teamId.ToString()).ToList();
                List<Guid> existingUserIds = new List<Guid>();
                foreach(var user in existingUsers)
                {
                    if (!userIds.Contains(Guid.Parse(user.UserId)))
                    {
                        user.DeleteDate = DateTime.UtcNow;
                        user.LastUpdateDate = DateTime.UtcNow;
                        if (changeUserId.HasValue) user.LastUpdateUserId = changeUserId.Value.ToString();
                    }
                    else if(user.DeleteDate.HasValue)
                    {
                        user.DeleteDate = null;
                        user.LastUpdateDate = DateTime.UtcNow;
                        if (changeUserId.HasValue) user.LastUpdateUserId = changeUserId.Value.ToString();
                    }
                    existingUserIds.Add(Guid.Parse(user.UserId));
                }

                foreach(Guid userId in userIds)
                {
                    if (!existingUserIds.Contains(userId))
                    {
                        context.TeamUsers.Add(new Domain.TeamUser()
                        {
                            TeamUserId = Guid.NewGuid().ToString(),
                            TeamId = teamId.ToString(),
                            UserId = userId.ToString(),
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = changeUserId.HasValue ? changeUserId.Value.ToString() : null,
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = changeUserId.HasValue ? changeUserId.Value.ToString() : null,
                            DeleteDate = null
                        });
                    }
                }

                context.SaveChanges();
            }

            return result;
        }

        public ChangeResult SaveUserTeams(Guid userId, List<Guid> teamIds, Guid? changeUserId = null)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var existingTeams = context.TeamUsers.Where(x => x.UserId == userId.ToString());
                List<Guid> existingTeamIds = new List<Guid>();
                foreach (var item in existingTeams)
                {
                    if (!teamIds.Contains(Guid.Parse(item.TeamId)))
                    {
                        item.DeleteDate = DateTime.UtcNow;
                        item.LastUpdateDate = DateTime.UtcNow;
                        if (changeUserId.HasValue) item.LastUpdateUserId = changeUserId.Value.ToString();
                    }  
                    else if (item.DeleteDate.HasValue)
                    {
                        item.DeleteDate = null;
                        item.LastUpdateDate = DateTime.UtcNow;
                        if (changeUserId.HasValue) item.LastUpdateUserId = changeUserId.Value.ToString();
                    }
                }

                foreach (Guid teamId in teamIds)
                {
                    if (!existingTeamIds.Contains(teamId))
                    {
                        context.TeamUsers.Add(new Domain.TeamUser()
                        {
                            TeamUserId = Guid.NewGuid().ToString(),
                            TeamId = teamId.ToString(),
                            UserId = userId.ToString(),
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = changeUserId.HasValue ? changeUserId.Value.ToString() : null,
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = changeUserId.HasValue ? changeUserId.Value.ToString() : null,
                            DeleteDate = null
                        });
                    }
                }

                context.SaveChanges();
            }

            return result;
        }


        public ChangeResult AddTeamUser(ITeamUser teamUser)
        {
            return AddTeamUser(new List<ITeamUser> { teamUser });
        }
        public ChangeResult AddTeamUser(List<ITeamUser> teamUsers)
        {
            var result = Validate(teamUsers, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in teamUsers)
                    {
                        context.TeamUsers.Add(new Domain.TeamUser()
                        {
                            TeamUserId = item.TeamUserId == Guid.Empty ? Guid.NewGuid().ToString() : item.TeamUserId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            UserId = item.UserId.ToString(),
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = item.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        public ChangeResult UpdateTeamUser(ITeamUser teamUser)
        {
            return UpdateTeamUser(new List<ITeamUser> { teamUser });
        }

        public ChangeResult UpdateTeamUser(List<ITeamUser> teamUsers)
        {
            var result = Validate(teamUsers);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in teamUsers)
                    {
                        context.TeamUsers.Update(new Domain.TeamUser()
                        {
                            TeamUserId = item.TeamUserId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            UserId = item.UserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }



        private ChangeResult Validate(List<ITeamUser> teamUsers, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            Team = new TeamService();
            User = new UserService();

            foreach (var item in teamUsers)
            {
                var existingCombo = GetTeamUser(item.TeamId, item.UserId);
                if (isAddNew)
                {
                    if (existingCombo != null)
                    {
                        var display = GetTeamNameUserDisplayName(item.TeamId, item.UserId);

                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"{display.Item2} is already a member of team {display.Item1}");
                    }
                }
                else
                {
                    if (item.TeamUserId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid TeamUserId");
                    }

                    if (existingCombo != null && existingCombo.TeamUserId != item.TeamUserId)
                    {
                        var display = GetTeamNameUserDisplayName(item.TeamId, item.UserId);

                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"{display.Item2} is already a member of team {display.Item1}");
                    }
                }

                var user = User.GetUser(item.UserId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid UserId");
                }

                var team = Team.GetTeam(item.TeamId);
                if (team == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid TeamId");
                }
            }

            return result;
        }


        public ChangeResult DeleteTeamUser(Guid teamUserId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var item = context.TeamUsers.FirstOrDefault(x => x.TeamUserId == teamUserId.ToString());
                if (item != null)
                {
                    context.TeamUsers.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

        private ITeamService Team;
        private IUserService User;

        private Tuple<string, string> GetTeamNameUserDisplayName(Guid teamId, Guid userId)
        {
            var a = Team.GetTeam(teamId);
            var b = User.GetUser(userId);

            string team = a != null ? a.TeamName : string.Empty;
            string user = b != null ? b.DisplayName : string.Empty;

            return new Tuple<string, string>(team, user);
        }
    }
}
