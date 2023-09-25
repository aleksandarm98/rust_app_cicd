using NLog;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.BLL
{
    public class ChatBLL : IChatBLL
    {
        private readonly IChatDAL _iChatDAL;
        private readonly INotificationBLL _iNotificationBLL;
        private Logger _logger;
        public ChatBLL(IChatDAL iChatDAL, INotificationBLL iNotificationBLL)
        {
            _iChatDAL = iChatDAL;
            _iNotificationBLL = iNotificationBLL;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IEnumerable<ChatModel> getDirectMessage(long userSender, long userReceiver)
        {
            IEnumerable<ChatModel> messages = _iChatDAL.getDirectMessage(userSender, userReceiver);
            return messages.OrderByDescending(x => x.SentOn);
        }

        public IEnumerable<ChatsModelDTO> getChatsByUser(long userId)
        {
            IEnumerable<ChatsModelDTO> chatsByUser = _iChatDAL.getChatsByUser(userId);
            return chatsByUser.OrderByDescending(x => x.SentOn);
        }

        public void SendMessage(ChatModel chatModel)
        {
            try
            {
                _iChatDAL.Create(chatModel);

                if (!_iNotificationBLL.SendMessageNotification(chatModel))
                {
                    _logger.Error(string.Format("Error on sending message notifications from: {0} to: {1}.", chatModel.UserSender, chatModel.UserReceiver));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}