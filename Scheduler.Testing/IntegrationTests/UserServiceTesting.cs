using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.User.Dto;
using Scheduler.BL.User.Implementation;
using Scheduler.BL.User.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class UserServiceTesting
    {
        private IUserService Service;

        public UserServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            TestUserId = Guid.NewGuid();
            Service = new UserService();
        }

        private Guid TestUserId { get; set; }
        private const string TEST_USERNAME = "testuser143597349573";
        private const string TEST_FIRST_NAME = "Johnny";
        private const string TEST_FIRST_NAME_2 = "Jonny";
        private const string TEST_MIDDLE_INITIAL = "M";
        private const string TEST_LAST_NAME = "Appleseed";
        private const string TEST_EMAIL = "testing@gamil.com";
        private const string TEST_PHONE = "5551235555";


        [TestMethod]
        public void AddRemoveUserTest()
        {
            UserDto dto = new UserDto()
            {
                UserName = TEST_USERNAME,
                UserId = TestUserId,
                FirstName = TEST_FIRST_NAME,
                MiddleInitial = TEST_MIDDLE_INITIAL,
                LastName = TEST_LAST_NAME,
                PrimaryEmail = TEST_EMAIL,
                PrimaryPhoneNumber = TEST_PHONE
            };

            //just in case the cleanup failed from the prior test
            Service.DeleteUser(dto.UserName);

            var addResult = Service.AddUser(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var user = Service.GetUser(TEST_USERNAME);
            Assert.IsNotNull(user);
            Assert.AreEqual(TestUserId, user.UserId);
            Assert.AreEqual(TEST_USERNAME.ToUpper(), user.UserName);
            Assert.AreEqual(TEST_FIRST_NAME, user.FirstName);
            Assert.AreEqual(TEST_MIDDLE_INITIAL, user.MiddleInitial);
            Assert.AreEqual(TEST_LAST_NAME, user.LastName);
            Assert.AreEqual(TEST_EMAIL, user.PrimaryEmail);
            Assert.AreEqual(TEST_PHONE, user.PrimaryPhoneNumber);

            dto.FirstName = TEST_FIRST_NAME_2;
            var updateResult = Service.UpdateUser(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            user = Service.GetUser(dto.UserId);
            Assert.IsNotNull(user);
            Assert.AreEqual(TEST_FIRST_NAME_2, user.FirstName);

            var deleteResult = Service.DeleteUser(dto.UserId);
            Assert.IsTrue(deleteResult.IsSuccess);
        }

        [TestMethod]
        public void DuplicateUserNameTest()
        {
            UserDto dto = new UserDto()
            {
                UserName = TEST_USERNAME,
                UserId = TestUserId,
                FirstName = TEST_FIRST_NAME,
                MiddleInitial = TEST_MIDDLE_INITIAL,
                LastName = TEST_LAST_NAME,
                PrimaryEmail = TEST_EMAIL,
                PrimaryPhoneNumber = TEST_PHONE
            };

            //just in case the cleanup failed from the prior test
            Service.DeleteUser(dto.UserName);

            var addResult = Service.AddUser(dto);
            Assert.IsTrue(addResult.IsSuccess);

            addResult = Service.AddUser(dto);
            Assert.IsFalse(addResult.IsSuccess);

            var deleteResult = Service.DeleteUser(dto.UserId);
            Assert.IsTrue(deleteResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidEmailsTest()
        {
            UserDto dto = new UserDto()
            {
                UserName = TEST_USERNAME,
                UserId = TestUserId,
                FirstName = TEST_FIRST_NAME,
                MiddleInitial = TEST_MIDDLE_INITIAL,
                LastName = TEST_LAST_NAME,
                PrimaryEmail = "woot",
                PrimaryPhoneNumber = TEST_PHONE
            };

            var addResult = Service.AddUser(dto);
            Assert.IsFalse(addResult.IsSuccess);

            dto.PrimaryEmail = TEST_EMAIL;
            dto.BackupEmail = "notreal";
            addResult = Service.AddUser(dto);
            Assert.IsFalse(addResult.IsSuccess);
        }

        [TestMethod]
        public void MissingFirstAndLastNameTest()
        {
            UserDto dto = new UserDto()
            {
                UserName = TEST_USERNAME,
                UserId = TestUserId,
                FirstName = string.Empty,
                MiddleInitial = TEST_MIDDLE_INITIAL,
                LastName = string.Empty,
                PrimaryEmail = TEST_EMAIL,
                PrimaryPhoneNumber = TEST_PHONE
            };

            var result = Service.AddUser(dto);
            Assert.IsFalse(result.IsSuccess);
        }


    }
}
