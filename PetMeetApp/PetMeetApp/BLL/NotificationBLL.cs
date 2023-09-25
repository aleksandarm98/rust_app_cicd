using FirebaseAdmin.Messaging;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common;
using PetMeetApp.Common.Extensions;
using PetMeetApp.Common.ExternalServices;
using PetMeetApp.DAL;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;

namespace PetMeetApp.BLL
{
    public class NotificationBLL : INotificationBLL
    {
        private readonly IUserBLL _IUserBLL;
        private readonly IFirebaseAccessTokenDAL _IFirebaseAccessTokenDAL;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        #region NotificationBLL Initialization

        public NotificationBLL(IUserBLL iUserBLL, IFirebaseAccessTokenDAL iFirebaseAccessTokenDAL)
        {
            _IUserBLL = iUserBLL;
            _IFirebaseAccessTokenDAL = iFirebaseAccessTokenDAL;
        }

        #endregion

        #region Public methods

        public bool SendLikeNotification(LikeModel likeModel, UserModel userReceiver)
        {
            UserModel userSender = _IUserBLL.GetUser(likeModel.UserId);

            string body = userSender.Username;
            Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "type" , Resources.NotificationType.LikeNotification }, { "senderId" , userSender.Id.ToString() }, { "postId", likeModel.PostId.ToString() }
                    };

            return this.FormAndSendMessage(userSender, userReceiver, Resources.NotificationTitle.LikeTitle, body, data);
        }

        public bool SendFollowNotification(FollowingRelation followingRelation)
        {
            UserModel userSender = _IUserBLL.GetUser(followingRelation.FollowingId);
            UserModel userReceiver = _IUserBLL.GetUserWithFirebaseAccessTokens(followingRelation.FollowedId);

            string body = userSender.Username;
            Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "type" , Resources.NotificationType.FollowNotification },  { "senderId" , userSender.Id.ToString() }, { "followingId", followingRelation.FollowingId.ToString() }
                    };

            return this.FormAndSendMessage(userSender, userReceiver, Resources.NotificationTitle.FollowTitle, body, data);
        }

        public bool SendCommentNotification(CommentModel commentModel)
        {
            UserModel userSender = _IUserBLL.GetUser(commentModel.UserId);
            UserModel userReceiver = _IUserBLL.GetUserByPostId(commentModel.PostId);

            string body = userSender.Username;
            Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "type" , Resources.NotificationType.CommentNotification },  { "senderId" , userSender.Id.ToString() }, { "postId", commentModel.PostId.ToString() }
                    };

            return this.FormAndSendMessage(userSender, userReceiver, Resources.NotificationTitle.CommentTitle, body, data);
        }

        public bool SendMessageNotification(ChatModel chatModel)
        {
            UserModel userSender = _IUserBLL.GetUser(chatModel.UserSender);
            UserModel userReceiver = _IUserBLL.GetUserWithFirebaseAccessTokens(chatModel.UserReceiver);

            string body = userSender.Username;
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "type" , Resources.NotificationType.ChatNotification },  { "senderId" , userSender.Id.ToString() }, { "receiverId", userReceiver.Id.ToString() }, { "content", chatModel.Content }
            };

            return this.FormAndSendMessage(userSender, userReceiver, Resources.NotificationTitle.ChatTitle, body, data);
        }

        public bool SendLostPetNotification(UserModel userSender, UserModelHelper userReceiver, PetProfileModelDTO pet)
        {
            List<FirebaseAccessToken> firebaseAccessTokens = _IFirebaseAccessTokenDAL.GetFirebaseAccessTokenByUserId(userReceiver.UserId).SafeToList();

            string body = userSender.Username;
            Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "type" , Resources.NotificationType.LostPetNotification },  { "senderId" , userSender.Id.ToString() }, { "petId", pet.Id.ToString() }
                    };

            return this.FormAndSendMessage(userSender, new UserModel { Id = userReceiver.UserId, FirebaseAccessTokens = firebaseAccessTokens}, Resources.NotificationTitle.LostPetTitle, body, data);
        }
        public bool SendWalkingNotification(UserModel userSender, UserModel userReceiver)
        {


            string body = userSender.Username;
            Dictionary<string, string> data = new Dictionary<string, string>
                    {
                        { "type" , Resources.NotificationType.WalkingNotification },  { "senderId" , userSender.Id.ToString() }
                    };

            return this.FormAndSendMessage(userSender, userReceiver, Resources.NotificationTitle.WalkingTitle, body, null);
 }
        #endregion

        #region Private methods

        /// <summary>
        /// Forms and sends message for each users's FirebaseAccessToken.
        /// Returns true if all messages were send successfully, returns false otherwise.
        /// </summary>
        /// <param name="userSender"></param>
        /// <param name="userReceiver"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool FormAndSendMessage(UserModel userSender, UserModel userReceiver, string title, string body, Dictionary<string, string> data)
        {
            bool success = true;
            Message message = null;

            MessageHelper messageHelper = new()
            {
                SenderId = userSender.Id,
                ReceiverId = userReceiver.Id,
                Title = title
            };

            if (userSender != null && userReceiver != null && userSender.Id != userReceiver.Id && userReceiver.FirebaseAccessTokens.SafeAny())
            {
                message = new Message
                {
                    Notification = new()
                    {
                        Title = messageHelper.Title,
                        Body =  body
                    },

                    Data = data
                };

                foreach (var firebaseAccessToken in userReceiver.FirebaseAccessTokens)
                {
                    message.Token = firebaseAccessToken.AccessToken;
                    success &= this.SendNotification(message);
                }
            }

            return success;
        }

        /// <summary>
        /// Sends notification as a message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool SendNotification(Message message)
        {
            bool? result;
            try
            {
                result = FirebaseService.SendNotification(message).Result != null;
            } catch (Exception ex) {
                _logger.Error(ex.Message);
                result = false;
            }

            return result.Value;
        }

        #endregion
    }
}