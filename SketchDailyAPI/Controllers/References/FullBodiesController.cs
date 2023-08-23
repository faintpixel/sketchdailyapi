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
    /// API for working with full body references
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class FullBodiesController : BaseController, IReferenceController<FullBodyReference, FullBodyClassifications>
    {
        private ReferenceDAO<FullBodyReference, FullBodyClassifications> _dao;

        /// <summary>
        /// Constructor
        /// </summary>
        public FullBodiesController(IOptions<AppSettings> appSettings)
        {
            var queryable = new FullBodiesQueryable();
            _dao = new ReferenceDAO<FullBodyReference, FullBodyClassifications>(ReferenceType.FullBody, queryable, appSettings.Value);
        }

        /// <summary>
        /// Gets full bodies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("")]
        public async Task<List<FullBodyReference>> Search([FromQuery(Name = "")] FullBodyClassifications criteria, [FromQuery(Name = "")] OffsetLimit offsetLimit)
        {
            if (criteria == null)
                criteria = new FullBodyClassifications();
            if (offsetLimit == null)
                offsetLimit = new OffsetLimit();

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
        public async Task<FullBodyReference> GetNext([FromQuery(Name = "")] FullBodyClassifications criteria, [FromBody] List<string> excludeIds, [FromQuery] bool? recentImagesOnly)
        {
            if (criteria == null)
                criteria = new FullBodyClassifications();
            if (excludeIds == null)
                excludeIds = new List<string>();

            var image = await _dao.Get(criteria, excludeIds, recentImagesOnly);

            return image;
        }

        /// <summary>
        /// Saves full bodies
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<List<FullBodyReference>> Save([FromBody] List<FullBodyReference> images)
        {
            var user = GetCurrentUser();
            var result = await _dao.Save(images, user);
            return images; // TO DO - return something better
        }

        /// <summary>
        /// Gets the number of full body references matching the criteria
        /// </summary>
        /// <param name="classifications"></param>
        /// <param name="recentImagesOnly"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public async Task<int> Count([FromQuery(Name = "")] FullBodyClassifications classifications, [FromQuery] bool? recentImagesOnly)
        {
            if (classifications == null)
                classifications = new FullBodyClassifications();
            return await _dao.Count(classifications, recentImagesOnly);
        }
    }
}
