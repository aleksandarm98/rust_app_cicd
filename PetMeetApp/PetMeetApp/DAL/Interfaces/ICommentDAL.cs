using System.Collections.Generic;
using Amazon.S3.Model;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL.Interfaces
{
    public interface ICommentDAL : IBaseDAL<CommentModel>
    {
        
        public IEnumerable<CommentHelperModelIDTO> getPostComments(long postId);
        public bool RemoveComment(long commentId);
        public long GetCommentsCountByPostId(long postId);
    }
}