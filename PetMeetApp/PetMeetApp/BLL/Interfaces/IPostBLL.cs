using Microsoft.AspNetCore.Mvc;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IPostBLL
    {
        public PostModel UploadPost(PostModelDTO data);
        public IEnumerable<HomepagePostDTO> GetHomepagePosts(long userId);
        public IEnumerable<HomepagePostDTO> GetRandomHomepagePosts(long currentUserId, int numberOfPostsToReturn);
        public IEnumerable<HomepagePostDTO> GetPetPosts(long petId, long currentUserId);
        public bool UploadFile(FileModelDTO data);
        public Task<IEnumerable<LocationsUploadPostDTO>> GetLocations(string searchFilter);
        public IEnumerable<HomepagePostDTO> GetReels(long userId);
        public IEnumerable<HomepagePostDTO> GetRandomReels(long currentUserId, int numberOfReelsToReturn);
        public IEnumerable<HomepagePostDTO> GetReelsByPetId(long userId, long currentUserId);
        public IEnumerable<HomepagePostDTO> GetExplorePosts(long userId);
        public bool DeletePost(long postId);
        PostModel ChangePostInfo(PostModel post);
        public List<PostData> GetPostDataByPostId(long postId);
        public HomepagePostDTO GetHomepagePostById(long userId, long postId);
        public List<HomepagePostDTO> GetFollowingPosts(long userId, int numberOfPostsToReturn);
        public List<HomepagePostDTO> RecommendedPosts(long userId, int numberOfPostsToReturn);
    }
}