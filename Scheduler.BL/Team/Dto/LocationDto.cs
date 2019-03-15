using System;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Dto
{
    public class LocationDto : ILocation
    {
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
