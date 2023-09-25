using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IPostDAL : IBaseDAL<PostModel>
    {
        public PostModel UploadPost(PostModel data);
        
        public string GetStorageUrlByCTID(long CTID);
        public IEnumerable<HomepagePostDTO> GetHomepagePosts(long userId);
        public IEnumerable<HomepagePostDTO> GetPetPosts(long petId, long userId);
        public long GetPostCountByUser(long userId);
        public Task<string> GetLocations(string searchQuery);
        public List<HomepagePostDTO> GetReels(long userId);
        public List<HomepagePostDTO> GetRandomReels(long currentUserId, int numberOfReelsToReturn);
        public List<HomepagePostDTO> GetReelsByPetId(long petId, long currentUserId);
        public List<HomepagePostDTO> GetExplorePosts(long userId);
        //Deprecated
        //public bool UpdatePostContentUrl(long id, string contentUrl);
        public bool UploadPostData(PostData postData);
        public bool DeletePost(long postId);
        public List<PostData> GetPostDataByPostId(long postId);
        public PostModel GetWithUser(long postId);
        public PostModel ChangePostInfo(PostModel post);
        public HomepagePostDTO GetHomepagePostById(long userId, long postId);
        public IEnumerable<HomepagePostDTO> GetRandomHomepagePosts(long currentUserId, int numberOfPostsToReturn);
        public IEnumerable<HomepagePostDTO> GetFollowingPosts(long userId, int numberOfPostsToReturn);
        public IEnumerable<HomepagePostDTO> RecommendedPosts(long userId, int numberOfPostsToReturn);


    }
}