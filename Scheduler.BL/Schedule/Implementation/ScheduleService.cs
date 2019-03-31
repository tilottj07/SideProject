using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Interface;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

namespace Scheduler.BL.Schedule.Implementation
{
    public class ScheduleService : IScheduleService
    {
        private IMapper Mapper;

        public ScheduleService()
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Domain.Schedule, ScheduleDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public enum SupportLevelType
        {
            Primary = 0,
            Backup = 1
        }

        public enum TimeInterval
        {
            Day = 0,
            Week = 1,
            Month = 2
        }

        public List<ISchedule> GetTeamScheduleByInterval(Guid teamId, DateTime startDate, DateTime endDate, TimeInterval interval)
        {
            return GroupScheduleByInterval(
                GetSchedulesByTeamId(teamId, startDate, endDate), 
                    startDate, endDate, interval);
        }

        public List<ISchedule> GetUserScheduleByInterval(Guid userId, DateTime startDate, DateTime endDate, TimeInterval interval)
        {
            return GroupScheduleByInterval(
                GetSchedulesByUserId(userId, startDate, endDate),
                    startDate, endDate, interval);
        }

        public List<ISchedule> GetAllSchedulesByInterval(DateTime startDate, DateTime endDate, TimeInterval interval)
        {
            return GroupScheduleByInterval(
                GetSchedules(startDate, endDate),
                    startDate, endDate, interval);
        }


        private List<ISchedule> GroupScheduleByInterval(List<ISchedule> schedules, DateTime beginningDate, DateTime finishDate, TimeInterval interval)
        {
            List<ISchedule> list = new List<ISchedule>();
            if (schedules.Count > 0)
            {
                int daysPerInterval = 1;
                switch(interval)
                {
                    case TimeInterval.Day:
                        daysPerInterval = 1;
                        break;
                    case TimeInterval.Week:
                        daysPerInterval = 7;
                        break;
                    case TimeInterval.Month:
                        daysPerInterval = 30;
                        break;  
                }

                DateTime startDate = beginningDate;
                while(startDate <= finishDate)
                {
                    DateTime endDate = startDate.AddDays(daysPerInterval);
                    var matches = schedules.Where(x => (x.StartDate >= startDate && x.StartDate <= endDate)
                        || (x.EndDate >= startDate && x.EndDate <= endDate)
                        || (x.StartDate <= startDate && x.EndDate >= endDate))
                            .OrderBy(x => x.TeamId).ThenBy(x => x.StartDate).ToList();

                    foreach(var match in matches)
                    {
                        var dto = FillScheduleDto(match);

                        if (dto.StartDate < startDate) dto.StartDate = startDate;
                        if (dto.EndDate > endDate) dto.EndDate = endDate;

                        list.Add(dto);
                    }

                    startDate = endDate;
                }
            }
            return list;
        }


        public ISchedule GetSchedule(Guid scheduleId)
        {
            ISchedule schedule = null;
            using (var context = new Data.ScheduleContext())
            {
                var item = context.Schedules.FirstOrDefault(x => x.ScheduleId == scheduleId.ToString());
                if (item != null) schedule = Mapper.Map<ScheduleDto>(item);
            }
            return schedule;
        }

        public List<ISchedule> GetSchedulesByTeamId(Guid teamId, DateTime startDate, DateTime endDate)
        {
            List<ISchedule> schedules = new List<ISchedule>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Schedules.Where(x => x.TeamId == teamId.ToString()
                    && ((x.StartDate >= startDate && x.StartDate <= endDate) 
                        || (x.EndDate >= startDate && x.EndDate <= endDate) 
                        || (x.StartDate <= startDate && x.EndDate >= endDate))
                    && !x.DeleteDate.HasValue);

                foreach (var item in items) schedules.Add(Mapper.Map<ScheduleDto>(item));
            }
            return schedules;
        }

        public List<ISchedule> GetSchedulesByUserId(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<ISchedule> schedules = new List<ISchedule>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Schedules.Where(x => x.UserId == userId.ToString()
                    && ((x.StartDate >= startDate && x.StartDate <= endDate) 
                        || (x.EndDate >= startDate && x.EndDate <= endDate) 
                        || (x.StartDate <= startDate && x.EndDate >= endDate))
                    && !x.DeleteDate.HasValue);

                foreach (var item in items) schedules.Add(Mapper.Map<ScheduleDto>(item));
            }
            return schedules;
        }

        public List<ISchedule> GetSchedules(DateTime startDate, DateTime endDate)
        {
            List<ISchedule> schedules = new List<ISchedule>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Schedules.Where(x =>
                    ((x.StartDate >= startDate && x.StartDate <= endDate) 
                        || (x.EndDate >= startDate && x.EndDate <= endDate) 
                        || (x.StartDate <= startDate && x.EndDate >= endDate))
                    && !x.DeleteDate.HasValue);

                foreach (var item in items) schedules.Add(Mapper.Map<ScheduleDto>(item));
            }
            return schedules;
        }

        public ChangeResult SaveSchedule(ISchedule schedule)
        {
            return SaveSchedule(new List<ISchedule> { schedule });
        }

        public ChangeResult SaveSchedule(List<ISchedule> schedules)
        {
            var result = Validate(schedules);
            if (result.IsSuccess)
            {
                //group schedules by teamid
                Dictionary<Guid, List<ISchedule>> teamDict = new Dictionary<Guid, List<ISchedule>>();
                foreach(var item in schedules)
                {
                    if (!teamDict.ContainsKey(item.TeamId)) teamDict.Add(item.TeamId, new List<ISchedule>());
                    teamDict[item.TeamId].Add(item);
                }

                using (var context = new Data.ScheduleContext())
                {
                    List<ISchedule> adds = new List<ISchedule>();
                    List<ISchedule> updates = new List<ISchedule>();

                    foreach (Guid teamId in teamDict.Keys)
                    {
                        var items = teamDict[teamId];
                        var existingRecords = GetSchedulesByTeamId(teamId, items.Select(x => x.StartDate).Min(), items.Select(x => x.EndDate).Max());

                        foreach (var item in items)
                        {
                            var overlaps = GetOverlappingRecords(item, existingRecords).OrderBy(x => x.StartDate).ToList();
                            if (overlaps.Count > 0)
                            {
                                foreach (var overlap in overlaps)
                                {
                                    //scenarios
                                    // #1 - the new record overlaps the existing record completely. In this case we can just mark it deleted.
                                    if (item.StartDate < overlap.StartDate && item.EndDate > overlap.EndDate)
                                    {
                                        ScheduleDto update = FillScheduleDto(overlap);
                                        update.DeleteDate = DateTime.UtcNow;

                                        updates.Add(update);
                                        adds.Add(item);
                                    }

                                    // #2 - the new record starts after the existing record's start date and the new record's end date is later than existing record's end date
                                    //      In this case, we adjust the end date of the existing record to match the new record's start date.
                                    else if (item.StartDate > overlap.StartDate && item.EndDate > overlap.EndDate)
                                    {
                                        ScheduleDto update = FillScheduleDto(overlap);
                                        update.EndDate = item.StartDate;

                                        updates.Add(update);
                                        adds.Add(item);
                                    }

                                    // #3 - the new record starts after the existing record starts AND ends before the existing record ends.
                                    //      In this case, the existing record needs to be split into 2 records with the new record sandwiched inbetween. 
                                    else if (item.StartDate > overlap.StartDate && item.EndDate < overlap.EndDate)
                                    {
                                        ScheduleDto update = FillScheduleDto(overlap);
                                        update.EndDate = item.StartDate;

                                        ScheduleDto add = FillScheduleDto(overlap);
                                        add.ScheduleId = Guid.NewGuid();
                                        add.StartDate = item.EndDate;

                                        updates.Add(update);
                                        adds.Add(add);
                                        adds.Add(item);
                                    }
                                    else
                                    {
                                        //to make sure the new record gets added, mark the old record deleted and add the new (same as scenario 1)
                                        ScheduleDto update = FillScheduleDto(overlap);
                                        update.DeleteDate = DateTime.UtcNow;

                                        updates.Add(update);
                                        adds.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                //no conflicts, add the new record
                                adds.Add(item);
                            }
                        }
                    }


                    foreach(var add in adds)
                    {
                        context.Schedules.Add(new Domain.Schedule()
                        {
                            ScheduleId = add.ScheduleId == Guid.Empty ? Guid.NewGuid().ToString() : add.ScheduleId.ToString(),
                            TeamId = add.TeamId.ToString(),
                            UserId = add.UserId.ToString(),
                            StartDate = add.StartDate,
                            EndDate = add.EndDate,
                            SupportLevel = (int)add.SupportLevel,
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = add.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = add.LastUpdateUserId.ToString(),
                            DeleteDate = add.DeleteDate
                        });
                    }

                    foreach(var update in updates)
                    {
                        context.Schedules.Update(new Domain.Schedule()
                        {
                            ScheduleId = update.ScheduleId.ToString(),
                            TeamId = update.TeamId.ToString(),
                            UserId = update.UserId.ToString(),
                            StartDate = update.StartDate,
                            EndDate = update.EndDate,
                            SupportLevel = (int)update.SupportLevel,
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = update.LastUpdateUserId.ToString(),
                            DeleteDate = update.DeleteDate
                        });
                    }

                    context.SaveChanges();
                }

                PurgeOldDeletedRecords();
            }

            return result;
        }


        private List<ISchedule> GetOverlappingRecords(ISchedule newRecord, List<ISchedule> existingRecords)
        {
            return existingRecords.Where(x => x.TeamId == newRecord.TeamId && x.SupportLevel == newRecord.SupportLevel
                && ((x.StartDate >= newRecord.StartDate && x.StartDate <= newRecord.EndDate)
                    || (x.EndDate >= newRecord.StartDate && x.EndDate <= newRecord.EndDate)
                    || (x.StartDate <= newRecord.StartDate && x.EndDate >= newRecord.EndDate))).ToList();
        }


        private ChangeResult Validate(List<ISchedule> schedules, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();

            Team = new TeamService();
            User = new UserService();

            var teams = Team.GetTeams(schedules.Select(x => x.TeamId).Distinct().ToList());
            var users = User.GetUsers(schedules.Select(x => x.UserId).Distinct().ToList());

            List<Guid> validTeamIds = teams.Select(x => x.TeamId).ToList();
            List<Guid> validUserIds = users.Select(x => x.UserId).ToList();

            foreach(var item in schedules)
            {
                if (!isAddNew && item.ScheduleId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid ScheduleId");
                }

                if (item.StartDate > item.EndDate)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Start Date cannot be after End Date");
                }

                if (!validTeamIds.Contains(item.TeamId))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid TeamId: {item.TeamId}");
                }

                if (!validUserIds.Contains(item.UserId))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add($"Invalid UserId: {item.UserId}");
                }

                //make sure there is no overlap amongst the new records
                var overlaps = GetOverlappingRecords(item, schedules.Where(x => x.TeamId != item.UserId && x.TeamId != item.TeamId 
                    && x.StartDate != item.StartDate && x.EndDate != item.EndDate).ToList());
                if (overlaps.Count > 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("There are conflicts within the schedules you're attempting to save, please correct.");
                }
            }

            return result;
        }


        private ITeamService Team;
        private IUserService User;


        /// <summary>
        /// Deletes the schedule.  Removes the record, does not just set the delete date
        /// </summary>
        /// <returns>The schedule.</returns>
        /// <param name="scheduleId">Schedule identifier.</param>
        public ChangeResult DeleteSchedule(Guid scheduleId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Schedules.FirstOrDefault(x => x.ScheduleId == scheduleId.ToString());
                if (item != null)
                {
                    context.Schedules.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }


        private void PurgeOldDeletedRecords(int daysOld = 90)
        {
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Schedules.Where(x => x.DeleteDate.HasValue && x.DeleteDate <= DateTime.UtcNow.AddDays(-daysOld));
                foreach (var item in items) context.Schedules.Remove(item);

                context.SaveChanges();
            }
        }




        private ScheduleDto FillScheduleDto(ISchedule schedule)
        {
            return new ScheduleDto()
            {
                ScheduleId = schedule.ScheduleId,
                ChangeDate = schedule.ChangeDate,
                CreateDate = schedule.CreateDate,
                CreateUserId = schedule.CreateUserId,
                DeleteDate = schedule.DeleteDate,
                EndDate = schedule.EndDate,
                StartDate = schedule.StartDate,
                LastUpdateDate = schedule.LastUpdateDate,
                LastUpdateUserId = schedule.LastUpdateUserId,
                SupportLevel = schedule.SupportLevel,
                TeamId = schedule.TeamId,
                UserId = schedule.UserId
            };
        }



    }
}
