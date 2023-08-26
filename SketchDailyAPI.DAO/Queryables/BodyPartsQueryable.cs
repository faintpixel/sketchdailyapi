using MongoDB.Driver.Linq;
using MongoDB.Driver;
using SketchDailyAPI.Models.References.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.DAO.Queryables
{
    public class BodyPartsQueryable : IQueryable<BodyPartReference, BodyPartClassifications>
    {
        public IMongoQueryable<BodyPartReference> GetQueryable(IMongoCollection<BodyPartReference> collection, BodyPartClassifications classifications, bool? recentImagesOnly)
        {
            var query = collection.AsQueryable();
            if (classifications.Gender.HasValue)
                query = query.Where(x => x.Classifications.Gender == classifications.Gender);
            if (classifications.BodyPart.HasValue)
                query = query.Where(x => x.Classifications.BodyPart == classifications.BodyPart);
            if (classifications.ViewAngle.HasValue)
                query = query.Where(x => x.Classifications.ViewAngle == classifications.ViewAngle);
            if (recentImagesOnly == true)
            {
                var mostRecentUpload = GetMostRecentImageUploadDate(collection);
                query = query.Where(x => x.UploadDate >= mostRecentUpload.AddDays(-30));
            }
            if (classifications.Status.HasValue)
                query = query.Where(x => x.Status == classifications.Status);

            return query;
        }

        private DateTime GetMostRecentImageUploadDate(IMongoCollection<BodyPartReference> collection)
        {
            var query = collection.AsQueryable().OrderByDescending(x => x.UploadDate).Take(1);
            var item = query.First();
            return item.UploadDate.Value;
        }
    }
}