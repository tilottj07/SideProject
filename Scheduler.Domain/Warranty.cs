using System;
using System.Collections.Generic;

namespace Scheduler.Domain
{
    public class Warranty : ChangeTracker
    {
        public string WarrantyId { get; set; }
        public string WarrantyName { get; set; }
        public string WarrentyDescription { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public List<WarrantyNote> WarrantyNotes { get; set; }

    }
}
