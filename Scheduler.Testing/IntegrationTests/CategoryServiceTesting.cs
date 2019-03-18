using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Team.Dto;
using Scheduler.BL.Team.Implementation;
using Scheduler.BL.Team.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class CategoryServiceTesting
    {
        private ICategoryService Service;

        public CategoryServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new CategoryService();
            TestCategoryId = Guid.NewGuid();
        }

        private Guid TestCategoryId { get; set; }
        private const string TEST_NAME = "Cat Test Name";
        private const string TEST_DESC = "this is a description";
        private const string TEST_DESC_2 = "this is still a description";
        private const string TEST_EMAIL = "test@gmail.com";

        [TestMethod]
        public void AddRemoveCategoryTest()
        {
            CategoryDto dto = new CategoryDto()
            {
                CategoryId = TestCategoryId,
                CategoryName = TEST_NAME,
                CategoryDescription = TEST_DESC,
                CategoryEmail = TEST_EMAIL
            };

            var addResult = Service.AddCategory(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var cat = Service.GetCategory(TestCategoryId);
            Assert.IsNotNull(cat);
            Assert.AreEqual(TestCategoryId, cat.CategoryId);
            Assert.AreEqual(TEST_NAME, cat.CategoryName);
            Assert.AreEqual(TEST_DESC, cat.CategoryDescription);
            Assert.AreEqual(TEST_EMAIL, cat.CategoryEmail);

            dto.CategoryDescription = TEST_DESC_2;
            var updateResult = Service.UpdateCategory(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            cat = Service.GetCategory(TestCategoryId);
            Assert.AreEqual(TEST_DESC_2, cat.CategoryDescription);

            var deleteResult = Service.DeleteCategory(dto.CategoryId);
            Assert.IsTrue(deleteResult.IsSuccess);
        }

        [TestMethod]
        public void InvalidCategoryNameTest()
        {
            CategoryDto dto = new CategoryDto()
            {
                CategoryId = TestCategoryId,
                CategoryDescription = TEST_DESC,
                CategoryEmail = TEST_EMAIL
            };

            var result = Service.AddCategory(dto);
            Assert.IsFalse(result.IsSuccess);

            dto.CategoryName = "dfgauydgflasdgflasdhglahsgdlgfhjawlejhgfljahsgeljcfhagsdvljhgajdfhgaljdghflasjhgdlahgvdljasghvljashgdvl";

            result = Service.AddCategory(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidCategoryIdTest()
        {
            CategoryDto dto = new CategoryDto()
            {
                CategoryId = Guid.Empty,
                CategoryName = TEST_NAME,
                CategoryDescription = TEST_DESC,
                CategoryEmail = TEST_EMAIL
            };

            var result = Service.UpdateCategory(dto);
            Assert.IsFalse(result.IsSuccess);
        }

    }
}
