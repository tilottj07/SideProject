using System;
using System.Collections.Generic;

namespace Scheduler.Domain
{
    public class Category : ChangeTracker
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryEmail { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public List<TeamCategory> TeamCategories { get; set; }
    }
}
