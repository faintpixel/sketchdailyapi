﻿using MongoDB.Driver.Linq;
using MongoDB.Driver;
using SketchDailyAPI.Models.References.Animals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.DAO.Queryables
{
    public class AnimalsQueryable : IQueryable<AnimalReference, AnimalClassifications>
    {
        public IMongoQueryable<AnimalReference> GetQueryable(IMongoCollection<AnimalReference> collection, AnimalClassifications classifications, bool? recentImagesOnly)
        {
            var query = collection.AsQueryable();
            if (classifications.Category.HasValue)
                query = query.Where(x => x.Classifications.Category == classifications.Category);
            if (classifications.Species.HasValue)
                query = query.Where(x => x.Classifications.Species == classifications.Species);
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

        private DateTime GetMostRecentImageUploadDate(IMongoCollection<AnimalReference> collection)
        {
            var query = collection.AsQueryable().OrderByDescending(x => x.UploadDate).Take(1);
            var item = query.First();
            return item.UploadDate.Value;
        }
    }
}
