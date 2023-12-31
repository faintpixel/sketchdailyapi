﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SketchDailyAPI.Models.References
{
    public class Image
    {
        [BsonId]
        public string? Id { get; set; }

        public string? File { get; set; }
        public string? Location { get; set; }
        public Contact? Photographer { get; set; }
        public Contact? Model { get; set; }
        public string? TermsOfUse { get; set; }
        public DateTime? UploadDate { get; set; }
        public string? UploadedBy { get; set; }
        public UploadType? Source { get; set; }
        public string? SourceUrl { get; set; }
        public Status? Status { get; set; }
        public string? BatchId { get; set; }

        public Image()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
