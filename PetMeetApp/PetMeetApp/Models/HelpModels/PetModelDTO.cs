using System.ComponentModel.DataAnnotations.Schema;

namespace PetMeetApp.Models.HelpModels
{
    public class PetModelHelper
    {
        public long PetId { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public string ImageData { set; get; }

        public PetTypeModel PetType { get; set; }
        
        
    }
}
