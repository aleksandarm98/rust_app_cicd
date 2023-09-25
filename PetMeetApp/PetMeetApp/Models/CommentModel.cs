using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class CommentModel
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        public long PostId { get; set; }
        public PostModel Post { get; set; }
        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        [JsonIgnore]
        public NotificationModel NotificationModel { get; set; }
    }
}
