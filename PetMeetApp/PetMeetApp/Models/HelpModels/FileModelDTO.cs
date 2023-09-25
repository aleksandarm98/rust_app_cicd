using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models.HelpModels
{
    public class FileModelDTO
    {
        [FromForm(Name = "Id")]
        public long Id { get; set; }
        
        [FromForm(Name = "File")]
        public IFormFile file {get;set;}
        
        [FromForm(Name = "ContentTypeId")]
        public long? ContentTypeId { get; set; }
        
    }
}
