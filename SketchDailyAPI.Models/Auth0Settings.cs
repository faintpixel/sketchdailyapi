using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models
{
    public class Auth0Settings
    {
        public string domain { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string apiClientId { get; set; }
        public string apiClientSecret { get; set; }
    }
}
