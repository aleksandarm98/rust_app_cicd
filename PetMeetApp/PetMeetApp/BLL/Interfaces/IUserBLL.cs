using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IUserBLL
    {
        string Login(LoginModel user);
        void Logout(LogoutModel user);
        string Register(UserModel user);
        bool UsernameExists(string username);
        bool EmailExists(string email);
        UserModel ChangeImage(FileModelDTO data);
        UserModel ChangeInfo(UserModel user);
        UserModel ChangePassword(UserModel user);
        UserModel GetUser(long id);
        UserModelWrapper GetUserProfile(long id);
        public IEnumerable<SearchModel> SearchUsers(string searchFilter);
        List<UserModel> GetRandomUsers(long currentUser, int numberOfRandomUsers);
        UserModel GetUserByPostId(long postId);
        public bool DeleteUser(long userId);
        public UserModel GetUserWithFirebaseAccessTokens(long id);
    }
}