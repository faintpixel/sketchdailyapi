using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SketchDailyAPI.DAO.Queryables;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models.References.People;
using SketchDailyAPI.Models.References;
using Microsoft.Extensions.Options;
using SketchDailyAPI.Models;

namespace SketchDailyAPI.Controllers.References
{
    /// <summary>
    /// API for working with body part references
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class BodyPartsController : BaseController, IReferenceController<BodyPartReference, BodyPartClassifications>
    {
        private ReferenceDAO<BodyPartReference, BodyPartClassifications> _dao;

        /// <summary>
        /// Constructor
        /// </summary>
        public BodyPartsController(IOptions<AppSettings> appSettings)
        {
            var queryable = new BodyPartsQueryable();
            _dao = new ReferenceDAO<BodyPartReference, BodyPartClassifications>(ReferenceType.BodyPart, queryable, appSettings.Value);
        }

        /// <summary>
        /// Get body parts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("")]
        public async Task<List<BodyPartReference>> Search([FromQuery(Name = "")] BodyPartClassifications criteria, [FromQuery(Name = "")] OffsetLimit offsetLimit)
        {
            if (criteria == null)
                criteria = new BodyPartClassifications();
            return await _dao.Search(criteria, offsetLimit.Offset, offsetLimit.Limit);
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
        public async Task<BodyPartReference> GetNext([FromQuery(Name = "")] BodyPartClassifications criteria, [FromBody] List<string> excludeIds, [FromQuery] bool? recentImagesOnly)
        {
            if (criteria == null)
                criteria = new BodyPartClassifications();
            if (excludeIds == null)
                excludeIds = new List<string>();

            var image = await _dao.Get(criteria, excludeIds, recentImagesOnly);

            return image;
        }

        /// <summary>
        /// Save body parts
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<List<BodyPartReference>> Save([FromBody] List<BodyPartReference> images)
        {
            var user = GetCurrentUser();
            var results = await _dao.Save(images, user);
            return images; // TO DO - return something better
        }

        /// <summary>
        /// Gets the number of body part references matching the criteria
        /// </summary>
        /// <param name="classifications"></param>
        /// <param name="recentImagesOnly"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public async Task<int> Count([FromQuery(Name = "")] BodyPartClassifications classifications, [FromQuery] bool? recentImagesOnly)
        {
            if (classifications == null)
                classifications = new BodyPartClassifications();
            return await _dao.Count(classifications, recentImagesOnly);
        }
    }
}
