using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class LikeDAL : BaseDAL<LikeModel>, ILikeDAL
    {
        private readonly ILikeDAL _ILikeDAL;
        public LikeDAL(Context context) : base(context) 
        { }

        public void DeleteLike(LikeModel likeModel)
        {
            var query = _context.Likes
                                .Include(f => f.NotificationModel)
                                .FirstOrDefault();

            this.Delete(query);
        }

        public IEnumerable<LikeModel> GetAllPostLikes(long postId)
        {
            var likes = _context.Likes.Where(x => x.PostId.Equals(postId));

            return likes;
        }

        public IEnumerable<LikeModelDTO> GetListOfUsersThatLikedPost(long postId)
        {
            var likes = from like in _context.Likes
                        join user in _context.Users on like.UserId equals user.Id
                        where like.PostId.Equals(postId)
                        select new LikeModelDTO 
                        { 
                            UserId = user.Id,
                            Username = user.Username,
                            Name = user.Name,
                            ProfileImageUrl = user.Image
                        };

            

            return likes.ToList();
        }

        public LikeModel Like(long userId, long postId)
        {
            LikeModel query =  _context.Likes.Where(x => x.UserId.Equals(userId) && x.PostId.Equals(postId)).FirstOrDefault();

            return query;
        }

        IEnumerable<PostModel> ILikeDAL.GetAllMyLikedPosts(long id)
        {
            var myLikes = (
                from x in _context.Likes.Where(x => x.UserId.Equals(id))
                select x.PostId
                );

            var query = _context.Posts.Include(x => x.Likes).AsEnumerable();// Where(x => myLikes.Contains(x.Id) && x.IsActive == true);
                
           // query.Include(x => x.PostData);
            //query.Include(x => x.Likes);
           // query.Include(x => x.Comments);
            return query.ToList();
        }
    }
}