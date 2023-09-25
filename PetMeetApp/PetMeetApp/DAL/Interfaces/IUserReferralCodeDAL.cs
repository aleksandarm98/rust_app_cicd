using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IUserReferralCodeDAL: IBaseDAL<UserReferralCodeModel>
    {
        public UserReferralCodeModel GetUserReferralCodeByUser(long userId);
        public UserReferralCodeModel GetUserReferralCodeByCode(string referralCode);
        public bool ReferralCodeExists(string referralCode);

    }
}