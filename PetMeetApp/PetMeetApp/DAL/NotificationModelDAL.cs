using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetMeetApp.DAL
{
    public class NotificationModelDAL : BaseDAL<NotificationModel>, INotificationModelDAL
    {
        public NotificationModelDAL(Context context) : base(context)
        { }

        public NotificationModel GetNotificationModelByCommentId(long commentId)
        {
            return _context.Notifications.Where(n => n.CommentModelId.HasValue && n.CommentModelId.Value.Equals(commentId)).FirstOrDefault();
        }

        public NotificationModel GetNotificationModelByLikeId(long likeModelId)
        {
            var a = _context.Notifications.Include(n => n.UserSender)
                .Where(n => n.LikeModelId.HasValue && n.LikeModelId.Value.Equals(likeModelId))
                .FirstOrDefault();
            return a;
        }

        public List<NotificationModel> GetNotificationsByUserId(long userId)
        {
            var query = _context.Notifications
                .Include(n => n.LikeModel)
                .Include(n => n.CommentModel)
                .Include(n => n.FollowingRelation)
                .Include(n => n.UserSender)
                .Where(n => n.UserReceiverId.Equals(userId) && (n.LikeModelId != null || n.CommentModelId != null || n.FollowingRelationId != null));

            return query.ToList();
        }

        public List<NotificationModel> GetLostPetNotificationsByPetId(long petId)
        {
            var query = from notifications in _context.Notifications.Include(n => n.UserSender)
                        join lostPet in _context.LostPets on notifications.LostPetModelId equals lostPet.Id
                        where lostPet.PetId == petId
                        select new NotificationModel
                        {
                            Id = notifications.Id,
                            LostPetModelId = notifications.LostPetModelId,
                            Date = notifications.Date,
                            NotificationType = notifications.NotificationType,
                            UserReceiverId = notifications.UserReceiverId,
                            UserSenderId = notifications.UserSenderId
                        };

            return query.ToList();
        }

        public IEnumerable<NotificationModel> GetLostPetNotificationsForUser(long userId)
        {
            var query = _context.Notifications
                .Include(n => n.LostPetModel)
                .Include(n => n.UserSender)
                .Where(n => n.UserReceiverId.Equals(userId) && n.LostPetModelId != null);

            return query;
        }

        public IEnumerable<NotificationModel> GetWalkingNotificationsForUser(long userId)
        {
            var currentDate = DateTime.UtcNow;

            var query = _context.Notifications
                .Include(n => n.WalkingModel)
                .Include(n => n.UserSender)
                .Include(n => n.WalkingModel.Pets)
                .Where(n => n.UserReceiverId.Equals(userId) 
                    && n.WalkingModelId != null
                    && n.WalkingModel.DateTime > currentDate);

            return query;
        }
    }
}