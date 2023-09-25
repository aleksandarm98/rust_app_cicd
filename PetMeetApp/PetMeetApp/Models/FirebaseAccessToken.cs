using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetMeetApp.Models
{
    public class FirebaseAccessToken
    {
        public long Id { get; set; }
        public long UserModelId { get; set; }
        public string AccessToken { get; set; }
        public DateTime? RefreshedOn { get; set; }

        public UserModel UserModel { get; set; }
    }
}
