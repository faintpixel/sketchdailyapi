using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class News
    {
        [BsonId]
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}
