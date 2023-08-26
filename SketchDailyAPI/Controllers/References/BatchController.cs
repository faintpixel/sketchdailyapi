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
    /// API for working with batches
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class BatchController : BaseController
    {
        private BatchDAO _batchDAO;
        private Logger _logger;
        private AppSettings _appSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public BatchController(IOptions<AppSettings> appSettings)
        {
            _batchDAO = new BatchDAO(appSettings.Value);
            _logger = new Logger("ImageController", appSettings.Value);
            _appSettings = appSettings.Value;
        }

        

        /// <summary>
        /// Gets batches
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<List<Batch>> GetBatches()
        {
            var user = GetCurrentUser().Email;
            return await _batchDAO.GetUserBatches(user);
        }

        

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> DeleteBatch(string id)
        {
            var user = GetCurrentUser();
            await _batchDAO.DeleteReference(id, user);
            return true;
        }

        /// <summary>
        /// Gets images from batch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{id}/Images")]
        public async Task<BatchImages> GetBatchImages(string id)
        {
            var results = new BatchImages();

            var currentUser = GetCurrentUser();
            results.Batch = await _batchDAO.Get(id);

            if (results.Batch == null)
                return results;

            if (!currentUser.IsAdmin && currentUser.Email != results.Batch.User)
                throw new Exception("Unauthorized");

            if (results.Batch.Type == ReferenceType.Animal)
            {
                var queryable = new AnimalsQueryable();
                var animalsDAO = new ReferenceDAO<AnimalReference, AnimalClassifications>(ReferenceType.Animal, queryable, _appSettings);
                var images = await animalsDAO.Search(new AnimalClassifications() { BatchId = id });
                results.Images = images.ToList<object>();
            }
            else if (results.Batch.Type == ReferenceType.BodyPart)
            {
                var queryable = new BodyPartsQueryable();
                var bodyPartsDAO = new ReferenceDAO<BodyPartReference, BodyPartClassifications>(ReferenceType.BodyPart, queryable, _appSettings);
                var images = await bodyPartsDAO.Search(new BodyPartClassifications { BatchId = id });
                results.Images = images.ToList<object>();
            }
            else if (results.Batch.Type == ReferenceType.FullBody)
            {
                var queryable = new FullBodiesQueryable();
                var fullBodiesDAO = new ReferenceDAO<FullBodyReference, FullBodyClassifications>(ReferenceType.FullBody, queryable, _appSettings);
                var images = await fullBodiesDAO.Search(new FullBodyClassifications { BatchId = id });
                results.Images = images.ToList<object>();
            }
            else if (results.Batch.Type == ReferenceType.Vegetation)
            {
                var queryable = new VegetationQueryable();
                var vegetationDAO = new ReferenceDAO<VegetationReference, VegetationClassifications>(ReferenceType.Vegetation, queryable, _appSettings);
                var images = await vegetationDAO.Search(new VegetationClassifications { BatchId = id });
                results.Images = images.ToList<object>();
            }
            else if (results.Batch.Type == ReferenceType.Structure)
            {
                var queryable = new StructuresQueryable();
                var structuresDAO = new ReferenceDAO<StructureReference, StructureClassifications>(ReferenceType.Structure, queryable, _appSettings);
                var images = await structuresDAO.Search(new StructureClassifications { BatchId = id });
                results.Images = images.ToList<object>();
            }

            return results;
        }
    }
}
