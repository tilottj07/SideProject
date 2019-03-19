using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class ScheduleServiceTesting : IntegrationBase
    {
        private IScheduleService Service;

        public ScheduleServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new ScheduleService();
        }

        [TestMethod]
        public void AddScheduleWithNoConflictsTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            DateTime startDate = Convert.ToDateTime("1/1/2000");
            DateTime endDate = startDate.AddDays(7).AddHours(1);

            ScheduleDto dto = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                StartDate = startDate,
                EndDate = endDate
            };

            var saveResult = Service.SaveSchedule(dto);
            Assert.IsTrue(saveResult.IsSuccess);

            var items = Service.GetSchedulesByTeamId(teamDto.TeamId, startDate.AddDays(1), endDate.AddDays(1));
            foreach (var item in items)
            {
                if (item.ScheduleId == dto.ScheduleId)
                {
                    Assert.AreEqual(dto.ScheduleId, item.ScheduleId);
                    Assert.AreEqual(dto.TeamId, item.TeamId);
                    Assert.AreEqual(dto.UserId, item.UserId);
                    Assert.AreEqual(dto.StartDate, item.StartDate);
                    Assert.AreEqual(dto.EndDate, item.EndDate);
                }
            }
            Assert.IsTrue(items.Count > 0);


            var deleteResult = Service.DeleteSchedule(dto.ScheduleId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void AddScheduleWithStartDateOverlapConflictTest()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            DateTime startDate1 = Convert.ToDateTime("2/1/2000");
            DateTime endDate1 = startDate1.AddDays(7);

            DateTime startDate2 = startDate1.AddDays(1);
            DateTime endDate2 = endDate1.AddDays(1);

            ScheduleDto dto1 = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                StartDate = startDate1,
                EndDate = endDate1
            };

            ScheduleDto dto2 = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                StartDate = startDate2,
                EndDate = endDate2
            };

            var saveResult = Service.SaveSchedule(dto1);
            Assert.IsTrue(saveResult.IsSuccess);

            saveResult = Service.SaveSchedule(dto2);
            Assert.IsTrue(saveResult.IsSuccess);

            bool hasDto1 = false;
            bool hasDto2 = false;

            var items = Service.GetSchedulesByTeamId(teamDto.TeamId, startDate1.AddDays(1), endDate2);
            foreach (var item in items)
            {
                if (item.ScheduleId == dto1.ScheduleId)
                {
                    hasDto1 = true;
                    Assert.AreEqual(dto1.StartDate, item.StartDate);
                    Assert.AreEqual(startDate2, item.EndDate);
                }
                else if (item.ScheduleId == dto2.ScheduleId)
                {
                    hasDto2 = true;
                    Assert.AreEqual(dto2.StartDate, item.StartDate);
                    Assert.AreEqual(dto2.EndDate, item.EndDate);
                }
            }

            Assert.IsTrue(hasDto1);
            Assert.IsTrue(hasDto2);

            var deleteResult = Service.DeleteSchedule(dto1.ScheduleId);
            Assert.IsTrue(deleteResult.IsSuccess);

            deleteResult = Service.DeleteSchedule(dto2.ScheduleId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededUser(userDto.UserId);
            DeleteSeededTeam(teamDto.TeamId);
        }


        [TestMethod]
        public void AddScheduleThatStartAfterEndsBeforeTesting()
        {
            var teamDto = SeedTeam();
            var userDto = SeedUser();

            DateTime startDate1 = Convert.ToDateTime("3/1/2000");
            DateTime endDate1 = startDate1.AddDays(7);

            DateTime startDate2 = startDate1.AddDays(1);
            DateTime endDate2 = endDate1.AddDays(-1);

            ScheduleDto dto1 = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                StartDate = startDate1,
                EndDate = endDate1
            };

            ScheduleDto dto2 = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = userDto.UserId,
                StartDate = startDate2,
                EndDate = endDate2
            };

            var saveResult = Service.SaveSchedule(dto1);
            Assert.IsTrue(saveResult.IsSuccess);

            saveResult = Service.SaveSchedule(dto2);
            Assert.IsTrue(saveResult.IsSuccess);

            bool hasDto1 = false;
            bool hasDto2 = false;
            bool hasDto3 = false;
            Guid dto3pk = Guid.Empty;

            var items = Service.GetSchedulesByTeamId(teamDto.TeamId, startDate1.AddDays(1), endDate2);
            foreach (var item in items)
            {
                if (item.ScheduleId == dto1.ScheduleId)
                {
                    hasDto1 = true;
                    Assert.AreEqual(dto1.StartDate, item.StartDate);
                    Assert.AreEqual(startDate2, item.EndDate);
                }
                else if (item.ScheduleId == dto2.ScheduleId)
                {
                    hasDto2 = true;
                    Assert.AreEqual(dto2.StartDate, item.StartDate);
                    Assert.AreEqual(dto2.EndDate, item.EndDate);
                }
                else if (item.StartDate == dto2.EndDate)
                {
                    hasDto3 = true;
                    dto3pk = item.ScheduleId;

                    Assert.AreEqual(item.StartDate, dto2.EndDate);
                    Assert.AreEqual(item.EndDate, dto1.EndDate);
                }
            }

            Assert.IsTrue(hasDto1);
            Assert.IsTrue(hasDto2);
            Assert.IsTrue(hasDto3);

            var deleteResult = Service.DeleteSchedule(dto1.ScheduleId);
            Assert.IsTrue(deleteResult.IsSuccess);

            deleteResult = Service.DeleteSchedule(dto2.ScheduleId);
            Assert.IsTrue(deleteResult.IsSuccess);

            deleteResult = Service.DeleteSchedule(dto3pk);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededUser(userDto.UserId);
            DeleteSeededTeam(teamDto.TeamId);
        }


        [TestMethod]
        public void InvalidTeamIdTest()
        {
            var userDto = SeedUser();

            DateTime startDate = Convert.ToDateTime("1/1/2000");
            DateTime endDate = startDate.AddDays(7).AddHours(1);

            ScheduleDto dto = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = Guid.NewGuid(),
                UserId = userDto.UserId,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = Service.SaveSchedule(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidUserIdTest()
        {
            var teamDto = SeedTeam();

            DateTime startDate = Convert.ToDateTime("1/1/2000");
            DateTime endDate = startDate.AddDays(7).AddHours(1);

            ScheduleDto dto = new ScheduleDto()
            {
                ScheduleId = Guid.NewGuid(),
                TeamId = teamDto.TeamId,
                UserId = Guid.NewGuid(),
                StartDate = startDate,
                EndDate = endDate
            };

            var result = Service.SaveSchedule(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededTeam(teamDto.TeamId);
        }
    }
}
