using System;

namespace PetMeetApp.Models.HelpModels
{
    public class PetPostsDTO
    {
        public long Id { get; set; }
        public string ContentUrl { get; set; }
        public long ContentTypeId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsLikedByUser { get; set; }
    }
}
