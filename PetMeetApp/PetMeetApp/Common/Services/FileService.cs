using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NLog;
using PetMeetApp.Common.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PetMeetApp.Common.Services
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment _environment;
        public IConfiguration _configuration;
        private string _AWSAccessKeyId;
        private string _AWSSecretKey;
        private string _AWSBucketName;
        private Logger _logger;

        public FileService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
            _AWSAccessKeyId = _configuration.GetSection("AWSCredentials:AWSAccessKeyId").Value;
            _AWSSecretKey = _configuration.GetSection("AWSCredentials:AWSSecretKey").Value;
            _AWSBucketName = _configuration.GetSection("AWSCredentials:AWSBucketName").Value;
        }

        public bool SaveFile(string folder, string fileName, string data)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(data);
                string wwwPath = _environment.WebRootPath;

                var path = Path.Combine(wwwPath, folder, fileName);

                File.WriteAllBytes(path, bytes);

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }

        }

        public string SaveFileInAWSBucket(string folder, IFormFile file)
        {
            string key = string.Format("{0}/{1}", folder, AddTimestamp(file.FileName));

            using (var client = new AmazonS3Client(_AWSAccessKeyId, _AWSSecretKey, RegionEndpoint.EUCentral1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = key,
                        BucketName = _AWSBucketName,
                        CannedACL = S3CannedACL.PublicRead,
                        AutoCloseStream = true
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    fileTransferUtility.Upload(uploadRequest);


                }
            }
            return key;
        }
        public string GetAWSFileURL(string contentURL)
        {
            using (var client = new AmazonS3Client(_AWSAccessKeyId, _AWSSecretKey, RegionEndpoint.EUCentral1))
            {
                var res = client.GetPreSignedURL(new GetPreSignedUrlRequest
                {
                    BucketName = _AWSBucketName,
                    Key = contentURL,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                return res;
            }
        }
        public bool DeleteFile(string folder, string fileName)
        {
            try
            {
                string wwwPath = _environment.WebRootPath;
                var path = Path.Combine(wwwPath, folder, fileName);
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("Error deleting file: " + fileName + " in folder: " + folder);
                return false;
            }

        }
        public string AddTimestamp(string filename)
        {
            var split = filename.Split('.');

            split[0] = split[0] + "_" + DateTime.UtcNow.Ticks;

            return split[0] + '.' + split[1];
        }

        public async Task<string> GetFileFromAWS(string bucketName, string filePath)
        {

            try
            {

                using (var client = new AmazonS3Client(_AWSAccessKeyId, _AWSSecretKey, RegionEndpoint.EUCentral1))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = filePath
                    };
                    using GetObjectResponse response = await client.GetObjectAsync(request);
                    {
                        try
                        {

                            StreamReader reader = new StreamReader(response.ResponseStream);
                            var locations = reader.ReadToEnd();
                            return locations;

                        }
                        catch (Exception e)
                        {
                            return "Could not find this location";
                        }
                    }
                }
            }
            catch
            {
                _logger.Error("Could not make a request for loading file from S3");
                return null;
            }

        }

        public bool DeleteAWSFile(string AWSKey)
        {
            try
            {


                using (var client = new AmazonS3Client(_AWSAccessKeyId, _AWSSecretKey, RegionEndpoint.EUCentral1))
                {

                    var deleteRequest = new DeleteObjectRequest
                    {

                        Key = AWSKey,
                        BucketName = _AWSBucketName,

                    };
                    client.DeleteObjectAsync(deleteRequest);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
