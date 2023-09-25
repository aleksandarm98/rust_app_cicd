using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Common
{
    public static class Constants
    {
        public enum NotificationType
        {
            LikeNotification = 1,
            FollowNotification = 2,
            CommentNotification = 3,
            LostPetNotification = 4,
            WalkingNotification = 5
        }

        public enum AchievementType
        {
            FollowersAchievement = 1,
            FollowsAchievement = 2,
            ReelsAchievement = 3,
            PostsAchievement = 4,
            PetsAchievement = 5,
            ReferralAchievement = 6,
            AdoptionAchievement = 7
        }

        public enum ReportType
        {
            Post = 1,
            Profile = 2,
            Comment = 3
        }
    }
}
