using System;
namespace Scheduler.Domain
{
    public class Location : ChangeTracker
    {
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
