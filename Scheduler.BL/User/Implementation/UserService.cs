using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.User.Dto;
using Scheduler.BL.User.Interface;
using Scheduler.BL.User.Interface.Models;
using Scheduler.BL.Shared;

namespace Scheduler.BL.User.Implementation
{
    public class UserService : IUserService
    {
        IMapper Mapper;

        public UserService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.User, UserDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public enum PreferredContactMehodType
        {
            Phone = 0,
            Email = 1
        }


        public IUser GetUser(Guid userId)
        {
            IUser user = null;
            using (var context = new Data.ScheduleContext())
            {
                var item = context.Users.FirstOrDefault(x => x.UserId == userId.ToString());
                if (item != null) user = Mapper.Map<UserDto>(item);
            }

            return user;
        }

        public List<IUser> GetUsers(List<Guid> userIds)
        {
            List<IUser> users = new List<IUser>();

            List<string> ids = new List<string>();
            foreach (Guid id in userIds.Distinct()) ids.Add(id.ToString());

            using(var context = new Data.ScheduleContext())
            {
                var items = context.Users.Where(x => ids.Contains(x.UserId) && !x.DeleteDate.HasValue);
                foreach (var item in items) users.Add(Mapper.Map<UserDto>(item));
            }

            return users;
        }

        public IUser GetUser(string userName)
        {
            IUser user = null;
            using (var context = new Data.ScheduleContext())
            {
                var item = context.Users.FirstOrDefault(x => x.UserName == userName.ToUpper());
                if (item != null) user = Mapper.Map<UserDto>(item);
            }

            return user;
        }

        public List<IUser> GetUsers()
        {
            List<IUser> users = new List<IUser>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Users.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) users.Add(Mapper.Map<UserDto>(item));
            }

            return users;
        }

        public List<IUser> GetTeamUsers(Guid teamId)
        {
            List<IUser> users = new List<IUser>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.TeamUsers.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue)
                    .Include(x => x.User).Select(x => x.User);
                foreach (var item in items)
                {
                    if (!item.DeleteDate.HasValue) users.Add(Mapper.Map<UserDto>(item));
                }
            }

            return users;
        }


        public ChangeResult AddUser(IUser user)
        {
            return AddUser(new List<IUser> { user });
        }

        public ChangeResult AddUser(List<IUser> users)
        {
            var result = Validate(users, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var user in users)
                    {
                        context.Users.Add(new Domain.User()
                        {
                            UserId = user.UserId == Guid.Empty ? Guid.NewGuid().ToString() : user.UserId.ToString(),
                            UserName = Helper.CleanString(user.UserName).ToUpper(),
                            FirstName = Helper.CleanString(user.FirstName),
                            MiddleInitial = Helper.CleanString(user.MiddleInitial),
                            LastName = Helper.CleanString(user.LastName),
                            Photo = user.Photo,
                            PrimaryEmail = Helper.CleanString(user.PrimaryEmail),
                            BackupEmail = Helper.CleanString(user.BackupEmail),
                            PrimaryPhoneNumber = Helper.FormatPhoneNumber(user.PrimaryPhoneNumber),
                            BackupPhoneNumber = Helper.FormatPhoneNumber(user.BackupPhoneNumber),
                            PreferredContactMethod = (int?)user.PreferredContactMethod,
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = user.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = user.LastUpdateUserId.ToString(),
                            DeleteDate = user.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }

            return result;
        }

        public ChangeResult UpdateUser(IUser user)
        {
            return UpdateUser(new List<IUser> { user });
        }
        public ChangeResult UpdateUser(List<IUser> users)
        {
            var result = Validate(users);
            if (result.IsSuccess)
            {
                List<string> userIds = new List<string>();
                foreach (var item in users) userIds.Add(item.UserId.ToString());

                using (var context = new Data.ScheduleContext())
                {
                    foreach (var user in users)
                    {
                        context.Users.Update(new Domain.User()
                        {
                            UserId = user.UserId.ToString(),
                            UserName = Helper.CleanString(user.UserName).ToUpper(),
                            FirstName = Helper.CleanString(user.FirstName),
                            MiddleInitial = Helper.CleanString(user.MiddleInitial),
                            LastName = Helper.CleanString(user.LastName),
                            Photo = user.Photo,
                            PrimaryEmail = Helper.CleanString(user.PrimaryEmail),
                            BackupEmail = Helper.CleanString(user.BackupEmail),
                            PrimaryPhoneNumber = Helper.FormatPhoneNumber(user.PrimaryPhoneNumber),
                            BackupPhoneNumber = Helper.FormatPhoneNumber(user.BackupPhoneNumber),
                            PreferredContactMethod = (int?)user.PreferredContactMethod,
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = user.LastUpdateUserId.ToString(),
                            DeleteDate = user.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        private ChangeResult Validate(List<IUser> users, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            foreach (var user in users)
            {
                if (!isAddNew && user.UserId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("UserId must be populated.");
                }

                if (isAddNew)
                {
                    //make sure this username doesn't exist already
                    var existingUser = GetUser(user.UserName);
                    if (existingUser != null)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Username: {user.UserName} already exists.");
                    }
                }


                if (string.IsNullOrWhiteSpace(user.UserName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Username is required.");
                }
                else if (user.UserName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Username cannot be longer than 100 characters.");
                }

                if (string.IsNullOrWhiteSpace(user.FirstName) && string.IsNullOrWhiteSpace(user.LastName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("User must have a first or last name.");
                }

                if (!string.IsNullOrWhiteSpace(user.FirstName) && user.FirstName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("First Name cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(user.MiddleInitial) && user.MiddleInitial.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Middle initial cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(user.LastName) && user.LastName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Last name cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(user.PrimaryEmail))
                {
                    if (!Helper.IsValidEmail(user.PrimaryEmail))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid primary email address: {user.PrimaryEmail}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(user.BackupEmail))
                {
                    if (!Helper.IsValidEmail(user.BackupEmail))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add($"Invalid backup email address: {user.BackupEmail}");
                    }
                }
            }

            return result;
        }

     


        /// <summary>
        /// Deletes the user. NOTE: this actually deletes the DB record, doesn't set the delete date
        /// </summary>
        /// <returns></returns>
        /// <param name="userId">User identifier.</param>
        public ChangeResult DeleteUser(Guid userId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var item = context.Users.FirstOrDefault(x => x.UserId == userId.ToString());
                if (item != null)
                {
                    context.Users.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }

        /// <summary>
        /// Deletes the user. NOTE: this actually deletes the DB record, doesn't set the delete date
        /// </summary>
        /// <returns></returns>
        /// <param name="userName">User name.</param>
        public ChangeResult DeleteUser(string userName)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var item = context.Users.FirstOrDefault(x => x.UserName == userName.ToUpper());
                if (item != null)
                {
                    context.Users.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }
    }
}
