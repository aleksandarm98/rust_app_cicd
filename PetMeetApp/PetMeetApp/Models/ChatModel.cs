using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models
{
    public class ChatModel
    {
        public long Id { get; set; }

        public long UserSender { get; set; }

        public long UserReceiver { get; set; }

        public string Content { get; set; }

        public DateTime SentOn { get; set; }

        public NotificationModel NotificationModel { get; set; }
    }
}
