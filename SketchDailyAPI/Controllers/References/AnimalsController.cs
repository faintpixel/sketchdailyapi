using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SketchDailyAPI.DAO.Queryables;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models;
using SketchDailyAPI.Models.References;
using SketchDailyAPI.Models.References.Animals;

namespace SketchDailyAPI.Controllers.References
{
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    public class AnimalsController : BaseController, IReferenceController<AnimalReference, AnimalClassifications>
    {
        private readonly AppSettings _appSettings;
        private ReferenceDAO<AnimalReference, AnimalClassifications> _dao;

        public AnimalsController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            var queryable = new AnimalsQueryable();
            _dao = new ReferenceDAO<AnimalReference, AnimalClassifications>(Models.References.ReferenceType.Animal, queryable, appSettings.Value);
        }

        /// <summary>
        /// Get animals
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("")]
        public async Task<List<AnimalReference>> Search([FromQuery(Name = "")] AnimalClassifications criteria, [FromQuery(Name = "")] OffsetLimit offsetLimit)
        {
            if (criteria == null)
                criteria = new AnimalClassifications();

            var image = await _dao.Search(criteria, offsetLimit.Offset, offsetLimit.Limit);

            return image;
        }

        /// <summary>
        /// Gets the next image for a drawing session
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="excludeIds"></param>
        /// <param name="recentImagesOnly"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Next")]
        public async Task<AnimalReference> GetNext([FromQuery(Name = "")] AnimalClassifications criteria, [FromBody] List<string> excludeIds, [FromQuery] bool? recentImagesOnly = null)
        {
            if (criteria == null)
                criteria = new AnimalClassifications();
            if (excludeIds == null)
                excludeIds = new List<string>();

            var image = await _dao.Get(criteria, excludeIds, recentImagesOnly);

            return image;
        }

        /// <summary>
        /// Save animals
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<List<AnimalReference>> Save([FromBody] List<AnimalReference> images)
        {
            var user = GetCurrentUser();
            var results = await _dao.Save(images, user);
            return images; // TO DO - return something better
        }

        /// <summary>
        /// Gets the number of animal references matching the criteria
        /// </summary>
        /// <param name="classifications"></param>
        /// <param name="recentImagesOnly"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public async Task<int> Count([FromQuery(Name = "")] AnimalClassifications classifications, [FromQuery] bool? recentImagesOnly)
        {
            if (classifications == null)
                classifications = new AnimalClassifications();
            return await _dao.Count(classifications, recentImagesOnly);
        }
    }
}
