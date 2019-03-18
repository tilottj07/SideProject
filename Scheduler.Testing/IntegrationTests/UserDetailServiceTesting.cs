using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.User.Dto;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;
using static Scheduler.BL.User.Implementation.UserDetailService;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class UserDetailServiceTesting : IntegrationBase
    {
        private IUserDetailService UserDetailService;
        private IUserService UserService;

        public UserDetailServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            UserDetailService = new UserDetailService();
            UserService = new UserService();

            TestUserDetailId = Guid.NewGuid();
            TestProficiencyLevel = ProficiencyLevelType.Medium;
        }

        private Guid TestUserDetailId { get; set; }
        private const string TEST_CHARACTERISTIC = ".NET";
        private const string TEST_DESCRIPTION = "5 years experience in .net.";
        private const string TEST_DESCRIPTION_2 = "7 years experience in .net.";
        private ProficiencyLevelType TestProficiencyLevel { get; set; }

        [TestMethod]
        public void AddRemoveUserDetailTest()
        {
            var userDto = SeedUser();

            UserDetailDto detailDto = new UserDetailDto()
            {
                UserDetailId = TestUserDetailId,
                UserId = userDto.UserId,
                Characteristic = TEST_CHARACTERISTIC,
                Description = TEST_DESCRIPTION,
                ProficiencyLevel = TestProficiencyLevel,
                LastUpdateUserId = userDto.UserId
            };

            //add user so FK is not violated
            UserService.DeleteUser(userDto.UserName);
            var addUserResult = UserService.AddUser(userDto);
            Assert.IsTrue(addUserResult.IsSuccess);

            var addDetailResult = UserDetailService.AddUserDetail(detailDto);
            Assert.IsTrue(addUserResult.IsSuccess);

            var detail = UserDetailService.GetUserDetail(detailDto.UserDetailId);
            Assert.IsNotNull(detail);
            Assert.AreEqual(TestUserDetailId, detail.UserDetailId);
            Assert.AreEqual(userDto.UserId, detail.UserId);
            Assert.AreEqual(TEST_CHARACTERISTIC, detail.Characteristic);
            Assert.AreEqual(TEST_DESCRIPTION, detail.Description);
            Assert.AreEqual(TestProficiencyLevel, detail.ProficiencyLevel);
            Assert.AreEqual(userDto.UserId, detail.LastUpdateUserId);

            detailDto.Description = TEST_DESCRIPTION_2;
            var updateResult = UserDetailService.UpdateUserDetail(detailDto);
            Assert.IsTrue(updateResult.IsSuccess);

            detail = UserDetailService.GetUserDetail(detailDto.UserDetailId);
            Assert.IsNotNull(detail);
            Assert.AreEqual(TEST_DESCRIPTION_2, detail.Description);

            foreach (var d in UserDetailService.GetUserDetails(detailDto.UserId))
            {
                var deleteDetailResult = UserDetailService.DeleteUserDetail(d.UserDetailId);
                Assert.IsTrue(deleteDetailResult.IsSuccess);
            }

            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void InvalidUserIdTest()
        {
            UserDetailDto detailDto = new UserDetailDto()
            {
                UserDetailId = TestUserDetailId,
                UserId = Guid.NewGuid(),
                Characteristic = TEST_CHARACTERISTIC,
                Description = TEST_DESCRIPTION,
                ProficiencyLevel = TestProficiencyLevel,
                LastUpdateUserId = Guid.NewGuid()
            };

            var result = UserDetailService.AddUserDetail(detailDto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidUserDetailIdTest()
        {
            var userDto = SeedUser();

            UserDetailDto detailDto = new UserDetailDto()
            {
                UserDetailId = Guid.Empty,
                UserId = userDto.UserId,
                Characteristic = TEST_CHARACTERISTIC,
                Description = TEST_DESCRIPTION,
                ProficiencyLevel = TestProficiencyLevel,
                LastUpdateUserId = userDto.UserId
            };

            var result = UserDetailService.UpdateUserDetail(detailDto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededUser(userDto.UserId);
        }

        [TestMethod]
        public void MissingCharacteristicTest()
        {
            var userDto = SeedUser();

            UserDetailDto detailDto = new UserDetailDto()
            {
                UserDetailId = TestUserDetailId,
                UserId = userDto.UserId,
                Description = TEST_DESCRIPTION,
                ProficiencyLevel = TestProficiencyLevel,
                LastUpdateUserId = userDto.UserId
            };

            var result = UserDetailService.AddUserDetail(detailDto);
            Assert.IsFalse(result.IsSuccess);

            detailDto.Characteristic = "a;djf;lajdf;lasjdf;ljksa;ldhgiuweituwieawgfjhagdhsadlghjaslkdgjhlkasjhdglkashjdglkahjgdlkhasdgakjhdflaksjhdflkasf";
            result = UserDetailService.AddUserDetail(detailDto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededUser(userDto.UserId);
        }

    }
}
