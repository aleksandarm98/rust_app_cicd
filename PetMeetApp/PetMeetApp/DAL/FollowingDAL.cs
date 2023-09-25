using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class FollowingDAL : BaseDAL<FollowingRelation>, IFollowingDAL
    {
        private readonly ILikeDAL _ILikeDAL;
        public FollowingDAL(Context context) : base(context)
        {
        }

        public void DeleteFollowing(FollowingRelation followingRelation)
        {
            var query = _context.Followers
                                .Include(f => f.NotificationModel)
                                .Where(x => x.Id == followingRelation.Id)
                                .FirstOrDefault();

            this.Delete(query);
        }

        public FollowingRelation FollowUser(long myId, long followUserId)
        {
            FollowingRelation query =  _context.Followers.Where(x => x.FollowingId.Equals(myId) && x.FollowedId.Equals(followUserId)).FirstOrDefault();
            
            return query;
            
        }

        public bool GetIsFollowedByUser(long postId, long currentUserId)
        {
            var result = from followers in _context.Followers
                         join posts in _context.Posts on followers.FollowedId equals posts.UserId
                         where posts.Id == postId && followers.FollowingId == currentUserId
                         select followers;

            return result.Any();
        }

        public IEnumerable<UserModelHelper> GetMyFollowers(long userId)
        {
            try
            {
                var result = from followers in _context.Followers
                             join users in _context.Users on followers.FollowingId equals users.Id
                             where followers.FollowedId == userId
                             select new UserModelHelper
                             {
                                 UserId = users.Id,
                                 Username = users.Username,
                                 Image = users.Image,
                                 ImageData = users.ImageData,
                                 IsFollowed  = (
                                     from followers in _context.Followers
                                     where(followers.FollowingId == userId && followers.FollowedId == users.Id)
                                     select followers
                                 ).Any()
                             };


                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public long GetFollowersCountByUser(long userId)
        {
            var result = (from followers in _context.Followers
                          join users in _context.Users on followers.FollowedId equals users.Id
                          where followers.FollowedId == userId
                          select followers).Count();

            return result;
        }

        public IEnumerable<UserModelHelper> GetMyFollowings(long userId)
        {
            var result= from followers in _context.Followers
                join users in _context.Users on followers.FollowedId equals users.Id
                where followers.FollowingId == userId
                select new UserModelHelper
                {
                    UserId = users.Id,
                    Username = users.Username,
                    Image = users.Image,
                    ImageData = users.ImageData,
                    IsFollowed  = (
                        from followers in _context.Followers
                        where(followers.FollowingId == userId && followers.FollowedId == users.Id)
                        select followers
                    ).Any()
                };

            return result;
        }

        public long GetFollowingsCountByUser(long userId)
        {
            var result = (from followers in _context.Followers
                         join users in _context.Users on followers.FollowedId equals users.Id
                         where followers.FollowingId == userId
                         select followers).Count();

            return result;
        }
    }
}