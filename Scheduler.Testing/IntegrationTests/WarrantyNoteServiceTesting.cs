using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scheduler.BL.Schedule.Dto;
using Scheduler.BL.Schedule.Implementation;
using Scheduler.BL.Schedule.Interface;

namespace Scheduler.Testing.IntegrationTests
{
    [TestClass]
    public class WarrantyNoteServiceTesting : IntegrationBase
    {
        private IWarrantyNoteService Service;

        public WarrantyNoteServiceTesting()
        {
            Data.ScheduleContext scheduleContext = new Data.ScheduleContext();
            scheduleContext.Migrate();

            Service = new WarrantyNoteService();
        }


        private const string TEST_NOTE = "Warranty Test note";


        [TestMethod]
        public void AddRemoveWarrantyNoteTest()
        {
            var warrantyDto = SeedWarranty();

            WarrantyNoteDto dto = new WarrantyNoteDto()
            {
                WarrantyNoteId = Guid.NewGuid(),
                WarrantyId = warrantyDto.WarrantyId,
                Note = TEST_NOTE,
                StartDate = Convert.ToDateTime("1/1/2000"),
                EndDate = Convert.ToDateTime("2/1/2000")
            };

            var addResult = Service.AddWarrantyNote(dto);
            Assert.IsTrue(addResult.IsSuccess);

            var note = Service.GetWarrantyNote(dto.WarrantyNoteId);
            Assert.IsNotNull(note);
            Assert.AreEqual(dto.WarrantyId, note.WarrantyId);
            Assert.AreEqual(dto.Note, note.Note);
            Assert.AreEqual(dto.StartDate, note.StartDate);
            Assert.AreEqual(dto.EndDate, note.EndDate);

            dto.EndDate = Convert.ToDateTime("3/1/2000");
            var updateResult = Service.UpdateWarrantyNote(dto);
            Assert.IsTrue(updateResult.IsSuccess);

            note = Service.GetWarrantyNote(dto.WarrantyNoteId);
            Assert.IsNotNull(note);
            Assert.AreEqual(dto.EndDate, note.EndDate);

            var deleteResult = Service.DeleteWarrantyNote(dto.WarrantyNoteId);
            Assert.IsTrue(deleteResult.IsSuccess);

            DeleteSeededWarranty(warrantyDto.WarrantyId);
        }

    }
}
