using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common;
using PetMeetApp.Common.Extensions;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL
{
    public class LikeBLL: ILikeBLL
    {
        private readonly ILikeDAL _ILikeDAL;
        private readonly INotificationBLL _INotificationBLL;
        private INotificationModelDAL _INotificationModelDAL;
        private INotificationModelBLL _INotificationModelBLL;
        private readonly IUserBLL _IUserBLL;
        private Logger _logger;

        public LikeBLL(ILikeDAL iLikeDAL, INotificationBLL iNotificationBLL,  INotificationModelDAL iNotificationModelDAL, IUserBLL iUserBLL, INotificationModelBLL iNotificationModelBLL)
        {
            _ILikeDAL = iLikeDAL;
            _INotificationBLL = iNotificationBLL;
            _INotificationModelDAL = iNotificationModelDAL;
            _IUserBLL = iUserBLL;
            _INotificationModelBLL = iNotificationModelBLL;
            _logger = LogManager.GetCurrentClassLogger();

        }

        public void Like(long userId, long postId)
        {
            LikeModel likeModel = _ILikeDAL.Like(userId, postId);
            if (likeModel == null)
            {
                UserModel userReceiver = _IUserBLL.GetUserByPostId(postId);

                likeModel = new LikeModel
                {
                    UserId = userId,
                    PostId = postId
                };
                _ILikeDAL.Create(likeModel);

                if (!_INotificationBLL.SendLikeNotification(likeModel, userReceiver))
                {
                    _logger.Error(string.Format("Error on sending like notifications for postId: {0}, liked by petId: {1}.", likeModel.PostId, likeModel.UserId));
                }

                _INotificationModelDAL.Create(new NotificationModel
                {
                    LikeModelId = likeModel.Id,
                    Date = DateTime.UtcNow,
                    NotificationType = (int)Constants.NotificationType.LikeNotification,
                    UserSenderId = likeModel.UserId,
                    UserReceiverId = userReceiver.Id
                });
            }
            else
            {
                NotificationModel notificationModel = _INotificationModelBLL.GetNotificationModelByLikeId(likeModel.Id);

                _ILikeDAL.DeleteLike(likeModel);
                if (notificationModel != null)
                {
                    _INotificationModelDAL.Delete(notificationModel);
                }
            }
        }

        public void CreateLike(LikeModel like)
        {
            this._ILikeDAL.Create(like);
        }

        public void DeleteLike(long id)
        {
            LikeModel like = this._ILikeDAL.GetById(id);
            
            if (like == null)
            {
                throw new Exception();
            }

            this._ILikeDAL.Delete(like);
        }
        

        public IEnumerable<PostModel> GetAllMyLikedPosts(long id)
        {

            return _ILikeDAL.GetAllMyLikedPosts(id);
        }
        
        public IEnumerable<LikeModel> GetAllPostLikes(long postId)
        {
            return _ILikeDAL.GetAllPostLikes(postId);
        }

        public IEnumerable<LikeModelDTO> GetListOfUsersThatLikedPost(long postId)
        {
            return _ILikeDAL.GetListOfUsersThatLikedPost(postId);
        }

        public LikeModel GetLike(long id)
        {
            return this._ILikeDAL.GetById(id);
        }

        public LikeModel CheckLike(long userId, long postId)
        {
            return this._ILikeDAL.Like(userId, postId);
        }

        public long GetLikesCountByPostId(long postId)
        {
            var likes = _ILikeDAL.GetAllPostLikes(postId).ToList();

            return likes.Count;
        }
    }
}