using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.Vegetation
{
    public class VegetationReference : Image
    {
        public VegetationClassifications Classifications { get; set; }
    }
}
