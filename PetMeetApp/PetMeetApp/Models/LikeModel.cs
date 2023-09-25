#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class LikeModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        
        [JsonIgnore]
        public PostModel Post { get; set; }
        [JsonIgnore]
        public NotificationModel NotificationModel { get; set; }

    }
}
