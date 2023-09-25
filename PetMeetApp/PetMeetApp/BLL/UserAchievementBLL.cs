using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;

namespace PetMeetApp.BLL

{
    public class UserAchievementBLL : IUserAchievementBLL
    {
        private readonly IUserAchievementDAL _IUserAchievementDAL;
        private Logger _logger;

        public UserAchievementBLL(IUserAchievementDAL _iUserAchievementDAL)
        {
            this._IUserAchievementDAL = _iUserAchievementDAL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IEnumerable<UserAchievementModel> GetAllUserAchievements(long userId)
        {
            return this._IUserAchievementDAL.GetAllUserAchievements(userId);
        }

        public void RecalculateFollowersUserAchievements(long userId, int userFollowersCount)
        {
            var followersUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.FollowersAchievement).ToList();
            foreach (var item in followersUserAchievements)
            {
                if (item.Achievement.ProgressIncrement <= userFollowersCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userFollowersCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }

        public void RecalculateFollowsUserAchievements(long userId, int userFollowsCount)
        {
            var followsUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.FollowsAchievement).ToList();
            foreach (var item in followsUserAchievements)
            {
                if (item.Achievement.ProgressIncrement <= userFollowsCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userFollowsCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }

        public void RecalculateReelsUserAchievements(long userId, int userReelsCount)
        {
            var reelsUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.ReelsAchievement).ToList();
            foreach (var item in reelsUserAchievements)
            {
                if (item.Achievement.ProgressIncrement <= userReelsCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userReelsCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }

        public void RecalculatePostsUserAchievements(long userId, int userPostsCount)
        {
            var postsUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.PostsAchievement).ToList();
            foreach (var item in postsUserAchievements)
            {
                if (item.Achievement.ProgressIncrement <= userPostsCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userPostsCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }

        public void RecalculatePetsUserAchievements(long userId, int userPetsCount)
        {
            var petsUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.PetsAchievement).ToList();
            foreach (var item in petsUserAchievements)
            {
                if(item.Achievement.ProgressIncrement <= userPetsCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userPetsCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }

        public void RecalculateReferralsUserAchievements(long userId, int userReferralsCount)
        {
            var referralsUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.ReferralAchievement).ToList();
            foreach (var item in referralsUserAchievements)
            {
                if (item.Achievement.ProgressIncrement <= userReferralsCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userReferralsCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }

        public void RecalculateAdoptionsUserAchievements(long userId, int userAdoptionsCount)
        {
            var adoptionsUserAchievements =
                this._IUserAchievementDAL.GetIncompleteUserAchievementsByAchievementType(userId, (int)Constants.AchievementType.AdoptionAchievement).ToList();
            foreach (var item in adoptionsUserAchievements)
            {
                if (item.Achievement.ProgressIncrement <= userAdoptionsCount)
                {
                    item.CurrentProgress = item.Achievement.ProgressIncrement;
                    item.IsDone = true;
                }
                else
                {
                    item.CurrentProgress = userAdoptionsCount;
                }
                this._IUserAchievementDAL.Update(item);
            }
        }
    }
}