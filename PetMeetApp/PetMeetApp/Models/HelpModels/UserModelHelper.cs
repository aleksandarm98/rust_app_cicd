using System.ComponentModel.DataAnnotations.Schema;

namespace PetMeetApp.Models.HelpModels
{
    public class UserModelHelper
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public bool IsFollowed { get; set; }

        [NotMapped]
        public string ImageData { set; get; }
    }
}
