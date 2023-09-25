using PetMeetApp.Models;
using System.Collections.Generic;
using PetMeetApp.Models.HelpModels;


namespace PetMeetApp.BLL.Interfaces
{
    public interface IPetBLL
    {
        public PetModel Register(PetModel pet, long ownerId);

        public IEnumerable<PetTypeModel> GetTypes();
        public bool UsernameExists(string username);

        public IEnumerable<PetModelHelper> GetOwnedPets(long userId);
        public PetModel ChangeUsername(PetModel pet);
        public PetModel ChangeName(PetModel pet);
        public PetModel ChangeImage(FileModelDTO data);

        public PetProfileModelDTO GetPet(long petId);
        public IEnumerable<SearchModel> SearchPets(string searchFilter);
        public LostPetModelDTO ReportPetLoss(long petId);

        public IEnumerable<PetModel> FindPartner(FindPartnerModelDTO pet);
        public void DeletePet(long petId);
        public List<PetModel> GetPetsByUserId(long userId);
        public void RemovePetLoss(long petId);

        public bool IsPetLost(long petId);

    }
}