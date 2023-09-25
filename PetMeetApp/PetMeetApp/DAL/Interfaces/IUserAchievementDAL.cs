using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IUserAchievementDAL: IBaseDAL<UserAchievementModel>
    {
        public IEnumerable<UserAchievementModel> GetAllUserAchievements(long userId);
        public IEnumerable<UserAchievementModel> GetIncompleteUserAchievementsByAchievementType(long userId, int achievementType);
    }
}