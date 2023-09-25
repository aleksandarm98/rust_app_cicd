using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Common
{
    public static class Resources
    {
        public static class NotificationTitle
        {
            public const string LikeTitle = "Novo sviđanje";
            public const string FollowTitle = "Novo praćenje";
            public const string CommentTitle = "Novi komentar";
            public const string ChatTitle = "Nova poruka";
            public const string LostPetTitle = "Izubljen ljubimac";
            public const string WalkingTitle = "Šetnja ";
        }

        public static class NotificationType
        {
            public const string LikeNotification = "Like";
            public const string FollowNotification = "Follow";
            public const string CommentNotification = "Comment";
            public const string LostPetNotification = "LostPet";
            public const string ChatNotification = "Chat";
            public const string WalkingNotification = "Walk";
        }
    }
}
