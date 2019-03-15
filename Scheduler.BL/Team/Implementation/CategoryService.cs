using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Implementation
{
    public class CategoryService : ICategoryService
    {
        private IMapper Mapper;

        public CategoryService()
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Domain.Category, CategoryDto>());
            Mapper = mapConfig.CreateMapper();
        }


        public ICategory GetCategory(Guid categoryId)
        {
            ICategory category = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Categories.FirstOrDefault(x => x.CategoryId == categoryId.ToString());
                if (item != null) category = Mapper.Map<CategoryDto>(item);
            }
            return category;
        }

        public List<ICategory> GetCategories()
        {
            List<ICategory> categories = new List<ICategory>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Categories.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) categories.Add(Mapper.Map<CategoryDto>(item));
            }
            return categories;
        }

        public ChangeResult AddCategory(ICategory category)
        {
            return AddCategory(new List<ICategory> { category });
        }

        public ChangeResult AddCategory(List<ICategory> categories)
        {
            var result = Validate(categories, isAddNew: true);
            if (result.IsSuccess)
            {
                using(var context = new Data.ScheduleContext())
                {
                    foreach(var item in categories)
                    {
                        context.Categories.Add(new Domain.Category()
                        {
                            CategoryId = item.CategoryId == Guid.Empty ? Guid.NewGuid().ToString() : item.CategoryId.ToString(),
                            CategoryName = Helper.CleanString(item.CategoryName),
                            CategoryDescription = Helper.CleanString(item.CategoryDescription),
                            CategoryEmail = Helper.CleanString(item.CategoryEmail),
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

        public ChangeResult UpdateCategory(ICategory category)
        {
            return UpdateCategory(new List<ICategory> { category });
        }

        public ChangeResult UpdateCategory(List<ICategory> categories)
        {
            var result = Validate(categories);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in categories)
                    {
                        context.Categories.Update(new Domain.Category()
                        {
                            CategoryId = item.CategoryId.ToString(),
                            CategoryName = Helper.CleanString(item.CategoryName),
                            CategoryDescription = Helper.CleanString(item.CategoryDescription),
                            CategoryEmail = Helper.CleanString(item.CategoryEmail),
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


        private ChangeResult Validate(List<ICategory> categories, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            foreach (var item in categories)
            {
                if (!isAddNew)
                {
                    if (item.CategoryId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid CategoryId");
                    }
                }

                if (string.IsNullOrWhiteSpace(item.CategoryName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Category Name is required");
                }
                else if (item.CategoryName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Category Name cannot be longer than 100 characters");
                }

                if (!string.IsNullOrWhiteSpace(item.CategoryDescription) && item.CategoryDescription.Trim().Length > 500)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Category Description cannot be longer than 100 characters");
                }

                if (!string.IsNullOrWhiteSpace(item.CategoryEmail))
                {
                    if (!Helper.IsValidEmail(item.CategoryEmail))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid Category Email: {item.CategoryEmail}");
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Deletes the category. Removes the record from the db... doesn't just set the delete date
        /// </summary>
        /// <returns>The category.</returns>
        /// <param name="categoryId">Category identifier.</param>
        public ChangeResult DeleteCategory(Guid categoryId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Categories.FirstOrDefault(x => x.CategoryId == categoryId.ToString());
                if (item != null)
                {
                    context.Categories.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}
