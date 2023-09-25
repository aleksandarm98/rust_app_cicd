using System;

namespace PetMeetApp.Models.HelpModels
{
    public class PetProfileModelDTO
    {
        public long Id { get; set; }
        public string PetUsername { get; set; }
        public string PetName { get; set; }
        public long PetTypeId { get; set; }
        public string PetTypeName { get; set; }
        
        
        public string PetImage { get; set; }

        public string Breed { get; set; }
        public DateTime Birthday { get; set; }
        public bool GenderType { get;set; }
        public string Country { get; set; }
        public string City { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public long UserId { get; set; }
        public string OwnerUsername { get; set; }
        public string UserImage { get; set; }

    }
}
