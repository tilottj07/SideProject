using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Interface
{
    public interface ITeamCategoryService
    {
        ITeamCategory GetTeamCategory(Guid teamId, Guid categoryId);
        List<ITeamCategory> GetTeamCategoriesByTeamId(Guid teamId);
        List<ITeamCategory> GetTeamCategories();

        ChangeResult AddTeamCategory(ITeamCategory teamCategory);
        ChangeResult AddTeamCategory(List<ITeamCategory> teamCategories);

        ChangeResult UpdateTeamCategory(ITeamCategory teamCategory);
        ChangeResult UpdateTeamCategory(List<ITeamCategory> teamCategories);

        ChangeResult SaveTeamCategories(Guid teamId, List<Guid> categoryIds, Guid? changeUserId = null);

        ChangeResult DeleteTeamCategory(Guid teamId, Guid categoryId);
    }
}
