using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class TeamServiceTesting : IntegrationBase
    {
        private ITeamService TeamService;

        public TeamServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            TeamService = new TeamService();

            TestTeamId = Guid.NewGuid();
        }

        private Guid TestTeamId { get; set; }
        private const string TEST_TEAM_NAME = "Testing Team";
        private const string TEST_TEAM_DESC = "Team desc";
        private const string TEST_TEAM_DESC_2 = "Team desc 2";
        private const string TEST_TEAM_EMAIL = "testing@gamil.com";

        [TestMethod]
        public void AddRemoveTeamTest()
        {
            var userDto = SeedUser();
            var locationDto = SeedLocation();

            TeamDto teamDto = new TeamDto()
            {
                TeamId = TestTeamId,
                TeamName = TEST_TEAM_NAME,
                TeamDescription = TEST_TEAM_DESC,
                TeamEmail = TEST_TEAM_EMAIL,
                TeamLeaderId = userDto.UserId,
                CreateUserId = userDto.UserId,
                LastUpdateUserId = userDto.UserId,
                LocationId = locationDto.LocationId
            };

            var addResult = TeamService.AddTeam(teamDto);
            Assert.IsTrue(addResult.IsSuccess);

            var team = TeamService.GetTeam(teamDto.TeamId);
            Assert.IsNotNull(team);
            Assert.AreEqual(TestTeamId, team.TeamId);
            Assert.AreEqual(TEST_TEAM_NAME, team.TeamName);
            Assert.AreEqual(TEST_TEAM_DESC, team.TeamDescription);
            Assert.AreEqual(TEST_TEAM_EMAIL, team.TeamEmail);
            Assert.AreEqual(userDto.UserId, team.TeamLeaderId);
            Assert.AreEqual(userDto.UserId, team.CreateUserId);
            Assert.AreEqual(userDto.UserId, team.LastUpdateUserId);

            teamDto.TeamDescription = TEST_TEAM_DESC_2;
            var updateResult = TeamService.UpdateTeam(teamDto);
            Assert.IsTrue(updateResult.IsSuccess);

            team = TeamService.GetTeam(TestTeamId);
            Assert.IsNotNull(team);
            Assert.AreEqual(TEST_TEAM_DESC_2, team.TeamDescription);


            DeleteSeededLocation(locationDto.LocationId);
            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            TeamDto teamDto = new TeamDto()
            {
                TeamName = TEST_TEAM_NAME,
                TeamDescription = TEST_TEAM_DESC
            };

            var result = TeamService.UpdateTeam(teamDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamNameTest()
        {
            TeamDto teamDto = new TeamDto()
            {
                TeamId = TestTeamId,
                TeamName = string.Empty,
                TeamDescription = TEST_TEAM_DESC
            };

            var result = TeamService.AddTeam(teamDto);
            Assert.IsFalse(result.IsSuccess);

            teamDto.TeamName = "uyagefuaygbfuayegabgjhbvjhbajhgfjhewgfghsjadhgfljawhegfjhagweljfhgawlehglawehgfljaghefljkhgalfgsjldfhgsaljdfhgalshjdfgds";
            result = TeamService.AddTeam(teamDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamLeaderIdTest()
        {
            TeamDto teamDto = new TeamDto()
            {
                TeamId = TestTeamId,
                TeamName = TEST_TEAM_NAME,
                TeamDescription = TEST_TEAM_DESC,
                TeamLeaderId = Guid.NewGuid()
            };

            var result = TeamService.AddTeam(teamDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidTeamEmailTest()
        {
            TeamDto teamDto = new TeamDto()
            {
                TeamId = TestTeamId,
                TeamName = TEST_TEAM_NAME,
                TeamDescription = TEST_TEAM_DESC,
                TeamEmail = "jibberish.test"
            };

            var result = TeamService.AddTeam(teamDto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
