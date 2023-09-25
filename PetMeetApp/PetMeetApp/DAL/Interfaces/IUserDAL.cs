using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IUserDAL : IBaseDAL<UserModel>
    {
        UserModel Login(LoginModel user);
        UserModel Register(UserModel user);
        bool UsernameExists(string username);
        bool EmailExists(string email);
        UserModel ChangeImage(long userId, string path);
        UserModel ChangeInfo(UserModel user);
        UserModel ChangePassword(UserModel user);
        public string GetUserImage(long userId);
        public IEnumerable<SearchModel> SearchUsers(string searchFilter);
        UserModel GetUserByPostId(long postId);
        UserModel GetUserByEmail(string email);
        List<UserModel> GetRandomUsers(long currentUser, int numberOfRandomUsers);
        public bool DeleteUser(long userId);
        public UserModel GetUserWithFirebaseAccessTokens(long userId);
    }
}