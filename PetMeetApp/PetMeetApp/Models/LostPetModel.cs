using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class LostPetModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PetId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        [JsonIgnore]
        public IEnumerable<NotificationModel> NotificationModel { get; set; }
    }
}
