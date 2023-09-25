using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetMeetApp.Common.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL
{
    public class PostDAL : BaseDAL<PostModel>, IPostDAL
    {
        private readonly IFileService _fileService;

        // private readonly int REELS_BATCH_SIZE = 5;
        private readonly int EXPLORE_BATCH_SIZE = 20;

        private readonly int FIRST_ITEM = 0;

        public PostDAL(Context context, IFileService fileService) : base(context)
        {
            _fileService = fileService;
        }

        // TODO: Optimization and refactoring needed
        public IEnumerable<HomepagePostDTO> GetHomepagePosts(long userId)
        {
            // Get posts by users that current user follow
            var query1 = (from post1 in _context.Posts.Include(x => x.PostData)
                          join postData1 in _context.PostData on post1.Id equals postData1.PostId
                          join userPet1 in _context.UserPetRelation on post1.PetId equals userPet1.PetId
                          join user1 in _context.Users on userPet1.UserId equals user1.Id
                          join following in _context.Followers on user1.Id equals following.FollowedId
                          join pet in _context.Pets on userPet1.PetId equals pet.Id
                          where following.FollowingId == userId && post1.IsActive == true && (postData1.ContentTypeModelId == 1 || postData1.ContentTypeModelId == 2)
                          select new HomepagePostDTO
                          {
                              Id = post1.Id,
                              PostData = post1.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                              DateCreated = post1.DateCreated,
                              Description = post1.Description,
                              Pet = pet,
                              UserName = user1.Username,
                              UserId = user1.Id,
                              PetId = pet.Id,
                              IsLikedByUser = (
                                  from like in _context.Likes
                                  where (like.UserId == userId && like.PostId == post1.Id)
                                  select like
                              ).Any()
                          }).AsEnumerable();

            // Get posts by current user
            var query2 = (from post2 in _context.Posts.Include(x => x.PostData)
                          join postData2 in _context.PostData on post2.Id equals postData2.PostId
                          join user2 in _context.Users on post2.UserId equals user2.Id
                          join pet2 in _context.Pets on post2.PetId equals pet2.Id
                          where post2.UserId == userId && post2.IsActive == true && (postData2.ContentTypeModelId == 1 || postData2.ContentTypeModelId == 2)
                          select new HomepagePostDTO
                          {
                              Id = post2.Id,
                              PostData = post2.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                              DateCreated = post2.DateCreated,
                              Description = post2.Description,
                              Pet = pet2,
                              UserName = user2.Username,
                              UserId = user2.Id,
                              PetId = pet2.Id,
                              IsLikedByUser = (
                                  from like in _context.Likes
                                  where (like.UserId == userId && like.PostId == post2.Id)
                                  select like
                              ).Any()
                          }).AsEnumerable();

            query1 = query1.DistinctBy(p => p.Id);
            query2 = query2.DistinctBy(p => p.Id);

            return query1.Union(query2).OrderByDescending(x => x.DateCreated);
        }

        public IEnumerable<HomepagePostDTO> GetPetPosts(long petId, long userId)
        {
            var result = (from post in _context.Posts.Include(x => x.PostData)
                          join postData in _context.PostData on post.Id equals postData.PostId
                          join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                          join user in _context.Users on userPet.UserId equals user.Id
                          join pet in _context.Pets on userPet.PetId equals pet.Id
                          where post.PetId == petId && post.IsActive == true && postData.ContentTypeModelId == 1
                          orderby post.DateCreated descending
                          select new HomepagePostDTO
                          {
                              Id = post.Id,
                              PostData = post.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                              DateCreated = post.DateCreated,
                              Description = post.Description,
                              Pet = pet,
                              UserName = user.Username,
                              UserId = user.Id,
                              PetId = pet.Id,
                              IsLikedByUser = (
                                  from like in _context.Likes
                                  where (like.UserId == userId && like.PostId == post.Id)
                                  select like
                              ).Any()
                          }).AsEnumerable(); ;

            result = result.DistinctBy(p => p.Id);
            return result;
        }

        public long GetPostCountByUser(long userId)
        {
            var result = (from post in _context.Posts
                          where post.UserId == userId && post.IsActive == true
                          select post).Count();

            return result;
        }

        public List<HomepagePostDTO> GetReels(long userId)
        {
            var result = (from post in _context.Posts.Include(x => x.PostData)
                          join postData in _context.PostData on post.Id equals postData.PostId
                          join contentType in _context.ContentTypes on postData.ContentTypeModelId equals contentType.Id
                          join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                          join user in _context.Users on userPet.UserId equals user.Id
                          join pet in _context.Pets on userPet.PetId equals pet.Id
                          where contentType.Id == 3 && post.IsActive == true
                          select new HomepagePostDTO
                          {
                              Id = post.Id,
                              PostData = post.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                              DateCreated = post.DateCreated,
                              Description = post.Description,
                              Pet = pet,
                              UserName = user.Username,
                              UserId = user.Id,
                              PetId = pet.Id,
                              IsLikedByUser = (
                                  from like in _context.Likes
                                  where (like.UserId == userId && like.PostId == post.Id)
                                  select like
                              ).Any()
                          }).ToList();

            return result;
        }

        public List<HomepagePostDTO> GetReelsByPetId(long petId, long currentUserId)
        {
            var result = (from post in _context.Posts.Include(x => x.PostData)
                          join postData in _context.PostData on post.Id equals postData.PostId
                          join contentType in _context.ContentTypes on postData.ContentTypeModelId equals contentType.Id
                          join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                          join user in _context.Users on userPet.UserId equals user.Id
                          join pet in _context.Pets on userPet.PetId equals pet.Id
                          where contentType.Id == 3 && pet.Id == petId && post.IsActive == true
                          select new HomepagePostDTO
                          {
                              Id = post.Id,
                              PostData = post.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                              DateCreated = post.DateCreated,
                              Description = post.Description,
                              Pet = pet,
                              UserName = user.Username,
                              UserId = user.Id,
                              PetId = pet.Id,
                              IsLikedByUser = (
                                  from like in _context.Likes
                                  where (like.UserId == currentUserId && like.PostId == post.Id)
                                  select like
                              ).Any()
                          }).ToList();

            return result;
        }

        public List<HomepagePostDTO> GetExplorePosts(long userId)
        {
            var result = (from post in _context.Posts
                          join postData in _context.PostData on post.Id equals postData.PostId
                          join contentType in _context.ContentTypes on postData.ContentTypeModelId equals contentType.Id
                          join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                          join user in _context.Users on userPet.UserId equals user.Id
                          join following in _context.Followers on user.Id equals following.FollowedId
                          join pet in _context.Pets on userPet.PetId equals pet.Id
                          where following.FollowingId != userId && post.IsActive == true && (contentType.Id == 1 || contentType.Id == 2)
                          select new HomepagePostDTO
                          {
                              Id = post.Id,
                              PostData = post.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                              DateCreated = post.DateCreated,
                              Description = post.Description,
                              Pet = pet,
                              UserName = user.Username,
                              UserId = user.Id,
                              PetId = pet.Id,
                              IsLikedByUser = (
                                  from like in _context.Likes
                                  where (like.UserId == userId && like.PostId == post.Id)
                                  select like
                              ).Any(),
                              ContentTypeId = contentType.Id,
                          })
              .OrderBy(x => Guid.NewGuid())
              .ToList()
              .Distinct(new HomepagePostDTOComparer())
              .Take(EXPLORE_BATCH_SIZE)
              .ToList();

            return result;
        }

        public List<PostData> GetPostDataByPostId(long postId)
        {
            var result = (from post in _context.Posts
                          join postData in _context.PostData on post.Id equals postData.PostId
                          where post.Id == postId

                          select new PostData
                          {
                              Id = postData.Id,
                              PostId = postData.PostId,
                              ContentTypeModelId = postData.ContentTypeModelId
                          }).ToList();

            return result;
        }

        public string GetStorageUrlByCTID(long CTID)
        {
            var type = _context.ContentTypes.Where(x => x.Id == CTID).FirstOrDefault();
            if (type == null) return null;
            else return type.StorageUrl;
        }

        public PostModel UploadPost(PostModel data)
        {
            var res = base.Create(data);
            return res;
        }

        public async Task<string> GetLocations(string searchQuery)
        {
            return await this._fileService.GetFileFromAWS("maps-petmeet", "new_post_maps/mapsAddPost.csv");
        }

        public bool UploadPostData(PostData postData)
        {
            try
            {
                _context.PostData.Add(postData);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePost(long postId)
        {
            var post = _context.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if (post != null)
            {
                post.IsActive = false;
                base.Update(post);
                return true;
            }
            return false;
        }

        public PostModel GetWithUser(long postId)
        {
            return _context.Posts.Where(post => post.Id == postId).Include(post => post.User).FirstOrDefault();
        }

        public PostModel ChangePostInfo(PostModel post)
        {
            var selectedPost = _context.Posts.Where(x => x.Id == post.Id).FirstOrDefault();

            if (selectedPost == null)
            {
                return null;
            }

            if (post.Description != null)
                selectedPost.Description = post.Description;
            selectedPost.CommentsAllowed = post.CommentsAllowed;
            selectedPost.Latitude = post.Latitude;
            selectedPost.Longitude = post.Longitude;

            _context.SaveChanges();

            return selectedPost;
        }

        public HomepagePostDTO GetHomepagePostById(long userId, long postId)
        {
            var query = (from post in _context.Posts.Include(x => x.PostData)
                         join postData in _context.PostData on post.Id equals postData.PostId
                         join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                         join user in _context.Users on userPet.UserId equals user.Id
                         join following in _context.Followers on user.Id equals following.FollowedId
                         join pet in _context.Pets on userPet.PetId equals pet.Id
                         where post.Id == postId
                         select new HomepagePostDTO
                         {
                             Id = post.Id,
                             PostData = post.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                             DateCreated = post.DateCreated,
                             Description = post.Description,
                             Pet = pet,
                             UserName = user.Username,
                             UserId = user.Id,
                             PetId = pet.Id,
                             IsLikedByUser = (
                                 from like in _context.Likes
                                 where (like.UserId == userId && like.PostId == post.Id)
                                 select like
                             ).Any()
                         });

            return query.FirstOrDefault();
        }

        public IEnumerable<HomepagePostDTO> GetRandomHomepagePosts(long currentUserId, int numberOfPostsToReturn)
        {
            var query = (from post1 in _context.Posts.Include(x => x.PostData)
                         join postData1 in _context.PostData on post1.Id equals postData1.PostId
                         join userPet1 in _context.UserPetRelation on post1.PetId equals userPet1.PetId
                         join user1 in _context.Users on userPet1.UserId equals user1.Id
                         join following in _context.Followers on user1.Id equals following.FollowedId
                         join pet in _context.Pets on userPet1.PetId equals pet.Id
                         where following.FollowingId != currentUserId && post1.IsActive == true && (postData1.ContentTypeModelId == 1 || postData1.ContentTypeModelId == 2)
                         select new HomepagePostDTO
                         {
                             Id = post1.Id,
                             PostData = post1.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                             DateCreated = post1.DateCreated,
                             Description = post1.Description,
                             Pet = pet,
                             UserName = user1.Username,
                             UserId = user1.Id,
                             PetId = pet.Id,
                             IsLikedByUser = (
                                 from like in _context.Likes
                                 where (like.UserId == currentUserId && like.PostId == post1.Id)
                                 select like
                             ).Any()
                         }).AsEnumerable();

            return query.OrderBy(o => Guid.NewGuid())
                .Take(numberOfPostsToReturn);
        }

        public List<HomepagePostDTO> GetRandomReels(long currentUserId, int numberOfReelsToReturn)
        {
            var query = (from post in _context.Posts.Include(x => x.PostData)
                         join postData in _context.PostData on post.Id equals postData.PostId
                         join contentType in _context.ContentTypes on postData.ContentTypeModelId equals contentType.Id
                         join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                         join user in _context.Users on userPet.UserId equals user.Id
                         join pet in _context.Pets on userPet.PetId equals pet.Id
                         where contentType.Id == 3 && post.IsActive == true
                         select new HomepagePostDTO
                         {
                             Id = post.Id,
                             PostData = post.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                             DateCreated = post.DateCreated,
                             Description = post.Description,
                             Pet = pet,
                             UserName = user.Username,
                             UserId = user.Id,
                             PetId = pet.Id,
                             IsLikedByUser = (
                                 from like in _context.Likes
                                 where (like.UserId == currentUserId && like.PostId == post.Id)
                                 select like
                             ).Any()
                         }).ToList();

            return query.OrderBy(o => Guid.NewGuid())
                .Take(numberOfReelsToReturn).ToList();
        }

        public IEnumerable<HomepagePostDTO> GetFollowingPosts(long userId, int numberOfPostsToReturn)
        {
            var query = (from post in _context.Posts.Include(x => x.PostData)
                         join postData in _context.PostData on post.Id equals postData.PostId
                         join userPet in _context.UserPetRelation on post.PetId equals userPet.PetId
                         join user in _context.Users on userPet.UserId equals user.Id
                         join pet in _context.Pets on userPet.PetId equals pet.Id
                         where (post.UserId == userId || _context.Followers.Any(f => f.FollowingId == userId && f.FollowedId == user.Id))
                               && post.IsActive == true
                         select new HomepagePostDTO
                         {
                             Id = post.Id,
                             PostData = post.PostData.Select(x => new PostData
                             {
                                 Id = x.Id,
                                 PostId = x.PostId,
                                 AWSKey = _fileService.GetAWSFileURL(x.AWSKey),
                                 ContentTypeModelId = x.ContentTypeModelId
                             }),
                             DateCreated = post.DateCreated,
                             Description = post.Description,
                             Pet = pet,
                             UserName = user.Username,
                             UserId = user.Id,
                             PetId = pet.Id,
                             IsLikedByUser = _context.Likes.Any(l => l.UserId == userId && l.PostId == post.Id)
                         }).AsEnumerable();

            query = query.DistinctBy(p => p.Id).OrderByDescending(x => x.DateCreated).Take(numberOfPostsToReturn);

            return query.OrderByDescending(x => x.DateCreated);
        }

        public IEnumerable<HomepagePostDTO> RecommendedPosts(long userId, int numberOfPostsToReturn)
        {
            var query = (from post1 in _context.Posts.Include(x => x.PostData)
                         join postData1 in _context.PostData on post1.Id equals postData1.PostId
                         join userPet1 in _context.UserPetRelation on post1.PetId equals userPet1.PetId
                         join user1 in _context.Users on userPet1.UserId equals user1.Id
                         join following in _context.Followers on user1.Id equals following.FollowedId
                         join pet in _context.Pets on userPet1.PetId equals pet.Id
                         where following.FollowingId != userId && post1.IsActive == true 
                         select new HomepagePostDTO
                         {
                             Id = post1.Id,
                             PostData = post1.PostData.ToList().Select(x => new PostData { Id = x.Id, PostId = x.PostId, AWSKey = _fileService.GetAWSFileURL(x.AWSKey), ContentTypeModelId = x.ContentTypeModelId }),
                             DateCreated = post1.DateCreated,
                             Description = post1.Description,
                             Pet = pet,
                             UserName = user1.Username,
                             UserId = user1.Id,
                             PetId = pet.Id,
                             IsLikedByUser = (
                                 from like in _context.Likes
                                 where (like.UserId == userId && like.PostId == post1.Id)
                                 select like
                             ).Any()
                         }).AsEnumerable();

            return query.OrderBy(o => Guid.NewGuid())
                .Take(numberOfPostsToReturn);
        }
    }
}