using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IFollowingDAL: IBaseDAL<FollowingRelation>
    {   
        public FollowingRelation FollowUser(long myId, long followUserId);
        public IEnumerable<UserModelHelper> GetMyFollowers(long userId);
        public long GetFollowersCountByUser(long userId);
        public IEnumerable<UserModelHelper> GetMyFollowings(long userId);
        public long GetFollowingsCountByUser(long userId);
        public void DeleteFollowing(FollowingRelation followingRelation);
        public bool GetIsFollowedByUser(long postId, long currentUserId);
    }
}