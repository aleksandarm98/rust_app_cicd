using System.Collections.Generic;

namespace PetMeetApp.Models
{
    public class AdoptionAdModel
    {
        public long Id { get; set; }
        public string PetImages { get; set; } 
        public string PetName { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public long PetTypeId { get; set; }
        public PetTypeModel PetType { get; set; }

        public string Breed { get; set; }
        public string? Gender { get; set; }
        public long UserId { get; set; }
        public UserModel User { get; set; }
    }
}
