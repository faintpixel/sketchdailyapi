using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class OffsetLimit
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        public OffsetLimit()
        {
            Offset = 0;
            Limit = int.MaxValue;
        }
    }
}
