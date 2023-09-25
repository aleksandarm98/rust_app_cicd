using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common.Extensions;
using PetMeetApp.Common.Interfaces;
using PetMeetApp.Common.Services;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PetMeetApp.BLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _userDAL;
        private readonly IFirebaseAccessTokenBLL _IFirebaseAccessTokenBLL;
        private readonly IPostDAL iPostDAL;
        private readonly IFollowingDAL iFollowingDAL;
        private readonly IUserAchievementDAL iUserAchievementDAL;
        private readonly IUserAchievementBLL iUserAchievementBLL;
        private readonly IAchievementDAL iAchievementDAL;
        private readonly IUserReferralCodeDAL iUserReferralCodeDAL;
        private readonly IUserReferralCodeBLL iUserReferralCodeBLL;

        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly JWTService _jwtService;

        public UserBLL(IUserDAL userDAL,
                       IPostDAL iPostDAL,
                       IFollowingDAL iFollowingDAL,
                       IUserAchievementDAL iUserAchievementDAL,
                       IUserAchievementBLL iUserAchievementBLL,
                       IAchievementDAL iAchievementDAL,
                       IUserReferralCodeDAL iUserReferralCodeDAL,
                       IUserReferralCodeBLL iUserReferralCodeBLL,
                       IFileService fileService, 
                       IConfiguration configuration, 
                       JWTService jwtService, 
                       IFirebaseAccessTokenBLL iFirebaseAccessTokenBLL,
                       IEmailService emailService)
        {
            _userDAL = userDAL;
            this.iPostDAL = iPostDAL;
            this.iFollowingDAL = iFollowingDAL;
            this.iUserAchievementDAL = iUserAchievementDAL;
            this.iUserAchievementBLL = iUserAchievementBLL;
            this.iAchievementDAL = iAchievementDAL;
            this.iUserReferralCodeDAL = iUserReferralCodeDAL;
            this.iUserReferralCodeBLL = iUserReferralCodeBLL;
            _fileService = fileService;
            _configuration = configuration;
            _jwtService = jwtService;
            _IFirebaseAccessTokenBLL = iFirebaseAccessTokenBLL;
            _emailService = emailService;
        }

        public string Login(LoginModel user)
        {
            user.Password = EncodingExtension.HashToSHA256(user.Password);
            var u = _userDAL.Login(user);

            if (u != null)
            {
                // TODO: Temporary implementation. This part should happend post-login ideally.
                if (user.AccessToken != null)
                {
                    _IFirebaseAccessTokenBLL.Create(new FirebaseAccessToken
                    {
                        UserModelId = u.Id,
                        AccessToken = user.AccessToken,
                        RefreshedOn = DateTime.UtcNow
                    });
                }

                return _jwtService.GenerateJwtToken(u, _configuration);
            }

            return null;
        }

        public void Logout(LogoutModel user)
        {
            if (!string.IsNullOrEmpty(user.AccessToken))
            {
                _IFirebaseAccessTokenBLL.DeleteAccessTokensOnLogout(user.UserId, user.AccessToken);
            } 
        }

        public string Register(UserModel user)
        {
            user.Password = EncodingExtension.HashToSHA256(user.Password);
            var u = _userDAL.Register(user);
            if (u != null)
            {
                _emailService.SendVerificationMail(u.Email, u.Name);
                if(user.AppliedReferralCode != null && iUserReferralCodeDAL.ReferralCodeExists(user.AppliedReferralCode))
                {
                    iUserReferralCodeBLL.IncrementCodeAppliedCount(user.AppliedReferralCode);
                    var userReferralCode = iUserReferralCodeBLL.GetUserReferralCodeByCode(user.AppliedReferralCode);
                    iUserAchievementBLL.RecalculateReferralsUserAchievements(userReferralCode.UserId, userReferralCode.CodeAppliedCount);
                }
                var achievements = iAchievementDAL.GetAll();
                foreach (var achievement in achievements)
                {
                    UserAchievementModel userAchievement = new UserAchievementModel();
                    userAchievement.UserId = u.Id;
                    userAchievement.AchievementId = achievement.Id;
                    userAchievement.CurrentProgress = 0;
                    userAchievement.IsDone = false;
                    iUserAchievementDAL.Create(userAchievement);
                } 
            }
            return _jwtService.GenerateJwtToken(u, _configuration);
        }

        public UserModel GetUserByPostId(long postId)
        {
            return _userDAL.GetUserByPostId(postId);
        }

        public bool UsernameExists(string username)
        {
            return _userDAL.UsernameExists(username);
        }
        public bool EmailExists(string email)
        {
            return _userDAL.EmailExists(email);
        }

        public UserModel ChangeImage(FileModelDTO data)
        {
            try
            {
                var folder = _configuration["Folders:UserProfileImages"];

                var oldImage = this._userDAL.GetUserImage(data.Id);
                var AWSKey = _fileService.SaveFileInAWSBucket(folder, data.file);

                _fileService.DeleteAWSFile(oldImage);
                var user = _userDAL.ChangeImage(data.Id, AWSKey);
                user.Password = null;
                user.Image = _fileService.GetAWSFileURL(user.Image);
                return user;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public UserModel ChangeInfo(UserModel user)
        {
            user = _userDAL.ChangeInfo(user);
            if (user == null) return null;
            user.Password = null;
            return user;
        }

        public UserModel ChangePassword(UserModel user)
        {
            user.Password = EncodingExtension.HashToSHA256(user.Password);
            user.OldPassword = EncodingExtension.HashToSHA256(user.OldPassword);
            user = _userDAL.ChangePassword(user);
            if (user == null) return null;
            user.Password = null;
            return user;
        }

        public UserModel GetUser(long id)
        {
            return _userDAL.GetById(id);
        }

        public UserModelWrapper GetUserProfile(long id)
        {
            UserModelWrapper userModelWrapper = null;
            UserModel userModel = _userDAL.GetById(id);

            if (userModel != null)
            {
                long userPostsCount = iPostDAL.GetPostCountByUser(userModel.Id);
                long userFollowingsCount = iFollowingDAL.GetFollowingsCountByUser(userModel.Id);
                long userFollowersCount = iFollowingDAL.GetFollowersCountByUser(userModel.Id);

                userModelWrapper = new() { 
                    UserModel = userModel,
                    UserPostsCount = userPostsCount, 
                    UserFollowingsCount = userFollowingsCount, 
                    UserFollowersCount = userFollowersCount
                };
            }

            return userModelWrapper;
        }

        public UserModel GetUserWithFirebaseAccessTokens(long id)
        {
            return _userDAL.GetUserWithFirebaseAccessTokens(id);
        }

        public IEnumerable<SearchModel> SearchUsers(string searchFilter)
        {
            return _userDAL.SearchUsers(searchFilter);
        }

        public List<UserModel> GetRandomUsers(long currentUser, int numberOfRandomUsers)
        {
            return _userDAL.GetRandomUsers(currentUser, numberOfRandomUsers);
        }

        public bool DeleteUser(long userId)
        {
            return _userDAL.DeleteUser(userId);
        }
    }
}