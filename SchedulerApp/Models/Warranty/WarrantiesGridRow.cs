using System;
using Scheduler.BL.Schedule.Interface.Models;

namespace SchedulerApp.Models.Warranty
{
    public class WarrantiesGridRow
    {
        public WarrantiesGridRow(IWarrantyDisplay warranty)
        {
            WarrantyId = warranty.WarrantyId;
            WarrantyName = warranty.WarrantyName;
            WarrentyDescription = warranty.WarrentyDescription;
            TeamName = warranty.TeamName;
            UserDisplayName = warranty.UserDisplayName;
            StartDate = warranty.StartDate.ToLocalTime().ToShortDateString();
            EndDate = warranty.EndDate.ToLocalTime().ToShortDateString();
        }

        public Guid WarrantyId { get; set; }
        public string WarrantyName { get; set; }
        public string WarrentyDescription { get; set; }

        public string TeamName { get; set; }
        public string UserDisplayName { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }


}
