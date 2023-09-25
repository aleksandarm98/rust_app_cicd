using System;

namespace PetMeetApp.Models
{
    public class ForgotPassword
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public UserModel User { get; set; }
    }
}
