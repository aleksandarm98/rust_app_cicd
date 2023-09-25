using System.Collections.Generic;

namespace PetMeetApp.Models.HelpModels
{
    public class LostPetModelDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PetId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        
        public IEnumerable<UserModelHelper> UserRecievers { get; set; }
    }
}