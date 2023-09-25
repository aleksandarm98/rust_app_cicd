using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface ICommentBLL
    {
        public CommentModel getComment(long commentId);
        
        public IEnumerable<CommentHelperModelIDTO> getPostComments(long postId);

        public void CommentPost(CommentInputDTO comment);

        public bool RemoveComment(long commentId);
        public long GetCommentsCountByPostId(long postId);
    }
}
