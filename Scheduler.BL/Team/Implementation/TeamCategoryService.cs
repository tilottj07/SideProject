using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Implementation
{
    public class TeamCategoryService : ITeamCategoryService
    {
        private IMapper Mapper;

        public TeamCategoryService()
        {
            var mapConfig = new MapperConfiguration(c => c.CreateMap<Domain.TeamCategory, TeamCategoryDto>());
            Mapper = mapConfig.CreateMapper();
        }


        public ITeamCategory GetTeamCategory(Guid teamId, Guid categoryId)
        {
            ITeamCategory teamCategory = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.TeamCategories.FirstOrDefault(x => x.TeamId == teamId.ToString() && x.CategoryId == categoryId.ToString());
                if (item != null) teamCategory = Mapper.Map<TeamCategoryDto>(item);
            }
            return teamCategory;
        }

        public List<ITeamCategory> GetTeamCategoriesByTeamId(Guid teamId)
        {
            List<ITeamCategory> teamCategories = new List<ITeamCategory>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.TeamCategories.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) teamCategories.Add(Mapper.Map<TeamCategoryDto>(item));
            }
            return teamCategories;
        }

        public List<ITeamCategory> GetTeamCategories()
        {
            List<ITeamCategory> teamCategories = new List<ITeamCategory>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.TeamCategories.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) teamCategories.Add(Mapper.Map<TeamCategoryDto>(item));
            }
            return teamCategories;
        }


        public ChangeResult AddTeamCategory(ITeamCategory teamCategory)
        {
            return AddTeamCategory(new List<ITeamCategory> { teamCategory });
        }

        public ChangeResult AddTeamCategory(List<ITeamCategory> teamCategories)
        {
            var result = Validate(teamCategories, isAddNew: true);
            if (result.IsSuccess)
            {
                using(var context = new Data.ScheduleContext())
                {
                    foreach(var item in teamCategories)
                    {
                        context.TeamCategories.Add(new Domain.TeamCategory()
                        {
                            TeamCategoryId = item.TeamCategoryId == Guid.Empty ? Guid.NewGuid().ToString() : item.TeamCategoryId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            CategoryId = item.CategoryId.ToString(),
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


        public ChangeResult UpdateTeamCategory(ITeamCategory teamCategory)
        {
            return UpdateTeamCategory(new List<ITeamCategory> { teamCategory });
        }

        public ChangeResult UpdateTeamCategory(List<ITeamCategory> teamCategories)
        {
            var result = Validate(teamCategories);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in teamCategories)
                    {
                        context.TeamCategories.Update(new Domain.TeamCategory()
                        {
                            TeamCategoryId = item.TeamCategoryId.ToString(),
                            TeamId = item.TeamId.ToString(),
                            CategoryId = item.CategoryId.ToString(),
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


        private ChangeResult Validate(List<ITeamCategory> teamCategories, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            Category = new CategoryService();
            Team = new TeamService();

            foreach(var item in teamCategories)
            {
                var existingCombo = GetTeamCategory(item.TeamId, item.CategoryId);
                if (isAddNew)
                {
                    if (existingCombo != null)
                    {
                        var teamNameCategoryName = GetTeamNameCategoryName(item.TeamId, item.CategoryId);

                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Team: {teamNameCategoryName.Item1} already belongs to Category: {teamNameCategoryName.Item2}");
                    }
                }
                else
                {
                    if (item.TeamCategoryId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid TeamCategoryId");
                    }

                    if (existingCombo != null && existingCombo.TeamCategoryId != item.TeamCategoryId)
                    {
                        var teamNameCategoryName = GetTeamNameCategoryName(item.TeamId, item.CategoryId);

                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Team: {teamNameCategoryName.Item1} already belongs to Category: {teamNameCategoryName.Item2}");
                    }
                }

                if (item.TeamId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid TeamId");
                }
                else
                {
                    var team = Team.GetTeam(item.TeamId);
                    if (team == null)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Team: {item.TeamId} does not exist");
                    }
                }

                if (item.CategoryId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid CategoryId");
                }
                else
                {
                    var cat = Category.GetCategory(item.CategoryId);
                    if (cat == null)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Category: {item.CategoryId} does not exist");
                    }
                }
            }
            return result;
        }


        public ChangeResult DeleteTeamCategory(Guid teamId, Guid categoryId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.TeamCategories.FirstOrDefault(x => x.TeamId == teamId.ToString() && x.CategoryId == categoryId.ToString());
                if (item != null)
                {
                    context.TeamCategories.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;  
        }

        private ICategoryService Category;
        private ITeamService Team;


        private Tuple<string, string> GetTeamNameCategoryName(Guid teamId, Guid categoryId)
        {
            var t = Team.GetTeam(teamId);
            var c = Category.GetCategory(categoryId);

            string teamName = t != null ? t.TeamName : string.Empty;
            string categoryName = c != null ? c.CategoryName : string.Empty;

            return new Tuple<string, string>(teamName, categoryName);
        }
    }
}
