using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL
{
    public class FirebaseAccessTokenDAL : BaseDAL<FirebaseAccessToken>, IFirebaseAccessTokenDAL
    {
        public FirebaseAccessTokenDAL(Context context) : base(context)
        { }

        public List<FirebaseAccessToken> GetAccessTokens(long userId, string accessToken)
        {
            return _context.FirebaseAccessTokens
                .Where(f => f.UserModelId == userId && f.AccessToken == accessToken)
                .ToList();
        }

        public IEnumerable<FirebaseAccessToken> GetFirebaseAccessTokenByUserId(long userId)
        {
            return _context.FirebaseAccessTokens.Where(f => f.UserModelId.Equals(userId));
        }
    }
}