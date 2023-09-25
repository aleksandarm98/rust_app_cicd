using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common.Extensions;
using PetMeetApp.Common.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;

namespace PetMeetApp.BLL
{
    public class AuthBLL : IAuthBLL
    {
        private readonly IAuthDAL _iAuthDAL;
        private readonly IUserDAL _iUserDAL;
        private readonly IUserBLL _iUserBLL;
        private readonly IEmailService _iEmailService;
        private readonly IFirebaseAccessTokenDAL _IFirebaseAccessTokenDAL;

        public AuthBLL(IAuthDAL iAuthDAL, IFirebaseAccessTokenDAL iFirebaseAccessTokenDAL, IUserDAL iUserDAL,IEmailService iEmailService, IUserBLL iUserBLL)
        {
            _iAuthDAL = iAuthDAL;
            _IFirebaseAccessTokenDAL = iFirebaseAccessTokenDAL;
            _iUserDAL = iUserDAL;
            _iEmailService = iEmailService;
            _iUserBLL = iUserBLL;
        }

        public bool ForgotPassword(ForgotPasswordModel data)
        {
            var user = _iUserDAL.GetUserByEmail(data.EmailAddress);
            if (user == null)
            {
                return false;
            }
            else
                try
                {
                    var code = EncodingExtension.GenerateRandomCode();

                    if (_iAuthDAL.AddForgotPassword(user.Id, code) == false) return false;
                    if (_iEmailService.SendForgotPasswordMail(user.Email, code))
                        return true;
                    return false;

                }
                catch
                {
                    return false;
                }
        }

        public bool ResetPassword(ForgotPasswordModel data)
        {
            var user = _iUserDAL.GetUserByEmail(data.EmailAddress);
            if (user == null)
            {
                return false;
            }
            else
            {
                var pass = _iAuthDAL.GetForgotPassword(user.Id, data.Code);
                if (pass != null && pass.Date.AddMinutes(10) > DateTime.Now)
                {
                    user.Password = EncodingExtension.HashToSHA256(data.NewPassword);
                    _iAuthDAL.ResetPassword(user);
                    return true;
                }
                return false;
            }
        }

        public bool SendValidationCode(EmailValidationModel data)
        {
            try
            {
                var code = EncodingExtension.GenerateRandomCode();

                if (_iAuthDAL.AddEmailValidation(data.EmailAddress, code) == false) return false;
                if(_iEmailService.SendValidationCode(data.EmailAddress, code)) 
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateEmail(EmailValidationModel data)
        {
            var code = _iAuthDAL.ValidateEmail(data.EmailAddress, data.Code);
            if (code != null && code.Date.AddMinutes(10) > DateTime.Now) return true;
            return false;
            
        }

        public UserModel Login(LoginModel user)
        {
            var result = _iAuthDAL.Login(user);

            // TODO: Temporary implementation. This part should happend post-login ideally.
            if(user.AccessToken != null && result != null)
            {
                _IFirebaseAccessTokenDAL.Create(new FirebaseAccessToken { 
                    UserModelId = result.Id,
                    AccessToken = user.AccessToken,
                    RefreshedOn = DateTime.UtcNow
                });
            }

            return result;
        }

        public UserModel Registration(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}