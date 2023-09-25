#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class UserReferralCodeModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserModel User { get; set; }
        public string Code { get; set; }
        public int CodeAppliedCount { get; set; }
    }
}