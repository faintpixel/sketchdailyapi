using MongoDB.Bson;
using MongoDB.Driver;
using SketchDailyAPI.Models.References;
using SketchDailyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace SketchDailyAPI.DAO.References
{
    public class BatchDAO
    {
        public MongoClient _mongoClient;
        private IMongoDatabase _db;
        private IMongoCollection<Batch> _collection;
        private readonly AppSettings _appSettings;

        public BatchDAO(AppSettings appSettings)
        {
            _mongoClient = new MongoClient(appSettings.MongoDBConnectionString);
            _db = _mongoClient.GetDatabase("refsite");
            _collection = _db.GetCollection<Batch>("batches");
            _appSettings = appSettings;
        }

        public BatchDAO(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
            _db = _mongoClient.GetDatabase("refsite");
            _collection = _db.GetCollection<Batch>("batches");
        }

        public void Save(Batch batch)
        {
            _collection.InsertOne(batch);
        }

        public async Task DeleteReference(string id, User user)
        {
            var batch = await Get(id);
            if (batch.User != user.Email)
                throw new Exception("Access to delete batch denied");
            _collection.DeleteOne(filter: new BsonDocument("_id", id));
            var type = batch.Type;
            if (type == ReferenceType.Animal)
            {
                var dao = ReferenceDAOFactory.GetAnimalsDAO(_appSettings);
                var images = await dao.Search(new Models.References.Animals.AnimalClassifications { BatchId = id });
                dao.DeleteReferences(images);
            }
            else if (type == ReferenceType.BodyPart)
            {
                var dao = ReferenceDAOFactory.GetBodyPartsDAO(_appSettings);
                var images = await dao.Search(new Models.References.People.BodyPartClassifications { BatchId = id });
                dao.DeleteReferences(images);
            }
            else if (type == ReferenceType.FullBody)
            {
                var dao = ReferenceDAOFactory.GetFullBodiesDAO(_appSettings);
                var images = await dao.Search(new Models.References.People.FullBodyClassifications { BatchId = id });
                dao.DeleteReferences(images);
            }
            else if (type == ReferenceType.Structure)
            {
                var dao = ReferenceDAOFactory.GetStructuresDAO(_appSettings);
                var images = await dao.Search(new Models.References.Structures.StructureClassifications { BatchId = id });
                dao.DeleteReferences(images);
            }
            else if (type == ReferenceType.Vegetation)
            {
                var dao = ReferenceDAOFactory.GetVegetationsDAO(_appSettings);
                var images = await dao.Search(new Models.References.Vegetation.VegetationClassifications { BatchId = id });
                dao.DeleteReferences(images);
            }
        }

        public async Task<List<Batch>> GetUserBatches(string user)
        {
            var query = _collection.AsQueryable();
            query = query.Where(x => x.User == user).OrderByDescending(x => x.CreationDate);

            return await query.ToListAsync();
        }

        public async Task<Batch> Get(string id)
        {
            IMongoQueryable<Batch> query = _collection.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public string GetExistingBatchId(string user, string batchName)
        {
            var query = _collection.AsQueryable();
            var id = query.Where(x => x.User == user && x.Name == batchName).Select(x => x.Id).FirstOrDefault();

            return id;
        }
    }
}
