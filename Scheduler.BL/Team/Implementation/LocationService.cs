using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Interface;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Implementation
{
    public class LocationService : ILocationService
    {
        private IMapper Mapper;

        public LocationService()
        {
            var mapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Domain.Location, LocationDto>());
            Mapper = mapConfig.CreateMapper();
        }

        public ILocation GetLocation(Guid locationId)
        {
            ILocation location = null;
            using(var context = new Data.ScheduleContext())
            {
                var item = context.Locations.FirstOrDefault(x => x.LocationId == locationId.ToString());
                if (item != null) location = Mapper.Map<LocationDto>(item);
            }

            return location;
        }

        public List<ILocation> GetLocations()
        {
            List<ILocation> locations = new List<ILocation>();
            using(var context = new Data.ScheduleContext())
            {
                var items = context.Locations.Where(x => !x.DeleteDate.HasValue);
                foreach (var item in items) locations.Add(Mapper.Map<LocationDto>(item));
            }

            return locations;
        }


        public ChangeResult AddLocation(ILocation location)
        {
            return AddLocation(new List<ILocation> { location });
        }
        public ChangeResult AddLocation(List<ILocation> locations)
        {
            var result = Validate(locations, isAddNew: true);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach(var item in locations)
                    {
                        context.Locations.Add(new Domain.Location()
                        {
                            LocationId = item.LocationId == Guid.Empty ? Guid.NewGuid().ToString() : item.LocationId.ToString(),
                            LocationName = Helper.CleanString(item.LocationName),
                            Description = Helper.CleanString(item.Description),
                            Address = Helper.CleanString(item.Address),
                            City = Helper.CleanString(item.City),
                            StateRegion = Helper.CleanString(item.StateRegion),
                            Country = Helper.CleanString(item.Country),
                            ZipCode = Helper.CleanString(item.ZipCode),
                            CreateDate = DateTime.UtcNow,
                            CreateUserId = item.CreateUserId.ToString(),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }
            return result;
        }

        public ChangeResult UpdateLocation(ILocation location)
        {
            return UpdateLocation(new List<ILocation> { location });
        }
        public ChangeResult UpdateLocation(List<ILocation> locations)
        {
            var result = Validate(locations);
            if (result.IsSuccess)
            {
                using (var context = new Data.ScheduleContext())
                {
                    foreach(var item in locations)
                    {
                        context.Locations.Update(new Domain.Location()
                        {
                            LocationId = item.LocationId.ToString(),
                            LocationName = Helper.CleanString(item.LocationName),
                            Description = Helper.CleanString(item.Description),
                            Address = Helper.CleanString(item.Address),
                            City = Helper.CleanString(item.City),
                            StateRegion = Helper.CleanString(item.StateRegion),
                            Country = Helper.CleanString(item.Country),
                            ZipCode = Helper.CleanString(item.ZipCode),
                            LastUpdateDate = DateTime.UtcNow,
                            LastUpdateUserId = item.LastUpdateUserId.ToString(),
                            DeleteDate = item.DeleteDate
                        });
                    }
                    context.SaveChanges();
                }
            }

            return result;
        }


        private ChangeResult Validate(List<ILocation> locations, bool isAddNew = false)
        {
            ChangeResult result = new ChangeResult();
            foreach (var item in locations)
            {
                if (!isAddNew)
                {
                    if (item.LocationId == Guid.Empty)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessages.Add("Invalid LocationId");
                    }
                }

                if (string.IsNullOrWhiteSpace(item.LocationName))
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Location Name is required.");
                }
                else if (item.LocationName.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Location Name cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(item.Address) && item.Address.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Address cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(item.City) && item.City.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("City cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(item.StateRegion) && item.StateRegion.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("State Region cannot be longer than 100 characters.");
                }

                if (!string.IsNullOrWhiteSpace(item.ZipCode) && item.ZipCode.Trim().Length > 100)
                {
                    result.IsSuccess = false;
                    result.ErrorMessages.Add("Zip Code cannot be longer than 100 characters.");
                }
            }

            return result;
        }


        /// <summary>
        /// this actually removes the record, does not set the delete date
        /// </summary>
        /// <returns>The location.</returns>
        /// <param name="locationId">Location identifier.</param>
        public ChangeResult DeleteLocation(Guid locationId)
        {
            ChangeResult result = new ChangeResult();
            using (var context = new Data.ScheduleContext())
            {
                var item = context.Locations.FirstOrDefault(x => x.LocationId == locationId.ToString());
                if (item != null)
                {
                    context.Locations.Remove(item);
                    context.SaveChanges();
                }
            }

            return result;
        }
    }
}
