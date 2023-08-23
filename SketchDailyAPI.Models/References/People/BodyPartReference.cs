using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.People
{
    public class BodyPartReference : Image
    {
        public BodyPartClassifications Classifications { get; set; }
    }
}
