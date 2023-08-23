using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public enum ReportType
    {
        Inappropriate = 1,
        WrongClassifications = 2,
        CopyrightViolation = 3,
        LowQuality = 4,
        Other = 5
    }
}
