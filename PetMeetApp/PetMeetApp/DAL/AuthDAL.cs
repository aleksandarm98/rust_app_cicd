using System;
using System.Linq;
using PetMeetApp.Models;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models.HelpModels;

namespace PetMeetApp.DAL
{
    public class AuthDAL : IAuthDAL
    {
        private readonly Context _context;

        public AuthDAL(Context context)
        {
            _context = context;
        }

        public bool AddForgotPassword(long userId, string code)
        {
            try
            {
                ForgotPassword entity = new ForgotPassword();
                entity.UserId = userId;
                entity.Code = code;
                entity.Date = DateTime.Now;
                _context.ForgotPasswords.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool AddEmailValidation(string email, string code)
        {
            try
            {
                EmailValidation entity = new EmailValidation();
                entity.EmailAddress = email;
                entity.Code = code;
                entity.Date = DateTime.Now;
                _context.EmailValidations.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public EmailValidation ValidateEmail(string email, string code)
        {
            return _context.EmailValidations.Where(x => x.EmailAddress == email && x.Code == code).OrderByDescending(x => x.Date).FirstOrDefault();
        }

        public ForgotPassword GetForgotPassword(long userId, string code)
        {
            return _context.ForgotPasswords.Where(x => x.UserId == userId && x.Code == code).OrderByDescending(x => x.Date).FirstOrDefault();
        }
        
        public UserModel ResetPassword(UserModel user)
        {
            var selectedUser = _context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if (selectedUser == null)
            {
                return null;
            }
            selectedUser.Password = user.Password;
            _context.SaveChanges();
            return selectedUser;
        }

        public UserModel Login(LoginModel user)
        {
            return _context.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
        }

        public UserModel Registration(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}