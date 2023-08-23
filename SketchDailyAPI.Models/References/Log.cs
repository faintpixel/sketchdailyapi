using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class Log
    {
        [BsonId]
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Parameters { get; set; }
        public string Exception { get; set; }
        public LogType Type { get; set; }

        public Log()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
