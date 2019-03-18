using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class TeamCategoryServiceTesting : IntegrationBase
    {
        private ITeamCategoryService Service;

        public TeamCategoryServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new TeamCategoryService();

        }


        [TestMethod]
        public void AddRemoveTeamCategoryTest()
        {
            var teamDto = SeedTeam();
            var categoryDto = SeedCategory();
            var category2Dto = SeedCategory();

            TeamCategoryDto dto = new TeamCategoryDto()
            {
                CategoryId = categoryDto.CategoryId,
                TeamId = teamDto.TeamId
            };

            var addResult = Service.AddTeamCategory(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var tc = Service.GetTeamCategory(teamDto.TeamId, categoryDto.CategoryId);
            Assert.IsNotNull(tc);
            Assert.AreEqual(teamDto.TeamId, tc.TeamId);
            Assert.AreEqual(categoryDto.CategoryId, tc.CategoryId);

            dto.CategoryId = category2Dto.CategoryId;
            dto.TeamCategoryId = tc.TeamCategoryId;
            var updateResult = Service.UpdateTeamCategory(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            tc = Service.GetTeamCategory(teamDto.TeamId, category2Dto.CategoryId);
            Assert.IsNotNull(tc);
            Assert.AreEqual(category2Dto.CategoryId, tc.CategoryId);

            var deleteResult = Service.DeleteTeamCategory(teamDto.TeamId, category2Dto.CategoryId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededCategory(categoryDto.CategoryId);
            DeleteSeededCategory(category2Dto.CategoryId);
        }

        [TestMethod]
        public void InvalidTestCategoryTest()
        {
            var teamDto = SeedTeam();
            var categoryDto = SeedCategory();

            TeamCategoryDto dto = new TeamCategoryDto()
            {
                TeamCategoryId = Guid.Empty,
                TeamId = teamDto.TeamId,
                CategoryId = categoryDto.CategoryId
            };

            var result = Service.UpdateTeamCategory(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededCategory(categoryDto.CategoryId);
        }

        [TestMethod]
        public void InvalidTeamIdTest()
        {
            var categoryDto = SeedCategory();
            TeamCategoryDto dto = new TeamCategoryDto()
            {
                TeamId = Guid.NewGuid(),
                CategoryId = categoryDto.CategoryId
            };

            var result = Service.AddTeamCategory(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededCategory(categoryDto.CategoryId);
        }

        [TestMethod]
        public void InvalidCategoryIdTest()
        {
            var teamDto = SeedTeam();
            TeamCategoryDto dto = new TeamCategoryDto()
            {
                TeamId = teamDto.TeamId,
                CategoryId = Guid.NewGuid()
            };

            var result = Service.AddTeamCategory(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
        }
    }
}
