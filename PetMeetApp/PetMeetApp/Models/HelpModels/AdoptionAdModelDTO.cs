namespace PetMeetApp.Models.HelpModels
{
    public class AdoptionAdModelDTO
    {
        public string PetImages { get; set; }
        public string PetName { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public long PetTypeId { get; set; }
        public string Breed { get; set; }
        public long UserId { get; set; }
        public string? Gender { get; set; }
    }
}
