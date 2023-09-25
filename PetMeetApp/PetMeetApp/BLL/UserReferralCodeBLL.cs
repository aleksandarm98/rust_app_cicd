using System;
using System.Security.Cryptography;
using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;

namespace PetMeetApp.BLL

{
    public class UserReferralCodeBLL : IUserReferralCodeBLL
    {
        private readonly IUserReferralCodeDAL _IUserReferralCodeDAL;
        private Logger _logger;

        public UserReferralCodeBLL(IUserReferralCodeDAL _iUserReferralCodeDAL)
        {
            this._IUserReferralCodeDAL = _iUserReferralCodeDAL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public UserReferralCodeModel GenerateUserReferralCode(long userId)
        {
            var userReferralCode = _IUserReferralCodeDAL.GetUserReferralCodeByUser(userId);

            if(userReferralCode == null)
                userReferralCode = CreateUserReferralCode(userId);

            return userReferralCode;
        }

        public UserReferralCodeModel CreateUserReferralCode(long userId)
        {
            UserReferralCodeModel userReferralCode = new UserReferralCodeModel();
            userReferralCode.UserId = userId;
            userReferralCode.CodeAppliedCount = 0;

            string uniqueCode;
            do
            {
                uniqueCode = GetUniqueString(20);
            } while (ReferralCodeExists(uniqueCode));
            userReferralCode.Code = uniqueCode;

            _IUserReferralCodeDAL.Create(userReferralCode);

            return userReferralCode;
        }

        public string GetUniqueString(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bitCount = (length * 6);
                var byteCount = ((bitCount + 7) / 8);
                var bytes = new byte[byteCount];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes).Replace("/","_");
            }
        }

        public bool ReferralCodeExists(string referralCode)
        {
            return _IUserReferralCodeDAL.ReferralCodeExists(referralCode);
        }

        public void IncrementCodeAppliedCount(string referralCode)
        {
            var userReferralCode = _IUserReferralCodeDAL.GetUserReferralCodeByCode(referralCode);

            if(userReferralCode != null)
            {
                userReferralCode.CodeAppliedCount++;
                _IUserReferralCodeDAL.Update(userReferralCode);
            }
        }
        
        public UserReferralCodeModel GetUserReferralCodeByCode(string referralCode)
        {
           return _IUserReferralCodeDAL.GetUserReferralCodeByCode(referralCode);
        }
    }
}
