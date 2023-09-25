using PetMeetApp.Models;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IFirebaseAccessTokenBLL
    {
        public void DeleteAccessTokensOnLogout(long userId, string firebaseAccessToken);
        public void Create(FirebaseAccessToken firebaseAccessToken);
    }
}
