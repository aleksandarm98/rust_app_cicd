using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IPetDAL : IBaseDAL<PetModel>
    {
        public PetModel Register(PetModel pet, long ownerId);
        public IEnumerable<PetTypeModel> GetTypes();
        public bool UsernameExists(string username);
        public IEnumerable<PetModel> GetOwnedPets(long userId);
        public PetModel ChangeUsername(PetModel pet);
        public PetModel ChangeName(PetModel pet);
        public PetModel ChangeImage(long petId, string path);
        public string GetPetImage(long petId);
        public IEnumerable<SearchModel> SearchPets(string searchFilter);
        public IEnumerable<PetModel> FindPartner(FindPartnerModelDTO pet);
        public PetProfileModelDTO GetPet(long petId);
        public void DeletePet(long petId);

        public List<PetModel> GetPetsByUserId(long userId);
        public List<UserModelHelper> ReportPetLoss(PetProfileModelDTO pet);

        public bool IsPetLost(long petId);

    }
}