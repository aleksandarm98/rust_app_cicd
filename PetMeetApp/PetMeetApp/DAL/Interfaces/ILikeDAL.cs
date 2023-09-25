using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL.Interfaces
{
    public interface ILikeDAL: IBaseDAL<LikeModel>
    {

        public IEnumerable<PostModel> GetAllMyLikedPosts(long id);

        public IEnumerable<LikeModel> GetAllPostLikes(long postId);
        public IEnumerable<LikeModelDTO> GetListOfUsersThatLikedPost(long postId);

        public LikeModel Like(long userId, long postId);

        public void DeleteLike(LikeModel likeModel);
    }
}