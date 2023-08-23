using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class ImageSaveResults
    {
        public List<Image> Images { get; set; }
        public string BatchId { get; set; }
        public bool Success { get; set; }
    }
}
