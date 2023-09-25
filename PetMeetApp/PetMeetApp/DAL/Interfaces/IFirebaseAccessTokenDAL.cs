using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IFirebaseAccessTokenDAL : IBaseDAL<FirebaseAccessToken>
    {
        public IEnumerable<FirebaseAccessToken> GetFirebaseAccessTokenByUserId(long userId);
        public List<FirebaseAccessToken> GetAccessTokens(long userId, string accessToken);
    }
}