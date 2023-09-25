using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PetMeetApp.DAL
{
    public class UserDAL:BaseDAL<UserModel>,IUserDAL
    {
        private readonly IPetDAL _iPetDAL;

        public UserDAL(Context context, IPetDAL petDAL) : base(context) 
        {
            _iPetDAL = petDAL;
        }

        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public UserModel Login(LoginModel user)
        {
            try
            {
                var result = _context.Users.Where(x => (x.Username == user.Username || x.Email== user.Email) && x.Password == user.Password).FirstOrDefault();

                return result;
            }
            catch
            {
                //TO DO logger
                return null;
            }
        }

        public UserModel Register(UserModel user)
        {
            try
            {
                if (_context.Users.Where(x => x.Username == user.Username).FirstOrDefault() != null) return null;
                if (_context.Users.Where(x => x.Email == user.Email).FirstOrDefault() != null) return null;

                user.DateCreated = DateTime.Now;

                base.Create(user);
                return user;
            }
            catch
            {
                return null;
            }
            
        }

        public UserModel GetUserByPostId(long postId)
        {
            var query = from posts in _context.Posts
                        join users in _context.Users.Include(u => u.FirebaseAccessTokens) on posts.UserId equals users.Id
                        where posts.Id.Equals(postId)
                        select users;

            return query.FirstOrDefault();
        }

        public bool UsernameExists(string username)
        {
            long result = _context.Users.Where(x => x.Username == username).Count();
            if (result != 0) return true;
            return false;
        }

        public bool EmailExists(string email)
        {
            long result = _context.Users.Where(x => x.Email == email).Count();
            if (result != 0) return true;
            return false;
        }

        public UserModel ChangeImage(long userId, string path)
        {
           var selectedUser =  _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if(selectedUser==null)
            {
                return null;
            }
            selectedUser.Image = path;
            _context.SaveChanges();
            return selectedUser;
        }

        public string GetUserImage(long userId)
        {
            var res = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if(res == null)
            {
                return null;
            }
            return res.Image;
        }

        public UserModel ChangeInfo(UserModel user)
        {
            var selectedUser = _context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            bool usernameExists = UsernameExists(user.Username);
            if (selectedUser == null || usernameExists)
            {
                return null;
            }

            if(user.Username != null)
                selectedUser.Username = user.Username;
            if(user.Name != null)
                selectedUser.Name = user.Name;
            if(user.Address != null)
                selectedUser.Address = user.Address;
            if(user.PhoneNumber != null)
                selectedUser.PhoneNumber = user.PhoneNumber;
            if(user.Gender != null)
                selectedUser.Gender = user.Gender;
            if (user.Biography != null)
                selectedUser.Biography = user.Biography;

            _context.SaveChanges();

            return selectedUser;
        }

       

        public UserModel ChangePassword(UserModel user)
        {
            var selectedUser = _context.Users.Where(x => x.Id == user.Id && x.Password == user.OldPassword).FirstOrDefault();
            if (selectedUser == null)
            {
                return null;
            }
            selectedUser.Password = user.Password;
            _context.SaveChanges();
            return selectedUser;
        }

        public IEnumerable<SearchModel> SearchUsers(string searchFilter)
        {
            return _context.Users.Where(x => x.Name.Contains(searchFilter) || x.Username.Contains(searchFilter)).Select(x => new SearchModel(x.Id, x.Username,x.Image));
        }


        public bool DeleteUser(long userId)
        {
            var user = GetById(userId);
            List < PetModel > pets = new List<PetModel>();
            bool res = false;
            if (user != null)
            {
                
               pets = _iPetDAL.GetPetsByUserId(userId);  
                if(pets.Count > 0)
                    res = CheckOwnerOfPets(userId, pets);
                if(res)
                {
                    foreach(var pet in pets)
                    {
                        _iPetDAL.DeletePet(pet.Id);
                    }
                }
                    
                    
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool CheckOwnerOfPets(long userId, List<PetModel> list)
        {

            foreach (var pets in list)
            {
                var ownerOfPets = pets.Users;
                if (ownerOfPets.Count() > 1)
                    return false;
            }
            return true;
        }

        public UserModel GetUserWithFirebaseAccessTokens(long userId)
        {
            return _context.Users.Where(u => u.Id.Equals(userId))
                                 .Include(u => u.FirebaseAccessTokens).FirstOrDefault();
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.Where(u=>u.Email == email).FirstOrDefault();
        }

        public List<UserModel> GetRandomUsers(long currentUser, int numberOfRandomUsers)
        {
            var query1 = from followers in _context.Followers
                         join users in _context.Users on followers.FollowingId equals users.Id
                         where followers.FollowedId == currentUser
                         select users;

            var query2 = _context.Users.Except(query1);

            return query2.OrderBy(o => Guid.NewGuid())
                .Take(numberOfRandomUsers)
                .Where(x=>x.Id!=currentUser)
                .ToList();
        }
    }
   
}