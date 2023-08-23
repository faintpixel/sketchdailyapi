using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models;
using SketchDailyAPI.Models.References;

namespace SketchDailyAPI.Controllers.References
{
    /// <summary>
    /// API for working with translations
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class TranslationsController : BaseController
    {
        private readonly AppSettings _appSettings;

        public TranslationsController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Submit a translation
        /// </summary>
        [HttpPost]
        [Route("")]
        public bool Save([FromBody] Translation translation)
        {
            var logger = new Logger("Translations", _appSettings);
            logger.Log("Translation Submission", $"A new translation has been submitted for language '{translation.Language}' by author '{translation.Author}'", LogType.Translation, translation.Comments, translation.TranslationFile);
            return true;
        }
    }
}
