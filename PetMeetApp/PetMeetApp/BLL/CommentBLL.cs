using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common;
using PetMeetApp.DAL;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL

{
    public class CommentBLL : ICommentBLL
    {
        private readonly ICommentDAL _ICommentDAL;
        private readonly IPostDAL _IPostDAL;
        private readonly INotificationBLL _INotificationBLL;
        private INotificationModelDAL _INotificationModelDAL;
        private readonly IUserBLL _IUserBLL;
        private Logger _logger;


        public CommentBLL(ICommentDAL _iCommentDAL, IPostDAL _iPostDAL, INotificationBLL iNotificationBLL, INotificationModelDAL iNotificationModelDAL, IUserBLL iUserBLL)
        {
            this._ICommentDAL = _iCommentDAL;
            this._IPostDAL = _iPostDAL;
            _INotificationBLL = iNotificationBLL;
            _INotificationModelDAL = iNotificationModelDAL;
            _IUserBLL = iUserBLL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public CommentModel getComment(long commentId)
        {
            return this._ICommentDAL.GetById(commentId);
        }

        public IEnumerable<CommentHelperModelIDTO> getPostComments(long postId)
        {
            IEnumerable<CommentHelperModelIDTO> comments = this._ICommentDAL.getPostComments(postId);
            return comments;
        }

        public void CommentPost(CommentInputDTO comment)
        {
            PostModel post = _IPostDAL.GetWithUser(comment.PostId);

            if (post != null)
            {
                var commentEntity = new CommentModel();
                commentEntity.UserId = comment.UserId;
                commentEntity.PostId = comment.PostId;
                commentEntity.Content = comment.Content;
                commentEntity.DatePublished = DateTime.Now;
                _ICommentDAL.Create(commentEntity);
                
                if (!_INotificationBLL.SendCommentNotification(commentEntity))
                {
                    _logger.Error(string.Format("Error on sending comment notifications for: {0} post, by: {1}.", comment.PostId, comment.UserId));
                }

                _INotificationModelDAL.Create(new NotificationModel
                {
                    CommentModelId = commentEntity.Id,
                    Date = DateTime.UtcNow,
                    NotificationType = (int)Constants.NotificationType.CommentNotification,
                    UserSenderId = commentEntity.UserId,
                    UserReceiverId = post.User.Id
                });
            }
            else throw new Exception("There is no post with Id: " + comment.PostId);

        }
        
        public bool RemoveComment(long id)
        {
            CommentModel comment = _ICommentDAL.GetById(id);

            if (comment != null)
            {
                var notificationModel = _INotificationModelDAL.GetNotificationModelByCommentId(comment.Id);

                comment.NotificationModel = notificationModel;
                _ICommentDAL.Delete(comment);

                if(notificationModel != null)
                {
                    _INotificationModelDAL.Delete(notificationModel);
                }

                return true;
            }

            return false;
        }

        public long GetCommentsCountByPostId(long postId)
        {
            return _ICommentDAL.GetCommentsCountByPostId(postId);
        }
    }
}