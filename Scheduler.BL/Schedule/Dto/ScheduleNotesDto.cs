﻿using System;
using Scheduler.BL.Schedule.Interface.Models;

namespace Scheduler.BL.Schedule.Dto
{
    public class ScheduleNoteDto : IScheduleNote
    {
        public Guid ScheduleNoteId { get; set; }
        public Guid ScheduleId { get; set; }
        public string Note { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
