using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class ScheduleNoteServiceTesting : IntegrationBase
    {
        private IScheduleNoteService Service;

        public ScheduleNoteServiceTesting()
        {
            Data.ScheduleContext context = new Data.ScheduleContext();
            context.Migrate();

            Service = new ScheduleNoteService();
        }

        private const string TEST_NOTE = "Test note";
        private const string TEST_NOTE_2 = "Test note 2";

        [TestMethod]
        public void AddRemoveScheduleNoteTest()
        {
            var scheduleDto = SeedSchedule();

            ScheduleNoteDto dto = new ScheduleNoteDto()
            {
                ScheduleNoteId = Guid.NewGuid(),
                ScheduleId = scheduleDto.ScheduleId,
                Note = TEST_NOTE
            };

            var addResult = Service.AddScheduleNote(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var note = Service.GetScheduleNote(dto.ScheduleNoteId);
            Assert.IsNotNull(note);
            Assert.AreEqual(dto.ScheduleNoteId, note.ScheduleNoteId);
            Assert.AreEqual(TEST_NOTE, note.Note);
            Assert.AreEqual(dto.ScheduleId, note.ScheduleId);

            dto.Note = TEST_NOTE_2;
            var updateResult = Service.UpdateScheduleNote(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            note = Service.GetScheduleNote(dto.ScheduleNoteId);
            Assert.IsNotNull(note);
            Assert.AreEqual(TEST_NOTE_2, note.Note);

            var deleteResult = Service.DeleteScheduleNote(dto.ScheduleNoteId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededSchedule(scheduleDto.ScheduleId);
        }

        [TestMethod]
        public void InvalidScheduleNoteIdTest()
        {
            var scheduleDto = SeedSchedule();

            ScheduleNoteDto dto = new ScheduleNoteDto()
            {
                ScheduleNoteId = Guid.Empty,
                ScheduleId = scheduleDto.ScheduleId,
                Note = TEST_NOTE
            };

            var result = Service.UpdateScheduleNote(dto);
            Assert.IsFalse(result.IsSuccess);

            DeleteSeededSchedule(scheduleDto.ScheduleId);
        }

        [TestMethod]
        public void TestInvalidScheduleIdTest()
        {
            ScheduleNoteDto dto = new ScheduleNoteDto()
            {
                ScheduleNoteId = Guid.NewGuid(),
                ScheduleId = Guid.Empty,
                Note = TEST_NOTE
            };

            var result = Service.AddScheduleNote(dto);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void InvalidNoteTest()
        {
            ScheduleNoteDto dto = new ScheduleNoteDto()
            {
                ScheduleNoteId = Guid.NewGuid(),
                ScheduleId = Guid.NewGuid(),
                Note = string.Empty
            };

            var result = Service.AddScheduleNote(dto);
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
