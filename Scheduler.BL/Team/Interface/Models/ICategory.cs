using System;
namespace Scheduler.BL.Team.Interface.Models
{
    public interface ICategory
    {
        Guid CategoryId { get;  }
        string CategoryName { get;  }
        string CategoryDescription { get;  }
        string CategoryEmail { get;  }

        DateTime CreateDate { get;  }
        Guid CreateUserId { get;  }

        DateTime LastUpdateDate { get;  }
        Guid LastUpdateUserId { get;  }

        DateTime ChangeDate { get;  }
        DateTime? DeleteDate { get;  }
    }
}
