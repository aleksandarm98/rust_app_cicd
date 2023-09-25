using PetMeetApp.Models;
using System.Collections.Generic;

namespace PetMeetApp.DAL.Interfaces
{
    public interface ILostPetModelDAL : IBaseDAL<LostPetModel>
    {
        public List<LostPetModel> GetLostPetsByPetId(long petId);
    }
}