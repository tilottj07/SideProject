using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Interface
{
    public interface ICategoryService
    {

        ICategory GetCategory(Guid categoryId);
        List<ICategory> GetCategories();

        ChangeResult AddCategory(ICategory category);
        ChangeResult AddCategory(List<ICategory> categories);

        ChangeResult UpdateCategory(ICategory category);
        ChangeResult UpdateCategory(List<ICategory> categories);

        ChangeResult DeleteCategory(Guid categoryId);

    }
}
