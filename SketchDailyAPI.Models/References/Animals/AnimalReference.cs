using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SketchDailyAPI.Models.References.Animals
{
    public class AnimalReference : Image
    {
        public AnimalClassifications Classifications { get; set; }
    }
}
