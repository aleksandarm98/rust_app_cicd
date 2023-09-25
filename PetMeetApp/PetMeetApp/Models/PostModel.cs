using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models
{
    public class PostModel
    {
        public long Id { get; set; }
        public long PetId { get; set; }
        public long UserId { get; set; }
        public PetModel Pet { get; set; }
        public UserModel User { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool CommentsAllowed { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<PostData> PostData { get; set; }
        
        public IEnumerable<CommentModel> Comments { get; set; }
        public IEnumerable<LikeModel> Likes { get; set; }


    }
}
