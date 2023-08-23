using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.Structures
{
    public class StructureReference : Image
    {
        public StructureClassifications Classifications { get; set; }
    }
}
