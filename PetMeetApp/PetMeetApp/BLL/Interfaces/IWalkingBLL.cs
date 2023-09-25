using PetMeetApp.Models.HelpModels;
using PetMeetApp.Models;

namespace PetMeetApp.BLL.Interfaces
{
    public interface IWalkingBLL
    {
        public WalkingModel InviteForWalk(WalkingDTO data);
    }
}
