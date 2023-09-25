using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetMeetApp.BLL;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;


namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserBLL _userBLL;
        private readonly IFollowingBLL _iFollowingBll;
        private INotificationModelBLL _INotificationModelBLL;
        private readonly IChatBLL _iChatBLL;
        private readonly IUserAchievementBLL _iUserAchievementBLL;
        private readonly IUserReferralCodeBLL _iUserReferralCodeBLL;

        public UserController(IUserBLL userBLL, IFollowingBLL iFollowingBll, INotificationModelBLL iNotificationModelBLL, IChatBLL iChatBLL, IUserAchievementBLL iUserAchievementBLL, IUserReferralCodeBLL iUserReferralCodeBLL)
        {
            _userBLL = userBLL;
            _iFollowingBll = iFollowingBll;
            _INotificationModelBLL = iNotificationModelBLL;
            _iChatBLL = iChatBLL;
            _iUserAchievementBLL = iUserAchievementBLL;
            _iUserReferralCodeBLL = iUserReferralCodeBLL;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data"></param>
        /// <returns> user or null</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel data)
        {
            string token = _userBLL.Login(data);

            if (token != null)
            {
                return Ok(token);
            }
            return BadRequest();
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="data"></param>
        /// <returns> user or null</returns>
        [HttpPost("[action]")]
        [Authorize]
        public IActionResult Logout([FromBody] LogoutModel data)
        {
            try
            {
                _userBLL.Logout(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// UserModel or null
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            var res = _userBLL.Register(user);
            if (res !=null)
            {
                return Ok(res);
            }
            return BadRequest();
        }
        /// <summary>
        /// Check if username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns> true or false</returns>
        [HttpGet("UsernameExists/{username}")]
        [AllowAnonymous]
        public IActionResult UsernameExists(string username)
        {
            bool result = _userBLL.UsernameExists(username);
            return Ok(result);
        }

        /// <summary>
        /// Check if email exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns> true or false</returns>
        [HttpGet("EmailExists/{email}")]
        [AllowAnonymous]
        public IActionResult EmailExists(string email)
        {
            bool result = _userBLL.EmailExists(email);
            return Ok(result);
        }

        /// <summary>
        /// Change user image
        /// </summary>
        /// <param name="user"></param>
        /// <returns> user data</returns>
        [HttpPost("ChangeImage")]
        [Authorize]
        public IActionResult ChangeImage([FromForm] FileModelDTO data)
        {
            var result = _userBLL.ChangeImage(data);
            return Ok(result);
        }

        /// <summary>
        /// Change username
        /// </summary>
        /// <param name="user"></param>
        /// <returns> user data</returns>
        [HttpPost("ChangeInfo")]
        [Authorize]
        public IActionResult ChangeInfo([FromBody] UserModel user)
        {
            var result = _userBLL.ChangeInfo(user);
            return Ok(result);
        }
        
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="user"></param>
        /// <returns> user data</returns>
        [HttpPost("ChangePassword")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] UserModel user)
        {
            var result = _userBLL.ChangePassword(user);
            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("[action]/{userId1}/{userId2}")]
        [Authorize]
        public ActionResult FollowUser(long userId1, long userId2)
        {

            try
            {
                this._iFollowingBll.FollowUser(userId1, userId2);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {message="Cannot like this post"});
            }

        }

        [HttpGet]
        [Route("[action]/{userId1}/{userId2}")]
        [Authorize]
        public ActionResult CheckFollowing(long userId1, long userId2)
        {

            try
            {
                bool result = this._iFollowingBll.CheckFollowing(userId1, userId2);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Cannot get this users!" });
            }
        }

        [HttpGet("GetUser/{id}")]
        [Authorize]
        public IActionResult GetUser(long id)
        {
            var result = _userBLL.GetUser(id);
            return Ok(result);
        }
        
        [HttpGet("GetUserProfile/{id}")]
        [Authorize]
        public IActionResult GetUserProfile(long id)
        {
            var result = _userBLL.GetUserProfile(id);
            return Ok(result);
        }
        
        

        [HttpGet]
        [Route("[action]/{searchFilter}")]
        [Authorize]
        public ActionResult SearchUsers(string searchFilter)
        {
            try
            {
                IEnumerable<SearchModel> searchedUsers = _userBLL.SearchUsers(searchFilter);
                return Ok(searchedUsers);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Cannot get users!" });
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("[action]/{currentUser}/{numberOfRandomUsers}")]
        public ActionResult GetRandomUsers(long currentUser, int numberOfRandomUsers)
        {
            try
            {
                var result = _userBLL.GetRandomUsers(currentUser, numberOfRandomUsers);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Cannot get users!" });
            }
        }

        [HttpGet]
        [Route("[action]/{userId1}/{userId2}")]
        [Authorize]
        public ActionResult CheckFollower(long userId1, long userId2)
        {

            try
            {
                bool result =  this._iFollowingBll.CheckFollower(userId1, userId2);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new {message="Cannot get this users!"});
            }

        }
        
        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public ActionResult GetMyFollowers(long userId)
        {

            try
            {
                
                IEnumerable<UserModelHelper> followers =  _iFollowingBll.GetMyFollowers(userId);
                return Ok(followers);
            }
            catch (Exception e)
            {
                return BadRequest(new {message="Cannot get user's followers!"});
            }
        }
        
        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public ActionResult GetMyFollowings(long userId)
        {

            try
            {
                IEnumerable<UserModelHelper> followings =  _iFollowingBll.GetMyFollowings(userId);
                return Ok(followings);
            }
            catch (Exception e)
            {
                return BadRequest(new {message="Cannot get user's followings!"});
            }
        }

        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public ActionResult GetNotificationsByUserId(long userId)
        {
            try
            {
                IEnumerable<NotificationModel> notificationModels = _INotificationModelBLL.GetNotificationsByUserId(userId);
                return Ok(notificationModels);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Cannot get user's notifications!" });
            }
        }
        
        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public ActionResult GetLostPetNotificationsForUser(long userId)
        {
            try
            {
                IEnumerable<NotificationModel> notificationModels = _INotificationModelBLL.GetLostPetNotificationsForUser(userId);
                return Ok(notificationModels);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Cannot get user's notifications!" });
            }
        }


        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public ActionResult GetWalkingNotificationsForUser(long userId)
        {
            try
            {
                IEnumerable<NotificationModel> notificationModels = _INotificationModelBLL.GetWalkingNotificationsForUser(userId);
                return Ok(notificationModels);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Cannot get user's notifications!" });
            }
        }

        /// <summary>
        /// Get direct message
        /// </summary>
        /// <param name="userSender"></param>
        /// <param name="userReceiver"></param>
        /// <returns> user data</returns>
        [HttpGet]
        [Route("[action]/{userSender}/{userReceiver}")]
        [Authorize]
        public IActionResult getDirectMessage(long userSender, long userReceiver)
        {
            try
            {
                IEnumerable<ChatModel> messages = _iChatBLL.getDirectMessage(userSender, userReceiver);
                return Ok(messages);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to get direct messages." });
            }
        }

        /// <summary>
        /// Get chats by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> user data</returns>
        [HttpGet]
        [Route("[action]/{userId}")]
        [Authorize]
        public IActionResult getChatsByUser(long userId)
        {
            try
            {
                IEnumerable<ChatsModelDTO> chats = _iChatBLL.getChatsByUser(userId);
                return Ok(chats);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to get chats for user." });
            }
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="chatModel"></param>
        /// <returns> user data</returns>
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public IActionResult SendMessage([FromBody] ChatModel chatModel)
        {
            try
            {
                _iChatBLL.SendMessage(chatModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to send a message." });
            }
        }
        
        [HttpGet("GetUserAchievements/{userId}")]
        [Authorize]
        public IActionResult GetUserAchievements(long userId)
        {
            try
            {
                IEnumerable<UserAchievementModel> userAchievements = _iUserAchievementBLL.GetAllUserAchievements(userId);
                return Ok(userAchievements);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to get achievements for user." });
            }
        }

        [HttpGet("GenerateUserReferralCode/{userId}")]
        [Authorize]
        public IActionResult GenerateUserReferralCode(long userId)
        {
            try
            {
                UserReferralCodeModel userReferralCode = _iUserReferralCodeBLL.GenerateUserReferralCode(userId);
                return Ok(userReferralCode);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to get referral code for user." });
            }
        }

        [HttpGet("ReferralCodeExists/{referralCode}")]
        [Authorize]
        public IActionResult ReferralCodeExists(string referralCode)
        {
            bool result = _iUserReferralCodeBLL.ReferralCodeExists(referralCode);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        [Authorize]
        public ActionResult DeleteUser(long userId)
        {
            try
            {
                var res = _userBLL.DeleteUser(userId);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Unable to delete this user." });
            }

        }
    }
}
