using System.Collections.Generic;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IFollowingBLL 
    {
        public void FollowUser(long myId, long followUserId);

        public bool CheckFollowing(long myId, long followUserId);

        public bool CheckFollower(long followUserId, long myId);

        public IEnumerable<UserModelHelper> GetMyFollowers(long userId);

        public IEnumerable<UserModelHelper> GetMyFollowings(long userId);
        public bool GetIsFollowedByUser(long postId, long currentUserId);
    }
}
