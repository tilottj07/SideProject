using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class LocationServiceTesting
    {
        private ILocationService Service;

        public LocationServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new LocationService();

            TestLocationId = Guid.NewGuid();
            TestUserId = Guid.NewGuid();
        }

        private Guid TestLocationId { get; set; }
        private Guid TestUserId { get; set; }
        private const string TEST_LOCATION_NAME = "La Crosse";
        private const string TEST_DESCRIPTION = "Home of the world's largest can of beer";
        private const string TEST_DESCRIPTION_2 = "Home of the world's second largest can of beer";
        private const string TEST_ADDRESS = "100 Harborview Plaza";
        private const string TEST_CITY = "La Crosse";
        private const string TEST_STATE = "WI";
        private const string TEST_ZIP = "54601";
        private const string TEST_COUNTRY = "USA";

        [TestMethod]
        public void AddRemoveLocationTest()
        {
            LocationDto dto = new LocationDto()
            {
                LocationId = TestLocationId,
                LocationName = TEST_LOCATION_NAME,
                Description = TEST_DESCRIPTION,
                Address = TEST_ADDRESS,
                City = TEST_CITY,
                StateRegion = TEST_STATE,
                ZipCode = TEST_ZIP,
                Country = TEST_COUNTRY,
                CreateUserId = TestUserId,
                LastUpdateUserId = TestUserId
            };

            var addResult = Service.AddLocation(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var loc = Service.GetLocation(dto.LocationId);
            Assert.IsNotNull(loc);
            Assert.AreEqual(TestLocationId, loc.LocationId);
            Assert.AreEqual(TEST_LOCATION_NAME, loc.LocationName);
            Assert.AreEqual(TEST_DESCRIPTION, loc.Description);
            Assert.AreEqual(TEST_ADDRESS, loc.Address);
            Assert.AreEqual(TEST_CITY, loc.City);
            Assert.AreEqual(TEST_STATE, loc.StateRegion);
            Assert.AreEqual(TEST_ZIP, loc.ZipCode);
            Assert.AreEqual(TEST_COUNTRY, loc.Country);
            Assert.AreEqual(TestUserId, loc.CreateUserId);
            Assert.AreEqual(TestUserId, loc.LastUpdateUserId);

            dto.Description = TEST_DESCRIPTION_2;
            var updateResult = Service.UpdateLocation(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            loc = Service.GetLocation(dto.LocationId);
            Assert.IsNotNull(loc);
            Assert.AreEqual(TEST_DESCRIPTION_2, loc.Description);

            var deleteResult = Service.DeleteLocation(dto.LocationId);
            Assert.IsTrue(deleteResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidLocationIdTest()
        {
            LocationDto dto = new LocationDto()
            {
                LocationName = TEST_LOCATION_NAME,
                Description = TEST_DESCRIPTION,
                Address = TEST_ADDRESS,
                City = TEST_CITY,
                StateRegion = TEST_STATE,
                ZipCode = TEST_ZIP,
                Country = TEST_COUNTRY,
                CreateUserId = TestUserId,
                LastUpdateUserId = TestUserId
            };

            var result = Service.UpdateLocation(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidLocationNameTest()
        {
            LocationDto dto = new LocationDto()
            {
                LocationId = TestLocationId,
                LocationName = "dewiruyaoietuyoiauyetiouyasoiudgyiasuydgiusydgioausydogiuyasoidguyaiosuydgoiasuydgoiusyadgoiuyasdgoiuyasdogiuy",
                Description = TEST_DESCRIPTION,
                Address = TEST_ADDRESS,
                City = TEST_CITY,
                StateRegion = TEST_STATE,
                ZipCode = TEST_ZIP,
                Country = TEST_COUNTRY,
                CreateUserId = TestUserId,
                LastUpdateUserId = TestUserId
            };

            var result = Service.AddLocation(dto);
            Assert.IsFalse(result.IsSuccess);

            dto.LocationName = string.Empty;
            result = Service.AddLocation(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
