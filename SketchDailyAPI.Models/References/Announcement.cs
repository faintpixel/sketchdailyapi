using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class Announcement
    {
        [BsonId]
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
