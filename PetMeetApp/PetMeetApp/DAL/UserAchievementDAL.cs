using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class UserAchievementDAL : BaseDAL<UserAchievementModel>, IUserAchievementDAL
    {
        private readonly IUserAchievementDAL _IUserAchievementDAL;
        public UserAchievementDAL(Context context) : base(context) 
        { }

        public IEnumerable<UserAchievementModel> GetAllUserAchievements(long userId)
        {
            var userAchievements = 
                _context.UserAchievements.Where(x => x.UserId.Equals(userId)).Include(x => x.Achievement);

            return userAchievements;
        }

        public IEnumerable<UserAchievementModel> GetIncompleteUserAchievementsByAchievementType(long userId, int achievementType)
        {
            var userAchievements =
                _context.UserAchievements
                    .Where(x => x.UserId.Equals(userId) && x.Achievement.AchievementType.Equals(achievementType) && x.IsDone.Equals(false))
                    .Include(x => x.Achievement);

            return userAchievements;
        }
    }
}