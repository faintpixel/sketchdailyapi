using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.Vegetation
{
    public class VegetationClassifications : BaseClassifications
    {
        public VegetationType? VegetationType { get; set; }
        public PhotoType? PhotoType { get; set; }
    }
}
