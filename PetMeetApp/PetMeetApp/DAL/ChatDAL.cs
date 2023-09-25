using PetMeetApp.DAL.Interfaces;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.DAL
{
    public class ChatDAL : BaseDAL<ChatModel>, IChatDAL
    {
        public ChatDAL(Context context) : base(context)
        {
        }

        public IEnumerable<ChatModel> getDirectMessage(long userSender, long userReceiver)
        {
            return from chat in _context.Chats
                   where (chat.UserSender == userSender && chat.UserReceiver == userReceiver)
                       || (chat.UserSender == userReceiver && chat.UserReceiver == userSender)
                   select new ChatModel
                   {
                       Id = chat.Id,
                       UserSender = chat.UserSender,
                       UserReceiver = chat.UserReceiver,
                       Content = chat.Content,
                       SentOn = chat.SentOn
                   };
        }

        public IEnumerable<ChatsModelDTO> getChatsByUser(long userId)
        {
            return from m in _context.Chats
                   join t1 in (
                       (from Chats in _context.Chats
                        where Chats.UserSender == userId || Chats.UserReceiver == userId
                        group Chats by new
                        {
                            V = Chats.UserSender > Chats.UserReceiver ? Chats.UserSender : Chats.UserReceiver,
                            V1 = Chats.UserSender > Chats.UserReceiver ? Chats.UserReceiver : Chats.UserSender
                        } into g
                        select new
                        {
                            maxid = g.Max(p => p.Id)
                        })
                   ) on new { Id = m.Id } equals new { Id = t1.maxid }
                   join u in _context.Users on new
                   {
                       Id = m.UserSender == userId ? m.UserReceiver : m.UserSender
                   } equals new { Id = u.Id }
                   select new ChatsModelDTO
                   {
                       Id = m.Id,
                       UserId = u.Id,
                       Content = m.Content,
                       SentOn = m.SentOn,
                       Username = u.Username,
                       ProfileImageUrl = u.Image
                   };
        }
    }
}