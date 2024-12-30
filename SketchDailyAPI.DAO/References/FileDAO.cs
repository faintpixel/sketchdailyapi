using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using SketchDailyAPI.Models;
using SketchDailyAPI.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Transfer;
using SharpCompress.Common;

namespace SketchDailyAPI.DAO.References
{
    public class FileDAO
    {
        private readonly string WEB_ROOT_PATH;
        private readonly AppSettings _appSettings;
        private readonly SpacesSettings _spacesSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webRootPath"></param>
        public FileDAO(string webRootPath, AppSettings appSettings, SpacesSettings spacesSettings)
        {
            WEB_ROOT_PATH = webRootPath;
            _appSettings = appSettings;
            _spacesSettings = spacesSettings;
        }

        public void Delete(string file)
        {
            File.Delete(file);
        }

        public List<Image> Upload(IFormFileCollection files, ref Batch batch, User user)
        {
            var images = new List<Image>();
            var batchId = CreateOrUpdateBatch(batch, user);
            batch.Id = batchId;

            var s3Client = new AmazonS3Client(_spacesSettings.AccessKey, _spacesSettings.SecretKey, new AmazonS3Config
            {
                ServiceURL = _spacesSettings.Endpoint,
                ForcePathStyle = true
            });

            foreach (var file in files)
            {
                var uniqueId = Guid.NewGuid().ToString();
                var timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileName.Length > 50)
                    fileName = fileName.Substring(0, 50);
                fileName = fileName.Replace("_", "");
                var extension = Path.GetExtension(file.FileName);

                var newFileName = $"{batchId}-{fileName}_{timestamp}-{uniqueId}{extension}";
                var s3Key = $"{_spacesSettings.ImagePath}{newFileName}";

                try
                {
                    // Upload file to Spaces
                    using (var stream = file.OpenReadStream())
                    {
                        var putRequest = new PutObjectRequest
                        {
                            BucketName = _spacesSettings.SpaceName,
                            Key = s3Key,
                            InputStream = stream,
                            ContentType = file.ContentType,
                            CannedACL = S3CannedACL.PublicRead,
                            UseChunkEncoding = false,
                        };

                        var response = s3Client.PutObjectAsync(putRequest).Result;
                    }

                    // Create the Image record
                    var image = new Image
                    {
                        File = $"/images/{newFileName}",
                        Model = new Contact(),
                        Photographer = new Contact(),
                        Source = UploadType.USER,
                        Status = Status.Pending,
                        TermsOfUse = "",
                        UploadDate = DateTime.Now,
                        UploadedBy = user.Name,
                        BatchId = batchId,
                        Location = "" // Not used anymore
                    };

                    images.Add(image);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error uploading file {file.FileName}: {ex.Message}", ex);
                }
            }

            return images;
        }

        private string CreateOrUpdateBatch(Batch batch, User user)
        {
            var batchDAO = new BatchDAO(_appSettings);
            batch.Id = batchDAO.GetExistingBatchId(batch.User, batch.Name);
            if (string.IsNullOrEmpty(batch.Id))
            {
                batch.Id = Guid.NewGuid().ToString();
                batch.CreationDate = DateTime.Now;
                batch.User = user.Email;
                batchDAO.Save(batch);
            }

            return batch.Id;
        }
    }
}
