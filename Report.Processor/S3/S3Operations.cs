using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Processor.S3
{
    public class S3Operations : IS3Operations
    {
        private static string bucketName = "aws-bucket-man";
        private readonly IAmazonS3 _amazons3;
        public S3Operations(IAmazonS3 amazons3)
        {
            _amazons3 = amazons3;
        }

        [Obsolete]
        public async void PutObject()
        {

            var bucketExists = await _amazons3.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                throw new Exception();

            string path = string.Format(Directory.GetCurrentDirectory() + "\\{0}\\{1}", "Reports", "s3File.txt");

            StreamReader reader = File.OpenText(path);

            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = Guid.NewGuid().ToString(),
                InputStream = reader.BaseStream
            };
            request.Metadata.Add("Content-Type", "txt");
            await _amazons3.PutObjectAsync(request);
        }
    }
}
