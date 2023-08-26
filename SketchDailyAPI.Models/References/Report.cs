using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class Report
    {
        public string? ImageId { get; set; }
        public string? Comment { get; set; }
        public User? User { get; set; }
        public ReportType? ReportType { get; set; }
        public DateTime? Date { get; set; }
        public string? ReferenceType { get; set; }
    }
}
