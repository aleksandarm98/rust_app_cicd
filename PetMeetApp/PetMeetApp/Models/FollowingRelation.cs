using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class FollowingRelation
    {
        public long Id { get; set; }
        public long FollowedId { get; set; }
        public UserModel Followed { get; set; }
        public long FollowingId { get; set; }
        public UserModel Following { get; set; }

        [JsonIgnore]
        public NotificationModel NotificationModel { get; set; }
    }
}
