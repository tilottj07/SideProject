using System;
using Scheduler.BL.Schedule.Interface.Models;

namespace Scheduler.BL.Schedule.Dto
{
    public class WarrantyDto : IWarranty
    {
        public Guid WarrantyId { get; set; }
        public string WarrantyName { get; set; }
        public string WarrentyDescription { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }

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
