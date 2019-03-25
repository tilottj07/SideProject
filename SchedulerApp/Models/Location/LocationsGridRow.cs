using System;
using Scheduler.BL.Team.Interface.Models;

namespace SchedulerApp.Models.Location
{
    public class LocationsGridRow
    {
        public LocationsGridRow(ILocation location)
        {
            LocationId = location.LocationId;
            LocationName = location.LocationName;
            Description = location.Description;
            Address = location.Address;
            City = location.City;
            StateRegion = location.StateRegion;
            Country = location.Country;
            ZipCode = location.ZipCode;
        }

        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
