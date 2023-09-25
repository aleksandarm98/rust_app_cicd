using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetMeetApp.DAL
{
    public class PetDAL : BaseDAL<PetModel>,IPetDAL
    {
     
        private readonly int LOST_PET_RADIUS = 10;
        public PetDAL(Context _context):base(_context)
        {}

        public PetModel ChangeImage(long petId, string path)
        {
            var selectedPet = _context.Pets.Where(x => x.Id == petId).FirstOrDefault();
            if (selectedPet == null)
            {
                return null;
            }
            selectedPet.Image = path;
            _context.SaveChanges();
            return selectedPet;
        }

        public PetModel ChangeName(PetModel pet)
        {
            var selectedPet = _context.Pets.Where(x => x.Id == pet.Id).FirstOrDefault();
            if (selectedPet == null)
            {
                return null;
            }
            selectedPet.PetName = pet.PetName;
            _context.SaveChanges();
            return selectedPet;
        }

        public PetModel ChangeUsername(PetModel pet)
        {
            var selectedPet = _context.Pets.Where(x => x.Id == pet.Id).FirstOrDefault();
            bool usernameExists = UsernameExists(pet.Username);
            if (selectedPet == null || usernameExists)
            {
                return null;
            }
            selectedPet.Username = pet.Username;
            _context.SaveChanges();
            return selectedPet;
        }

        public IEnumerable<PetModel> GetOwnedPets(long userId)
        {
            var result = from userPet in _context.UserPetRelation
                         join pet in _context.Pets.Include(x=>x.PetType) on userPet.PetId equals pet.Id
                         join petType in _context.PetTypes on pet.PetTypeId equals petType.Id
                         where userPet.UserId == userId
                         select pet;
            return result;
        }

        public string GetPetImage(long petId)
        {
            var res = _context.Pets.Where(x => x.Id == petId).FirstOrDefault();
            if (res == null)
            {
                return null;
            }
            return res.Image;
        }

        public IEnumerable<PetTypeModel> GetTypes()
        {
            return _context.PetTypes;
        }

        public PetModel Register(PetModel pet,long ownerId)
        {
            try
            {
                if (_context.Pets.Where(x => x.Username == pet.Username).FirstOrDefault() != null) return null;

                base.Create(pet);
                UserPetRelation up = new UserPetRelation();
                up.UserId = ownerId;
                up.PetId = pet.Id;
               
                _context.UserPetRelation.Add(up);
                _context.SaveChanges();

                return pet;
            }
            catch
            {
                return null;
            }
        }

        public bool UsernameExists(string username)
        {
            var result = _context.Pets.Where(x => x.Username == username).Count();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<SearchModel> SearchPets(string searchFilter)
        {
           return _context.Pets.Where(x => x.PetName.Contains(searchFilter) || x.Username.Contains(searchFilter)).Select(x => new SearchModel(x.Id,x.Username,x.Image));
        }

        public IEnumerable<PetModel> FindPartner(FindPartnerModelDTO pet)
        {
            double maxLat, minLat, maxLng, minLng;
            double lat1 = pet.Lat - pet.Radius / 111.3;
            double lat2 = pet.Lat + pet.Radius / 111.3;
            double lng1 = pet.Lng - pet.Radius / (111.3 * Math.Cos(pet.Lat));
            double lng2 = pet.Lng + pet.Radius / (111.3 * Math.Cos(pet.Lat));

            if (lng1 < lng2)
            {
                minLng= lng1;
                maxLng = lng2;
            }
            else
            {
                minLng = lng2;
                maxLng = lng1;
            }

            if (lat1 < lat2)
            {
                minLat = lat1;
                maxLat = lat2;
            }
            else
            {
                minLat = lat2;
                maxLat = lat1;
            }
            return _context.Pets.Where(x => 
                    x.Breed == pet.Breed && 
                    x.GenderType == pet.GenderType && 
                    x.PetTypeId == pet.PetTypeId &&
                    (x.Lat <= maxLat & x.Lat >= minLat) & (x.Lng <= maxLng & x.Lng >= minLng)
                    )
                .ToList();
        }

        public PetProfileModelDTO GetPet(long petId)
        {
            var result = from pet in _context.Pets
                join userPet in _context.UserPetRelation on pet.Id equals userPet.PetId
                join user in _context.Users on userPet.UserId equals user.Id
                where pet.Id == petId
                select new PetProfileModelDTO()
                {
                    Id = pet.Id,
                    PetUsername = pet.Username,
                    PetName = pet.PetName,
                    PetTypeId = pet.PetTypeId,
                    PetImage = pet.Image,
                    Breed = pet.Breed,
                    Birthday  = pet.Birthday,
                    PetTypeName=pet.PetType.TypeName,
                    GenderType = pet.GenderType,
                    Country = pet.Country,
                    City = pet.City,
                    Lat = pet.Lat,
                    Lng = pet.Lng,
                    UserId = user.Id,
                    OwnerUsername = user.Username,
                    UserImage = user.Image
                };
            return result.FirstOrDefault();
        }
        public PetModel GetPetModelById(long petId)
        {
            var result = from pet in _context.Pets
                         join userPet in _context.UserPetRelation on pet.Id equals userPet.PetId
                         join user in _context.Users on userPet.UserId equals user.Id
                         where pet.Id == petId
                         select pet;
            return result.FirstOrDefault();
        }

        public void DeletePet(long petId)
        {
            var petDB = GetPetModelById(petId);
            if (petDB != null)
                _context.Pets.Remove(petDB);


            _context.SaveChanges();
        }

        public List<UserModelHelper> ReportPetLoss(PetProfileModelDTO pet)
        {
            double minLat = pet.Lat - LOST_PET_RADIUS / 111.3;
            double maxLat = pet.Lat + LOST_PET_RADIUS / 111.3;
            double minLng = pet.Lng - LOST_PET_RADIUS / (111.3 * Math.Cos(pet.Lat));
            double maxLng = pet.Lng + LOST_PET_RADIUS / (111.3 * Math.Cos(pet.Lat));
            var result = from pets in _context.Pets
                join userPet in _context.UserPetRelation on pets.Id equals userPet.PetId
                join user in _context.Users on userPet.UserId equals user.Id
                where (pets.Lat <= maxLat & pets.Lat >= minLat) & (pets.Lng <= maxLng & pets.Lng >= minLng)
                select new UserModelHelper()
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Image = user.Image
                };
            return result.ToList();
        }

        public bool IsPetLost(long petId)
        {
            var result = _context.LostPets.Where(x => x.PetId == petId).Any();
            return result;
        }

        public List<PetModel> GetPetsByUserId(long userId)
        {
            var result = from userPet in _context.UserPetRelation
                         join pet in _context.Pets.Include("Users") on userPet.PetId equals pet.Id
                         join user in _context.Users on userPet.UserId equals user.Id
                         where userPet.UserId == userId
                         select pet;
            var result1 = result.ToList();

            return result.ToList();
        }

    } 
    
}