using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common.Extensions;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;

namespace PetMeetApp.BLL
{
    public class FirebaseAccessTokenBLL : IFirebaseAccessTokenBLL
    {
        private readonly IFirebaseAccessTokenDAL _IFirebaseAccessTokenDAL;

        public FirebaseAccessTokenBLL(IFirebaseAccessTokenDAL iFirebaseAccessTokenDAL)
        {
            _IFirebaseAccessTokenDAL = iFirebaseAccessTokenDAL;
        }

        public void Create(FirebaseAccessToken firebaseAccessToken)
        {
            _IFirebaseAccessTokenDAL.Create(firebaseAccessToken);
        }

        public void DeleteAccessTokensOnLogout(long userId, string accessToken)
        {
            var firebaseAccessTokens = _IFirebaseAccessTokenDAL.GetAccessTokens(userId, accessToken);
            if (firebaseAccessTokens.SafeAny())
            {
                foreach (var firebaseAccessToken in firebaseAccessTokens)
                {
                    if (firebaseAccessToken != null)
                    {
                        _IFirebaseAccessTokenDAL.Delete(firebaseAccessToken);
                    }
                }
            }
        }
    }
}
