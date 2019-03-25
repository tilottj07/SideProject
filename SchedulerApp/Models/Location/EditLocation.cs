using System;
using System.ComponentModel.DataAnnotations;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Location
{
    public class EditLocation
    {
        public EditLocation()
        {
            IsAddNew = true;
        }

        public EditLocation(ILocation location)
        {
            if (location != null)
            {
                LocationId = location.LocationId;
                LocationName = location.LocationName;
                Description = location.Description;
                Address = location.Address;
                City = location.City;
                StateRegion = location.StateRegion;
                Country = location.Country;
                ZipCode = location.ZipCode;

                IsAddNew = false;
            }
        }

        public bool IsAddNew { get; set; }

        public Guid LocationId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        [Display(Name = "State/Region")]
        public string StateRegion { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string ZipCode { get; set; }

        public string ModalTitle
        {
            get
            {
                if (IsAddNew) return "Add Location";
                return $"Edit {LocationName}";
            }
        }

        public ChangeResult Result { get; set; }
    }
}
