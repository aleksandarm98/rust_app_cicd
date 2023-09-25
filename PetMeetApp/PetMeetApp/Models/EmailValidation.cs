using System;

namespace PetMeetApp.Models
{
    public class EmailValidation
    {
        public long Id { get; set; }
        public string EmailAddress { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
    }
}
