using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.BLL.Interfaces
{
    public interface INotificationModelBLL
    {
        public NotificationModel GetNotificationModelByLikeId(long likeModelId);
        public IEnumerable<NotificationModel> GetNotificationsByUserId(long userId);

        public List<NotificationModel> GetLostPetNotificationsByPetId(long petId);
        
        public IEnumerable<NotificationModel> GetLostPetNotificationsForUser(long petId);
        public IEnumerable<NotificationModel> GetWalkingNotificationsForUser(long userId);
        public void Create(NotificationModel notificationModel);

        public void Delete(NotificationModel notificationModel);
    }
}