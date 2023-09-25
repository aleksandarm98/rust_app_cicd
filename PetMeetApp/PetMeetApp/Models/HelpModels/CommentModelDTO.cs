using System;

namespace PetMeetApp.Models.HelpModels
{
    public class CommentHelperModelIDTO
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        
        public string Username { get; set; }
        
        public string ProfileImageUrl { get; set; }

        public long PostId { get; set; }
        
        public string Content { get; set; }
        
        
        public DateTime DatePublished { get; set; }
    }

    public class CommentInputDTO
    {

        public long UserId { get; set; }

        public long PostId { get; set; }

        public string Content { get; set; }

    }
}
