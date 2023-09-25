using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class PetModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string PetName { get; set; }
        public long PetTypeId { get; set; }
        
        // [JsonIgnore]

        public PetTypeModel PetType { get; set; }

        [JsonIgnore]
        public IEnumerable<UserPetRelation> Users { get; set; }
        
        public string Image { get; set; }

        [NotMapped]
        public string ImageData { set; get; }
        public string Breed { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public bool GenderType { get; set; }
        
        public string Country { get; set; }
        public string City { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        
    }
}
