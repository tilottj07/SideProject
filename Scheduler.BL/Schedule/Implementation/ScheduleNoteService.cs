using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Interface;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;

namespace Scheduler.BL.Schedule.Implementation
{
    public class ScheduleNoteService : IScheduleNoteService
    {
        private IMapper Mapper;

        public ScheduleNoteService()
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Domain.ScheduleNote, ScheduleNoteDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public IScheduleNote GetScheduleNote(Guid scheduleNoteId)
        {
            IScheduleNote scheduleNote = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.ScheduleNotes.FirstOrDefault(x => x.ScheduleNoteId == scheduleNoteId.ToString());
                if (item != null) scheduleNote = Mapper.Map<ScheduleNoteDto>(item);
            }
            return scheduleNote;
        }

        public List<IScheduleNote> GetScheduleNotes(Guid scheduleId)
        {
            List<IScheduleNote> scheduleNotes = new List<IScheduleNote>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.ScheduleNotes.Where(x => x.ScheduleId == scheduleId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) scheduleNotes.Add(Mapper.Map<ScheduleNoteDto>(item));
            }
            return scheduleNotes;
        }

        public List<IScheduleNote> GetScheduleNotesByTeam(Guid teamId, DateTime startDate, DateTime endDate)
        {
            List<IScheduleNote> scheduleNotes = new List<IScheduleNote>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Schedules.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue
                    && ((x.StartDate >= startDate && x.StartDate <= endDate)
                        || (x.EndDate >= startDate && x.EndDate <= endDate)
                        || (x.StartDate <= startDate && x.EndDate >= endDate))).
                    Include(x => x.ScheduleNotes.Where(y => !y.DeleteDate.HasValue))
                        .Select(x => x.ScheduleNotes);

                foreach (var item in items) scheduleNotes.Add(Mapper.Map<ScheduleNoteDto>(item));
            }
            return scheduleNotes;
        }

        public ChangeResult AddScheduleNote(IScheduleNote scheduleNote)
        {
            return AddScheduleNote(new List<IScheduleNote> { scheduleNote });
        }

        public ChangeResult AddScheduleNote(List<IScheduleNote> scheduleNotes)
        {
            var result = Validate(scheduleNotes, isAddNew: true);
            if (result.IsSuccess)
            {
                using(var context = new Data.ScheduleContext())
                {
                    foreach(var item in scheduleNotes)
                    {
                        context.ScheduleNotes.Add(new Domain.ScheduleNote()
                        {
                            ScheduleNoteId = item.ScheduleNoteId == Guid.Empty ? Guid.NewGuid().ToString() : item.ScheduleNoteId.ToString(),
                            ScheduleId = item.ScheduleId.ToString(),
                            Note = Helper.CleanString(item.Note),
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = item.CreateUserId.ToString(),
                            LastUpdated = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        public ChangeResult UpdateScheduleNote(IScheduleNote scheduleNote)
        {
            return UpdateScheduleNote(new List<IScheduleNote> { scheduleNote });
        }

        public ChangeResult UpdateScheduleNote(List<IScheduleNote> scheduleNotes)
        {
            var result = Validate(scheduleNotes);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in scheduleNotes)
                    {
                        context.ScheduleNotes.Update(new Domain.ScheduleNote()
                        {
                            ScheduleNoteId = item.ScheduleNoteId.ToString(),
                            ScheduleId = item.ScheduleId.ToString(),
                            Note = Helper.CleanString(item.Note),
                            LastUpdated = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        private ChangeResult Validate(List<IScheduleNote> scheduleNotes, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            foreach(var item in scheduleNotes)
            {
                if (!isAddNew)
                {
                    if (item.ScheduleNoteId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Schedule Note Id");
                    }
                }

                if (item.ScheduleId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Schedule Id");
                }

                if (string.IsNullOrWhiteSpace(item.Note))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Note is required");
                }
                else if (item.Note.Trim().Length > 1000)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Note cannot be longer than 1000 characters");
                }
            }
            return result;
        }


        public ChangeResult DeleteScheduleNote(Guid scheduleNoteId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.ScheduleNotes.FirstOrDefault(x => x.ScheduleNoteId == scheduleNoteId.ToString());
                if (item != null)
                {
                    context.ScheduleNotes.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}
