using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References.People
{
    public class FullBodyClassifications : BaseClassifications
    {
        [JsonConverter(typeof(StringToNullableBoolConverter))]
        public bool? NSFW { get; set; }

        [JsonConverter(typeof(StringToNullableBoolConverter))]
        public bool? Clothing { get; set; }

        public Gender? Gender { get; set; }
        public PoseType? PoseType { get; set; }
        public ViewAngle? ViewAngle { get; set; }
    }
}
