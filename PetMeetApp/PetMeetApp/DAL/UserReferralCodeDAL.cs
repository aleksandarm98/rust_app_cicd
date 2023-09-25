using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class UserReferralCodeDAL : BaseDAL<UserReferralCodeModel>, IUserReferralCodeDAL
    {
        private readonly IUserReferralCodeDAL _IUserReferralCodeDAL;
        public UserReferralCodeDAL(Context context) : base(context) 
        { }

        public UserReferralCodeModel GetUserReferralCodeByUser(long userId)
        {
            var userReferralCode =
                _context.UserReferralCodes.Where(x => x.UserId == userId).FirstOrDefault();

            return userReferralCode;
        }

        public bool ReferralCodeExists(string referralCode)
        {
            var result = _context.UserReferralCodes.Where(x => x.Code == referralCode).Count();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public UserReferralCodeModel GetUserReferralCodeByCode(string referralCode)
        {
            var userReferralCode =
                _context.UserReferralCodes.Where(x => x.Code == referralCode).FirstOrDefault();

            return userReferralCode;
        }
    }
}