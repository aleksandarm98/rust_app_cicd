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
    public class AchievementBLL : IAchievementBLL
    {
        private readonly IAchievementDAL _IAchievementDAL;
        private Logger _logger;

        public AchievementBLL(IAchievementDAL _iAchievementDAL)
        {
            this._IAchievementDAL = _iAchievementDAL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public AchievementModel GetAchievement(long achievementId)
        {
            return this._IAchievementDAL.GetById(achievementId);
        }

        public IEnumerable<AchievementModel> GetAllAchievements()
        {
            return this._IAchievementDAL.GetAll();
        }
    }
}