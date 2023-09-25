using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface ILikeBLL
    {
        public IEnumerable<PostModel> GetAllMyLikedPosts(long userId);
        public IEnumerable<LikeModel> GetAllPostLikes(long postId);
        public IEnumerable<LikeModelDTO> GetListOfUsersThatLikedPost(long postId);
        public void Like(long userId, long postId);
        public void DeleteLike(long id);
        public LikeModel GetLike(long id);
        public LikeModel CheckLike(long userId, long postId);
        public long GetLikesCountByPostId(long postId);
    }
}