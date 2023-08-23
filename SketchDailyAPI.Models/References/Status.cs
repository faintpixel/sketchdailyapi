using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public enum Status
    {
        Active = 1,
        Deleted = 2,
        Pending = 3,
        Rejected = 4,
        DeleteRequested = 5
    }
}
