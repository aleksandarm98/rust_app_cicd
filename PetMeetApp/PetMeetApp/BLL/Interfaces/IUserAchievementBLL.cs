using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IUserAchievementBLL
    {   
        public IEnumerable<UserAchievementModel> GetAllUserAchievements(long userId);
        public void RecalculateFollowersUserAchievements(long userId, int userFollowersCount);
        public void RecalculateFollowsUserAchievements(long userId, int userFollowsCount);
        public void RecalculateReelsUserAchievements(long userId, int userReelsCount);
        public void RecalculatePostsUserAchievements(long userId, int userPostsCount);
        public void RecalculatePetsUserAchievements(long userId, int userPetsCount);
        public void RecalculateReferralsUserAchievements(long userId, int userReferralsCount);
        public void RecalculateAdoptionsUserAchievements(long userId, int userAdoptionsCount);
    }
}