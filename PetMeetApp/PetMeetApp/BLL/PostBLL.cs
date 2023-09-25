using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PetMeetApp.Common.Extensions;
using NLog;
using PetMeetApp.DAL;
using PetMeetApp.Common.Interfaces;

namespace PetMeetApp.BLL
{
    public class PostBLL : IPostBLL
    {

        #region PostBLL Implementation

        private readonly IPostDAL _postDAL;
        private IFileService _fileService;
        private ILikeBLL _likeBLL;
        private ICommentBLL _commentBLL;
        private IFollowingBLL _followingBLL;
        private readonly IUserAchievementBLL _iUserAchievementBLL;
        private Logger _logger;

        public PostBLL(IPostDAL postDAL, IFileService fileService, ILikeBLL likeBLL, ICommentBLL commentBLL, IFollowingBLL followingBLL, IUserAchievementBLL iUserAchievementBLL)
        {
            _postDAL = postDAL;
            _fileService = fileService;
            _likeBLL = likeBLL;
            _commentBLL = commentBLL;
            _followingBLL = followingBLL;
            _iUserAchievementBLL = iUserAchievementBLL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IEnumerable<HomepagePostDTO> GetHomepagePosts(long userId)
        {
            var posts = _postDAL.GetHomepagePosts(userId).ToList();

            this.SetPostsNecessaryValues(posts, userId, setAWSFileURL: true);

            return posts;
        }

        public IEnumerable<HomepagePostDTO> GetRandomHomepagePosts(long currentUserId, int numberOfPostsToReturn)
        {
            var posts = _postDAL.GetRandomHomepagePosts(currentUserId, numberOfPostsToReturn).ToList();

            this.SetPostsNecessaryValues(posts, currentUserId, setAWSFileURL: true);

            return posts;
        }

        public IEnumerable<HomepagePostDTO> GetExplorePosts(long userId)
        {
            var posts = _postDAL.GetExplorePosts(userId).ToList();

            this.SetPostsNecessaryValues(posts, userId, setAWSFileURL: true);

            return posts;
        }

        public IEnumerable<HomepagePostDTO> GetPetPosts(long petId, long currentUserId) 
        {
            var posts = _postDAL.GetPetPosts(petId, currentUserId).ToList();

            this.SetPostsNecessaryValues(posts, currentUserId, setAWSFileURL: true);

            return posts;
        }

        public IEnumerable<HomepagePostDTO> GetReels(long userId)
        {
            var posts = _postDAL.GetReels(userId);

            this.SetPostsNecessaryValues(posts, userId, setAWSFileURL: true);

            return posts;
        }

        public IEnumerable<HomepagePostDTO> GetRandomReels(long currentUserId, int numberOfReelsToReturn)
        {
            var posts = _postDAL.GetRandomReels(currentUserId, numberOfReelsToReturn);

            this.SetPostsNecessaryValues(posts, currentUserId, setAWSFileURL: true);

            return posts;
        }

        public IEnumerable<HomepagePostDTO> GetReelsByPetId(long petId, long currentUserId)
        {
            var posts = _postDAL.GetReelsByPetId(petId, currentUserId);

            this.SetPostsNecessaryValues(posts, currentUserId, setAWSFileURL: true);

            return posts;
        }

        public bool UploadFile(FileModelDTO data)
        {
            try
            {
                string contentFolder = _postDAL.GetStorageUrlByCTID((long)data.ContentTypeId);
                var AWSKey = _fileService.SaveFileInAWSBucket(contentFolder, data.file);
                if (AWSKey != "")
                {
                    //_postDAL.UpdatePostContentUrl(data.Id, contentUrl);
                    PostData postData = new PostData();
                    postData.AWSKey = AWSKey;
                    postData.ContentTypeModelId = (long)data.ContentTypeId;
                    postData.PostId = data.Id;
                    _postDAL.UploadPostData(postData);
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
           
        }

        public async Task<IEnumerable<LocationsUploadPostDTO>> GetLocations(string searchFilter)
        {
            var location_strings = await this._postDAL.GetLocations(searchFilter);
            var lines = location_strings.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Skip(1);
            List<LocationsUploadPostDTO> locations = new List<LocationsUploadPostDTO>();
            foreach (var item in lines)
            {
                var values = item.Split(',');
                var model = new LocationsUploadPostDTO
                {
                    City = values[1],
                    CityAscii = values[2],
                    Lat = values[3],
                    Lng = values[4],
                    Country = values[5],
                    AdministrativeName = values[8],
                };

                locations.Add(model);
            }

            return locations.Where(x=> x.CityAscii.ToLower().StartsWith(searchFilter.ToLower()) || x.City.ToLower().StartsWith(searchFilter.ToLower()) || x.AdministrativeName.ToLower().StartsWith(searchFilter.ToLower())).Take(5);
        }

        public PostModel UploadPost(PostModelDTO data)
        {
            PostModel post = new PostModel();
            post.DateCreated = DateTime.UtcNow;
            post.Description = data.Description;
            post.PetId = data.PetId;
            post.UserId = data.UserId;
            post.CommentsAllowed = data.CommentsAllowed;
            post.IsActive = true;
            post.Latitude = data.Latitude;
            post.Longitude = data.Longitude;

            var createdPost = _postDAL.UploadPost(post);

            int userPostsCount = ((int)_postDAL.GetPostCountByUser(post.UserId));
            _iUserAchievementBLL.RecalculatePostsUserAchievements(post.UserId, userPostsCount);

            return createdPost;
        }

        public bool DeletePost(long postId)
        {
            long userId = _postDAL.GetById(postId).UserId;

            bool postDelete = _postDAL.DeletePost(postId);

            int userPostsCount = ((int)_postDAL.GetPostCountByUser(userId));
            _iUserAchievementBLL.RecalculatePostsUserAchievements(userId, userPostsCount);

            return postDelete;
        }

        public PostModel ChangePostInfo(PostModel post)
        {
            post = _postDAL.ChangePostInfo(post);
            if (post == null) return null;
            return post;
        }

        public List<PostData> GetPostDataByPostId(long postId)
        {
            List<PostData> postDatas = _postDAL.GetPostDataByPostId(postId);

            if (postDatas.SafeAny())
            {
                foreach (var postData in postDatas)
                {
                    postData.AWSKey = _fileService.GetAWSFileURL(postData.AWSKey);
                }
            }

            return postDatas;
        }

        public HomepagePostDTO GetHomepagePostById(long userId, long postId)
        {
            var post = _postDAL.GetHomepagePostById(userId, postId);

            this.SetPostsNecessaryValues(new List<HomepagePostDTO> { post }, userId, setAWSFileURL: true);

            return post;
        }

        public List<HomepagePostDTO> GetFollowingPosts(long userId, int numberOfPostsToReturn)
        {
            var posts = _postDAL.GetFollowingPosts(userId, numberOfPostsToReturn).ToList();
       
            this.SetPostsNecessaryValues(posts, userId, setAWSFileURL: true);

            return posts;
        }

        public List<HomepagePostDTO> RecommendedPosts(long userId, int numberOfPostsToReturn)
        {
            var posts = _postDAL.RecommendedPosts(userId, numberOfPostsToReturn).ToList();

            this.SetPostsNecessaryValues(posts, userId, setAWSFileURL: true);

            return posts;
        }

        #endregion

        #region Private methods

        private void SetPostsNecessaryValues(List<HomepagePostDTO> posts, long currentUserId, bool setAWSFileURL = false)
        {
            if (posts.SafeAny())
            {
                foreach (var post in posts)
                {
                    if (post != null)
                    {
                        if (post.Pet != null && setAWSFileURL)
                        {
                            post.Pet.Image = _fileService.GetAWSFileURL(post.Pet.Image);
                        }

                       // post.PostData ??= this.GetPostDataByPostId(post.Id);

                        post.NumberOfLikes = _likeBLL.GetLikesCountByPostId(post.Id);
                        post.NumberOfComments = _commentBLL.GetCommentsCountByPostId(post.Id);
                        post.IsFollowedByUser = _followingBLL.GetIsFollowedByUser(post.Id, currentUserId);
                    }
                }
            }
        }
        #endregion
    }
}