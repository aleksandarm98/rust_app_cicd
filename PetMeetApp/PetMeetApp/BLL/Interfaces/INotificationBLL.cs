using FirebaseAdmin.Messaging;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface INotificationBLL
    {
        /// <summary>
        /// Sends a like notification.
        /// </summary>
        /// <param name="likeModel"></param>
        /// <returns></returns>
        public bool SendLikeNotification(LikeModel likeModel, UserModel userReceiver);

        /// <summary>
        /// Sends a follow notification.
        /// </summary>
        /// <param name="likeModel"></param>
        /// <returns></returns>
        public bool SendFollowNotification(FollowingRelation follow);

        /// <summary>
        /// Sends a comment notification.
        /// </summary>
        /// <param name="likeModel"></param>
        /// <returns></returns>
        public bool SendCommentNotification(CommentModel comment);

        /// <summary>
        /// Sends a message notification.
        /// </summary>
        /// <param name="chatModel"></param>
        /// <returns></returns>
        public bool SendMessageNotification(ChatModel chatModel);

        /// <summary>
        /// Sends a lost pet notification.
        /// </summary>
        /// <param name="userSender"></param>
        /// <param name="userReceiver"></param>
        /// <returns></returns>
        public bool SendLostPetNotification(UserModel userSender, UserModelHelper userReceiver, PetProfileModelDTO pet);

        /// <summary>
        /// Sends a walk notification.
        /// </summary>
        /// <param name="userSender"></param>
        /// <param name="userReceiver"></param>
        /// <returns></returns>
        public bool SendWalkingNotification(UserModel userSender, UserModel userReceiver);

    }
}