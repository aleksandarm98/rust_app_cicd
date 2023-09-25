using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IAchievementBLL
    {
        public AchievementModel GetAchievement(long achievementId);
        
        public IEnumerable<AchievementModel> GetAllAchievements();
    }
}