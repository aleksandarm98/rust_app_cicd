using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models.HelpModels
{
    public class ChatsModelDTO
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Username { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Content { get; set; }

        public DateTime SentOn { get; set; }
    }
}
