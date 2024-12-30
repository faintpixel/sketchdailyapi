using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models
{
    public class SpacesSettings
    {
        public string SpaceName { get; set; }
        public string Endpoint { get; set; }
        public string ImagePath { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
