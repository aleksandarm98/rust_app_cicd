namespace PetMeetApp.Models.HelpModels
{
    public class FindPartnerModelDTO
    {
        public long PetTypeId { get; set; }
        public string Breed { get; set; }
        public bool GenderType { get; set; }
        public double Lat { get; set; } 
        public double Lng { get; set; }
        public long Radius { get; set; }
    }
}
