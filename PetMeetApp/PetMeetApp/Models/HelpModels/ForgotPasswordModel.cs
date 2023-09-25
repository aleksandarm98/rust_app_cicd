namespace PetMeetApp.Models.HelpModels
{
    public class ForgotPasswordModel
    {
        public string EmailAddress { get; set; }
        public string? Code { get; set; }
        
        public string? NewPassword { get; set; }

    }
}
