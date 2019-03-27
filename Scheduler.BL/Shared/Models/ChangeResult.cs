using System;
using System.Collections.Generic;

namespace Scheduler.BL.Shared.Models
{
    public class ChangeResult
    {
        public ChangeResult()
        {
            ErrorMessages = new List<string>();
            Ids = new List<Guid>();
            IsSuccess = true;
        }

        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<Guid> Ids { get; set; }
    }
}
