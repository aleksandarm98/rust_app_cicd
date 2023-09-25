using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class CommentDAL : BaseDAL<CommentModel>, ICommentDAL
    {
        public CommentDAL(Context context) : base(context)
        {
        }

        public long GetCommentsCountByPostId(long postId)
        {
            var result = _context.Comments
                                    .Where(c => c.PostId.Equals(postId))
                                    .Count();

            return result;
        }

        public IEnumerable<CommentHelperModelIDTO> getPostComments(long postId)
        {
            var query = from comment in _context.Comments
                join user in _context.Users on comment.UserId equals user.Id
                where comment.PostId == postId
                orderby comment.DatePublished descending 
                select new CommentHelperModelIDTO
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    Username = user.Username,
                    ProfileImageUrl = user.Image,
                    PostId = comment.PostId,
                    Content = comment.Content,
                    DatePublished = comment.DatePublished
                };

            return query;
        }

        public bool RemoveComment(long commentId)
        {
            var comment = _context.Comments.Where(x => x.Id == commentId).FirstOrDefault();
            if (comment != null)
            {
                //  comment.IsActive = false; not exists column in base 
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}