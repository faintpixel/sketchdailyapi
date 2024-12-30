using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using SketchDailyAPI.DAO.Queryables;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models.References.Animals;
using SketchDailyAPI.Models.References.People;
using SketchDailyAPI.Models.References.Structures;
using SketchDailyAPI.Models.References.Vegetation;
using SketchDailyAPI.Models.References;
using Microsoft.Extensions.Options;
using SketchDailyAPI.Models;
using Newtonsoft.Json;

namespace SketchDailyAPI.Controllers.References
{
    /// <summary>
    /// API for working with images
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class ImageController : BaseController
    {
        private FileDAO _fileDAO;
        private Logger _logger;
        private AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public ImageController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IOptions<AppSettings> appSettings, IOptions<SpacesSettings> spacesSettings)
        {
            _fileDAO = new FileDAO(hostingEnvironment.WebRootPath, appSettings.Value, spacesSettings.Value);
            _logger = new Logger("ImageController", appSettings.Value);
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Upload image
        /// </summary>
        /// <returns>Batch id</returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public ImageSaveResults UploadImage([FromForm] string batch)
        {
            var results = new ImageSaveResults();
            Batch deserializedBatch = null;
            try
            {
                var converter = new StringEnumConverter();
                deserializedBatch = JsonConvert.DeserializeObject<Batch>(batch, converter);
                deserializedBatch.User = GetCurrentUser().Email;
                var files = Request.Form.Files;
                var images = _fileDAO.Upload(files, ref deserializedBatch, GetCurrentUser());
                results.Images = images;
                results.BatchId = deserializedBatch.Id;
                results.Success = true;
            }
            catch (Exception ex)
            {
                if (batch != null)
                    results.BatchId = deserializedBatch.Id;
                results.Success = false;
                _logger.Log("UploadImage", ex, batch);
            }
            return results;
        }        

        /// <summary>
        /// Report image
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/Report")]
        public bool ReportImage(string id, [FromBody] Report report)
        {
            report.ImageId = id;
            report.User = GetCurrentUser();
            report.Date = DateTime.Now;
            _logger.Log("Image Report", $"User has reported image", LogType.Report, report);
            return true;
        }

    }
}
