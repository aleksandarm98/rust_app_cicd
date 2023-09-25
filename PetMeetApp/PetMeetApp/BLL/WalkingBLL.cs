using Microsoft.EntityFrameworkCore;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common;
using PetMeetApp.Common.Extensions;
using PetMeetApp.DAL;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetMeetApp.BLL
{
    public class WalkingBLL : IWalkingBLL
    {
        private readonly IWalkingDAL _walkingDAL;
        private readonly IUserBLL _IUserBLL;
        private readonly IPetDAL _IPetDAL;
        private readonly INotificationBLL _INotificationBLL;
        private readonly INotificationModelBLL _INotificationModelBLL;
        private Logger _logger = LogManager.GetCurrentClassLogger();

        public WalkingBLL(IWalkingDAL IWalkingDAL, IUserBLL IUserBLL, INotificationModelBLL INotificationModelBLL, INotificationBLL INotificationBLL, IPetDAL IPetDAL)
        {
            _walkingDAL = IWalkingDAL;
            _IUserBLL = IUserBLL;
            _INotificationBLL = INotificationBLL;
            _INotificationModelBLL = INotificationModelBLL;
            _IPetDAL= IPetDAL;
        }

        public WalkingModel InviteForWalk(WalkingDTO data)
        {
            double walkingRadius = 5; // or 10km
            double minLat = data.LocationX - walkingRadius / 111.3;
            double maxLat = data.LocationX + walkingRadius / 111.3;
            double minLng = data.LocationY - walkingRadius / (111.3 * Math.Cos(data.LocationX));
            double maxLng = data.LocationY + walkingRadius / (111.3 * Math.Cos(data.LocationX));


            if (_walkingDAL.ExistsWalkInDB(data.DateTime, data.Description, data.LocationX, data.LocationY)) return null;

            var pets = new List<PetModel>();

            foreach (var petId in data.PetIds)
            {
                var pet = _IPetDAL.GetById(petId);
                if (pet != null)
                {
                    pets.Add(pet);
                }
            }

            var walk = new WalkingModel
            {
                DateTime = data.DateTime,
                Description = data.Description,
                LocationX = data.LocationX,
                LocationY = data.LocationY,
                Pets = pets
            };

            var res = _walkingDAL.InviteForWalk(walk);
      
            if (res != null)
            {
                List<UserModel> userReceiversFromDB = _walkingDAL.FindsUserToWalk(walk, minLat, maxLat, minLng, maxLng);
              

                UserModel userSender = _IUserBLL.GetUser(data.UserId);
           

                if (userReceiversFromDB.SafeAny())
                {
                    foreach (var userReceiver in userReceiversFromDB)
                    {
                        if (!_INotificationBLL.SendWalkingNotification(userSender, userReceiver))
                        {
                            _logger.Error(string.Format("Error on sending walking notification."));
                        }

                        _INotificationModelBLL.Create(new NotificationModel
                        {
                            WalkingModelId = res.Id,
                            Date = DateTime.UtcNow,
                            NotificationType = (int)Constants.NotificationType.WalkingNotification,
                            UserSenderId = userSender.Id,
                            UserReceiverId = userReceiver.Id
                        });
                    }
                }
                return res;
            }
            return res;

        }
    }
}