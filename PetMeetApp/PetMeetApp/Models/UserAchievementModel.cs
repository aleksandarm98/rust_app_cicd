#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public class UserAchievementModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserModel User { get; set; }
        public long AchievementId { get; set; }
        public AchievementModel Achievement { get; set; }
        public int CurrentProgress { get; set; }
        public bool IsDone { get; set; }
    }
}