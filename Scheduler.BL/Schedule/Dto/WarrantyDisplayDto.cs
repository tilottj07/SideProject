using System;
using Scheduler.BL.Schedule.Interface.Models;

namespace Scheduler.BL.Schedule.Dto
{
    public class WarrantyDisplayDto : IWarrantyDisplay
    {
        public Guid WarrantyId { get; set; }
        public string WarrantyName { get; set; }
        public string WarrentyDescription { get; set; }

        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

        public string TeamName { get; set; }
        public string UserDisplayName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
