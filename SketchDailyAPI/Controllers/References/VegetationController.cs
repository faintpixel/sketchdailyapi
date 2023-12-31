﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SketchDailyAPI.DAO.Queryables;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models.References.Vegetation;
using SketchDailyAPI.Models.References;
using Microsoft.Extensions.Options;
using SketchDailyAPI.Models;

namespace SketchDailyAPI.Controllers.References
{
    /// <summary>
    /// API for working with vegetation references
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class VegetationController : BaseController, IReferenceController<VegetationReference, VegetationClassifications>
    {
        private ReferenceDAO<VegetationReference, VegetationClassifications> _dao;

        /// <summary>
        /// Constructor
        /// </summary>
        public VegetationController(IOptions<AppSettings> appSettings)
        {
            var queryable = new VegetationQueryable();
            _dao = new ReferenceDAO<VegetationReference, VegetationClassifications>(ReferenceType.Vegetation, queryable, appSettings.Value);
        }

        /// <summary>
        /// Get vegetation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("")]
        public async Task<List<VegetationReference>> Search([FromQuery(Name = "")] VegetationClassifications criteria, [FromQuery(Name = "")] OffsetLimit offsetLimit)
        {
            if (criteria == null)
                criteria = new VegetationClassifications();

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
        public async Task<VegetationReference> GetNext([FromQuery(Name = "")] VegetationClassifications criteria, [FromBody] List<string> excludeIds, [FromQuery] bool? recentImagesOnly = null)
        {
            if (criteria == null)
                criteria = new VegetationClassifications();
            if (excludeIds == null)
                excludeIds = new List<string>();

            var image = await _dao.Get(criteria, excludeIds, recentImagesOnly);

            return image;
        }

        /// <summary>
        /// Save vegetation
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<List<VegetationReference>> Save([FromBody] List<VegetationReference> images)
        {
            var user = GetCurrentUser();

            var results = await _dao.Save(images, user);
            return images; // TO DO - return something better
        }

        /// <summary>
        /// Gets the number of vegetation references matching the criteria
        /// </summary>
        /// <param name="classifications"></param>
        /// <param name="recentImagesOnly"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public async Task<int> Count([FromQuery(Name = "")] VegetationClassifications classifications, [FromQuery] bool? recentImagesOnly)
        {
            if (classifications == null)
                classifications = new VegetationClassifications();
            return await _dao.Count(classifications, recentImagesOnly);
        }
    }
}
