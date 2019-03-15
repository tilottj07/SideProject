using System;
using System.Collections.Generic;

namespace Scheduler.Domain
{
    public class Team : ChangeTracker
    {

        public string TeamId { get; set; }
        public string LocationId { get; set; }
        public string TeamName { get; set; }
        public string TeamDescription { get; set; }
        public string TeamLeaderId { get; set; }
        public string TeamEmail { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public string LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Location Location { get; set; }
        public List<TeamCategory> TeamCategories { get; set; }
        public List<TeamUser> TeamUsers { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Warranty> Warranties { get; set; }

    }
}
