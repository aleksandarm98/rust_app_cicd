using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models.HelpModels
{
    public class HomepagePostDTO
    {
        public long Id { get; set; }
        public long PetId { get; set; }
        public long? UserId { get; set; }
        public PetModel Pet { get; set; }
        public string ContentUrl { get; set; }
        public long ContentTypeId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public long NumberOfLikes { get; set; }
        public long NumberOfComments { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsLikedByUser { get; set; }
        public bool IsFollowedByUser { get; set; }
        public IEnumerable<PostData> PostData { get; set; }
    }
}
