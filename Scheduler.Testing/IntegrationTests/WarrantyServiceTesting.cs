using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class WarrantyServiceTesting : IntegrationBase
    {
        private IWarrantyService Service;

        public WarrantyServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new WarrantyService();
        }

        private const string TEST_WARRANTY_NAME = "Test Name";
        private const string TEST_WARRANT_DESC = "Test Desc";
        private const string TEST_WARRANT_DESC_2 = "Test Desc 2";

        [TestMethod]
        public void AddRemoveWarrantyTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            WarrantyDto dto = new WarrantyDto()
            {
                WarrantyId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                WarrantyName = TEST_WARRANTY_NAME,
                WarrentyDescription = TEST_WARRANT_DESC
            };

            var addResult = Service.AddWarranty(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var warranty = Service.GetWarranty(dto.WarrantyId);
            Assert.IsNotNull(warranty);
            Assert.AreEqual(dto.WarrantyId, warranty.WarrantyId);
            Assert.AreEqual(dto.WarrantyName, warranty.WarrantyName);
            Assert.AreEqual(dto.WarrentyDescription, warranty.WarrentyDescription);

            dto.WarrentyDescription = TEST_WARRANT_DESC_2;
            var updateResult = Service.UpdateWarranty(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            warranty = Service.GetWarranty(dto.WarrantyId);
            Assert.IsNotNull(warranty);
            Assert.AreEqual(TEST_WARRANT_DESC_2, warranty.WarrentyDescription);

            var deleteResult = Service.DeleteWarranty(dto.WarrantyId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidWarrantyIdTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            WarrantyDto dto = new WarrantyDto()
            {
                WarrantyId = Guid.Empty,
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                WarrantyName = TEST_WARRANTY_NAME,
                WarrentyDescription = TEST_WARRANT_DESC
            };

            var result = Service.UpdateWarranty(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            var userDto = SeedUser();

            WarrantyDto dto = new WarrantyDto()
            {
                WarrantyId = Guid.NewGuid(),
                TeamId = Guid.NewGuid(),
                UserId = userDto.UserId,
                WarrantyName = TEST_WARRANTY_NAME,
                WarrentyDescription = TEST_WARRANT_DESC
            };

            var result = Service.AddWarranty(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidUserIdTest()
        {
            var teamDto = SeedTeam();

            WarrantyDto dto = new WarrantyDto()
            {
                WarrantyId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = Guid.NewGuid(),
                WarrantyName = TEST_WARRANTY_NAME,
                WarrentyDescription = TEST_WARRANT_DESC
            };

            var result = Service.AddWarranty(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
        }

        [TestMethod]
        public void InvalidWarrantyNameTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            WarrantyDto dto = new WarrantyDto()
            {
                WarrantyId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                WarrantyName = string.Empty,
                WarrentyDescription = TEST_WARRANT_DESC
            };

            var result = Service.AddWarranty(dto);
            Assert.IsFalse(result.IsSuccess);

            dto.WarrantyName = "audyfgadfiagdiagsdighuasidguhasidghaisdhgaslkhdglkajshdglkjahdgkjasdghakjhdglkasjhdgklajhdgkjhadkghjaldghjaldghaldghaldjghaksdghlkadg";
            result = Service.AddWarranty(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededUser(userDto.UserId);
        }
    }
}
