using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface INotificationModelDAL : IBaseDAL<NotificationModel>
    {
        public NotificationModel GetNotificationModelByLikeId(long likeModelId);

        public NotificationModel GetNotificationModelByCommentId(long commentId);

        public List<NotificationModel> GetNotificationsByUserId(long userId);

        public List<NotificationModel> GetLostPetNotificationsByPetId(long petId);
        
        public IEnumerable<NotificationModel> GetLostPetNotificationsForUser(long userId);
        public IEnumerable<NotificationModel> GetWalkingNotificationsForUser(long userId);
    }
}