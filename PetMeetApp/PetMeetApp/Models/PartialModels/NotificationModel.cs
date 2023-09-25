using System.ComponentModel.DataAnnotations.Schema;

namespace PetMeetApp.Models
{
    public partial class NotificationModel
    {
        [NotMapped]
        public bool? IsFollowingUser { get; set; }
    }
}
