﻿using MongoDB.Driver.Linq;
using MongoDB.Driver;
using SketchDailyAPI.Models.References.Vegetation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.DAO.Queryables
{
    public class VegetationQueryable : IQueryable<VegetationReference, VegetationClassifications>
    {
        public IMongoQueryable<VegetationReference> GetQueryable(IMongoCollection<VegetationReference> collection, VegetationClassifications classifications, bool? recentImagesOnly)
        {
            var query = collection.AsQueryable();
            if (classifications.PhotoType.HasValue)
                query = query.Where(x => x.Classifications.PhotoType == classifications.PhotoType);
            if (classifications.VegetationType.HasValue)
                query = query.Where(x => x.Classifications.VegetationType == classifications.VegetationType);
            if (recentImagesOnly == true)
            {
                var mostRecentUpload = GetMostRecentImageUploadDate(collection);
                query = query.Where(x => x.UploadDate >= mostRecentUpload.AddDays(-30));
            }
            if (classifications.Status.HasValue)
                query = query.Where(x => x.Status == classifications.Status);

            return query;
        }

        private DateTime GetMostRecentImageUploadDate(IMongoCollection<VegetationReference> collection)
        {
            var query = collection.AsQueryable().OrderByDescending(x => x.UploadDate).Take(1);
            var item = query.First();
            return item.UploadDate.Value;
        }
    }
}
