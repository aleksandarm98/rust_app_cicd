using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models.HelpModels
{
    public class PostModelDTO
    {
        public long PetId { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool CommentsAllowed { get; set; }
    }
}
