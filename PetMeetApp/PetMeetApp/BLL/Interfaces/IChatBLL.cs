using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System.Collections.Generic;


namespace PetMeetApp.BLL.Interfaces
{
    public interface IChatBLL
    {
        public IEnumerable<ChatModel> getDirectMessage(long userSender, long userReceiver);

        public IEnumerable<ChatsModelDTO> getChatsByUser(long userId);

        public void SendMessage(ChatModel chatModel);
    }
}
