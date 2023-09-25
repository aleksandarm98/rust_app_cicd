using System.Collections.Generic;
using System.Linq;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;

namespace PetMeetApp.DAL
{
    public class LostPetModelDAL : BaseDAL<LostPetModel>, ILostPetModelDAL
    {
        public LostPetModelDAL(Context context) : base(context)
        {
            
        }

        public List<LostPetModel> GetLostPetsByPetId(long petId)
        {
            var query = _context.LostPets.Where(l => l.PetId == petId).ToList();
            return query;
        }
    }
}