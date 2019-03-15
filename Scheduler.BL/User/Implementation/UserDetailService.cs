using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.User.Dto;
using Scheduler.BL.User.Interface;
using Scheduler.BL.User.Interface.Models;

namespace Scheduler.BL.User.Implementation
{
    public class UserDetailService : IUserDetailService
    {
        IMapper Mapper;

        public UserDetailService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.UserDetail, UserDetailDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public enum ProficiencyLevelType
        {
            Unknown = 0,
            Low = 1,
            Medium = 2,
            High = 3,
            Master = 4
        }

        public IUserDetail GetUserDetail(Guid userDetailId)
        {
            IUserDetail data = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.UserDetails.FirstOrDefault(x => x.UserDetailId == userDetailId.ToString());
                if (item != null) data = Mapper.Map<UserDetailDto>(item);
            }

            return data;
        }

        public List<IUserDetail> GetUserDetails(Guid userId)
        {
            List<IUserDetail> userDetails = new List<IUserDetail>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.UserDetails.Where(x => x.UserId == userId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) userDetails.Add(Mapper.Map<UserDetailDto>(item));
            }

            return userDetails;
        }

        public List<IUserDetail> GetUserDetails()
        {
            List<IUserDetail> userDetails = new List<IUserDetail>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.UserDetails.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) userDetails.Add(Mapper.Map<UserDetailDto>(item));
            }

            return userDetails;
        }


        public ChangeResult AddUserDetail(IUserDetail userDetail)
        {
            return AddUserDetail(new List<IUserDetail> { userDetail });
        }
        public ChangeResult AddUserDetail(List<IUserDetail> userDetails)
        {
            var result = Validate(userDetails, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in userDetails)
                    {
                        context.UserDetails.Add(new Domain.UserDetail()
                        {
                            UserDetailId = item.UserDetailId == Guid.Empty ? Guid.NewGuid().ToString() : item.UserDetailId.ToString(),
                            UserId = item.UserId.ToString(),
                            Characteristic = Helper.CleanString(item.Characteristic),
                            Description = Helper.CleanString(item.Description),
                            CreateUserId = item.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            ProficiencyLevel = (int)item.ProficiencyLevel,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        public ChangeResult UpdateUserDetail(IUserDetail userDetail)
        {
            return UpdateUserDetail(new List<IUserDetail> { userDetail });
        }
        public ChangeResult UpdateUserDetail(List<IUserDetail> userDetails)
        {
            var result = Validate(userDetails);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in userDetails)
                    {
                        context.UserDetails.Update(new Domain.UserDetail()
                        {
                            UserDetailId = item.UserDetailId.ToString(),
                            UserId = item.UserId.ToString(),
                            Characteristic = Helper.CleanString(item.Characteristic),
                            Description = Helper.CleanString(item.Description),
                            CreateUserId = item.CreateUserId.ToString(),
                            ProficiencyLevel = (int)item.ProficiencyLevel,
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


        private ChangeResult Validate(List<IUserDetail> userDetails, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            UserService = new UserService();

            foreach(var item in userDetails)
            {
                if (!isAddNew)
                {
                    if (item.UserDetailId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Must have UserDetailId populated.");
                    }
                }

                if (item.UserId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid UserId.");
                }
                else
                {
                    var user = UserService.GetUser(item.UserId);
                    if (user == null)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid UserId: {item.UserId}");
                    }
                }

                if (string.IsNullOrWhiteSpace(item.Characteristic))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Characteristic is required.");
                }
                else if (item.Characteristic.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Characteristic cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(item.Description) && item.Description.Trim().Length > 500)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Description cannot be longer than 500 characters.");
                }
            }

            return result;
        }

        private IUserService UserService;

        /// <summary>
        /// Deletes the user detail. NOTE: this actually deletes the record from the DB.
        /// </summary>
        /// <returns>The user detail.</returns>
        /// <param name="userDetailId">User detail identifier.</param>
        public ChangeResult DeleteUserDetail(Guid userDetailId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var item = context.UserDetails.FirstOrDefault(x => x.UserDetailId == userDetailId.ToString());
                if (item != null)
                {
                    context.UserDetails.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }
    }
}
