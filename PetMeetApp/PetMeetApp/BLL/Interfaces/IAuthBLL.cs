using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IAuthBLL
    {
        public UserModel Login(LoginModel user);
        public UserModel Registration(UserModel user);
        public bool ForgotPassword(ForgotPasswordModel data);
        public bool ResetPassword(ForgotPasswordModel data);
        
        public bool SendValidationCode(EmailValidationModel data);

        public bool ValidateEmail(EmailValidationModel data);

    }
}
