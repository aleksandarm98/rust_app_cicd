using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Common.Interfaces
{
    public interface IFileService
    {
        bool SaveFile(string folder, string fileName, string data);
        string SaveFileInAWSBucket(string folder, IFormFile data);
        public string GetAWSFileURL(string contentURL);
        public bool DeleteAWSFile(string AWSKey);

        bool DeleteFile(string folder, string fileName);
        string AddTimestamp(string filename);
        Task<string> GetFileFromAWS(string bucketName, string filePath);
    }
}
