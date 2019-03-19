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
    public class WarrantyNoteService : IWarrantyNoteService
    {
        private IMapper Mapper;

        public WarrantyNoteService()
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Domain.WarrantyNote, WarrantyNoteDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public IWarrantyNote GetWarrantyNote(Guid warrantyNoteId)
        {
            IWarrantyNote warrantyNote = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.WarrantyNotes.FirstOrDefault(x => x.WarrantyNoteId == warrantyNoteId.ToString());
                if (item != null) warrantyNote = Mapper.Map<WarrantyNoteDto>(item);
            }
            return warrantyNote;
        }

        public List<IWarrantyNote> GetWarrantyNotes(Guid warrantyId, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<IWarrantyNote> warrantyNotes = new List<IWarrantyNote>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.WarrantyNotes.Where(x => x.WarrantyId == warrantyId.ToString() && !x.DeleteDate.HasValue);
                foreach (var item in items) warrantyNotes.Add(Mapper.Map<WarrantyNoteDto>(item));
            }

            if (startDate.HasValue)
                warrantyNotes = warrantyNotes.Where(x => x.StartDate >= startDate.Value || x.EndDate >= startDate.Value).ToList();
            if (endDate.HasValue)
                warrantyNotes = warrantyNotes.Where(x => x.StartDate <= endDate.Value || x.EndDate <= endDate.Value).ToList();

            return warrantyNotes;
        }

        public List<IWarrantyNote> GetScheduleNotesByTeam(Guid teamId, DateTime startDate, DateTime endDate)
        {
            List<IWarrantyNote> notes = new List<IWarrantyNote>();
            using (var context = new Data.ScheduleContext())
            {
                var items = context.Warranties.Where(x => x.TeamId == teamId.ToString() && !x.DeleteDate.HasValue
                    && ((x.StartDate >= startDate && x.StartDate <= endDate)
                        || (x.EndDate >= startDate && x.EndDate <= endDate)
                        || (x.StartDate <= startDate && x.EndDate >= endDate))).
                    Include(x => x.WarrantyNotes.Where(y => !y.DeleteDate.HasValue))
                        .Select(x => x.WarrantyNotes);

                foreach (var item in items) notes.Add(Mapper.Map<WarrantyNoteDto>(item));
            }

            notes = notes.Where(x => (x.StartDate >= startDate || x.EndDate >= startDate)
                && (x.StartDate <= endDate || x.EndDate <= endDate)).ToList();

            return notes;
        }

        public ChangeResult AddWarrantyNote(IWarrantyNote note)
        {
            return AddWarrantyNote(new List<IWarrantyNote> { note });
        }

        public ChangeResult AddWarrantyNote(List<IWarrantyNote> notes)
        {
            var result = Validate(notes, isAddNew: true);
            if (result.IsSuccess)
            {
                using(var context = new Data.ScheduleContext())
                {
                    foreach(var item in notes)
                    {
                        context.WarrantyNotes.Add(new Domain.WarrantyNote()
                        {
                            WarrantyNoteId = item.WarrantyNoteId == Guid.Empty ? Guid.NewGuid().ToString() : item.WarrantyNoteId.ToString(),
                            WarrantyId = item.WarrantyId.ToString(),
                            Note = Helper.CleanString(item.Note),
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = item.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = null
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        public ChangeResult UpdateWarrantyNote(IWarrantyNote note)
        {
            return UpdateWarrantyNote(new List<IWarrantyNote> { note });
        }

        public ChangeResult UpdateWarrantyNote(List<IWarrantyNote> notes)
        {
            var result = Validate(notes);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach (var item in notes)
                    {
                        context.WarrantyNotes.Update(new Domain.WarrantyNote()
                        {
                            WarrantyNoteId = item.WarrantyNoteId.ToString(),
                            WarrantyId = item.WarrantyId.ToString(),
                            Note = Helper.CleanString(item.Note),
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = null
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }


        private ChangeResult Validate(List<IWarrantyNote> notes, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            foreach(var item in notes)
            {
                if (!isAddNew)
                {
                    if (item.WarrantyNoteId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid Warrant Note Id");
                    }
                }

                if (item.WarrantyId == Guid.Empty)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Invalid Warranty Id");
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

                if (item.StartDate > item.EndDate)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Start date cannot be after end date");
                }
            }
            return result;
        }

        public ChangeResult DeleteWarrantyNote(Guid warrantyNoteId)
        {
            ChangeResult result = new ChangeResult();
            using(var context = new Data.ScheduleContext())
            {
                var item = context.WarrantyNotes.FirstOrDefault(x => x.WarrantyNoteId == warrantyNoteId.ToString());
                if (item != null)
                {
                    context.WarrantyNotes.Remove(item);
                    context.SaveChanges();
                }
            }
            return result;
        }

    }
}
