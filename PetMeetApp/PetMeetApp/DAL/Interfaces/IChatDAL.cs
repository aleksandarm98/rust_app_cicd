using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL.Interfaces
{
    public interface IChatDAL : IBaseDAL<ChatModel>
    {
        public IEnumerable<ChatModel> getDirectMessage(long userSender, long userReceiver);

        public IEnumerable<ChatsModelDTO> getChatsByUser(long userId);
    }
}