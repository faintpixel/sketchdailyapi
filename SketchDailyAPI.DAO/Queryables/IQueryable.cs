using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.DAO.Queryables
{
    public interface IQueryable<TReference, TClassifications>
    {
        IMongoQueryable<TReference> GetQueryable(IMongoCollection<TReference> collection, TClassifications classifications, bool? recentImagesOnly);
    }
}
