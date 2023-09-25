using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common.Extensions;
using PetMeetApp.DAL;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.BLL
{
    public class NotificationModelBLL : INotificationModelBLL
    {
        private INotificationModelDAL _INotificationModelDAL;
        private readonly IFollowingDAL iFollowingDAL;

        public NotificationModelBLL(INotificationModelDAL iNotificationModelDAL, IFollowingDAL iFollowingDAL)
        {
            _INotificationModelDAL = iNotificationModelDAL;
            this.iFollowingDAL = iFollowingDAL;
        }

        public List<NotificationModel> GetLostPetNotificationsByPetId(long petId)
        {
            return _INotificationModelDAL.GetLostPetNotificationsByPetId(petId);
        }

        public IEnumerable<NotificationModel> GetLostPetNotificationsForUser(long petId)
        {
            return _INotificationModelDAL.GetLostPetNotificationsForUser(petId);
        }

        public void Create(NotificationModel notificationModel)
        {
            _INotificationModelDAL.Create(notificationModel);
        }

        public void Delete(NotificationModel notificationModel)
        {
            _INotificationModelDAL.Delete(notificationModel);
        }

        public NotificationModel GetNotificationModelByLikeId(long likeModelId)
        {
            return _INotificationModelDAL.GetNotificationModelByLikeId(likeModelId);
        }

        public IEnumerable<NotificationModel> GetNotificationsByUserId(long userId)
        {
            var notifications =  _INotificationModelDAL.GetNotificationsByUserId(userId);
            if (notifications.SafeAny())
            {
                foreach(var notification in notifications)
                {
                    if(notification.FollowingRelation != null)
                    {
                        if (iFollowingDAL.FollowUser(userId, notification.FollowingRelation.FollowedId) != null)
                        {
                            notification.IsFollowingUser = true;
                        }
                    }
                }
            }

            return notifications;
        }

        public IEnumerable<NotificationModel> GetWalkingNotificationsForUser(long userId)
        {
            return _INotificationModelDAL.GetWalkingNotificationsForUser(userId);
        }
    }
}