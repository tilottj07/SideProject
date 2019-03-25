using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL.Shared;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;
using SchedulerApp.Models.Location;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchedulerApp.Controllers
{
    public class LocationController : Controller
    {
        private ILocationService LocationService;

        public LocationController()
        {
            LocationService = new LocationService();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return RedirectToAction("Locations");
        }


        public IActionResult Locations()
        {
            Locations vm = new Locations();
            return View(vm);
        }

        public IActionResult _getLocationsGridData()
        {
            List<LocationsGridRow> rows = new List<LocationsGridRow>();

            foreach (var item in LocationService.GetLocations())
                rows.Add(new LocationsGridRow(item));

            return Json(new { data = rows });
        }


        public IActionResult EditLocationModal(string id)
        {
            EditLocation model = new EditLocation();

            Guid? locationId = Helper.ConvertToGuid(id);
            if (locationId.HasValue)
                model = new EditLocation(LocationService.GetLocation(locationId.Value));

            return PartialView("_EditLocationPartial", model);
        }

        [HttpPost]
        public IActionResult EditLocationModal(EditLocation model)
        {
            if (ModelState.IsValid)
            {
                LocationDto dto = new LocationDto()
                {
                    Address = model.Address,
                    City = model.City,
                    Country = model.Country,
                    Description = model.Description,
                    LocationId = model.LocationId,
                    LocationName = model.LocationName,
                    StateRegion = model.StateRegion,
                    ZipCode = model.ZipCode
                };

                if (model.IsAddNew) model.Result = LocationService.AddLocation(dto);
                else model.Result = LocationService.UpdateLocation(dto);
            }

            return PartialView("_EditLocationPartial", model);
        }

        [HttpPost]
        public IActionResult _deleteLocation(string id)
        {
            ChangeResult result = new ChangeResult();
            Guid? locationId = Helper.ConvertToGuid(id);
            if (locationId.HasValue)
            {
                var model = LocationService.GetLocation(locationId.Value);
                if (model != null)
                {
                    LocationDto dto = new LocationDto()
                    {
                        Address = model.Address,
                        City = model.City,
                        Country = model.Country,
                        Description = model.Description,
                        LocationId = model.LocationId,
                        LocationName = model.LocationName,
                        StateRegion = model.StateRegion,
                        ZipCode = model.ZipCode
                    };

                    result = LocationService.UpdateLocation(dto);
                }
            }

            return new JsonResult(result);
        }

    }
}
