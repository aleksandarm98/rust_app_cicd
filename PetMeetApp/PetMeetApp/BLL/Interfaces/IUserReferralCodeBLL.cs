using System.Collections.Generic;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IUserReferralCodeBLL
    {
        public UserReferralCodeModel GenerateUserReferralCode(long userId);
        public UserReferralCodeModel CreateUserReferralCode(long userId);
        public bool ReferralCodeExists(string referralCode);
        public void IncrementCodeAppliedCount(string referralCode);
        public UserReferralCodeModel GetUserReferralCodeByCode(string referralCode);
    }
}