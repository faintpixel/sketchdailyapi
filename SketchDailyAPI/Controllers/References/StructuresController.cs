﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SketchDailyAPI.DAO.Queryables;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models.References.Structures;
using SketchDailyAPI.Models.References;
using Microsoft.Extensions.Options;
using SketchDailyAPI.Models;

namespace SketchDailyAPI.Controllers.References
{
    /// <summary>
    /// API for working with structure references
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class StructuresController : BaseController, IReferenceController<StructureReference, StructureClassifications>
    {
        private ReferenceDAO<StructureReference, StructureClassifications> _dao;

        /// <summary>
        /// Constructor
        /// </summary>
        public StructuresController(IOptions<AppSettings> appSettings)
        {
            var queryable = new StructuresQueryable();
            _dao = new ReferenceDAO<StructureReference, StructureClassifications>(ReferenceType.Structure, queryable, appSettings.Value);
        }

        /// <summary>
        /// Get Structures
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("")]
        public async Task<List<StructureReference>> Search([FromQuery(Name = "")] StructureClassifications criteria, [FromQuery(Name = "")] OffsetLimit offsetLimit)
        {
            if (criteria == null)
                criteria = new StructureClassifications();

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
        public async Task<StructureReference> GetNext([FromQuery(Name = "")] StructureClassifications criteria, [FromBody] List<string> excludeIds, [FromQuery] bool? recentImagesOnly = null)
        {
            if (criteria == null)
                criteria = new StructureClassifications();
            if (excludeIds == null)
                excludeIds = new List<string>();

            var image = await _dao.Get(criteria, excludeIds, recentImagesOnly);

            return image;
        }

        /// <summary>
        /// Save structures
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<List<StructureReference>> Save([FromBody] List<StructureReference> images)
        {
            var user = GetCurrentUser();
            var results = await _dao.Save(images, user);
            return images; // TO DO - return something better
        }

        /// <summary>
        /// Gets the number of structure references matching the criteria
        /// </summary>
        /// <param name="classifications"></param>
        /// <param name="recentImagesOnly"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public async Task<int> Count([FromQuery(Name = "")] StructureClassifications classifications, [FromQuery] bool? recentImagesOnly)
        {
            if (classifications == null)
                classifications = new StructureClassifications();
            return await _dao.Count(classifications, recentImagesOnly);
        }
    }
}
