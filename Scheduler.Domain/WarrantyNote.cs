﻿using System;
namespace Scheduler.Domain
{
    public class WarrantyNote : ChangeTracker
    {
        public string WarrantyNoteId { get; set; }
        public string WarrantyId { get; set; }
        public string Note { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

    }
}
