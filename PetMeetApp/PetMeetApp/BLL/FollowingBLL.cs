using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common;
using PetMeetApp.Common.Extensions;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL
{
    public class FollowingBLL: IFollowingBLL
    {
        private readonly IFollowingDAL _iFollowingDAL;
        private readonly IUserDAL _iUserDAL;
        private readonly INotificationBLL _INotificationBLL;
        private readonly INotificationModelBLL _INotificationModelBLL;
        private readonly IUserAchievementBLL _iUserAchievementBLL;
        private Logger _logger;

        public FollowingBLL(IFollowingDAL iFollowingDAL, IUserDAL iUserDal, INotificationBLL iNotificationBLL, INotificationModelBLL iNotificationModelBLL, IUserAchievementBLL iUserAchievementBLL)
        {
            _iFollowingDAL = iFollowingDAL;
            _iUserDAL = iUserDal;
            _INotificationBLL = iNotificationBLL;
            _INotificationModelBLL = iNotificationModelBLL;
            _iUserAchievementBLL = iUserAchievementBLL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void FollowUser(long myId, long followUserId)
        {
            var query = _iFollowingDAL.FollowUser(myId, followUserId);
            if (query == null)
            {
                FollowingRelation follow = new FollowingRelation();
                follow.FollowedId = followUserId;
                follow.FollowingId = myId;
                _iFollowingDAL.Create(follow);

                if (!_INotificationBLL.SendFollowNotification(follow))
                {
                    _logger.Error(string.Format("Error on sending follow notifications for: {0}, by: {1}.", follow.FollowedId, follow.FollowingId));
                }

                _INotificationModelBLL.Create(new NotificationModel
                {
                    FollowingRelationId = follow.Id,
                    Date = DateTime.UtcNow,
                    NotificationType = (int)Constants.NotificationType.FollowNotification,
                    UserSenderId = follow.FollowingId,
                    UserReceiverId = follow.FollowedId
                });
            }
            else
            {

                try
                {
                    _iFollowingDAL.DeleteFollowing(query);
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
                
            }

            int myFollowsCount = GetMyFollowings(myId).Count();
            int followUserFollowersCount = GetMyFollowers(followUserId).Count();

            _iUserAchievementBLL.RecalculateFollowsUserAchievements(myId, myFollowsCount);
            _iUserAchievementBLL.RecalculateFollowersUserAchievements(followUserId, followUserFollowersCount);
        }

        public bool CheckFollowing(long myId, long followUserId)
        {
            var query = _iFollowingDAL.FollowUser(myId, followUserId);
            if (query == null)
            {
                return false;
                
            }
            return true;
        }
        
        public bool CheckFollower(long followUserId, long myId)
        {
            var query = _iFollowingDAL.FollowUser(myId, followUserId);
            if (query == null)
            {
                return false;
                
            }
            return true;
        }

        public IEnumerable<UserModelHelper> GetMyFollowers(long userId)
        {
            IEnumerable<UserModelHelper> followers = _iFollowingDAL.GetMyFollowers(userId);
            return followers;
        }

        public IEnumerable<UserModelHelper> GetMyFollowings(long userId)
        {
            IEnumerable<UserModelHelper> followings = _iFollowingDAL.GetMyFollowings(userId);
            return followings;
        }

        public bool GetIsFollowedByUser(long postId, long currentUserId)
        {
            return _iFollowingDAL.GetIsFollowedByUser(postId, currentUserId);
        }
    }
}
