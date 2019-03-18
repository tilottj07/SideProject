using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class TeamUserServiceTesting : IntegrationBase
    {
        ITeamUserService Service;

        public TeamUserServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new TeamUserService();
        }

        [TestMethod]
        public void AddRemoveTeamUserTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();
            var user2Dto = SeedUser();

            TeamUserDto dto = new TeamUserDto()
            {
                TeamUserId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId
            };

            var addResult = Service.AddTeamUser(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var tu = Service.GetTeamUser(dto.TeamUserId);
            Assert.IsNotNull(tu);
            Assert.AreEqual(dto.TeamUserId, tu.TeamUserId);
            Assert.AreEqual(teamDto.TeamId, tu.TeamId);
            Assert.AreEqual(userDto.UserId, tu.UserId);

            dto.UserId = user2Dto.UserId;
            var updateResult = Service.UpdateTeamUser(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            tu = Service.GetTeamUser(dto.TeamUserId);
            Assert.IsNotNull(tu);
            Assert.AreEqual(user2Dto.UserId, tu.UserId);

            var deleteResult = Service.DeleteTeamUser(tu.TeamUserId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededUser(userDto.UserId);
            DeleteSeededUser(user2Dto.UserId);
        }

        [TestMethod]
        public void InvalidTeamUserIdTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            TeamUserDto dto = new TeamUserDto()
            {
                TeamUserId = Guid.Empty,
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId
            };

            var result = Service.UpdateTeamUser(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededUser(userDto.UserId);
            DeleteSeededTeam(teamDto.TeamId);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            var userDto = SeedUser();

            TeamUserDto dto = new TeamUserDto()
            {
                TeamUserId = Guid.NewGuid(),
                TeamId = Guid.NewGuid(),
                UserId = userDto.UserId
            };

            var result = Service.AddTeamUser(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidUserIdTest()
        {
            var teamDto = SeedTeam();
            TeamUserDto dto = new TeamUserDto()
            {
                TeamUserId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = Guid.NewGuid()
            };

            var result = Service.AddTeamUser(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
        }
    }
}
