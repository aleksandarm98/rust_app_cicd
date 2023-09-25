using Microsoft.Extensions.Configuration;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PetMeetApp.Models.HelpModels;
using PetMeetApp.Common.Extensions;
using PetMeetApp.DAL;
using PetMeetApp.Common;
using NLog;
using PetMeetApp.Common.Interfaces;

namespace PetMeetApp.BLL
{
    public class PetBLL : IPetBLL
    {
        private readonly IPetDAL _petDAL;
        private readonly IFileService _fileService;
        private readonly INotificationBLL _INotificationBLL;
        private readonly INotificationModelBLL _INotificationModelBLL;
        private readonly IUserBLL _IUserBLL;
        private readonly IUserAchievementBLL _iUserAchievementBLL;
        private readonly ILostPetModelDAL _ILostPetModelDAL;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IConfiguration _configuration;

        public PetBLL(IPetDAL petDAL, INotificationBLL iNotificationBLL, IUserBLL iUserBLL, IUserAchievementBLL iUserAchievementBLL, IFileService fileService, IConfiguration configuration, ILostPetModelDAL iLostPetModelDAL, INotificationModelBLL iNotificationModelBLL)
        {
            _petDAL = petDAL;
            _INotificationBLL = iNotificationBLL;
            _IUserBLL = iUserBLL;
            _iUserAchievementBLL = iUserAchievementBLL;
            _configuration = configuration;
            _fileService = fileService;
            _ILostPetModelDAL = iLostPetModelDAL;
            _INotificationModelBLL = iNotificationModelBLL;
        }

        public PetModel ChangeImage(FileModelDTO data)
        {
            try
            {
                var folder = _configuration["Folders:PetProfileImages"];
                var AWSKey = _fileService.SaveFileInAWSBucket(folder, data.file);

                var oldImage = this._petDAL.GetPetImage(data.Id);
                _fileService.DeleteAWSFile(oldImage);

                var pet = _petDAL.ChangeImage(data.Id, AWSKey);
                pet.Image = _fileService.GetAWSFileURL(AWSKey);

                return pet;
            }
            catch(Exception e)
            {
                return null;
            }
            
          
        }

        public PetProfileModelDTO GetPet(long petId)
        {
            return _petDAL.GetPet(petId);
        }


        public PetModel ChangeName(PetModel pet)
        {
            pet = _petDAL.ChangeName(pet);
            return pet;
        }

        public PetModel ChangeUsername(PetModel pet)
        {
            pet = _petDAL.ChangeUsername(pet);
            return pet;
        }

        public IEnumerable<PetModelHelper> GetOwnedPets(long userId)
        {
            IEnumerable<PetModel> pets = _petDAL.GetOwnedPets(userId);
            List<PetModelHelper> petsHelper = new List<PetModelHelper>();
            
            foreach (var i in pets)
            {
                PetModelHelper petHelp = new PetModelHelper();
                PetModel pet = _petDAL.GetById(i.Id);
                petHelp.PetId = pet.Id;
                petHelp.Username = pet.Username;
                petHelp.Image = pet.Image;
                petHelp.ImageData = pet.ImageData;
                petHelp.PetType = pet.PetType;
                petsHelper.Add(petHelp);
            }

            return petsHelper;
        }

        public IEnumerable<PetTypeModel> GetTypes()
        {
            return _petDAL.GetTypes();
        }

        public PetModel Register(PetModel pet, long ownerId)
        {
            var registeredPet = _petDAL.Register(pet,ownerId);
            if (registeredPet != null)
            {
                int userPetsCount = _petDAL.GetPetsByUserId(ownerId).Count();
                _iUserAchievementBLL.RecalculatePetsUserAchievements(ownerId, userPetsCount);
            }

            return registeredPet;
        }

        public bool UsernameExists(string username)
        {
            return _petDAL.UsernameExists(username);
        }

        public IEnumerable<SearchModel> SearchPets(string searchFilter)
        {
            return _petDAL.SearchPets(searchFilter);
        }

        public IEnumerable<PetModel> FindPartner(FindPartnerModelDTO pet)
        {
            return _petDAL.FindPartner(pet);
        }

        public void DeletePet(long petId)
        {
                long userId = _petDAL.GetPet(petId).UserId;

                _petDAL.DeletePet(petId);

                int userPetsCount = _petDAL.GetPetsByUserId(userId).Count();
                _iUserAchievementBLL.RecalculatePetsUserAchievements(userId, userPetsCount);
        }

        public LostPetModelDTO ReportPetLoss(long petId)
        {
            LostPetModelDTO result = null;
            LostPetModel lostPetModel = null;

            PetProfileModelDTO pet = _petDAL.GetPet(petId);
            UserModel userSender = _IUserBLL.GetUser(pet.UserId);
            List<UserModelHelper> userReceivers = _petDAL.ReportPetLoss(pet);

            if (userReceivers.SafeAny())
            {
                lostPetModel = new LostPetModel
                {
                    PetId = pet.Id,
                    UserId = pet.UserId,
                    Lat = pet.Lat,
                    Lng = pet.Lng,
                };
                _ILostPetModelDAL.Create(lostPetModel);

                foreach (var userReceiver in userReceivers)
                {
                    if (!_INotificationBLL.SendLostPetNotification(userSender, userReceiver, pet))
                    {
                        _logger.Error(string.Format("Error on sending lost pet notifications."));
                    }

                    _INotificationModelBLL.Create(new NotificationModel
                    {
                        LostPetModelId = lostPetModel.Id,
                        Date = DateTime.UtcNow,
                        NotificationType = (int)Constants.NotificationType.LostPetNotification,
                        UserSenderId = userSender.Id,
                        UserReceiverId = userReceiver.UserId
                    });
                }

                result = new LostPetModelDTO
                {
                    PetId = lostPetModel.PetId,
                    UserId = lostPetModel.UserId,
                    Lat = lostPetModel.Lat,
                    Lng = lostPetModel.Lng,
                    UserRecievers = userReceivers
                };
            }

            return result;
        }


        public void RemovePetLoss(long petId)
        {
            var lostPetNotifications = _INotificationModelBLL.GetLostPetNotificationsByPetId(petId);
            if (lostPetNotifications.SafeAny())
            {
                foreach(var notification in lostPetNotifications)
                {
                    _INotificationModelBLL.Delete(notification);

                }
            }

            var lostPets = _ILostPetModelDAL.GetLostPetsByPetId(petId);
            if (lostPets.SafeAny())
            {
                foreach (var lostPet in lostPets)
                {
                    _ILostPetModelDAL.Delete(lostPet);
                }
            }
        }

        public bool IsPetLost(long petId)
        {
            return _petDAL.IsPetLost(petId);
        }

        public List<PetModel> GetPetsByUserId(long userId)
        {
            return _petDAL.GetPetsByUserId(userId);
        }
    }
}