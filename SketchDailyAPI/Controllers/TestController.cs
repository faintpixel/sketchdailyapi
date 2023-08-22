using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SketchDailyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController
    {
        private readonly AppSettings _appSettings;

        public TestController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("Config")]
        public string GetConfig()
        {
            return _appSettings.TestSetting;
        }
    }
}
