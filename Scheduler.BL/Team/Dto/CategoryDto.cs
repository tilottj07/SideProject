using System;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Dto
{
    public class CategoryDto : ICategory
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryEmail { get; set; }

        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }

        public DateTime LastUpdateDate { get; set; }
        public Guid LastUpdateUserId { get; set; }

        public DateTime ChangeDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
