using System;
using System.Collections.Generic;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.Team.Interface.Models;

namespace Scheduler.BL.Team.Interface
{
    public interface ILocationService
    {

        ILocation GetLocation(Guid locationId);
        List<ILocation> GetLocations();

        ChangeResult AddLocation(ILocation location);
        ChangeResult AddLocation(List<ILocation> locations);

        ChangeResult UpdateLocation(ILocation location);
        ChangeResult UpdateLocation(List<ILocation> locations);

        ChangeResult DeleteLocation(Guid locationId);

    }
}
