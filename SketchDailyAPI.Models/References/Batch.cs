using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class Batch
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Source { get; set; }
        public string Permission { get; set; }
        public string Licensed { get; set; }
        public string From { get; set; }
        public string User { get; set; }
        public ReferenceType Type { get; set; }
        public string Comments { get; set; }
    }
}
