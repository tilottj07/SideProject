﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Interface;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

namespace Scheduler.BL.Schedule.Implementation
{
    public class WarrantyService : IWarrantyService
    {
        private IMapper Mapper;

        public WarrantyService()
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Domain.Warranty, WarrantyDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public IWarranty GetWarranty(Guid warrantyId)
        {
            IWarranty warranty = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Warranties.FirstOrDefault(x => x.WarrantyId == warrantyId.ToString());
                if (item != null) warranty = Mapper.Map<WarrantyDto>(item);
            }
            return warranty;
        }

        public List<IWarranty> GetWarrantiesByTeamId(Guid teamId, DateTime startDate, DateTime endDate)
        {
            List<IWarranty> warranties = new List<IWarranty>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Warranties.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue
                    && ((x.StartDate >= startDate && x.StartDate <= endDate)
                        || (x.EndDate >= startDate && x.EndDate <= endDate)
                        || (x.StartDate <= startDate && x.EndDate >= endDate)));

                foreach (var item in items) warranties.Add(Mapper.Map<WarrantyDto>(item));
            }
            return warranties;
        }

        public List<IWarranty> GetWarrantiesByUserId(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<IWarranty> warranties = new List<IWarranty>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Warranties.Where(x => x.UserId == userId.ToString() && !x.DeleteDate.HasValue
                    && ((x.StartDate >= startDate && x.StartDate <= endDate)
                        || (x.EndDate >= startDate && x.EndDate <= endDate)
                        || (x.StartDate <= startDate && x.EndDate >= endDate)));

                foreach (var item in items) warranties.Add(Mapper.Map<WarrantyDto>(item));
            }
            return warranties;
        }

        public List<IWarranty> GetWarranties(DateTime startDate, DateTime endDate)
        {
            List<IWarranty> warranties = new List<IWarranty>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Warranties.Where(x => !x.DeleteDate.HasValue
                    && ((x.StartDate >= startDate && x.StartDate <= endDate)
                        || (x.EndDate >= startDate && x.EndDate <= endDate)
                        || (x.StartDate <= startDate && x.EndDate >= endDate)));

                foreach (var item in items) warranties.Add(Mapper.Map<WarrantyDto>(item));
            }
            return warranties;
        }

        public IWarrantyDisplay GetWarrantyDisplay(Guid warrantyId)
        {
            var warranty = GetWarranty(warrantyId);
            return FillWarrantyDisplay(new List<IWarranty> { warranty }).FirstOrDefault();
        }

        public List<IWarrantyDisplay> GetWarranyDisplaysByTeamId(Guid teamId, DateTime startDate, DateTime endDate)
        {
            var items = GetWarrantiesByTeamId(teamId, startDate, endDate);
            return FillWarrantyDisplay(items);
        }

        public List<IWarrantyDisplay> GetWarranyDisplaysByUserId(Guid userId, DateTime startDate, DateTime endDate)
        {
            var items = GetWarrantiesByUserId(userId, startDate, endDate);
            return FillWarrantyDisplay(items);
        }

        public List<IWarrantyDisplay> GetWarrantyDisplays(DateTime startDate, DateTime endDate)
        {
            var items = GetWarranties(startDate, endDate);
            return FillWarrantyDisplay(items);
        }

        private List<IWarrantyDisplay> FillWarrantyDisplay(List<IWarranty> warranties)
        {
            List<IWarrantyDisplay> list = new List<IWarrantyDisplay>();

            if (Team == null) Team = new TeamService();
            if (User == null) User = new UserService();

            var userDict = User.GetUsers(warranties.Select(x => x.UserId).ToList()).ToDictionary(x => x.UserId);
            var teamDict = Team.GetTeams(warranties.Select(x => x.TeamId).ToList()).ToDictionary(x => x.TeamId);

            foreach(var warranty in warranties)
            {
                string teamName = teamDict.ContainsKey(warranty.TeamId) ? teamDict[warranty.TeamId].TeamName : string.Empty;
                string userDisplayName = userDict.ContainsKey(warranty.UserId) ? userDict[warranty.UserId].DisplayName : string.Empty;

                list.Add(FillWarrantyDisplay(warranty, teamName, userDisplayName));
            }

            return list;
        }

        public ChangeResult AddWarranty(IWarranty warranty)
        {
            return AddWarranty(new List<IWarranty> { warranty });
        }

        public ChangeResult AddWarranty(List<IWarranty> warranties)
        {
            var result = Validate(warranties, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in warranties)
                    {
                        context.Warranties.Add(new Domain.Warranty()
                        {
                            WarrantyId = item.WarrantyId == Guid.Empty ? Guid.NewGuid().ToString() : item.WarrantyId.ToString(),
                            WarrantyName = Helper.CleanString(item.WarrantyName),
                            WarrentyDescription = Helper.CleanString(item.WarrentyDescription),
                            TeamId = item.TeamId.ToString(),
                            UserId = item.UserId.ToString(),
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
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

        public ChangeResult UpdateWarranty(IWarranty warranty)
        {
            return UpdateWarranty(new List<IWarranty> { warranty });
        }

        public ChangeResult UpdateWarranty(List<IWarranty> warranties)
        {
            var result = Validate(warranties);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in warranties)
                    {
                        context.Warranties.Update(new Domain.Warranty()
                        {
                            WarrantyId = item.WarrantyId.ToString(),
                            WarrantyName = Helper.CleanString(item.WarrantyName),
                            WarrentyDescription = Helper.CleanString(item.WarrentyDescription),
                            TeamId = item.TeamId.ToString(),
                            UserId = item.UserId.ToString(),
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
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


        private ChangeResult Validate(List<IWarranty> warranties, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            Team = new TeamService();
            User = new UserService();

            var users = User.GetUsers(warranties.Select(x => x.UserId).Distinct().ToList());
            var teams = Team.GetTeams(warranties.Select(x => x.TeamId).Distinct().ToList());

            List<Guid> validUserIds = users.Select(x => x.UserId).ToList();
            List<Guid> validTeamIds = teams.Select(x => x.TeamId).ToList();

            foreach (var item in warranties)
            {
                if (!isAddNew)
                {
                    if (item.WarrantyId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Warranty Id");
                    }
                }

                if (string.IsNullOrWhiteSpace(item.WarrantyName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Warranty Name is required");
                }
                else if (item.WarrantyName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Warranty Name cannot be more than 100 characters");
                }

                if (!string.IsNullOrWhiteSpace(item.WarrentyDescription) && item.WarrentyDescription.Trim().Length > 500)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Warrenty Description cannot be more than 500 characters");
                }

                if (item.StartDate > item.EndDate)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Start date cannot be after end date");
                }

                if (!validUserIds.Contains(item.UserId))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid User Id");
                }

                if (!validTeamIds.Contains(item.TeamId))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Team Id");
                }

            }
            return result;
        }

        private ITeamService Team;
        private IUserService User;


        public ChangeResult DeleteWarranty(Guid warrantyId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Warranties.FirstOrDefault(x => x.WarrantyId == warrantyId.ToString());
                if (item != null)
                {
                    context.Warranties.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }



        private WarrantyDisplayDto FillWarrantyDisplay(IWarranty warranty, string teamName, string userDisplayName)
        {
            return new WarrantyDisplayDto()
            {
                EndDate = warranty.EndDate,
                StartDate = warranty.StartDate,
                TeamId = warranty.TeamId,
                UserId = warranty.UserId,
                WarrantyId = warranty.WarrantyId,
                WarrantyName = warranty.WarrantyName,
                WarrentyDescription = warranty.WarrentyDescription,
                TeamName = teamName,
                UserDisplayName = userDisplayName
            };
        }

    }
}
