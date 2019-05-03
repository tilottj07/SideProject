using System;
using System.Collections.Generic;
using Scheduler.BL.Schedule.Interface.Models;
using Scheduler.BL.Shared.Models;

namespace Scheduler.BL.Schedule.Interface
{
    public interface IWarrantyService
    {

        IWarranty GetWarranty(Guid warrantyId);
        List<IWarranty> GetWarrantiesByTeamId(Guid teamId, DateTime startDate, DateTime endDate);
        List<IWarranty> GetWarrantiesByUserId(Guid userId, DateTime startDate, DateTime endDate);
        List<IWarranty> GetWarranties(DateTime startDate, DateTime endDate);

        IWarrantyDisplay GetWarrantyDisplay(Guid warrantyId);
        List<IWarrantyDisplay> GetWarranyDisplaysByTeamId(Guid teamId, DateTime startDate, DateTime endDate);
        List<IWarrantyDisplay> GetWarranyDisplaysByUserId(Guid userId, DateTime startDate, DateTime endDate);
        List<IWarrantyDisplay> GetWarrantyDisplays(DateTime startDate, DateTime endDate);

        ChangeResult AddWarranty(IWarranty warranty);
        ChangeResult AddWarranty(List<IWarranty> warranties);

        ChangeResult UpdateWarranty(IWarranty warranty);
        ChangeResult UpdateWarranty(List<IWarranty> warranties);

        ChangeResult DeleteWarranty(Guid warrantyId);

    }
}
