using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly ILikeBLL _iLikeBll;
        
        public LikeController(ILikeBLL likeBLL)
        {
            _iLikeBll = likeBLL;
        }
       
        [HttpGet]
        [Route("[action]/{postId}")]
        [Authorize]
        public ActionResult GetAllPostLikes(long postId)
        {
            IEnumerable<LikeModel> likes = null;
            try
            {
                likes = _iLikeBll.GetAllPostLikes(postId);
            }
            catch (Exception e)
            {
                return BadRequest(new {message = "Error when calling GetAllPostLIkes"});
            }

            return Ok(likes);
        }

        [HttpGet]
        [Route("[action]/{postId}")]
        [Authorize]
        public ActionResult GetListOfUsersThatLikedPost(long postId)
        {
            IEnumerable<LikeModelDTO> likes = null;
            try
            {
                likes = _iLikeBll.GetListOfUsersThatLikedPost(postId);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error when calling GetListOfUsersThatLikedPost" });
            }

            return Ok(likes);
        }

        [HttpGet]
        [Route("[action]/{userId}/{postId}")]
        [Authorize]
        public ActionResult CheckLike(long userId, long postId)
        {
            
            
            var result = _iLikeBll.CheckLike(userId, postId);
            if (result != null)
            {
                
                return Ok(true);   
            }
            //Todo: Error here, instead of False it returns Code 500!!!!
            return Ok(false);


        }

        [HttpPost]
        [Route("[action]/{userId}/{postId}")]
        [Authorize]
        public ActionResult LikePost(long userId, long postId)
        {
            try
            {
                _iLikeBll.Like(userId, postId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {message = string.Format("Cannot like this post. Error: {0}", e)});
            }
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public ActionResult GetMyLikedPosts(long userId)
        {
            IEnumerable<PostModel> likedPosts = null;
            
            try
            {
                likedPosts = _iLikeBll.GetAllMyLikedPosts(userId);
            }
            catch (Exception e)
            {
                return BadRequest(new {message = string.Format("Error when returning all user's liked posts. Error: {0}", e)});
            }

            return Ok(likedPosts);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public ActionResult Delete(long id)
        {
            try
            {
                this._iLikeBll.DeleteLike(id);
            }
            catch (Exception e)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}