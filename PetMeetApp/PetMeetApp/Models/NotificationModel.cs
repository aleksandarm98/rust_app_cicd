using System;
using System.Data.SqlClient;
using System.Text.Json.Serialization;

namespace PetMeetApp.Models
{
    public partial class NotificationModel
    {
        public long Id { get; set; }
        
        public long? LikeModelId { get; set; }
        public LikeModel LikeModel { get; set; }

        public long? LostPetModelId { get; set; }
        public LostPetModel LostPetModel { get; set; }

        public long? CommentModelId { get; set; }
        public CommentModel CommentModel { get; set; }

        public long? FollowingRelationId { get; set; }
        public FollowingRelation FollowingRelation { get; set; }

        public long? ChatModelId { get; set; }        
        public ChatModel ChatModel { get; set; }

        public DateTime? Date { get; set; }
        public int NotificationType { get; set; }
        
        public long UserReceiverId { get; set; }
        public long? UserSenderId { get; set; }

        public long? WalkingModelId { get; set; }
        public WalkingModel WalkingModel { get; set; }

        [JsonIgnore]
        public virtual UserModel UserReceiver { get; set; }
        [JsonIgnore]
        public virtual UserModel UserSender { get; set; }
    }
}
