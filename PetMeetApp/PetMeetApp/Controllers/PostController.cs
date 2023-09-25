using Microsoft.AspNetCore.Mvc;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PetMeetApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostBLL _postBLL;
        private readonly ICommentBLL _iCommentBLL;

        public PostController(IPostBLL postBLL, ICommentBLL ICommentBLL)
        {
            _postBLL = postBLL;
            _iCommentBLL = ICommentBLL;
        }

        /// <summary>
        /// Upload post
        /// </summary>
        /// <param name="data"></param>
        /// <returns>
        /// boolean
        /// </returns>
        [HttpPost("UploadPost")]
        [Authorize]
        public IActionResult UploadPost([FromBody] PostModelDTO data)
        {
            var res = _postBLL.UploadPost(data);

            return Ok(res);
        }

        /// <summary>
        /// Upload file for post
        /// </summary>
        /// <returns>
        /// True or false
        /// </returns>
        [HttpPost("UploadFile")]
        [AllowAnonymous]
        public IActionResult UploadFile([FromForm] FileModelDTO data)
        {
            var res =_postBLL.UploadFile(data);
            return Ok(res);
        }

        /// <summary>
        /// Get homepage posts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of posts
        /// </returns>
        [HttpGet("GetHomepagePosts/{userId}")]
        [Authorize]
        public IActionResult GetHomepagePosts(long userId)
        {
            var res = _postBLL.GetHomepagePosts(userId);

            return Ok(res);
        }

        /// <summary>
        /// Get random homepage posts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of posts
        /// </returns>
        [HttpGet("[action]/{currentUserId}/{numberOfPostsToReturn}")]
        [Authorize]
        public IActionResult GetRandomHomepagePosts(long currentUserId, int numberOfPostsToReturn)
        {
            var res = _postBLL.GetRandomHomepagePosts(currentUserId, numberOfPostsToReturn);

            return Ok(res);
        }

        [HttpPost("CommentPost")]
        [Authorize]
        public IActionResult CommentPost([FromBody] CommentInputDTO data)
        {
            try
            {
                _iCommentBLL.CommentPost(data);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to comment this post." });
            }
            
        }

        [HttpDelete("DeleteComment/{commentId}")]
        [Authorize]
        public IActionResult DeleteComment(long commentId)
        {
            try
            {
                _iCommentBLL.RemoveComment(commentId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to delete this comment." });
            }
            
        }

        [HttpDelete("DeletePost/{postId}")]
        [Authorize]
        public IActionResult DeletePost(long postId)
        {
            try
            {
                var res= _postBLL.DeletePost(postId);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to delete this post." });
            }
        }

        [HttpGet("GetPostComments/{postId}")]
        [Authorize]
        public IActionResult GetPostComments(long postId)
        {
            IEnumerable<CommentHelperModelIDTO> res = _iCommentBLL.getPostComments(postId);
            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest( new {message = "Unable to show post's comments!"});
            
        }

        /// <summary>
        /// Get Pet posts
        /// </summary>
        /// <param name="petId"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// List of posts
        /// </returns>

        [HttpGet("GetPetPosts/{petId}/{currentUserId}")]
        [Authorize]
        public IActionResult GetPetPosts(long petId, long currentUserId)
        {
            var res = _postBLL.GetPetPosts(petId, currentUserId);

            return Ok(res);
        }
        
        [HttpGet("GetLocations/{searchFilter}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocations(string searchFilter)
        {
            var locations = await this._postBLL.GetLocations(searchFilter); 
            
            return Ok(locations);
        }

        /// <summary>
        /// Get Reels
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of reels
        /// </returns>
        [HttpGet("GetReels/{userId}")]
        [Authorize]
        public IActionResult GetReels(long userId)
        {
            var res = _postBLL.GetReels(userId);
            return Ok(res);
        }

        /// <summary>
        /// Get Random Reels
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of reels
        /// </returns>
        [HttpGet("[action]/{currentUserId}/{numberOfReelsToReturn}")]
        [Authorize]
        public IActionResult GetRandomReels(long currentUserId, int numberOfReelsToReturn)
        {
            var res = _postBLL.GetRandomReels(currentUserId, numberOfReelsToReturn);
            return Ok(res);
        }

        /// <summary>
        /// Get Reels
        /// </summary>
        /// <param name="petId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>
        /// List of reels
        /// </returns>
        [HttpGet("GetReelsByPetId/{petId}/{currentUserId}")]
        public IActionResult GetReelsByPetId(long petId, long currentUserId)
        {
            var res = _postBLL.GetReelsByPetId(petId, currentUserId);
            return Ok(res);
        }
        
        /// <summary>
        /// Get Reels
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of explore posts
        /// </returns>
        [HttpGet("GetExplorePosts/{userId}")]
        [Authorize]
        public IActionResult GetExplorePosts(long userId)
        {
            var res = _postBLL.GetExplorePosts(userId);
            return Ok(res);
        }
        
        /// <summary>	
        /// Change post info (description)	
        /// </summary>	
        /// <param name="post"></param>	
        /// <returns> post data</returns>	
        [HttpPost("ChangePostInfo")]	
        [Authorize]
        public IActionResult ChangePostInfo([FromBody] PostModel post)	
        {	
            var result = _postBLL.ChangePostInfo(post);	
            return Ok(result);	
        }

        /// <summary>
        /// Get homepage post by identifier
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// List of posts
        /// </returns>
        [HttpGet("[action]/{userId}/{postId}")]
        public IActionResult GetHomepagePostById(long userId, long postId)
        {
            var res = _postBLL.GetHomepagePostById(userId, postId);
            return Ok(res);
        }

        /// <summary>
        /// Get posts for user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="numberOfPostsToReturn"></param>
        /// <returns>
        /// List of posts
        /// </returns>
        [HttpGet("GetFollowingPosts/{userId}/{numberOfPostsToReturn}")]
        public IActionResult GetFollowingPosts(long userId, int numberOfPostsToReturn)
        {
            var res = _postBLL.GetFollowingPosts(userId, numberOfPostsToReturn);

            return Ok(res);
        }
        
        /// <summary>
        /// Get recommended posts for user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="numberOfPostsToReturn"></param>
        /// <returns>
        /// List of posts
        /// </returns>
        [HttpGet("RecommendedPosts/{userId}/{numberOfPostsToReturn}")]
        public IActionResult RecommendedPosts(long userId, int numberOfPostsToReturn)
        {
            var res = _postBLL.RecommendedPosts(userId, numberOfPostsToReturn);

            return Ok(res);
        }
    }
}
