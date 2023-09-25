using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IAuthDAL
    {
        public UserModel Login(LoginModel user);
        public UserModel Registration(UserModel user);
        public bool AddForgotPassword(long userId, string code);
        public ForgotPassword GetForgotPassword(long userId, string code);
        public bool AddEmailValidation(string email, string code);
        public EmailValidation ValidateEmail(string email, string code);
        public UserModel ResetPassword(UserModel user);
    }
}