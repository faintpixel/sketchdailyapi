using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.People
{
    public class BodyPartClassifications : BaseClassifications
    {
        public BodyPart? BodyPart { get; set; }
        public ViewAngle? ViewAngle { get; set; }
        public Gender? Gender { get; set; }
    }
}
